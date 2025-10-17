using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2.Elements;

class Snake : Enemy
{
    public Snake(int x, int y) : base(x, y)
    {
        Name = "Snake";
        Health = 25;
        Symbol = 's';
        Color = ConsoleColor.Green;

        AttackDice = new Dice(3, 4, 2);
        DefenceDice = new Dice(1, 8, 5);
    }

    public override void Update(Player player, List<LevelElement> elements)
    {
        AttackMessage = "";
        IsAttacking = false;

        double distanceToPlayer = Math.Sqrt(Math.Pow(X - player.X, 2) + Math.Pow(Y - player.Y, 2));
        if (distanceToPlayer > 2) return;

        var directions = new List<(int directionX, int directionY)> { (0, -1), (0, 1), (-1, 0), (1, 0) };

        var SortedDirections = directions
            .OrderByDescending(d => Math.Sqrt(Math.Pow(X + d.directionX - player.X, 2) + Math.Pow(Y + d.directionY - player.Y, 2)))
            .ToList();

        (int directionX, int directionY)? chosenDir = null;

        foreach (var dir in SortedDirections)
        {
            var obstacle = elements.FirstOrDefault(e => e.X == X + dir.directionX && e.Y == Y + dir.directionY);

            if (obstacle is not Wall && obstacle is not Enemy)
            {
                chosenDir = dir;
                break;
            }
        }

        if (chosenDir == null) return;

        Console.SetCursorPosition(X, Y);
        Console.Write(' ');

        X += chosenDir.Value.directionX; 
        Y += chosenDir.Value.directionY;
    }
}
    



