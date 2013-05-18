using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Mars
    {
        private bool[,] grid;
        private IEnumerable<Instruction> instructions;
        private int sizeX;
        private int sizeY;

        public Mars(int sizeX, int sizeY, IEnumerable<Instruction> instructions)
        {
            Contract.Requires(sizeX > 0 && sizeX <= 50);
            Contract.Requires(sizeY > 0 && sizeY <= 50);
            Contract.Requires(instructions != null);

            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.instructions = instructions;
            this.grid = new bool[sizeX, sizeY];
        }

        public MoveResult Move(Position currentPosition, Position newPosition)
        {
            Contract.Requires(currentPosition != null);
            Contract.Requires(newPosition != null);

            if ((newPosition.X >= 0 && newPosition.X <= sizeX) &&
                (newPosition.Y >= 0 && newPosition.Y <= sizeY))
            {
                return MoveResult.Safe;
            }
            else
            {
                if (this.grid[currentPosition.X - 1, currentPosition.Y - 1])
                {
                    return MoveResult.Scented;
                }
                else
                {
                    this.grid[currentPosition.X - 1, currentPosition.Y - 1] = true;
                    return MoveResult.Lost;
                }
            }
        }
    }
}
