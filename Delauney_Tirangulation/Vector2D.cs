using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delauney_Tirangulation
{
    class Vector2D
    {
        private int x;
        private int y;

        public Vector2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2D()
        {
        }

        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }

        public Vector2D minus (Vector2D vector)
        {
            return new Vector2D((this.x - vector.x), (this.y - vector.y));
        }
        public Vector2D plus (Vector2D vector)
        {
            return new Vector2D((this.x + vector.x), (this.y + vector.y));
        }
        public Vector2D skalar(double d)
        {
            return new Vector2D((int)(this.x * d), (int)(this.y * d));
        }
        public double cross(Vector2D vector)
        {
            return this.y * vector.x - this.x * vector.y;
        }
        public double mag()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y);
        }
        public double dot(Vector2D vector)
        {
            return this.x * vector.x + this.y * vector.y;
        }

    }
}
