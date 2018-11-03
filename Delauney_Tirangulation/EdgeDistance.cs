using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delauney_Tirangulation
{
    class EdgeDistance
    {
        public Edge edge;
        public double d;

        public EdgeDistance(Edge e, double d)
        {
            this.edge = e;
            this.d = d;
        }
        public int primerjaj(EdgeDistance e)
        {
            return this.d.CompareTo(e.d);
        }
    }
}
