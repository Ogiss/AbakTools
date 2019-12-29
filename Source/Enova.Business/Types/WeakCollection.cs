using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Types
{
    public class WeakCollection<TValue> : LogCounter, IEnumerable<TValue>, IEnumerable where TValue : class
    {
        // Fields
        private int addN;
        private readonly LinkedList<WeakReference> list;

        // Methods
        public WeakCollection()
        {
            this.list = new LinkedList<WeakReference>();
        }

        public void Add(TValue item)
        {
            lock (((WeakCollection<TValue>)this))
            {
                if ((++this.addN % 50) == 0)
                {
                    LinkedListNode<WeakReference> first = this.list.First;
                    while (first != null)
                    {
                        LinkedListNode<WeakReference> node = first;
                        first = first.Next;
                        if (!node.Value.IsAlive)
                        {
                            this.list.Remove(node);
                        }
                    }
                }
                this.list.AddLast(new WeakReference(item));
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return this.ToQueue().GetEnumerator();
        }

        public bool Remove(TValue item)
        {
            lock (((WeakCollection<TValue>)this))
            {
                for (LinkedListNode<WeakReference> node = this.list.First; node != null; node = node.Next)
                {
                    TValue target = (TValue)node.Value.Target;
                    if ((target != null) && (target == item))
                    {
                        this.list.Remove(node);
                        return true;
                    }
                }
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public TValue[] ToArray()
        {
            return this.ToQueue().ToArray();
        }

        private Queue<TValue> ToQueue()
        {
            lock (((WeakCollection<TValue>)this))
            {
                Queue<TValue> queue = new Queue<TValue>();
                LinkedListNode<WeakReference> first = this.list.First;
                while (first != null)
                {
                    LinkedListNode<WeakReference> node = first;
                    TValue target = (TValue)first.Value.Target;
                    first = first.Next;
                    if (target != null)
                    {
                        queue.Enqueue(target);
                    }
                    else
                    {
                        this.list.Remove(node);
                    }
                }
                return queue;
            }
        }

        // Properties
        public override int Count
        {
            get
            {
                lock (((WeakCollection<TValue>)this))
                {
                    int num = 0;
                    LinkedListNode<WeakReference> first = this.list.First;
                    while (first != null)
                    {
                        LinkedListNode<WeakReference> node = first;
                        first = first.Next;
                        if (node.Value.IsAlive)
                        {
                            num++;
                        }
                        else
                        {
                            this.list.Remove(node);
                        }
                    }
                    return num;
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                lock (((WeakCollection<TValue>)this))
                {
                    for (LinkedListNode<WeakReference> node = this.list.First; node != null; node = node.Next)
                    {
                        if (node.Value.IsAlive)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
    }
}
