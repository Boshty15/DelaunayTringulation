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
        public List<Triangle2D> listTriangle2D { get;  set;}

       

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
                    max = Math.Max(Math.Max(v.x, v.y), max);
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

                        Triangle2D t1 = new Triangle2D(tmp.a, tmp.b, vector);
                        Triangle2D t2 = new Triangle2D(tmp.b, tmp.c, vector);
                        Triangle2D t3 = new Triangle2D(tmp.c, tmp.a, vector);

                        listTriangle2D.Add(t1);
                        listTriangle2D.Add(t2);
                        listTriangle2D.Add(t3);

                        preveriKot(t1, new Edge(tmp.a, tmp.b), vector);
                        preveriKot(t2, new Edge(tmp.b, tmp.c), vector);
                        preveriKot(t3, new Edge(tmp.c, tmp.a), vector);
                    }
                    else
                    {
                        Edge edge = findNearestEdge(vector);

                        Triangle2D a = null;
                        foreach(Triangle2D t in listTriangle2D)
                        {
                            if (t.isSosed(edge))
                            {
                                a = t;
                            }
                        }
                        Triangle2D b = null;
                        foreach(Triangle2D t in listTriangle2D)
                        {
                            if (t.isSosed(edge))
                            {
                                b = t;
                            }
                        }

                        Vector2D v1 = a.isNotDelTrikotnik(edge);
                        Vector2D v2 = b.isNotDelTrikotnik(edge);

                        listTriangle2D.Remove(a);
                        listTriangle2D.Remove(b);

                        Triangle2D tr1 = new Triangle2D(edge.a, v1, vector);
                        Triangle2D tr2 = new Triangle2D(edge.b, v1, vector);
                        Triangle2D tr3 = new Triangle2D(edge.a, v2, vector);
                        Triangle2D tr4 = new Triangle2D(edge.b, v2, vector);

                        listTriangle2D.Add(tr1);
                        listTriangle2D.Add(tr2);
                        listTriangle2D.Add(tr3);
                        listTriangle2D.Add(tr4);

                        preveriKot(tr1, new Edge(edge.a, v1), vector);
                        preveriKot(tr2, new Edge(edge.b, v1), vector);
                        preveriKot(tr3, new Edge(edge.a, v2), vector);
                        preveriKot(tr4, new Edge(edge.b, v2), vector);


                    }
                    //remove(superTriangle.a);
                    //remove(superTriangle.b);
                    //remove(superTriangle.c);

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
                if(tr.isSosed(e) )
                {
                    tmp = tr;
                }
            }

            if(tmp != null)
            {
                if (tmp.vKrogu(v))
                {
                    listTriangle2D.Remove(t);
                    listTriangle2D.Remove(tmp);
                    Vector2D tmp2 = tmp.isNotDelTrikotnik(e);
                    Triangle2D t1 = new Triangle2D(tmp2, e.a, v);
                    Triangle2D t2 = new Triangle2D(tmp2, e.b, v);

                    listTriangle2D.Add(t1);
                    listTriangle2D.Add(t2);

                    preveriKot(t1, new Edge(tmp2, e.a), v);
                    preveriKot(t2, new Edge(tmp2, e.b), v);
                }
                
            }

        }
        private Edge findNearestEdge(Vector2D point)
        {
            List<EdgeDistance> edgeList = new List<EdgeDistance>();

            foreach (Triangle2D triangle in listTriangle2D)
            {
                edgeList.Add(triangle.najdiNajblizjiRob(point));
            }
            
            edgeList.Sort();
            return edgeList[0].edge;
        }
        private void remove(Vector2D vector)
        {
            foreach(Triangle2D t in listTriangle2D)
            {
                if (t.isTockaTrikotnik(vector))
                {
                    listTriangle2D.Remove(t);
                }
            }
        }
    }
}
