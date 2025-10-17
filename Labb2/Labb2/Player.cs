using Labb2.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2;

class Player : LevelElement
{
    public string Name { get; set; }
    public int Health { get; set; }
    public Dice AttackDice { get; set; }
    public Dice DefenceDice { get; set; }
    public bool IsAttacking { get; set; }
    public string AttackMessage { get; set; }

    public Player(int x, int y) : base(x, y)
    {
        Name = "Player";
        Symbol = '@';
        Color = ConsoleColor.Blue;
        Health = 100;

        AttackDice = new Dice(2, 6, 2);
        DefenceDice = new Dice(2, 6, 0);

        IsAttacking = false;
        AttackMessage = "";
    }

    public void Move(int x, int y, List<LevelElement> elements)
    {
        int oldX = X;
        int oldY = Y;

        int newX = X + x;
        int newY = Y + y;

        var obstacle = elements.FirstOrDefault(e => e.X == newX && e.Y == newY);
        if (obstacle is Wall) return;
        
        if (obstacle is Enemy enemy)
        {
            AttackMessage = Attack(enemy);
            IsAttacking = true;

            if (enemy.Health > 0)
            {
                enemy.AttackMessage = enemy.Attack(this);
            }
            return;
        }

        Console.SetCursorPosition(oldX, oldY);
        Console.Write(' ');

        X = newX;
        Y = newY;
    }

    public string Attack(Enemy enemy)
    {
        int playerAttackRoll = AttackDice.Throw();
        int enemyDefenceRoll = enemy.DefenceDice.Throw();

        int damage = playerAttackRoll - enemyDefenceRoll;

        if (damage > 0)
        {
            enemy.Health -= damage;
            if(enemy.Health <= 0)
            {
                Console.SetCursorPosition(enemy.X, enemy.Y);
                Console.Write(' ');
                enemy.IsAlive = false;
            }
        }

        return $"{this.Name} attackerar med ({this.AttackDice}) och fick {playerAttackRoll}! {enemy.Name} försvarar sig med ({enemy.DefenceDice}) och fick {enemyDefenceRoll}";
    }
}

