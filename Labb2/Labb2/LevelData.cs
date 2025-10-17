using Labb2.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2;
class LevelData
{
    private List<LevelElement> elements;
    public readonly List<LevelElement> Elements;

    public int PlayerPositionX { get; set; }
    public int PlayerPositionY { get; set; }

    public LevelData()
    {
        elements = new List<LevelElement>();
        Elements = elements;
    }

    public void Load(string filename)
    {
        StreamReader reader = new StreamReader(filename);
        int x = 0, y = 0;

        while (!reader.EndOfStream)
        {
            switch((char)reader.Read())
            {
                case '#':
                    elements.Add(new Wall(x,y));
                    break;

                case 'r':
                    elements.Add(new Rat(x, y));
                    break;

                case 's':
                    elements.Add(new Snake(x, y));
                    break;

                case '@':
                    PlayerPositionX = x;
                    PlayerPositionY = y;
                break;

                case '\n':
                    y++;
                    x = -1;
                break;
            }
            x++;
        }
    }
}

