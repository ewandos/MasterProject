using System.Collections.Generic;
using CreepAI.BehaviorTree;
using CreepAI.Check;
using CreepAI.Task;
using UnityEngine;
using UnityEngine.AI;
using Tree = CreepAI.BehaviorTree.Tree;

namespace CreepAI
{
    public class CreepBehaviorTree : Tree
    {
        public float agency = 0;
        
        // Detection
        public static float fovRange = 15;
        public static LayerMask EnemyLayer = 1 << 6;
        public static LayerMask AllyLayer = 1 << 7;
        
        // Wander
        public static float wanderSpeed = 2;
        public static float wanderRadius = 15;
        
        // Pursue 
        public static float pursuingSpeed = 6;
        public static float lookAheadMultiplier = 2;

        // Patrol
        public GameObject[] waypoints;
        public static float patrolSpeed = 1;
        public static float patrollingDistance = 3;
        
        // Flee
        public static float fleeSpeed = 8;
        public static float fleeMultiplier = 1;

        private NavMeshAgent _agent;

        protected override Node SetupTree()
        {
            _agent = GetComponent<NavMeshAgent>();

            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckTargetInRange(transform),
                    new CheckIsAlone(transform),
                    new CheckLineOfSightToTarget(transform),
                    new CheckIsStuck(_agent, transform, false),
                    new TaskFlee(_agent, transform),
                }),
                new Sequence(new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new CheckHasTarget(true),
                        new CheckTargetInRange(transform),
                    }),
                    new Selector(new List<Node>
                    {
                        new CheckAlliesInRange(transform),
                        new CheckIsStuck(_agent, transform, true),
                    }),
                    new TaskTellAlliesAboutTarget(),
                    new TaskPursue(transform, _agent)
                }),
                new Sequence(new List<Node>
                {
                    new TaskWander(_agent, transform),
                    new CheckAlliesInRange(transform),
                    new TaskFlock(_agent, transform),
                })
            });
            
            return root;
        }
    }

}
