using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente_Kill.Models
{
    class Board
    {
        private int size;
        public int Height { get; set; }
        public int Width { get; set; }

        public int[,] MakeBoard(int size)
        {
            this.Width = size;
            this.Height = size;
            int[,] board = new int[Width, Height];
            return board;
        }


    }
}
