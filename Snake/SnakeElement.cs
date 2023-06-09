using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class SnakeElement
    {
        // Эти два свойства представляют координаты части змеи по X и Y в PictureBox.
        public int X { get; set; }
        public int Y { get; set; }

        // Конструктор класса
        public SnakeElement()
        {
            X = 0;
            Y = 0;
        }
    }
}
