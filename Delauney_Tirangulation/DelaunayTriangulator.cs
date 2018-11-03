using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IxMilia.Stl;

namespace Delauney_Tirangulation
{
    class DelaunayTriangulator
    {
        private List<Vector2D> pointCloud;
        private List<Triangle2D> listTriangle2D;


        public DelaunayTriangulator()
        {
            this.pointCloud = new List<Vector2D>();
            this.listTriangle2D = new List<Triangle2D>();
        }

        public DelaunayTriangulator(List<Vector2D> pointCloud)
        {
            this.pointCloud = pointCloud;
            this.listTriangle2D = new List<Triangle2D>();
        }

        public void triangulate() 
        {
            if(pointCloud != null && pointCloud.Count > 3)
            {
                int max = 0;
                foreach(Vector2D v in pointCloud)
                {
                    max = Math.Max(Math.Max(v.getX(), v.getY()), max);
                }
                max *= 16;

                Vector2D p1 = new Vector2D(0, 3 * max);
                Vector2D p2 = new Vector2D(3 * max, 0);
                Vector2D p3 = new Vector2D(-3 * max, -3 * max);

                Triangle2D superTriangle = new Triangle2D(p1, p2, p3);
                listTriangle2D.Add(superTriangle);

                foreach(Vector2D vector in pointCloud)
                {
                    Triangle2D tmp = poisciTrikotni(vector);

                    if(tmp != null)
                    {
                        // Tocka je znotraj trikotnika.
                        listTriangle2D.Remove(tmp);

                        Triangle2D t1 = new Triangle2D(tmp.A, tmp.B, vector);
                        Triangle2D t2 = new Triangle2D(tmp.B, tmp.C, vector);
                        Triangle2D t3 = new Triangle2D(tmp.C, tmp.A, vector);

                        listTriangle2D.Add(t1);
                        listTriangle2D.Add(t2);
                        listTriangle2D.Add(t3);

                        preveriKot(t1, new Edge(tmp.A, tmp.B), vector);
                        preveriKot(t2, new Edge(tmp.B, tmp.C), vector);
                        preveriKot(t3, new Edge(tmp.C, tmp.A), vector);
                    }
                    else
                    {

                    }

                }



            }


        }
        private Triangle2D poisciTrikotni(Vector2D vector)
        {
            foreach(Triangle2D t in listTriangle2D)
            {
                if (t.contains(vector))
                {
                    return t;
                }
            }
            return null;
        }
        private void preveriKot(Triangle2D t, Edge e, Vector2D v)
        {
            Triangle2D tmp = new Triangle2D();
            foreach(Triangle2D tr in listTriangle2D)
            {
                if(tr.isSosed(e) && tr != t)
                {
                    tmp = tr;
                }
            }

            if(tmp != null)
            {
                listTriangle2D.Remove(t);
                listTriangle2D.Remove(tmp);
                Vector2D tmp2 = tmp.isNotDelTrikotnik(e);
                Triangle2D t1 = new Triangle2D(tmp2, e.A, v);
                Triangle2D t2 = new Triangle2D(tmp2, e.B, v);

                listTriangle2D.Add(t1);
                listTriangle2D.Add(t2);

                preveriKot(t1, new Edge(tmp2, e.A), v);
                preveriKot(t2, new Edge(tmp2, e.B), v);
            }

        }
    }
}
