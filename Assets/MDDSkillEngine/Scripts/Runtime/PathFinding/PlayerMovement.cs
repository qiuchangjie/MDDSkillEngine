using MDDGameFramework;
using MDDGameFramework.Runtime;
using Pathfinding;
using Pathfinding.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MDDSkillEngine
{
    public class PlayerMovement : MonoBehaviour
    {

        public Seeker seeker;

        public Player player;

        public Transform tr;

        public Vector3 previousMovementOrigin;

        public Vector3 previousMovementDirection;

        public PathInterpolator interpolator = new PathInterpolator();

        /// <summary>Speed in world units</summary>
        public float speed = 3;

        /// <summary>
        /// If true, some interpolation will be done when a new path has been calculated.
        /// This is used to avoid short distance teleportation.
        /// </summary>
        public bool interpolatePathSwitches = true;

        /// <summary>
        /// Time since the path was replaced by a new path.
        /// See: <see cref="interpolatePathSwitches"/>
        /// </summary>
        protected float pathSwitchInterpolationTime = 0;

        /// <summary>Current path which is followed</summary>
		protected ABPath path;


        /// <summary>How quickly to rotate</summary>
		public float rotationSpeed = 10;

        Vector3 previousPosition1, previousPosition2, simulatedPosition;
        Quaternion simulatedRotation;

        [UnityEngine.Serialization.FormerlySerializedAs("rotationIn2D")]
        public OrientationMode orientation = OrientationMode.ZAxisForward;

        /// <summary>
        /// 朝向
        /// </summary>
        public Quaternion rotation { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public Vector3 position
        {
            get
            {
                return tr.position;
            }
            set
            {
                tr.position = value;
            }
        }
            

        /// <summary>
        /// 速度
        /// </summary>
        public Vector3 velocity { get; }

        /// <summary>
        /// 速度
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// 是否到达目的地
        /// </summary>
        public bool reachedDestination { get; }


        /// <summary>
        /// 距离目的地的距离
        /// </summary>
        public float remainingDistance { get; }

        /// <summary>
        /// 目的地坐标
        /// </summary>
        public Vector3 destination 
        {
            get
            {
                return Game.Select.pathFindingTarget.transform.position;
            }          
        }

        public bool canSearch { get; set; }

        public bool canMove 
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 代理此时是否路径
        /// </summary>
        public bool hasPath { get; }

        /// <summary>
        /// 路径是否在计算中
        /// </summary>
        public bool pathPending { get; }

        /// <summary>
        /// 想开始或者停止代理
        /// </summary>
        public bool isStopped { get; set; }

        /// <summary>
        /// 每次计算路径时开始调用
        /// </summary>
        public System.Action onSearchPath { get; set; }

        /// <summary>
        /// 重新计算路径的频率
        /// </summary>
        public float repathRate = 0.5F;

        [System.NonSerialized]
        public bool updatePosition = true;

        /// <summary>
		/// Determines if the character's rotation should be coupled to the Transform's rotation.
		/// If false then all movement calculations will happen as usual, but the object that this component is attached to will not rotate
		/// instead only the <see cref="rotation"/> property will change.
		///
		/// See: <see cref="updatePosition"/>
		/// </summary>
		[System.NonSerialized]
        public bool updateRotation = true;

        /// <summary>
		/// If true, the AI will rotate to face the movement direction.
		/// See: <see cref="orientation"/>
		/// </summary>
		public bool enableRotation = true;

        /// <summary>True if the end of the current path has been reached</summary>
		public bool reachedEndOfPath { get; private set; }

        protected bool canSearchAgain = true;

        /// <summary>How quickly to interpolate to the new path</summary>
		public float switchPathInterpolationSpeed = 5;

        /// <summary>
        /// Holds if the Start function has been run.
        /// Used to test if coroutines should be started in OnEnable to prevent calculating paths
        /// in the awake stage (or rather before start on frame 0).
        /// </summary>
        bool startHasRun = false;


        protected virtual bool shouldRecalculatePath
        {
            get
            {
                return Time.time - lastRepath >= repathRate && canSearchAgain && canSearch && !float.IsPositiveInfinity(destination.x);
            }
        }

        /// <summary>Time when the last path request was sent</summary>
		protected float lastRepath = -9999;

        IFsm<Player> fsm;

        public void Awake()
        {
            tr = transform;

            seeker = GetComponent<Seeker>();

            seeker.startEndModifier.adjustStartPoint = () => simulatedPosition;
        }

        private void Start()
        {
            player = GetComponent<Player>();
            seeker.pathCallback += OnPathComplete;
            startHasRun = true;
            Init();
        }

        private void Init()
        {
            fsm = Game.Fsm.GetFsm<Player>(player.Id.ToString());

            if (startHasRun)
            {
                // The Teleport call will make sure some variables are properly initialized (like #prevPosition1 and #prevPosition2)
                Teleport(position, false);

                lastRepath = float.NegativeInfinity;

                if (shouldRecalculatePath)
                    SearchPath();
            }
        }

        public  Vector3 GetFeetPosition()
        {
            return position;
        }



        protected void ConfigureNewPath()
        {
            var hadValidPath = interpolator.valid;
            var prevTangent = hadValidPath ? interpolator.tangent : Vector3.zero;

            interpolator.SetPath(path.vectorPath);
            interpolator.MoveToClosestPoint(GetFeetPosition());

            if (interpolatePathSwitches && switchPathInterpolationSpeed > 0.01f && hadValidPath)
            {
                var correctionFactor = Mathf.Max(-Vector3.Dot(prevTangent.normalized, interpolator.tangent.normalized), 0);
                interpolator.distance -= speed * correctionFactor * (1f / switchPathInterpolationSpeed);
            }
        }

        /// <summary>
		/// Called when a requested path has finished calculation.
		/// A path is first requested by <see cref="SearchPath"/>, it is then calculated, probably in the same or the next frame.
		/// Finally it is returned to the seeker which forwards it to this function.
		/// </summary>
		protected void OnPathComplete(Path _p)
        {
            ABPath p = _p as ABPath;

            if (p == null)
            {
                throw new System.Exception("This function only handles ABPaths, do not use special path types");
            }
                

            canSearchAgain = true;

            // Increase the reference count on the path.
            // This is used for path pooling
            p.Claim(this);

            // Path couldn't be calculated of some reason.
            // More info in p.errorLog (debug string)
            if (p.error)
            {
                p.Release(this);
                return;
            }

            if (interpolatePathSwitches)
            {
                ConfigurePathSwitchInterpolation();
            }


            // Replace the old path
            var oldPath = path;
            path = p;
            reachedEndOfPath = false;

            // Just for the rest of the code to work, if there
            // is only one waypoint in the path add another one
            if (path.vectorPath != null && path.vectorPath.Count == 1)
            {
                path.vectorPath.Insert(0, GetFeetPosition());
            }

            // Reset some variables
            ConfigureNewPath();

            // Release the previous path
            // This is used for path pooling.
            // This is done after the interpolator has been configured in the ConfigureNewPath method
            // as this method would otherwise invalidate the interpolator
            // since the vectorPath list (which the interpolator uses) will be pooled.
            if (oldPath != null) oldPath.Release(this);

            if (interpolator.remainingDistance < 0.0001f && !reachedEndOfPath)
            {
                reachedEndOfPath = true;
                OnTargetReached();
            }
        }


        /// <summary>
        /// The end of the path has been reached.
        /// If you want custom logic for when the AI has reached it's destination
        /// add it here.
        /// You can also create a new script which inherits from this one
        /// and override the function in that script.
        /// </summary>
        public virtual void OnTargetReached()
        {
            Log.Info("寻路结束");

            interpolator.SetPath(null);
                

            fsm.SetData<VarBoolean>("isMove", false);
        }


        protected void ConfigurePathSwitchInterpolation()
        {
            bool reachedEndOfPreviousPath = interpolator.valid && interpolator.remainingDistance < 0.0001f;

            if (interpolator.valid && !reachedEndOfPreviousPath)
            {
                previousMovementOrigin = interpolator.position;
                previousMovementDirection = interpolator.tangent.normalized * interpolator.remainingDistance;
                pathSwitchInterpolationTime = 0;
            }
            else
            {
                previousMovementOrigin = Vector3.zero;
                previousMovementDirection = Vector3.zero;
                pathSwitchInterpolationTime = float.PositiveInfinity;
            }
        }

        /// <summary>
        /// 开始计算当前的路径
        /// </summary>
        public void SearchPath()
        {
            if (float.IsPositiveInfinity(destination.x)) 
                return;

            if (onSearchPath != null) 
                onSearchPath();

            lastRepath = Time.time;

            // This is where the path should start to search from
            var currentPosition = GetFeetPosition();

            // If we are following a path, start searching from the node we will
            // reach next this can prevent odd turns right at the start of the path
            /*if (interpolator.valid) {
			    var prevDist = interpolator.distance;
			    // Move to the end of the current segment
			    interpolator.MoveToSegment(interpolator.segmentIndex, 1);
			    currentPosition = interpolator.position;
			    // Move back to the original position
			    interpolator.distance = prevDist;
			}*/

            canSearchAgain = false;

            // Alternative way of creating a path request
            //ABPath p = ABPath.Construct(currentPosition, targetPoint, null);
            //seeker.StartPath(p);

            // Create a new path request
            // The OnPathComplete method will later be called with the result
            Log.Info("search");
            seeker.StartPath(currentPosition, destination);
        }

        /// <summary>
        /// 设定路径
        /// </summary>
        public void SetPath(Path path)
        {
            if (path.PipelineState == PathState.Created)
            {
                // Path has not started calculation yet
                lastRepath = Time.time;
                canSearchAgain = false;
                seeker.CancelCurrentPathRequest();
                seeker.StartPath(path);
            }
            else if (path.PipelineState == PathState.Returned)
            {
                // Path has already been calculated

                // We might be calculating another path at the same time, and we don't want that path to override this one. So cancel it.
                if (seeker.GetCurrentPath() != path)
                    seeker.CancelCurrentPathRequest();
                else
                    throw new System.ArgumentException("If you calculate the path using seeker.StartPath then this script will pick up the calculated path anyway as it listens for all paths the Seeker finishes calculating. You should not call SetPath in that case.");

                OnPathComplete(path);
            }
            else
            {
                // Path calculation has been started, but it is not yet complete. Cannot really handle this.
                throw new System.ArgumentException("You must call the SetPath method with a path that either has been completely calculated or one whose path calculation has not been started at all. It looks like the path calculation for the path you tried to use has been started, but is not yet finished.");
            }
        }


        /// <summary>
        /// 瞬移
        /// </summary>
        /// <param name="newPosition"></param>
        /// <param name="clearPath"></param>
        public void Teleport(Vector3 newPosition, bool clearPath = true)
        {
            if (clearPath)
            {
                interpolator.SetPath(null);
            }
                
            simulatedPosition = previousPosition1 = previousPosition2 = position;

            if (updatePosition)
            {
                tr.position = position;
            }

            reachedEndOfPath = false;
            if (clearPath)
            {
                SearchPath();
            }                
        }
        

        /// <summary>
        /// 外部影响的移动
        /// </summary>
        public void Move()
        {
            
        }

        private void Update()
        {
            if (shouldRecalculatePath) SearchPath();

            if (canMove && interpolator.valid)
            {
                Vector3 nextPosition;
                Quaternion nextRotation;
                MovementUpdate(Time.deltaTime, out nextPosition, out nextRotation);
                FinalizeMovement(nextPosition, nextRotation);
            }
        }

        /// <summary>
        /// 计算移动坐标
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="nextPosition"></param>
        /// <param name="nextRotation"></param>
        public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
        {
            if (updatePosition) 
                simulatedPosition = tr.position;

            if (updateRotation) 
                simulatedRotation = tr.rotation;

            Vector3 direction;

            nextPosition = CalculateNextPosition(out direction, isStopped ? 0f : deltaTime);

            if (enableRotation) 
                nextRotation = SimulateRotationTowards(direction, deltaTime);
            else 
                nextRotation = simulatedRotation;
        }


        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="nextPosition"></param>
        /// <param name="nextRotation"></param>
        public void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
        {
            previousPosition2 = previousPosition1;
            previousPosition1 = simulatedPosition = nextPosition;
            simulatedRotation = nextRotation;

            if (updatePosition) 
                tr.position = nextPosition;

            if (updateRotation) 
                tr.rotation = nextRotation;
        }



        /// <summary>Calculate the AI's next position (one frame in the future).</summary>
        /// <param name="direction">The tangent of the segment the AI is currently traversing. Not normalized.</param>
        protected Vector3 CalculateNextPosition(out Vector3 direction, float deltaTime)
        {
            if (!interpolator.valid)
            {
                direction = Vector3.zero;
                return simulatedPosition;
            }

            //Log.Info("寻路进行中。。。。。。。");

            fsm.SetData<VarBoolean>("isMove", true);

            interpolator.distance += deltaTime * speed;

            direction = interpolator.tangent;

            if (interpolator.remainingDistance < 0.0001f && !reachedEndOfPath)
            {
                reachedEndOfPath = true;
                OnTargetReached();
            }

            if (!interpolator.valid)
            {
                direction = Vector3.zero;
                return simulatedPosition;
            }

            pathSwitchInterpolationTime += deltaTime;
            var alpha = switchPathInterpolationSpeed * pathSwitchInterpolationTime;
            if (interpolatePathSwitches && alpha < 1f)
            {
                // Find the approximate position we would be at if we
                // would have continued to follow the previous path
                Vector3 positionAlongPreviousPath = previousMovementOrigin + Vector3.ClampMagnitude(previousMovementDirection, speed * pathSwitchInterpolationTime);

                // Interpolate between the position on the current path and the position
                // we would have had if we would have continued along the previous path.
                return Vector3.Lerp(positionAlongPreviousPath, interpolator.position, alpha);
            }
            else
            {
                return interpolator.position;
            }
        }

        Quaternion SimulateRotationTowards(Vector3 direction, float deltaTime)
        {
            // Rotate unless we are really close to the target
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, orientation == OrientationMode.YAxisForward ? Vector3.back : Vector3.up);
                // This causes the character to only rotate around the Z axis
                if (orientation == OrientationMode.YAxisForward) targetRotation *= Quaternion.Euler(90, 0, 0);
                return Quaternion.Slerp(simulatedRotation, targetRotation, deltaTime * rotationSpeed);
            }
            return simulatedRotation;
        }
    }

}

