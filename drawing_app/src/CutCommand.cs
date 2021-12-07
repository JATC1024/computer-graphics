using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class CutCommand : Command
    {
        private LinkedList<int> list;
        private LinkedList<Frame> cache;
        public CutCommand(Canvas canvas) : base(canvas)
        {
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
            cache = canvas.Cut(list);
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
