using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProgramaTest
{
    class Program
    {
        #region Atributes
        //relacion de consola 8 alto 15 de largo (cuadrado perfecto);

        public static ConsoleColor console_color = ConsoleColor.Red;

        public static char empty = ' ';
        public static char limits = '.';
        public static char wall = '#';
        public static char door = '@';
        public static char path = '+';
        public static char pathDown = '+';

        public static List<Room> rooms = new List<Room>();
        public static List<int[]> memory = new List<int[]>();

        public static Grid grid = new Grid
            (
                new Coords
                {
                    x = 15 * 7,
                    y = 26//8 * 3.8
                }
            );

        #endregion
        
        static void Main(string[] args)
        {
            Console.ForegroundColor = console_color;

            //main Loop
            while (true)
            {
                Console.Clear();

                var watch = Stopwatch.StartNew();

                CreateLimits();
                CreateBoxes(15);
                BuildBoxes();

                //Worst algorithm ever//
                //CreatePaths();

                //Better algorithm//
                Path();

                //Rebuild Boxes
                BuildBoxes();

                Draw();

                watch.Stop();

                Console.WriteLine($"\t\tFinished in {watch.ElapsedMilliseconds}ms.");

                Console.ReadLine();
            }
        }
        
        #region Methods

        static void Path()
        {
            OrderArray();
            
            Random rd = new Random();
            var orientations = new[] { 1, 2, 3, 4 };


            foreach (Room room in rooms)
            {
                Shuffle(orientations);

                do
                {
                    foreach (int orientation in orientations)
                    {
                        DrawPath(room.corners, orientation);
                    }
                } while (memory.Count != 0);
            }
        }

        static bool DrawPath(int[][] corners, int orientation)
        {
            //orientation=
            //  1
            //4   2
            //  3

            Random rd = new Random();

            int o = orientation;

            if (rd.NextDouble() >= 0.3) o = rd.Next(1,4);

            int randX = 1;
            int randY = 1;

            if (o == 1)
            {
                randX = rd.Next(corners[0][0], corners[2][0]);
                randY = corners[0][1];
            }
            else if (o == 2)
            {
                randX = corners[2][0];
                randY = rd.Next(corners[2][1], corners[3][1]);
            }
            else if (o == 3)
            {
                randX = rd.Next(corners[1][0], corners[3][0]);
                randY = corners[3][1];
            }
            else if (o == 4)
            {
                randX = corners[0][0];
                randY = rd.Next(corners[0][1], corners[1][1]);
            }


            //PrintPixel(randX,randY,corners);

            #region Works pretty well

            int X = 0;
            int Y = 0;

            try
            {
                do
                {
                    grid.content[randX + X, randY + Y] = path.ToString();
                    //memory.Add(new int[] { randX + X, randY + Y });

                    if (rd.NextDouble() >= 0.5 && grid.content[randX + X + 1, randY + Y] != wall.ToString() && o != 4)
                        X++;
                    if (rd.NextDouble() >= 0.5 && grid.content[randX + X, randY + Y + 1] != wall.ToString() && o != 1)
                        Y++;
                    if (rd.NextDouble() >= 0.5 && grid.content[randX + X - 1, randY + Y] != wall.ToString() && o != 2)
                        X--;
                    if (rd.NextDouble() >= 0.5 && grid.content[randX + X, randY + Y - 1] != wall.ToString() && o != 3)
                        Y--;

                    if (o == 1)
                        Y--;
                    else if (o == 2)
                        X++;
                    else if (o == 3)
                        Y++;
                    else if (o == 4)
                        X--;

                }
                while (grid.content[randX + X, randY + Y] != limits.ToString() &&
                                 grid.content[randX + X, randY + Y] != wall.ToString() &&
                                 grid.content[randX + X, randY + Y] != path.ToString()
                                 );

            }
            catch (Exception){
                memory.Clear();
                return false;
            }

            
            //Debug.WriteLine(grid.content[randX + X, randY + Y]);
            
            #endregion
            
            return false;
        }

        public static void PrintPixel(int X,int Y,int[][] corners)
        {
            memory.Add(new int[] { X, Y });
            grid.content[X, Y] = path.ToString();

            Random rd = new Random();

            int randX = rd.Next(-1, 1);
            int randY = rd.Next(-1, 1);

            if (randY == 0 && randX == 0) PrintPixel(X + randX, Y + randY, corners);

            if (X + randX > corners[0][0] && X + randX < corners[2][0] &&
                Y + randY > corners[0][1] && Y + randY < corners[3][1] ||
                grid.content[X + randX, Y + randY] == path.ToString() ||
                grid.content[X + randX, Y + randY] == wall.ToString())
            {
                memory.Clear();
                return;
            }

            if (grid.content[X + randX, Y + randY] == limits.ToString())
            {
                DeleteHistory();
                return;
            }

            PrintPixel(X + randX,Y + randY, corners);
        }

        public static void DeleteHistory()
        {
            foreach (int[] item in memory)
            {
                grid.content[item[0], item[1]] = empty.ToString();
            }
            memory.Clear();
        }

        public static void Shuffle<T>(IList<T> values)
        {
            var n = values.Count;
            var rnd = new Random();
            for (int i = n - 1; i > 0; i--)
            {
                var j = rnd.Next(0, i);
                var temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }

        static void CreatePaths()
        {
            OrderArray();
            
            for (int r = 0; r < rooms.Count; r++)
            {
                
                if (CheckPath(rooms[r].position.x + rooms[r].size.x, rooms[r].position.y, 2))
                {
                    int x = 1;
                    while (grid.content[rooms[r].position.x + rooms[r].size.x + x, rooms[r].position.y ] != wall.ToString())
                    {
                        grid.content[rooms[r].position.x + rooms[r].size.x + x, rooms[r].position.y ] = path.ToString();
                        x++;
                    }
                }
                else
                {
                    if (CheckPath(rooms[r].position.x, rooms[r].position.y ,1))
                    {
                        int y = -1;
                        while (grid.content[rooms[r].position.x, rooms[r].position.y + y] != path.ToString() &&
                            grid.content[rooms[r].position.x, rooms[r].position.y + y] != wall.ToString())
                        {
                            grid.content[rooms[r].position.x, rooms[r].position.y + y] = pathDown.ToString();
                            y--;
                        }
                    }
                }
            }
        }

        static bool CheckPath(int x, int y, int orientation)
        {
            //orientation=
            //  1
            //4   2
            //  3
            if (orientation == 1 || orientation == 3)
            {
                int c = 0;
                while (grid.content[x, y + c] != limits.ToString())
                {
                    if (grid.content[x, y + c] == path.ToString() || grid.content[x, y + c] == wall.ToString())
                    {
                        return true;
                    }
                    if (orientation == 3)
                        c++;
                    else
                        c--;
                }
                return false;
            }
            else
            {
                int c = 1;
                while (grid.content[x + c, y] != limits.ToString())
                {
                    if (grid.content[x + c, y] == wall.ToString())
                    {
                        return true;
                    }
                    if (orientation == 2)
                        c++;
                    else
                        c--;
                }
                return false;
            }
        }

        static void BuildBoxes()
        {
            Random rd = new Random();

            for (int r = 0; r < rooms.Count; r++)
            {
                int x = 0;
                int y = 0;

                grid.content[rooms[r].corners[0][0], rooms[r].corners[0][1]] = wall.ToString();
                grid.content[rooms[r].corners[1][0], rooms[r].corners[1][1]] = wall.ToString();
                grid.content[rooms[r].corners[2][0], rooms[r].corners[2][1]] = wall.ToString();
                grid.content[rooms[r].corners[3][0], rooms[r].corners[3][1]] = wall.ToString();

                while (rooms[r].corners[1][1] - rooms[r].position.y != y)
                {
                    grid.content[rooms[r].corners[0][0], rooms[r].corners[0][1] + y] = wall.ToString();
                    y++;
                }

                while (rooms[r].corners[3][0] - rooms[r].position.x != x)
                {
                    grid.content[rooms[r].corners[3][0] - x, rooms[r].corners[3][1]] = wall.ToString();
                    x++;
                }

                x = 0;
                y = 0;

                while (rooms[r].corners[3][1] - rooms[r].position.y != y)
                {
                    grid.content[rooms[r].corners[2][0], rooms[r].corners[2][1] + y] = wall.ToString();
                    y++;
                }
                while (rooms[r].corners[2][0] - rooms[r].position.x != x)
                {
                    grid.content[rooms[r].corners[0][0] + x, rooms[r].corners[0][1]] = wall.ToString();
                    x++;
                }
            }
        }

        static void CreateLimits()
        {
            for (int y = 0; y < grid.size.y; y++)
            {
                for (int x = 0; x < grid.size.x; x++)
                {
                    if (y == 0 || y == grid.size.y - 1 ||
                        x == 0 || x == grid.size.x - 1)
                    {
                        grid.content[x, y] = limits.ToString();
                    }
                    else
                    {
                        grid.content[x, y] = empty.ToString();
                    }
                }
            }
        }

        static void Draw()
        {
            for (int y = 0; y < grid.size.y; y++)
            {
                for (int x = 0; x < grid.size.x; x++)
                {
                    Console.Write(grid.content[x, y]);
                }
                Console.Write("\n");
            }
        }

        static void CreateBoxes(int quantity)
        {
            rooms = new List<Room>();
            bool done;

            for (int i = 0; i < quantity; i++)
            {
                done = false;

                Room room = new Room();
                room.GenerateRoom(grid);

                done = CheckContacts(room);

                if (done) rooms.Add(room);
            }

            Console.WriteLine("Population Done");
        }

        static bool CheckContacts(Room room)
        {
            for (int r = 0; r < rooms.Count; r++)
            {
                for (int m = 0; m < 4; m++)
                {
                    bool in_range_x = room.corners[m][0] <= rooms[r].position.x + rooms[r].size.x + 1 && room.corners[m][0] >= rooms[r].position.x - 1;
                    bool in_range_y = room.corners[m][1] <= rooms[r].position.y + rooms[r].size.y + 1 && room.corners[m][1] >= rooms[r].position.y - 1;

                    if (in_range_x && in_range_y)
                    {
                        return false;
                    }
                }
            }

            if (grid.size.x - 1 <= room.position.x + room.size.x ||
                   grid.size.y - 1 <= room.position.y + room.size.y ||
                   1 >= room.position.x ||
                   1 >= room.position.y)
            {
                return false;
            }

            return true;
        }

        private static void OrderArray()
        {
            rooms.Sort((a, b) => a == null ? 1 : b == null ? -1 : a.position.x.CompareTo(b.position.x));
        }

#endregion
    }
}
