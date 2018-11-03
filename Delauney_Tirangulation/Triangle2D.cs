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
        private Vector2D a;
        private Vector2D b;
        private Vector2D c;

        public Triangle2D(Vector2D a, Vector2D b, Vector2D c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }
        public Triangle2D()
        {
        }
        public Vector2D C { get => c; set => c = value; }
        public Vector2D B { get => b; set => b = value; }
        public Vector2D A { get => a; set => a = value; }

        /*
         Preverjanje, če je točka v trikotniku.
             */
        public bool contains(Vector2D vector)
        {
            double aa = vector.minus(a).cross(b.minus(a));
            double bb = vector.minus(b).cross(a.minus(b));

            if (!istiPredznak(aa, bb) || !istiPredznak(bb,aa)){
                return false;
            }

            return true;
        }
        private bool istiPredznak(double a, double b)
        {
            return Math.Sign(a) == Math.Sign(b);
        }

        public bool vKrogu(Vector2D vector)
        {
            double a11 = a.getX() - vector.getX();
            double a21 = b.getX() - vector.getX();
            double a31 = c.getX() - vector.getX();

            double a12 = a.getY() - vector.getY();
            double a22 = b.getY() - vector.getY();
            double a32 = c.getY() - vector.getY();

            double a13 = Math.Pow(a.getX() - vector.getX(), 2) + Math.Pow(a.getY() - vector.getY(), 2);
            double a23 = Math.Pow(b.getX() - vector.getX(), 2) + Math.Pow(b.getY() - vector.getY(), 2);
            double a33 = Math.Pow(c.getX() - vector.getX(), 2) + Math.Pow(c.getY() - vector.getY(), 2);

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
            double a11 = a.getX() - c.getX();
            double a21 = b.getX() - c.getX();

            double a12 = a.getY() - c.getY();
            double a22 = b.getY() - c.getY();

            double determinanta = a11 * a22 - a12 * a21;

            return determinanta > 0.0d;
        }

        public bool isSosed(Edge edge)
        {
            return (a == edge.A || b == edge.A || c == edge.A) && (a == edge.B || b == edge.B || c == edge.B);
        }

        public Vector2D isNotDelTrikotnik(Edge edge)
        {
            if (a != edge.A && a != edge.B)
            {
                return a;
            }
            else if (b != edge.A && b != edge.B)
            {
                return b;
            }
            else if (c != edge.A && c != edge.B)
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
            Vector2D vector = e.B.minus(e.A);
            double tmp = v.minus(e.A).dot(vector) / vector.dot(vector);

            return e.A.plus(vector.skalar(tmp));
        }

        public EdgeDistance najdiNajblizjiRob(Vector2D vector)
        {
            List<EdgeDistance> listEdges = new List<EdgeDistance>();

            listEdges.Add(new EdgeDistance(new Edge(a, b), izracunMinTocke(new Edge(a, b), vector).minus(vector).mag()));
            listEdges.Add(new EdgeDistance(new Edge(b, c), izracunMinTocke(new Edge(b, c), vector).minus(vector).mag()));
            listEdges.Add(new EdgeDistance(new Edge(c, a), izracunMinTocke(new Edge(c, a), vector).minus(vector).mag()));

            listEdges.Sort();
            return listEdges[0];
        }

    }
}
