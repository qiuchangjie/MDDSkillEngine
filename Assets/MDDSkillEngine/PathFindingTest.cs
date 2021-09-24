using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;
using Pathfinding;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class PathFindingTest : MonoBehaviour
    {
        public System.Action idleAction;
        public System.Action workAction;
        public System.Action attackAction;
        public System.Action died;

        public bool isPath;

        public Transform target;

        public AnimancerComponent animancer;
       

        [SerializeField] private ClipState.Transition Idle;

        [SerializeField] private ClipState.Transition Work;

        [SerializeField] private ClipState.Transition Attack;

        [SerializeField] private ClipState.Transition Died;

        IAstarAI ai;

        AIPath aIPath;

        public void Start()
        {
            target=GameObject.Find("GameObject").transform;
            died += () => { animancer.Play(Died); };
            idleAction += () => { animancer.Play(Idle); };
            workAction += () => { animancer.Play(Work); };
            attackAction += () => 
            {
                //Attack.Events.OnEnd += () => { animancer.Play(Attack); };
                animancer.Play(Attack);
                //Attack.Speed = 3f;
            };

            Attack.Events.OnEnd += () => { animancer.Play(Idle); };


        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.LogError(other.name);
        }

        void OnEnable()
        {
            aIPath = GetComponent<AIPath>();
            if(!isPath)
            ai = GetComponent<IAstarAI>();
            animancer = GetComponent<AnimancerComponent>();
            // Update the destination right before searching for a path as well.
            // This is enough in theory, but this script will also update the destination every
            // frame as the destination is used for debugging and may be used for other things by other
            // scripts as well. So it makes sense that it is up to date every frame.

            animancer.Play(Idle);

            aIPath.reach += reach;

            if (ai != null) ai.onSearchPath += Update;
        }

        
        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }

        public void reach(object obj,EventArgs e)
        {
            animancer.Play(Idle);
        }

        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
        {
            //if (ai.reachedDestination)
            //{
            //    Log.Error(ai.reachedDestination);
            //    animancer.Play(Idle);
            //}

           // ai.onSearchPath

            if (target != null && ai != null)
            {
                if (ai.destination == target.position)
                {
                    
                    return;
                }
                   

                ai.destination = target.position;
            }
        }
    }
}
