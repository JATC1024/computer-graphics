using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class Sequence
    {
        private LinkedList<Command> list;
        private LinkedListNode<Command> current;
        public Sequence()
        {
            list = new LinkedList<Command>();
            current = null;
        }
        public void Clear()
        {
            current = null;
            list.Clear();
        }
        public void Add(Command cmd)
        {
            if (!cmd.Valid()) return;
            if (current != null)
            {
                while (current.Next != null)
                    list.Remove(current.Next);
                list.AddAfter(current, cmd);
                current = current.Next;
            }
            else
            {
                list.Clear();
                list.AddLast(cmd);
                current = list.Last;
            }
        }
        public void Remove(int count)
        {
            if (current == null) list.Clear();
            else while (current.Next != null)
                    list.RemoveLast();
            for (int i = 0; i < count && current != null; ++i)
            {
                var pre = current.Previous;
                list.Remove(current);
                current = pre;
            }
        }
        public void Undo()
        {
            if (current != null)
            {
                bool ok = current.Value.Rollback();
                if (ok) current = current.Previous;
            }
        }
        public void Redo()
        {
            if (current == null && list.Count > 0)
            {
                current = list.First;
                bool ok = current.Value.Commit();
                if (!ok) current = null;
            }
            else if (current != null && current.Next != null)
            {
                current = current.Next;
                bool ok = current.Value.Commit();
                if (!ok) current = current.Previous;
            }
        }
    }
}
