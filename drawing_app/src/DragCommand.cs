using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace sketch_2d
{
    class DragCommand:Command
    {
        private Canvas.ShapeType shape;
        private Point start, end;
        bool complete;
        Frame active;
        public DragCommand(Canvas canvas): base(canvas)
        {
            shape = canvas.GetShape();
            active = null;
            complete = false;
        }
        public bool AddStartAndCommit(Point p, bool control = false)
        {
            start = end = p;
            active = canvas.Select(p, control);
            return active != null;
        }
        public int CancelShape()
        {
            int sign = canvas.CancelShape();
            if (sign < 0) active = null;
            else
                StopEdit();
            return sign;
        }
        public override bool Valid() => active != null && (shape != Canvas.ShapeType.None || start != end);
        public void EditEndAndCommit(Point p)
        {
            if (active != null && !complete)
                end = canvas.Drag(start, p);
        }
        public void StopEdit()
        {
            if (active!=null && !complete)
            {
                canvas.Unselect(start, end);
                complete = true;
            }
        }

        public override bool Commit()
        {
            if (!Canvas.OnePointShape(shape) && !canvas.Available()) return false;
            if (shape == Canvas.ShapeType.None) active.IsSelected = true;
            canvas.ChangeShape(shape);
            canvas.Select(start);
            canvas.Drag(start, end);
            canvas.Unselect(start, end);
            return true;
        }
        public override bool Rollback()
        {
            if (!Canvas.OnePointShape(shape) && !canvas.Available()) return false;
            if (shape != Canvas.ShapeType.None)
                canvas.Rewind(shape);
            else
            {
                active.IsSelected = true;
                canvas.ChangeShape(shape);
                canvas.Select(end);
                canvas.Drag(end, start);
                canvas.Unselect(end, start);
            }
            return true;
        }
    }
}
