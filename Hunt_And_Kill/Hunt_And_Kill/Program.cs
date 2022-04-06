using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunt_And_Kill
{
    class Program
    {
        static void Main(string[] args)
        {
            Draw.GridCreate();
            Draw.GridDisplay();
            Draw.Hunt();
            Draw.GridClearer();
            Console.ReadLine();
        }
    }

    public class Draw
    {
        public static Random Number = new Random();
        public static int startx;
        public static int starty;
        public static int x = 63;
        public static int y = 63;
        public static string[,] Grid = new string[x, y];
        public static string Row = "";
        public static string TopLeft = "┌";
        public static string TopRight = "┐";
        public static string BottomLeft = "└";
        public static string BottomRight = "┘";
        public static string Horizontal = "─";
        public static string Vertical = "|";
        public static string Clear = "C";
        public static string Empty = "E";
        public static void GridCreate()
        {
            for (int vert = 0; vert < 63; vert++)
            {
                for (int hori = 0; hori < 63; hori++)
                {

                    if (vert == 0)
                    {
                        if (hori == 0)
                        {
                            Grid[hori, vert] = TopLeft;
                        }
                        else if (hori == 62)
                        {
                            Grid[hori, vert] = TopRight;
                        }
                        else
                        {
                            Grid[hori, vert] = Horizontal;
                        }
                    }
                    else if (vert == 62)
                    {
                        if (hori == 0)
                        {
                            Grid[hori, vert] = BottomLeft;
                        }
                        else if (hori == 62)
                        {
                            Grid[hori, vert] = BottomRight;
                        }
                        else
                        {
                            Grid[hori, vert] = Horizontal;
                        }
                    }

                    else
                    {
                        if (hori == 0 || hori == 62)
                        {
                            Grid[hori, vert] = Vertical;
                        }
                        else
                        {
                            if (hori % 2 != 0 && vert % 2 != 0)
                            {
                                Grid[hori, vert] = Empty;
                            }
                            else
                            {
                                Grid[hori, vert] = " ";
                            }
                            
                        }
                    }
                }
            }
        }
        public static void GridDisplay()
        {
            for (int vert = 0; vert < 63; vert++)
            {
                for (int hori = 0; hori < 63; hori++)
                {
                    Row += Grid[hori, vert];
                }
                Console.WriteLine(Row);
                Row = "";
            }
        }

        public static void Hunt()
        {
            while (true)
            {


                bool found = false;
                int vert = 0;
                bool free = false;
                while (vert < 63)
                {
                    int hori = 0;
                    while (hori < 63)
                    {
                        if (Grid[hori, vert] == Empty)
                        {
                            Grid[hori, vert] = Clear;
                            startx = hori;
                            starty = vert;
                            Enclose();
                            free = true;
                            found = true;
                            break;
                        }
                        else
                        {
                            hori += 1;
                        }
                    }
                    if (free == true)
                    {
                        break;
                    }
                    else
                    {
                        vert += 1;
                    }
                }
                Kill();
                if (found == false)
                {
                    Console.WriteLine("Maze Generation Complete");
                    break;
                }
            }
        }

        public static void Kill()
        {
            int Direction;
            bool Available = true;
            while (Available == true)
            {
                GridDisplay();
                Direction = Number.Next(1, 5);
                /*
                 1 = left
                 2 = up
                 3 = right
                 4 = down
                */
                if (Direction == 1)
                {
                    if (startx >= 3)
                    {
                        if (Grid[startx - 2, starty] == Empty)
                        {
                            Grid[startx - 2, starty] = Clear;
                            startx = startx - 2;
                            Enclose();
                            Break(Direction);
                            Direction = 0;
                        }
                    }
                }

                else if (Direction == 2)
                {
                    if (starty >= 3)
                    {
                        if (Grid[startx, starty - 2] == Empty)
                        {
                            Grid[startx, starty - 2] = Clear;
                            starty = starty - 2;
                            Enclose();
                            Break(Direction);
                            Direction = 0;
                        }
                    }
                }

                else if (Direction == 3)
                {
                    if (startx <= 59)
                    {
                        if (Grid[startx + 2, starty] == Empty)
                        {
                            Grid[startx + 2, starty] = Clear;
                            startx = startx + 2;
                            Enclose();
                            Break(Direction);
                            Direction = 0;
                        }
                    }
                }

                else if (Direction == 4)
                {
                    if (starty <= 59)
                    {
                        if (Grid[startx, starty + 2] == Empty)
                        {
                            Grid[startx, starty + 2] = Clear;
                            starty = starty + 2;
                            Enclose();
                            Break(Direction);
                            Direction = 0;
                        }
                    }
                }
                Available = AvailabilityScan();
            }
            
        }
        public static void Enclose()
        {
            /*
            1 = left
            2 = up
            3 = right
            4 = down
            */
            if (Grid[startx - 1, starty] == " ")
            {
                Grid[startx - 1, starty] = "|";
            }

            if (Grid[startx, starty + 1] == " ")
            {
                Grid[startx, starty + 1] = "─";
            }

            if (Grid[startx + 1, starty] == " ")
            {
                Grid[startx + 1, starty] = "|";
            }

            if (Grid[startx, starty - 1] == " ")
            {
                Grid[startx, starty - 1] = "─";
            }

        }
        public static void Break(int Direction)
        {
            /*
            1 = left
            2 = up
            3 = right
            4 = down
            */
            if (Direction == 1)
            {
                Grid[startx + 1, starty] = " ";
            }
            else if (Direction == 2)
            {
                Grid[startx, starty + 1] = " ";
            }
            else if (Direction == 3)
            {
                Grid[startx - 1, starty] = " ";
            }
            else if (Direction == 4)
            {
                Grid[startx, starty - 1] = " ";
            }
        }
        public static bool AvailabilityScan()
        {
            bool Available = false;
            if (startx >= 3)
            {
                if (Grid[startx - 2, starty] == Empty)
                {
                    Available = true;
                }
            }

            if (starty >= 3)
            {
                if (Grid[startx, starty - 2] == Empty)
                {
                    Available = true;
                }
            }
            
            if (startx <= 59)
            {
                if (Grid[startx + 2, starty] == Empty)
                {
                    Available = true;
                }
            }

            if (starty <= 59)
            {
                if (Grid[startx, starty + 2] == Empty)
                {
                    Available = true;
                }
            }

            else
            {
                Available = false;
            }
            return Available;
        }

        public static void GridClearer()
        {
            for (int vert = 0; vert < 63; vert++)
            {
                for (int hori = 0; hori < 63; hori++)
                {
                    if (Grid[hori, vert] == Clear)
                    {
                        Grid[hori, vert] = " ";
                    }
                }
            }
            GridDisplay();
            Console.ReadLine();
        }
    }
}
