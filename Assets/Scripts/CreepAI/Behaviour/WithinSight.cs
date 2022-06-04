using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Drawing;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace CreepAI.Behaviour
{
    public class WithinSight : Conditional
    {
        public float fovAngle;
        public float fovDistance;
        public string targetTag;
        public SharedTransform sharedTarget;

        private Transform target;

        public override void OnAwake()
        {
            target = GameObject.FindGameObjectsWithTag(targetTag)[0].transform;
        }

        public override TaskStatus OnUpdate()
        {
            float deg = fovAngle * 0.5f;
            float passiveDistance = fovDistance * 0.4f;
            
            Vector3 position = transform.position;
            Vector3 foo = transform.forward * fovDistance;
            Vector3 rotatedVector1 = Quaternion.AngleAxis(deg, Vector3.up) * foo;
            Vector3 rotatedVector2 = Quaternion.AngleAxis(-deg, Vector3.up) * foo;
            Draw.SolidArc(position, position + rotatedVector1, position + rotatedVector2);
            Draw.SolidCircle(position, Vector3.up, passiveDistance);

            Vector3 targetTransform = target.transform.position;
            Vector3 direction = targetTransform - position;
            float distance = Vector3.Distance(targetTransform, position);
            bool withinAngle = Vector3.Angle(direction, transform.forward) < deg;
            bool withingDistance = distance <= fovDistance;
            bool withinPassiveDistance = distance <= passiveDistance;
            bool withingSight = (withinAngle && withingDistance) || withinPassiveDistance;

            if (withingSight)
                sharedTarget.Value = target;
            
            return withingSight ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}