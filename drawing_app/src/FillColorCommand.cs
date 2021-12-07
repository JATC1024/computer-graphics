using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace sketch_2d
{
    class FillColorCommand : Command
    {
        private LinkedList<Tuple<int, Color>> list;
        private Color newColor;
        public FillColorCommand(Canvas canvas, Color color): base(canvas)
        {
            list = canvas.ExportSelectedFillColor();
            newColor = color;
        }
        public override bool Valid()
        {
            return list.Count > 0;
        }
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.FillColor(list, newColor);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.FillColor(list);
            return true;
        }
    }
}
