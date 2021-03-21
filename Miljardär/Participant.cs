using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miljardär
{
    public class Participant
    {
        public string Name { get; set; } //Spelarens namn.
        public int StartSum { get; set; } //Spelarens startsumma.
        public Colors PlayerColor { get; set; } //Spelarens färg.

        public Participant(string name, int startSum, Colors playerColor)
        {
            Name = name;
            StartSum = startSum;
            PlayerColor = playerColor;
        }
    }
}
