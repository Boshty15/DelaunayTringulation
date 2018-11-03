using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delauney_Tirangulation
{
    class Edge
    {
        private Vector2D a;
        private Vector2D b;

        public Vector2D B { get => b; set => b = value; }
        public Vector2D A { get => a; set => a = value; }

        public Edge(Vector2D a, Vector2D b)
        {
            this.A = a;
            this.B = b;
        }
    }
}
