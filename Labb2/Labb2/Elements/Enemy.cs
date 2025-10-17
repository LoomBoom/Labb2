using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2.Elements;

abstract class Enemy : LevelElement
{
    public string Name {  get; set; }
    public int Health { get; set; }
    public Dice AttackDice { get; set; }
    public Dice DefenceDice { get; set; }
    public bool IsAlive { get; set; }
    public bool IsAttacking { get; set; }
    public string AttackMessage { get; set; }

    public Enemy(int x, int y) : base(x, y)
    {
        IsAlive = true;
        IsAttacking = false;
        AttackMessage = "";
    }

    public abstract void Update(Player player, List<LevelElement> elements);

    public string Attack(Player player)
    {
        IsAttacking = true;
        int enemyAttackRoll = this.AttackDice.Throw();
        int playerDefenceRoll = player.DefenceDice.Throw();

        int damage = enemyAttackRoll - playerDefenceRoll;

        if (damage > 0)
        {
            player.Health -= damage;
        }
        return $"{this.Name} attackerar med ({this.AttackDice}) och fick {enemyAttackRoll}! {player.Name} försvarar sig med ({player.DefenceDice}) och fick {playerDefenceRoll}";
    }
}

