using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace sketch_2d
{
    class BorderLineCommand: Command
    {
        private LinkedList<Tuple<int, DashStyle>> list;
        private DashStyle newDash;
        public BorderLineCommand(Canvas canvas, DashStyle dash): base(canvas)
        {
            newDash = dash;
            list = canvas.ExportSelectedLine();
        }
        public override bool Valid()
        {
            return list.Count > 0;
        }
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.Border(list, newDash);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.Border(list);
            return true;
        }
    }
}
