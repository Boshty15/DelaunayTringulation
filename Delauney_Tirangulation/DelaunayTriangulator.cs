using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IxMilia.Stl;

namespace Delauney_Tirangulation
{
    public class DelaunayTriangulator
    {
        private List<Vector2D> pointCloud;
        private List<Triangle2D> listTriangle2D = new List<Triangle2D>();


        public DelaunayTriangulator(List<Vector2D> pointSet)
        {
            this.pointCloud = pointSet;
        }

        public void triangulate() 
        {
            


        }
    }
}
