using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2.Elements;

class Rat : Enemy
{
    private static readonly Random rnd = new Random();

    public Rat(int x, int y) : base(x, y)
    {
        Name = "Rat";
        Health = 10;
        Symbol = 'r';
        Color = ConsoleColor.Red;

        AttackDice = new Dice(1, 6, 3);
        DefenceDice = new Dice(1, 6, 1);
    }

    public override void Update(Player player, List<LevelElement> elements)
    {
        AttackMessage = "";
        IsAttacking= false;

        var directions = new List<(int directionX, int directionY)> { (0, -1), (0, 1), (-1, 0), (1, 0) };
        var randomDirection = directions[rnd.Next(directions.Count)];

        int oldX = X;
        int oldY = Y;

        int newX = X + randomDirection.directionX;
        int newY = Y + randomDirection.directionY;

        var obstacle = elements.FirstOrDefault(e => e.X == newX && e.Y == newY);

        if (obstacle is Wall || obstacle is Enemy) return;

        if (player.X == newX && player.Y == newY)
        {
            AttackMessage = Attack(player);
            if(player.Health > 0)
            {
                player.AttackMessage = player.Attack(this);
                if(!IsAlive) CleanUpOldEnemyPos(oldX, oldY);
            }
            return;
        }

        CleanUpOldEnemyPos(oldX, oldY);

        X = newX;
        Y = newY;
    }
    public void CleanUpOldEnemyPos(int oldX, int oldY)
    {
        Console.SetCursorPosition(oldX, oldY);
        Console.Write(' ');
    }
}

