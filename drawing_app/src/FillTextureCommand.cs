using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace sketch_2d
{
    class FillTextureCommand : Command
    {
        private LinkedList<Tuple<int, Bitmap>> list;
        Bitmap newTexture;
        public FillTextureCommand(Canvas canvas, Bitmap texture): base(canvas)
        {
            list = canvas.ExportSelectedTexture();
            newTexture = texture;
        }
        public override bool Valid() => list.Count > 0;
        public override bool Commit()
        {
            if (!canvas.Available()) return false;
            canvas.FillTexture(list, newTexture);
            return true;
        }
        public override bool Rollback()
        {
            if (!canvas.Available()) return false;
            canvas.FillTexture(list);
            return true;
        }
    }
}
