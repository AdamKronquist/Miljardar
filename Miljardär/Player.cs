using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miljardär
{
    class Player
    {
        public string Name { get; }
        public int Money { get; set; }
        public Colors PlayerColor { get; }
        public int BoardIndex { get; set; } = 0;
        public bool InJail { get; set; } = false;
        public int TurnsLeft { get; set; }
        public bool BookLoan { get; set; } = false;
        public int BookLoanTurns { get; set; }
        public int Salary { get; set; } = 10000;
        public bool Mayor { get; set; } = false;

        

        public Player(string name, int money, Colors playerColor)
        {
            Name = name;
            Money = money;
            PlayerColor = playerColor;
        }
    }
}
