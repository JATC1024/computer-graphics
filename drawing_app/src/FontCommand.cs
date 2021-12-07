using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace sketch_2d
{
    class FontCommand : Command
    {
        private LinkedList<Tuple<int, Font>> list;
        private Font newFont;
        public FontCommand(Canvas canvas, Font font) : base(canvas)
        {
            newFont = font;
            list = canvas.ExportSelectedFont();
        }
        public override bool Valid()
        {
            return list.Count > 0;
        }
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.SetFont(list, newFont);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.SetFont(list);
            return true;
        }
    }
}
