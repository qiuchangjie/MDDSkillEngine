using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;


namespace Pathfinding
{
    public class PathFindingTest : VersionedMonoBehaviour
    {
        public static Action idleAction;
        public static Action workAction;
        public static Action attackAction;



        public Transform target;

        public AnimancerComponent animancer;
       

        [SerializeField] private ClipState.Transition _MainAnimation;

        [SerializeField] private ClipState.Transition Idle;

        [SerializeField] private ClipState.Transition Work;

        [SerializeField] private ClipState.Transition Attack;

        IAstarAI ai;

        public void Start()
        {
            idleAction += () => { animancer.Play(Idle); };
            workAction += () => { animancer.Play(Work); };
            attackAction += () => 
            {
                Attack.Events.OnEnd += () => { animancer.Play(Attack); };
                animancer.Play(Attack);
                //Attack.Speed = 3f;
            };


        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.LogError(other.name);
        }

        void OnEnable()
        {
            ai = GetComponent<IAstarAI>();
            animancer = GetComponent<AnimancerComponent>();
            // Update the destination right before searching for a path as well.
            // This is enough in theory, but this script will also update the destination every
            // frame as the destination is used for debugging and may be used for other things by other
            // scripts as well. So it makes sense that it is up to date every frame.

            animancer.Play(Idle);

            

            if (ai != null) ai.onSearchPath += Update;
        }

        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }

        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
        {
            if (target != null && ai != null)
            {
                if (ai.destination == target.position)
                    return;

                ai.destination = target.position;
            }
        }
    }
}
