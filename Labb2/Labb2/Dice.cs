using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2;

class Dice
{
    private int NumberOfDice;
    private int SidesPerDice;
    private int Modifier;
    private static Random rnd;

    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        NumberOfDice = numberOfDice;
        SidesPerDice = sidesPerDice;
        Modifier = modifier;

        rnd = new Random();
    }

    public int Throw()
    {
        int sum = 0;
        for (int i = 0; i < NumberOfDice; i++)
        {
            sum += rnd.Next(1, SidesPerDice +1);
        }
        return sum + Modifier;
    }

    public override string ToString()
    {
        return $"{NumberOfDice}d{SidesPerDice}+{Modifier}";
    }
}

