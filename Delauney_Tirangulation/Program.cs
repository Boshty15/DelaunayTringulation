using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IxMilia.Stl;

namespace Delauney_Tirangulation
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string file = @"C:\Users\Bostjan\Documents\FRI\1.Letnik\Napredna_Računalnišk_Grafika\Vaje\Delauney_Tirangulation\Delauney_Tirangulation\Delauney_Tirangulation\bin\Debug\TM1_524_124.asc";
            List<Vector2D> pointCloud = new List<Vector2D>();
            pointCloud = readFile(file);



            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i + 1);
                Console.WriteLine("X: " + pointCloud[i].getX() + " Y: " + pointCloud[i].getY());
            }

            






        

        }
        private static List<Vector2D> readFile(string path)
        {
            StreamReader reader = File.OpenText(path);
            List<Vector2D> pointCloud = new List<Vector2D>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(';');
                List<int> listInt = new List<int>();

                foreach (string item in items)
                {
                    string[] item2 = item.Split('.');
                    listInt.Add(int.Parse(item2[0]));
                }

                pointCloud.Add(new Vector2D(listInt[0], listInt[1]));
                listInt = new List<int>();

            }
            return pointCloud;

        }
        
    }
}
