using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Robot
    {
        private Position currentPosition;

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

            this.Instructions = new List<Instruction>(instructions);
            this.StartPosition = startPosition;
            this.currentPosition = startPosition;
        }

        public Position FinishPosition { get; set; }

        public List<Instruction> Instructions { get; set; }

        public bool Lost { get; set; }

        public Position StartPosition { get; set; }

        public void Traverse(bool[,] grid)
        {
            foreach (var instruction in this.Instructions)
            {

            }
        }
    }
}
