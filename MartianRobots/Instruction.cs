using System;

namespace MartianRobots
{
    public class Instruction
    {
        public Instruction(char letter, Func<Position,Position> action)
        {
            this.Letter = letter;
            this.Act = action;
        }

        public char Letter { get;private set; }

        public Func<Position,Position> Act { get;private set; }
    }
}
