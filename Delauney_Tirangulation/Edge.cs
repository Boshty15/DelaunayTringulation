using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delauney_Tirangulation
{
    class Edge
    {
        public Vector2D a { get; set; }
        public Vector2D b { get; set; }


        public Edge(Vector2D a, Vector2D b)
        {
            this.a = a;
            this.b = b;
        }
        
    }
}
