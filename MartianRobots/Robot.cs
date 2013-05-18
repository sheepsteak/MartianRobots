using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace MartianRobots
{
    /// <summary>
    /// Represents a single robot on the surface of Mars.
    /// </summary>
    public class Robot
    {
        private Position currentPosition;
        private IEnumerable<Instruction> instructions;

        /// <summary>
        /// Creates a new robot.
        /// </summary>
        /// <param name="instructions">This robot's set of instructions.</param>
        /// <param name="startPosition">The robot's start position.</param>
        public Robot(IEnumerable<Instruction> instructions, Position startPosition)
        {
            if (instructions == null)
            {
                throw new ArgumentNullException("instructions");
            }

            if (startPosition == null)
            {
                throw new ArgumentNullException("startPosition");
            }

            this.instructions = instructions;
            this.currentPosition = startPosition;
        }

        /// <summary>
        /// Gets where the robot finished up or it's last place on Mars if lost.
        /// </summary>
        public Position FinishPosition { get; private set; }

        /// <summary>
        /// Gets whether this robot was unfortunately lost on Mars.
        /// </summary>
        public bool Lost { get; private set; }

        public void Traverse(Mars mars)
        {
            Contract.Requires(mars != null);

            foreach (var instruction in this.instructions)
            {
                var newPosition = instruction.Act(this.currentPosition);

                var moveResult = mars.Move(this.currentPosition, newPosition);

                if (moveResult == MoveResult.Lost)
                {
                    this.FinishPosition = this.currentPosition;
                    this.Lost = true;
                    return;
                }
                else if (moveResult == MoveResult.Safe)
                {
                    this.currentPosition = newPosition;
                }
            }

            this.FinishPosition = this.currentPosition;
        }
    }
}
