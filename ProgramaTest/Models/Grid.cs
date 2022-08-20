using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaTest
{
    public class Grid
    {
        public Coords size;

        public string[,] content;

        public Grid(Coords size)
        {
            this.size = size;

            content = new string[size.x, size.y];
        }
    }
}
