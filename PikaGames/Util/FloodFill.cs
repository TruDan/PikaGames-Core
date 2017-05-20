using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace PikaGames.Games.Core.Util
{
    public static class FloodFill
    {

        public static int FillGrid(byte[,] grid, Point pt, byte replacement)
        {
            //DumpGrid(grid);
            var c = 0;
            Stack<Point> pixels = new Stack<Point>();
            byte target = grid[pt.X,pt.Y];
            pixels.Push(pt);

            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                if (a.X < grid.GetLength(0) && a.X >= 0 &&
                    a.Y < grid.GetLength(1) && a.Y >= 0)//make sure we stay within bounds
                {

                    if (grid[a.X,a.Y] == target)
                    {
                        grid[a.X,a.Y] = replacement;
                        c++;

                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }

            //DumpGrid(grid);

            return c;
        }

        public static void DumpGrid(byte[,] grid)
        {
            Debug.WriteLine("----------------------------");
            for (int i=0;i<grid.GetLength(0);i++)
            {
                string str = "";
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    str += "\t" + grid[i, j];
                }
                Debug.WriteLine(str);
            }
            Debug.WriteLine("----------------------------");
        }
    }
}
