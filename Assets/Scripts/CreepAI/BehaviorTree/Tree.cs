using System.Collections.Generic;
using UnityEngine;

namespace CreepAI.BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        public Node Root = null;
        private readonly Dictionary<string, object> _data = new Dictionary<string, object>();

        protected void Start()
        {
            Root = SetupTree();
        }

        private void Update()
        {
            if (Root != null)
                Root.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}
