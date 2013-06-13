using MartianRobots.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace MartianRobots
{
    /// <summary>
    /// Represents a single robot on the surface of Mars.
    /// </summary>
    public class Robot : IRobot
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
            this.StartPosition = startPosition;
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

        /// <summary>
        /// Gets where the robot started from.
        /// </summary>
        public Position StartPosition { get; private set; }
        
        /// <summary>
        /// Signals the <see cref="Robot"/> to start moving.
        /// </summary>
        /// <param name="grid">The <see cref="IGrid"/> to traverse.</param>
        public void Traverse(IGrid grid)
        {
            Contract.Requires(grid != null);
            Contract.Ensures(this.FinishPosition != null);

            foreach (var instruction in this.instructions)
            {
                var newPosition = instruction.TransformPosition(this.currentPosition);

                var moveResult = grid.Move(this.currentPosition, newPosition);

                if (moveResult == RobotFeedback.Lost)
                {
                    this.FinishPosition = this.currentPosition;
                    this.Lost = true;
                    return;
                }
                else if (moveResult == RobotFeedback.Safe)
                {
                    this.currentPosition = newPosition;
                }
            }

            this.FinishPosition = this.currentPosition;
        }
    }
}
