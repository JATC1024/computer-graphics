using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    class PasteCommand: Command
    {
        private int size;
        public PasteCommand(Canvas canvas): base(canvas)
        {
            size = canvas.CacheSize();
        }
        public override bool Valid()
        {
            return size > 0;
        }
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.Paste();
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.Unpaste(size);
            return true;
        }
    }
}
