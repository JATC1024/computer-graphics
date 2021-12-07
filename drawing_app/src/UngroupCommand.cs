using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class UngroupCommand : Command
    {
        private LinkedList<Tuple<int, int>> list;
        public UngroupCommand(Canvas canvas) : base(canvas)
        {
            list = canvas.ExportSelectedCount();
        }
        public override bool Valid() => list.Count > 0;
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.Ungroup(list);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.UngroupRewind(list);
            return true;
        }
    }
}
