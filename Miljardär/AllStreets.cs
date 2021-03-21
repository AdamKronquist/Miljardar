using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miljardär
{
    class AllStreets
    {
        public int ForSaleStreetPrice { get; private set; } //Priset för en gata som ingen äger.
        public string ForSaleStreetName { get; private set; } //Namnnet på en gata som ingen äger.
        public int StreetPrice { get; private set; } //Priset för en gata som någon äger.
        public string StreetName { get; private set; } //Namnet på en gata som någon äger.
        public string StreetOwner { get; private set; } //Namnet på den spelare som äger gatan.

        //Lista över alla gator/rutor i spelet.
        public List<Street> streets = new List<Street>()
        {
            new Street("Gå", StreetTypes.PassGo, 0),
            new Street("Borgmästare", StreetTypes.Mayor, 0),
            new Street("Gata 1", StreetTypes.Street, 10000000),
            new Street("Gata 2", StreetTypes.Street, 20000),
            new Street("Godisbutik", StreetTypes.CandyShop, 0),
            new Street("Bibliotek", StreetTypes.Library, 0),
            new Street("Gata 3", StreetTypes.Street, 40000),
            new Street("Gata 4", StreetTypes.Street, 50000),
            new Street("Gata 5", StreetTypes.Street, 60000),
            new Street("Tidning", StreetTypes.NewsPaper, 0),
            new Street("Fängelse", StreetTypes.Jail, 0),
            /*
            new Street("Friruta", StreetTypes.FreeSquare, 120),
            new Street("Rånare", StreetTypes.Robber, 130),
            new Street("Gata 6", StreetTypes.Street, 140),
            new Street("Gata 7", StreetTypes.Street, 150),
            new Street("?", StreetTypes.QuestionMark, 0),
            new Street("Spelbutik", StreetTypes.GameShop, 170),
            new Street("Gym", StreetTypes.Gym, 180),
            new Street("Gata 8", StreetTypes.Street, 190),
            new Street("Gata 9", StreetTypes.Street, 200),
            new Street("Gata 10", StreetTypes.Street, 500),
            new Street("Friruta", StreetTypes.FreeSquare, 210),
            new Street("Skralott", StreetTypes.SkrapLott, 220),
            new Street("Bar", StreetTypes.Bar, 230),
            new Street("Souvenirshop", StreetTypes.SouvenirShop, 240),
            new Street("Hästlöpning", StreetTypes.HorseRiding, 250),
            new Street("?", StreetTypes.QuestionMark, 260),
            new Street("Gata 11", StreetTypes.Street, 270),
            new Street("Gata 12", StreetTypes.Street, 280),
            new Street("Nöjespark", StreetTypes.AmusementPark, 290),
            new Street("Friruta", StreetTypes.FreeSquare, 300),
            new Street("Gata 13", StreetTypes.Street, 310),
            new Street("Gata 14", StreetTypes.Street, 320),
            new Street("Gata 15", StreetTypes.Street, 330),
            new Street("Tidning", StreetTypes.NewsPaper, 340),
            new Street("Löneförhandling", StreetTypes.LöneFörhandling, 350),
            new Street("Friruta", StreetTypes.FreeSquare, 360),
            new Street("Bio", StreetTypes.Cinema, 370),
            new Street("Obligationsdragning", StreetTypes.Obligationsdragning, 380),
            new Street("Gata 16", StreetTypes.Street, 390),
            new Street("Gata 17", StreetTypes.Street, 400),
            new Street("Gata 18", StreetTypes.Street, 410),
            new Street("Aktieutdlning", StreetTypes.Aktieutdelning, 420),
            new Street("Musikal", StreetTypes.Musical, 430),
            new Street("?", StreetTypes.QuestionMark, 44),
            new Street("Juvelare", StreetTypes.Jewelers, 450),
            new Street("Friruta", StreetTypes.FreeSquare, 460),
            new Street("Gata 19", StreetTypes.Street, 470),
            new Street("Gata 20", StreetTypes.Street, 480),
            new Street("Gata 21", StreetTypes.Street, 490),
            */
        };

        //Lista som lagrar alla uppköpta gator.
        private List<Street> boughtStreets = new List<Street>();

        /// <summary>
        /// Lägger till en gata i listan för uppköpta gator.
        /// </summary>
        public void BuyStreet(Street street)
        {
            boughtStreets.Add(street);
        }

        /// <summary>
        /// Går igenom alla gator i listan med köpta gator.
        /// </summary>
        /// <param name="boardIndex">Det index som spelaren står på.</param>
        /// <returns>
        /// True om det finns ett matchande boardIndex (Gatan är redan köpt).
        /// False om det inte finns ett matchande boardIndex (Gatan är till salu).
        /// </returns>
        public bool SearchHasOwner(int boardIndex)
        {
            foreach (var street in boughtStreets)
            {
                if(street.BoardIndex == boardIndex)
                {
                    StreetPrice = street.Price; //Finns det en match, vill jag även veta priset.
                    StreetName = street.Name; //Och namn.
                    StreetOwner = street.Owner; //Och ägare.
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Tar fram namn och pris för gator som ingen äger.
        /// </summary>
        public void SearchForSaleStreet(int boardIndex)
        {
            ForSaleStreetName = streets[boardIndex].Name;
            ForSaleStreetPrice = streets[boardIndex].Price;
        }
    }
}
