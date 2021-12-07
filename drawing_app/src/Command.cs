using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sketch_2d
{
    abstract class Command
    {
        protected Canvas canvas;
        public Command(Canvas canvas)
        {
            this.canvas = canvas;
        }
        public virtual bool Valid() => true;
        public abstract bool Commit();
        public abstract bool Rollback();
    }
}
