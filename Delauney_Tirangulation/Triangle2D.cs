using Aardvark.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delauney_Tirangulation
{
    class Triangle2D
    {
        public Vector2D a { get; set; }
        public Vector2D b { get; set; }
        public Vector2D c { get; set; }



        public Triangle2D(Vector2D a, Vector2D b, Vector2D c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public Triangle2D()
        {
        }
     

        /*
         Preverjanje, če je točka v trikotniku.
             */
        public bool contains(Vector2D vector)
        {
            double aa = vector.minus(a).cross(b.minus(a));
            double bb = vector.minus(b).cross(a.minus(b));
            
            return true;
        }

        public bool vKrogu(Vector2D vector)
        {
            
            double a11 = a.x - vector.x;
            double a21 = b.x - vector.x;
            double a31 = c.x - vector.x;

            double a12 = a.y - vector.y;
            double a22 = b.y - vector.y;
            double a32 = c.y - vector.y;

            double a13 = Math.Pow(a.x - vector.x, 2) + Math.Pow(a.y - vector.y, 2);
            double a23 = Math.Pow(b.x - vector.x, 2) + Math.Pow(b.y - vector.y, 2);
            double a33 = Math.Pow(c.x - vector.x, 2) + Math.Pow(c.y - vector.y, 2);

            double determinanta = a11 * a22 * a33 + a12 * a23 * a31 + a13 * a21 * a32 - a13 * a22 * a31 - a12 * a21 * a33
                - a11 * a23 * a32;

            if (preverjanjeTrikotnika())
            {
                return true;
            }
                return false;
        }
        public bool preverjanjeTrikotnika()
        {
            double a11 = a.x - c.x;
            double a21 = b.x - c.x;

            double a12 = a.y - c.y;
            double a22 = b.y - c.y;

            double determinanta = a11 * a22 - a12 * a21;

            return determinanta > 0.0d;
        }

        public bool isSosed(Edge edge)
        {
            return (a == edge.a || b == edge.a || c == edge.a) && (a == edge.b || b == edge.b || c == edge.b);
        }

        public Vector2D isNotDelTrikotnik(Edge edge)
        {
            if (a != edge.a && a != edge.b)
            {
                return a;
            }
            else if (b != edge.a && b != edge.b)
            {
                return b;
            }
            else if (c != edge.a && c != edge.b)
            {
                return c;
            }

            return null;

        }
        /*
         Preveri, če je točka katera od trikotnika.
             */
        public bool isTockaTrikotnik(Vector2D vector)
        {
            if(a == vector || b == vector || c == vector)
            {
                return true;
            }
            return false;
        }



        private Vector2D izracunMinTocke(Edge e, Vector2D v)
        {
            Vector2D vector = e.b.minus(e.a);
            double tmp = v.minus(e.a).dot(vector) / vector.dot(vector);

            return e.a.plus(vector.skalar(tmp));
        }

        public EdgeDistance najdiNajblizjiRob(Vector2D vector)
        {
            List<EdgeDistance> listEdges = new List<EdgeDistance>();

            listEdges.Add(new EdgeDistance(new Edge(a, b), izracunMinTocke(new Edge(a, b), vector).minus(vector).mag()));
            listEdges.Add(new EdgeDistance(new Edge(b, c), izracunMinTocke(new Edge(b, c), vector).minus(vector).mag()));
            listEdges.Add(new EdgeDistance(new Edge(c, a), izracunMinTocke(new Edge(c, a), vector).minus(vector).mag()));

           // listEdges.Sort();
            return listEdges[0];
        }
        public override string ToString()
        {
            return "Tikontik: a(" + a + ") b(" + b + ") c(" + c + ")"; 
        }

    }
}
