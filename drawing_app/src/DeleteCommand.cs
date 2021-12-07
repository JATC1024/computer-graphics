using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class DeleteCommand : Command
    {
        private LinkedList<int> list;
        private LinkedList<Frame> cache;
        public DeleteCommand(Canvas canvas, bool all = false) : base(canvas)
        {
            if (all)
                list = canvas.ExportAll();
            else
                list = canvas.ExportSelected();
            cache = null;
        }
        public override bool Valid()
        {
            return list.Count > 0;
        }
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            cache = canvas.Delete(list);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.DeleteRewind(list, cache);
            return true;
        }
    }
}
