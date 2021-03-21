using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miljardär
{
    class Dice
    {   
        /// <summary>
        /// Kastar tärningen.
        /// </summary>
        /// <returns>Ett tal mellan 1 och 6.</returns>
        public int RollDice()
        {
            Random random = new Random();
            return random.Next(1, 7);
        }
    }
}
