using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CreepAI.Behaviour
{
    public class DealDamage : Action
    {
        public SharedTransform target;
        public int amount = 25;
        public float cooldown = 1f;
        private float timer;
        private HealthSystem playerHealthSystem;

        public override void OnStart()
        {
            playerHealthSystem = target.Value.gameObject.GetComponent<HealthSystem>();
        }

        public override TaskStatus OnUpdate()
        {
            if (timer >= 0f)
            {
                timer -= Time.deltaTime;
                return TaskStatus.Running;
            }

            timer = cooldown;
            playerHealthSystem.TakeDamage(amount);
            
            return TaskStatus.Success;
        }
    }
}
