using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaTest.Models
{
    class OldVersion
    {/*

        //relacion de consola 8 alto 15 de largo (cuadrado perfecto);

        public static Coords grid = new Coords
        {
            x = 15 * 3,
            y = 8 * 3
        };

        public static ConsoleColor console_color = ConsoleColor.Red;
        public static char empty = ' ';
        public static char limits = '.';
        public static char wall = '@';
        public static char door = '#';

        public static List<Room> rooms = new List<Room>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = console_color;

            bool finished = false;
            while (!finished)
            {
                PopulateGrid(15);
                OrderArray();
                finished = Draw();
            }

            Console.ReadKey();
        }

        private static bool Draw()
        {
            Random rand = new Random();
            if (rooms.Count < 4)
            {
                Console.Clear();
                return false;
            }

            Console.WriteLine(new string(limits, grid.x + 2));

            for (int y = 0; y < grid.y; y++)
            {
                //string line = limits.ToString();
                string[] line = new string[grid.x + 2];
                line[0] = ".";

                for (int x = 0; x < grid.x; x++)
                {
                    bool escribio = false;

                    for (int r = 0; r < rooms.Count; r++)
                    {
                        if (rooms[r].coord.x + rooms[r].width - 1 >= x &&
                        rooms[r].coord.x <= x &&
                        rooms[r].coord.y + rooms[r].height - 1 >= y &&
                        rooms[r].coord.y <= y && !escribio)
                        {
                            if (rooms[r].coord.y + rooms[r].height - 1 == y ||
                                rooms[r].coord.y == y)
                            {
                                if (rooms[r].doors != 0 && rand.NextDouble() >= 0.2 &&
                                    rooms[r].coord.x + rooms[r].width - 1 != x &&
                                    rooms[r].coord.x != x && line[x] == door.ToString())
                                {
                                    line[x + 1] = door.ToString();
                                    rooms[r].doors--;
                                }
                                else
                                {
                                    line[x + 1] = wall.ToString();
                                }
                            }
                            else
                            {
                                if (rooms[r].coord.x + rooms[r].width - 1 == x ||
                                rooms[r].coord.x == x)
                                    if (rooms[r].doors != 0 && rand.NextDouble() >= 0.7)
                                    {
                                        line[x + 1] = door.ToString();
                                        rooms[r].doors--;
                                    }
                                    else
                                    {
                                        line[x + 1] = wall.ToString();
                                    }
                                else
                                    line[x + 1] = empty.ToString();
                            }
                            escribio = true;
                        }
                    }

                    if (!escribio)
                    {

                        line[x + 1] = empty.ToString();

                        escribio = false;

                    }
                }

                line[line.Length - 1] = limits.ToString();

                Console.WriteLine(string.Join("", line));
            }
            Console.WriteLine(new string(limits, grid.x + 2));
            return true;
        }
        private static void PopulateGrid(int quantity)
        {
            rooms = new List<Room>();

            for (int i = 0; i < quantity; i++)
            {
                Random rd = new Random();

                Coords randomPos = new Coords
                {
                    x = rd.Next(0, grid.x),
                    y = rd.Next(0, grid.y)
                };

                int randomWith = rd.Next(5, 10);
                int randomHeight = rd.Next(4, 7);

                bool canBuild = true;

                int[,] corners = new int[,]
                {
                    {randomPos.x , randomPos.y},
                    {randomPos.x , randomPos.y + randomHeight },
                    {randomPos.x + randomWith , randomPos.y},
                    {randomPos.x + randomWith , randomPos.y + randomHeight }
                };


                for (int r = 0; r < rooms.Count; r++)
                {
                    //if (rooms[r] == null) break;
                    for (int m = 0; m < 4; m++)
                    {
                        bool in_range_x = corners[m, 0] <= rooms[r].coord.x + rooms[r].width && corners[m, 0] >= rooms[r].coord.x;
                        bool in_range_y = corners[m, 1] <= rooms[r].coord.y + rooms[r].width && corners[m, 1] >= rooms[r].coord.y;

                        if (in_range_x && in_range_y)
                        {
                            canBuild = false;
                        }
                    }
                }

                if (grid.x <= randomPos.x + randomWith ||
                   grid.y <= randomPos.y + randomHeight)
                    canBuild = false;

                if (canBuild)
                {
                    rooms.Add(new Room
                    {
                        coord = randomPos,
                        width = randomWith,
                        height = randomHeight,
                        doors = 2
                    });
                }
            }
        }
        private static void OrderArray()
        {
            // Ordeno de menor a mayor por coordenada y
            rooms.Sort((a, b) => a == null ? 1 : b == null ? -1 : a.coord.y.CompareTo(b.coord.y));
            //
        }*/
    }
}
