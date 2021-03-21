using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miljardär
{
    class Library
    {
        public int Turns { get; private set; } //Hur många varv man lånar en bok.

        //Array med olika böcker man kan låna.
        private string[] books = new string[]
        {
            "Du lånar en bok om en pojke som upptäcker att han har magiska krafter och får bo på en skola med liknande barn.",
            "Du hittar en bok om en man som bor alldeles ensam på en öde ö.",
            "Ditt stora matintresse gör att du fastnar för en bok om tysk husmanskost.",
            "Du hittar en bok om en start liten flicka som verkar intressant. Du tvivlar på om honverkligen kan bära en hel häst.",
            "Du hittar en underlig bok med drakar och där alla människor verkar vara släkt med varandra. Trots det försöker de döda varandra.",
            "du lånar en bok med vampyrer och varulvar och en stackars liten flicka. Påminner lite om rödluvan tycker du."
        };

        /// <summary>
        /// Tar fram antal varv en bok lånas och vilken bok.
        /// </summary>
        /// <returns>Textsträngen som berättar om boken.</returns>
        public string LoanBook()
        {
            Random random = new Random();
            Turns = random.Next(1, 6);
            //Om Turns är lika med 1 slumpar den ett nytt tal. Detta för att det inte ska bli lika stor chans att få en etta.
            if(Turns == 1)
            {
                Turns = random.Next(1, 6);
            }
            int whatBook = random.Next(books.Length);
            return books[whatBook];
        }

        /// <summary>
        /// Räknar ut vilken avgift man får betala om man inte lämnar tillbaka boken i tid.
        /// </summary>
        /// <returns>Den kostnad spelaren får betala.</returns>
        public int PayFee(int turnsOverTime, int playerSum)
        {
            double startFee = playerSum * 0.1; //Startavgiften är 10% av ens belopp.
            double extraFee = startFee * (Math.Abs(turnsOverTime) / 10); //Extraavgiften är 10% extra per varvs som man inte hunnit lämna tillbaka boken.
            double allFee = startFee + extraFee;
            return Convert.ToInt32(allFee);
        }
    }
}
