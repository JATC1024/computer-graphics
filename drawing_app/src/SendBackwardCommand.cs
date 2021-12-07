using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class SendBackwardCommand : Command
    {
        private LinkedList<int> list;
        public SendBackwardCommand(Canvas canvas): base(canvas)
        {
            list = canvas.ExportSelected();
        }
        public override bool Valid()
        {
            return list.Count > 0;
        }
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.SendBackward(list);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.BringForward(list);
            return true;
        }
    }
}
