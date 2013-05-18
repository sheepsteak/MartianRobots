using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace MartianRobots.Instructions
{
    public class RightInstruction : IInstruction
    {
        private static char command = 'R';

        public char Command
        {
            get { return command; }
        }

        public Position Act(Position position)
        {
            Contract.Requires(position != null);
            Contract.Ensures(Contract.Result<Position>() != null);

            switch (position.Orientation)
            {
                case Orientation.N:
                    return new Position(position.X, position.Y, Orientation.E);
                case Orientation.S:
                    return new Position(position.X, position.Y, Orientation.W);
                case Orientation.W:
                    return new Position(position.X, position.Y, Orientation.N);
                case Orientation.E:
                    return new Position(position.X, position.Y, Orientation.S);
                default:
                    return new Position(position.X, position.Y, position.Orientation);
            }
        }
    }
}
