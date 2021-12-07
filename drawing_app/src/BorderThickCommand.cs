using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class BorderThickCommand : Command
    {
        private LinkedList<Tuple<int, int>> list;
        private int newThick;
        public BorderThickCommand(Canvas canvas, int thick): base(canvas)
        {
            newThick = thick;
            list = canvas.ExportSelectedThick();
        }
        public override bool Valid()
        {
            return list.Count > 0;
        }
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.BorderThick(list, newThick);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.BorderThick(list);
            return true;
        }
    }
}
