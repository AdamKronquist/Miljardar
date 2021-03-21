using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miljardär
{
    class Jail
    {
        public int numberOfTurns { get; private set; } //Antalet omgångar man får stanna i fängelse.

        //Spelarnas positioner i fängelset.
        public Position[] jailPosition = new Position[]
        {
            new Position(-478, -340), //Spelare 1
            new Position(-448, -346), //Spelare 2

        };

        /// <summary>
        /// Array med olika straff för en omgång.
        /// </summary>
        string[] crime1 = new string[]
        {
            "Du är anklagad för att helt enkelt vara ful. Straffet för det är en omgång.",
            "Du är anklagad för att ha stulit godis från ett barn. Straffet för det är en omgång.",
        };

        /// <summary>
        /// Array med olika straff för två omgångar.
        /// </summary>
        string[] crime2 = new string[]
        {
            "Du är anklagad fär att ha försökt påverka tärningens resultat. Straffet för detta är två omgångar.",
            "Du är anklagad för att ha sålt falska Pokémonkort. Straffet för detta är två omgångar.",
            "Du är anklagad för att ha kört mot rött. Straffet för detta är två omgångar.",
            "Du är anklagad för att ha struntat i att betala hyran. Straffet för det är två omgångar.",
        };

        /// <summary>
        /// Array med olika straff för tre omgångar.
        /// </summary>
        string[] crime3 = new string[]
        {
            "Du är anklagad för att ha laddat ner illegalt. Straffet för detta är tre omgångar.",
            "Du är anklagad för att ha försökt kasta två gånger i rad. Straffet för detta är tre omgångar.",
            "Du är anklagad för att ha försökt sno pengar från andra spelare. Straffet för detta är tre omgångar.",
            "Du är anklagad för att ha rånat den andra spelaren. Straffet för detta är tre omgångar.",
        };

        /// <summary>
        /// Array med olika straff för fyra omgångar.
        /// </summary>
        string[] crime4 = new string[]
        {
            "Du är anklagad för att ha försökt råna banken. Straffet för detta är fyra omgångar.",
            "Du är anklagad för att ha mördat en hel familj (inklusive hunden), druckit deras blod och anordnat en grillfest med deras kroppar. Straffet för detta är normalt fem omgångar men det var en helt fantastisk fest. Straffet reduceras till fyra omgångar.",
        };

        /// <summary>
        /// Array med olika straff för fem omgångar.
        /// </summary>
        string[] crime5 = new string[]
        {
            "Du är anklagad för att ha gjort svarta pengar vita. Straffet för detta är fem omgångar.",
            "Du är anklagad för att ha avslöjat sanningen om tomten. Straffet för detta är fem omgångar, samt inga julklappar.",
            "Du är anklagad för att ha stulit dimanter i juvelbutiken. Straffet för detta är fem omgångar.",
        };


        /// <summary>
        /// Räknar ut en procent som kommer innebära olika stor
        /// sannolik till att sitta inne olika många omgångar.
        /// </summary>
        private int GetProcent()
        {
            Random randomProcent = new Random();

            return randomProcent.Next(100);
        }


        /// <summary>
        /// Räknar ut hur många omgångar man kommer sitta i fängelse.
        /// </summary>
        private void HowManyTurns()
        {
            int procent = GetProcent();

            if(procent < 10)
            {
                numberOfTurns = 1;
            }
            else if(procent >= 10 && procent <30)
            {
                numberOfTurns = 2;
            }
            else if (procent >= 30 && procent < 60)
            {
                numberOfTurns = 3;
            }
            else if (procent >= 60 && procent < 85)
            {
                numberOfTurns = 4;
            }
            else
            {
                numberOfTurns = 5;
            }
        }


        /// <summary>
        /// Tar fram vilket brott man blivit anklagad för.
        /// </summary>
        /// <returns></returns>
        public string WhatCrime()
        {
            Random GetCrime = new Random();
            int crimeIndex;

            HowManyTurns();

            switch (numberOfTurns)
            {
                case 1:
                    crimeIndex = GetCrime.Next(crime1.Length);
                    return crime1[crimeIndex];
                case 2:
                    crimeIndex = GetCrime.Next(crime2.Length);
                    return crime2[crimeIndex];
                case 3:
                    crimeIndex = GetCrime.Next(crime3.Length);
                    return crime3[crimeIndex];
                case 4:
                    crimeIndex = GetCrime.Next(crime4.Length);
                    return crime4[crimeIndex];
                default:
                    crimeIndex = GetCrime.Next(crime5.Length);
                    return crime5[crimeIndex];
            }
        }
    }
}
