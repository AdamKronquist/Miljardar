using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miljardär
{
    class Street
    {
        public string Name { get; set; } //Gatans namn
        public int Price { get; set; } //Gatans pris
        public string Owner { get; set; } //Gatans ägare
        public int BoardIndex { get; set; } //Gatans index på spelbrädet
        public StreetTypes StreetTypes { get; private set; } //Vilken typ utav ruta gatan är

        /// <summary>
        /// Konstruktor för alla gator.
        /// </summary>
        public Street(string name, StreetTypes streetTypes, int price)
        {
            Name = name;
            StreetTypes = streetTypes;
            Price = price;
        }

        /// <summary>
        /// Konstruktor när man köper en gata.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        /// <param name="boardIndex"></param>
        public Street(string name, string owner, int price, int boardIndex)
        {
            Name = name;
            Owner = owner;
            Price = price;
            BoardIndex = boardIndex;
            StreetTypes = StreetTypes.Street;
        }

        
    }
}
