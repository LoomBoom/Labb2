using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2.Elements;
abstract class LevelElement
{
    public bool Discovered { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }

    public LevelElement(int x, int y)
    {
        this.X = x;
        this.Y = y;
        Discovered = false;
    }

    public void Draw()
    {
        Console.ForegroundColor = Color;
        Console.SetCursorPosition(X, Y);
        Console.Write(Symbol);
    }
}

