using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaTest
{
    public class Room
    {
        public Coords position = new Coords();

        public Coords size = new Coords();


        public bool isConnected = false;

        public int[][] corners;

        public Room()
        {
            Random rd = new Random();

        }

        public void GenerateRoom(Grid grid)
        {
            Random rd = new Random();

            position = new Coords
            {
                x = rd.Next(0, grid.size.x),
                y = rd.Next(0, grid.size.y)
            };

            size.x = rd.Next(5, 10);
            size.y = rd.Next(4, 7);

            corners = new int[][]
            {
                    new int[]{position.x , position.y}, // sup izq
                    new int[]{position.x , position.y + size.y }, //inf izq
                    new int[]{position.x + size.x , position.y}, // superior der
                    new int[]{position.x + size.x , position.y + size.y } // inf der
            };
        }
    }
}
