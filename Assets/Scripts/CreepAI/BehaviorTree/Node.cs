using System.Collections.Generic;

namespace CreepAI.BehaviorTree
{
    public enum NodeState
    {
        Running, Success, Failure
    }
    
    public class Node
    {
        protected NodeState State;
        public Node Parent;
        protected readonly List<Node> Children = new List<Node>();

        /**
         * Acts as working memory of a behavior tree.
         * Any action can store data in the dictionary.
         */
        private readonly Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        protected Node()
        {
            Parent = null;
        }

        protected Node(List<Node> children)
        {
            foreach (Node child in children)
                _Attach(child);
        }

        private void _Attach(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.Failure;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        protected object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value)) return value;

            Node node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.Parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.Parent;
            }

            return false;
        }
    }
}


