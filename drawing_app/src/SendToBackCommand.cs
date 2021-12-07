using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class SendToBackCommand : Command
    {
        private LinkedList<int> list;
        public SendToBackCommand(Canvas canvas): base(canvas)
        {
            list = canvas.ExportSelected();
        }
        public override bool Valid() => list.Count > 0;
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.SendToBack(list);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.SendToBackRewind(list);
            return true;
        }
    }
}
