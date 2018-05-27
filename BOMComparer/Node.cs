using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMComparer

{
    class Node<t>
    {
        private t value;
        private Node<t> next;
        public Node(t value)
        {
            this.value = value;
            this.next = null;
        }
        public Node()
        {
            
            this.next = null;
        }
        public Node(t value,Node<t> next)
        {
            this.value = value;
            this.next = next;
        }
        public t GetValue()
        {
            return this.value;
        }
        public Node<t> GetNext()
        {
            return this.next;
        }
        public bool HasNext()
        {
            return (this.next != null);
        }
        public void SetValue(t value)
        {
            this.value = value;
        }
        public void SetNext(Node<t> next)
        {
            this.next = next;
        }
        public override string ToString()
        {
            return value.ToString() + "-->\n" + next;
        }

    }
}
