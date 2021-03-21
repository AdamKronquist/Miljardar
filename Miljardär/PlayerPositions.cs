using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miljardär
{
    class PlayerPositions
    {
        //Array som innehåller de positioner spelpjäserna ska stå på.
        public Position[,] positioner = new Position[,]
        {    //Spelare 1 = index 0    Spelare 2 = index 1
            {new Position(-588, 170), new Position(-558, 170), },
            {new Position(-563, 110), new Position(-536, 110), },
            {new Position(-538, 61), new Position(-514, 60), },
            {new Position(-519, 7), new Position(-494, 10), },
            {new Position(-499, -37), new Position(-475, -31), },
            {new Position(-479, -83), new Position(-455, -76), },
            {new Position(-461, -121), new Position(-439, -118), },
            {new Position(-444, -159), new Position(-420, -158), },
            {new Position(-429, -196), new Position(-404, -194), },
            {new Position(-414, -229), new Position(-391, -228), },
            {new Position(-399, -264), new Position(-377, -261), },
            //Utkommenterat tillsvodare för att spelarna inte ska gå så längt innen det gått ett varv.
            /*
            {new Position(-342, -260), },
            {new Position(-289, -258), },
            {new Position(-233, -256), },
            {new Position(-179, -255), },
            {new Position(-123, -254), },
            {new Position(-69, -252), },
            {new Position(-12, -251), },
            {new Position(42, -249), },
            {new Position(98, -247), },
            {new Position(152, -246), },
            {new Position(210, -244), },
            {new Position(264, -242), },
            {new Position(321, -240), },
            {new Position(377, -239), },
            {new Position(436, -238), },
            {new Position(444, -205), },
            {new Position(452, -169), },
            {new Position(461, -132), },
            {new Position(472, -91), },
            {new Position(482, -51), },
            {new Position(497, -3), },
            {new Position(510, 45), },
            {new Position(523, 98), },
            {new Position(538, 152), },
            {new Position(556, 212), },
            {new Position(476, 210), },
            {new Position(396, 208), },
            {new Position(322, 206), },
            {new Position(244, 201), },
            {new Position(169, 199), },
            {new Position(92, 196), },
            {new Position(12, 192), },
            {new Position(-64, 188), },
            {new Position(-135, 185), },
            {new Position(-212, 183), },
            {new Position(-289, 181), },
            {new Position(-362, 177), },
            {new Position(-436, 173), },
            {new Position(-511, 171), },
            */
        };

    }
}
