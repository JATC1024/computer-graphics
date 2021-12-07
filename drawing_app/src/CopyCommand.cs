using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class CopyCommand : Command
    {
        private LinkedList<int> list;
        public CopyCommand(Canvas canvas) : base(canvas)
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
            canvas.Copy(list);
            return true;
        }
        public override bool Rollback()
        {
            return true;
        }
    }
}
