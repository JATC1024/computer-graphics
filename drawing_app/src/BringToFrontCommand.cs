using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class BringToFrontCommand : Command
    {
        private LinkedList<int> list;
        public BringToFrontCommand(Canvas canvas) : base(canvas)
        {
            list = canvas.ExportSelected();
        }
        public override bool Valid() => list.Count > 0;
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.BringToFront(list);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.BringToFrontRewind(list);
            return true;
        }
    }
}
