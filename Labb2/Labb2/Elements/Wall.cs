using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2.Elements;

class Wall : LevelElement
{
    public Wall(int x, int y) : base(x,y)
    {
        Color = ConsoleColor.Gray;
        Symbol = '#';
    }
}

