using Labb2.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2;

class GameLoop
{
    private LevelData levelData;
    private Player player;
    private bool gameActive;
    private int turns;

    public GameLoop()
    {
        levelData = new LevelData();
        levelData.Load(@".\Levels\Level1.txt");
        player = new Player(levelData.PlayerPositionX, levelData.PlayerPositionY);
        gameActive = true;
        turns = 0;
        RenderLevel();
    }

    public void Run()
    {
        while (gameActive)
        {
            turns++;
            var key = Console.ReadKey(true).Key;

            levelData.Elements.RemoveAll(e => e is Enemy enemy && !enemy.IsAlive);
            player.AttackMessage = "";
            if (player.Health <= 0)
            {
                gameActive = false;
                LoadGameOverScreen();
                return;
            }

            switch (key)
            {
                case ConsoleKey.UpArrow: player.Move(0, -1, levelData.Elements); break;
                case ConsoleKey.DownArrow: player.Move(0, +1, levelData.Elements); break;
                case ConsoleKey.LeftArrow: player.Move(-1, 0, levelData.Elements); break;
                case ConsoleKey.RightArrow: player.Move(+1, 0, levelData.Elements); break;
                case ConsoleKey.Escape:
                {
                    gameActive = false;
                    LoadGameOverScreen();
                    return;
                }
            }
            if (!player.IsAttacking)
            {
                Update();
            }
            else
            {
                UpdateHud();
                player.IsAttacking = false;
            }
        }
    }
    void Update()
    {
        foreach (LevelElement element in levelData.Elements)
        {
            if (element is Enemy)
            {
                ((Enemy)element).Update(player, levelData.Elements);
                if (IsInSight(player, element) && ((Enemy)element).IsAlive) element.Draw();
            }
            else
            {
                if (!element.Discovered && (element.Discovered = IsInSight(player, element)))
                {
                    element.Draw();
                }
            }
        }
        if(player.Health > 0) player.Draw();
        UpdateHud();
    }
    public void UpdateHud()
    {
        //Removes old attack messages
        Console.SetCursorPosition(0, 22);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(11, 20);
        Console.Write(new string(' ', Console.WindowWidth));

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.SetCursorPosition(0, 18);
        Console.WriteLine($"Turn: {turns}");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.SetCursorPosition(0, 20);
        if(player.Health > 0)
        {
            Console.WriteLine($"Health: {player.Health}        {player.AttackMessage}");
        }
        else
        {
            Console.WriteLine($"Du dog!    ");
        }

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.SetCursorPosition(0, 22);

        foreach (LevelElement element in levelData.Elements)
        {
            if (element is Enemy)
            {
                if (((Enemy)element).IsAttacking && ((Enemy)element).IsAlive)
                {
                    Console.WriteLine($"{((Enemy)element).Name} Health: {((Enemy)element).Health}      {((Enemy)element).AttackMessage}");
                }
                else if (!((Enemy)element).IsAlive)
                {
                    Console.WriteLine($"{((Enemy)element).Name} dog!");
                }
            }
        }
    }

    public void RenderLevel()
    {
        Console.CursorVisible = false;

        player.Draw();
        foreach (LevelElement element in levelData.Elements)
        {
            if(element.Discovered = IsInSight(player, element))
            {
                element.Draw();
            }
        }

        UpdateHud();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(15, 25);

        Console.WriteLine($"Move Left = Left Arrow   Move Right = Right Arrow   Move Up = Up Arrow   Move Down = Down Arrow");
        Console.SetCursorPosition(15, 26);
        Console.WriteLine("End Game = Escape");
        Console.ResetColor();
    }

    public bool IsInSight(Player player, LevelElement element)
    {
        int radius = 5;
        int directionX = player.X - element.X;
        int directionY = player.Y - element.Y;
        return (directionX * directionX + directionY * directionY) <= radius * radius;
    }

    public void LoadGameOverScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(5, 5);
        Console.WriteLine("Game Over!");
    }
}

