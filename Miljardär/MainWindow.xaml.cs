using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Miljardär
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayerPositions playerPositions = new PlayerPositions(); //För att komma åt alla spelares olika positioner.
        AllStreets allStreets = new AllStreets(); //För att komma åt listan med alla olika gator.
        StreetTypes streetTypes; //För att kunna sätta enum StreetTypes till en variabel.
        List<Player> players = new List<Player>(); //En lista som alla spelare hamnar i.
        int numberOfMoves = 0; //Så många steg en spelare kommer få flytta.
        int playerTurn = 0; //Vilken spelaren tur det är.
        bool rollDice = true; //Kollar om det är dags att kasta tärningen eller avsluta sin runda.
        Player player; //Kommer innehålla den aktuelle spelarens information.
        Player streetOwner; //Kommer innehålla information om den spelare som äger gatan som den aktuelle spelaren hamnat på.

        public MainWindow(List<Participant> participants, int numberOfParticipants)
        {
            InitializeComponent();
            
            //Lägger till alla spelare från tidigare fönstret i listan för spelare.
            for (int i = 0; i < numberOfParticipants; i++)
            {
                players.Add(new Player(participants[i].Name, participants[i].StartSum, participants[i].PlayerColor));
            }

            StartUp();
        }

        /// <summary>
        /// Inställningar som ska sättas i början av spelet.
        /// </summary>
        public void StartUp()
        {
            //Skapa spelare1
            player1.RenderTransform = new TranslateTransform(playerPositions.positioner[0,0].PosX, playerPositions.positioner[0, 0].PosY); //Startposition.
            player1.Source  = new BitmapImage(new Uri($"player{players[0].PlayerColor}.png", UriKind.Relative)); //Färg.

            //Skapa spelare2
            player2.RenderTransform = new TranslateTransform(playerPositions.positioner[0, 1].PosX, playerPositions.positioner[0, 1].PosY); //Starposition.
            player2.Source = new BitmapImage(new Uri($"player{players[1].PlayerColor}.png", UriKind.Relative)); //Färg.

            lblDice.Content = "";
            UpdatePlayerInformation();
            ChangeButtonColor();
            ChangePlayerInfoBackground();
        }

        
        /// <summary>
        /// Allt som händer man man trycker på "Kasta-knapen".
        /// </summary
        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            //Skapar ett objekt av den spelare vars tur det är. Så att man enkelt får fram dennes information.
            player = GetPlayer(playerTurn);

            //Om spelaren inte sitter i fängelse utförs detta.
            if (!player.InJail)
            {
                //Kollar vilket läge knappen är i.
                switch (rollDice)
                {
                    case true: //Kasta-läge.
                        RollDice(); //Kastar tärningen.
                        Move(); //Flyttar spelaren.
                        btnMove.Content = "Avsluta din tur"; //Ändrar texten på knappen.
                        rollDice = false; //Ändrar till Avsluta din tur-läge.
                        break;
                    case false: //Avsluta din tur-läge.
                        SavePlayerInformation();
                        PlayerTurn(); //Ändrar vilken spelares tur det är.
                        btnMove.Content = "Kasta"; //Ändrar texten på knappen.
                        rollDice = true; //Ändrar till kasta-läge.
                        break;
                }
            }
            //Sitter spelaren i fängelse utförs detta.
            else
            {
                MessageBox.Show($"Det är {player.TurnsLeft} omgångar kvar tills du har suttit av ditt straff.");
                player.TurnsLeft -= 1;
                if (player.TurnsLeft == 0)
                {
                    player.InJail = false;
                }
                
                PlayerTurn();
            }
            
            ChangeButtonColor();
        }


        /// <summary>
        /// Flyttar spelaren framåt på spelplanen.
        /// </summary>
        private async void Move()
        {
            //Loopar igenom så många gånger som man fick antal steg på tärningen.
            for (int i = 0; i < numberOfMoves; i++)
            {
                await Task.Delay(500); //Vänta en halv sekund mellan varje förflyttning.
                player.BoardIndex++;
                if (player.BoardIndex == playerPositions.positioner.GetLength(0)) //Om spelarens boardIndex är lika med sista positionen så nollställ.
                {
                    player.BoardIndex = 0;
                    OneLap(); //Detta innebär att man gått ett varv och då anropas denna metod.
                }

                //Kollar vilken spelare det är som ska förflyttas beroende på vilken spelares tur det är.
                switch (playerTurn)
                {
                    case 0:
                        player1.RenderTransform = new TranslateTransform(playerPositions.positioner[player.BoardIndex, 0].PosX, playerPositions.positioner[player.BoardIndex, 0].PosY);
                        break;
                    case 1:
                        player2.RenderTransform = new TranslateTransform(playerPositions.positioner[player.BoardIndex, 1].PosX, playerPositions.positioner[player.BoardIndex, 1].PosY);
                        break;
                }

                CheckBookLoan(); //Kollar om något av stegen går förbi bibliotekets gata.
            }

            CheckSquare(); //Kollar vilken ruta man hamnat på när alla förflyttningar är klara.
            UpdatePlayerInformation(); //Uppdaterar spelarens information.
        }


        /// <summary>
        /// Uppdaterar informationen om spelarna.
        /// </summary>
        private void UpdatePlayerInformation()
        {
            for (int i = 0; i < players.Count; i++)
            {
                //Skapa labelar
                Label playerName = (Label)this.FindName($"lblPlayer{i + 1}Name");
                Label playerSum = (Label)this.FindName($"lblPlayer{i + 1}Summa");
                Label playerJail = (Label)this.FindName($"lblPlayer{i + 1}Jail");
                Label playerTurnsLeft = (Label)this.FindName($"lblPlayer{i + 1}TurnsLeft");
                Label playerBookLoan = (Label)this.FindName($"lblPlayer{i + 1}BookLoan");
                Label playerBookLoanTurns = (Label)this.FindName($"lblPlayer{i + 1}BookLoanTurns");
                Label playerMayor = (Label)this.FindName($"lblPlayer{i + 1}Mayor");
                Label playerSalary = (Label)this.FindName($"lblPlayer{i + 1}Salary");

                //Skriver ut texten
                playerName.Content = players[i].Name;
                playerSum.Content = $"{players[i].Money.ToString("N0")}kr";
                playerJail.Content = players[i].InJail;
                playerTurnsLeft.Content = players[i].TurnsLeft;
                playerBookLoan.Content = players[i].BookLoan;
                playerBookLoanTurns.Content = players[i].BookLoanTurns;
                playerMayor.Content = players[i].Mayor;

                if (players[i].Mayor)
                {
                    playerSalary.Content = ((int)(players[i].Salary * 1.1)).ToString("N0") + "kr";
                }
                else
                {
                    playerSalary.Content = players[i].Salary.ToString("N0") + "kr";
                }
            }
            
        }
        

        /// <summary>
        /// Kollar upp vilken typ av ruta man står på.
        /// </summary>
        private void CheckSquare()
        {
            //streetTypes blir värdet för den ruta som den aktiva spelaren hamnar på.
            streetTypes = allStreets.streets[player.BoardIndex].StreetTypes;

            switch (streetTypes)
            {
                case StreetTypes.PassGo:
                    MessageBox.Show($"Du inkasserar din lön på {player.Salary.ToString("N0")}kr.");
                    break;
                case StreetTypes.Mayor:
                    SquareMayor(); //Metod som hanterar borgmästarerutan.
                    break;
                case StreetTypes.Street:
                    SquareStreet(); //Metod som hanterar rutor som klassas som gator (de man kan köpa).
                    break;
                case StreetTypes.CandyShop:
                    MessageBox.Show("Välkommen in i godisaffären. I dagsläget händer ingenting.");
                    break;
                case StreetTypes.Library:
                    SquareLibrary(); //Metood som hanterar biblioteksrutan.
                    break;
                case StreetTypes.NewsPaper:
                    MessageBox.Show("Välkommen till nyhetskiosken. I dagsläget händer ingenting.");
                    break;
                case StreetTypes.Jail:
                    SquareJail(); //Metod som hanterar fängelserutan.
                    break;
            }
        }
        

        #region Metoder för de olika gaturuterna
        /// <summary>
        /// Om man hamnar på en gata som går att köpa händer detta.
        /// </summary>
        private void SquareStreet()
        {
            string header = "";
            string message = "";

            //Gata som inte ägs av någon (går därför att köpa).
            if (!allStreets.SearchHasOwner(player.BoardIndex))
            {
                allStreets.SearchForSaleStreet(player.BoardIndex); //Kollar upp namn och pris på den gata man hamnat på.
                //Om spelaren har råd att köpa gatan.
                if (player.Money >= allStreets.ForSaleStreetPrice)
                {
                    header = "Gata till salu!";
                    message = $"Du hamnade på {allStreets.ForSaleStreetName}. Vill du köpa gatan för {allStreets.ForSaleStreetPrice.ToString("N0")}kr?";
                    MessageBoxResult answer = MessageBox.Show(message, header, MessageBoxButton.YesNo);
                    switch (answer)
                    {
                        case MessageBoxResult.Yes:
                            BuyStreet();
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                //Om spelaren har mindre pengar än vad gatan kostar (har inte råd att köpa den).
                else
                {
                    header = "Ont om pengar!";
                    message = $"Tyvärr har du inte råd att köpa {allStreets.ForSaleStreetName}. Det saknas {(allStreets.ForSaleStreetPrice - player.Money).ToString("N0")}kr.";
                    MessageBox.Show(message, header);
                }
            }
            //Gata man själv äger.
            else if (player.Name == allStreets.StreetOwner)
            {
                header = "På hembesök";
                message = $"Du äger redan {allStreets.StreetName}. Passa på att njuta.";
                MessageBox.Show(message, header);
            }
            //Gata någon annan äger.
            else
            {
                streetOwner = GetStreetOwner();
                //Man ska betala hyra.
                if (!streetOwner.InJail)
                {
                    header = "Ooops";
                    message = $"{allStreets.StreetName} ägs av {streetOwner.Name}. Du måste betala!\n" +
                        $"Priset är {allStreets.StreetPrice.ToString("N0")}kr.";
                    MessageBox.Show(message, header);
                    PayRent();
                }
                //Om gatans ägare sitter i fängelset.
                else
                {
                    MessageBox.Show("Gatans ägare sitter i fängelse. Passa på att njuta. Detta besök kostar inget.");
                }
                SaveStreetOwnerInformtion();
            }
        }


        /// <summary>
        /// Vad som ska hända om man hamnar på fängelserutan.
        /// </summary>
        private void SquareJail()
        {
            Jail jail = new Jail();
            MessageBox.Show(jail.WhatCrime(), "I Fängelset");
            player.InJail = true;
            player.TurnsLeft = jail.numberOfTurns;

            //Kollar vilken spelares pjäs som ska flyttas till fängelset.
            switch (playerTurn)
            {
                case 0:
                    player1.RenderTransform = new TranslateTransform(jail.jailPosition[playerTurn].PosX, jail.jailPosition[playerTurn].PosY);
                    break;
                case 1:
                    player2.RenderTransform = new TranslateTransform(jail.jailPosition[playerTurn].PosX, jail.jailPosition[playerTurn].PosY);
                    break;
            }

            PlayerTurn();
            ChangeButtonColor();
            btnMove.Content = "Kasta"; //Ändrar texten på knappen.
            rollDice = true;
        }


        /// <summary>
        /// Vad som händer när man hamnar på biblioteksrutan.
        /// </summary>
        private void SquareLibrary()
        {
            Library library = new Library();
            //Om man inte har en bok på lån vill man låna en.
            if (!player.BookLoan)
            {
                string message = library.LoanBook();
                MessageBox.Show(message +
                    $"\nDu behöver lämna tillbaka den inom {library.Turns} varv.", "Inne på biblioteket");
                player.BookLoan = true;
                player.BookLoanTurns = library.Turns;
            }
            //Annars har man en bok som man ska lämna tillbaka.
            else
            {
                //Man hinner lämna tillbaka boken innan lånetiden har gått ut.
                if (player.BookLoanTurns >= 0)
                {
                    MessageBox.Show("Du han lämna tillbaka boken i tid.", "Återbesök hos biblioteket");
                    player.BookLoan = false;
                }
                //Boken lämnas tillbaka försent och man måste betala en avgift.
                else
                {
                    int fee = library.PayFee(player.BookLoanTurns, player.Money);
                    MessageBox.Show($"Du lämnade tillbaka boken {Math.Abs(player.BookLoanTurns)} varv försent." +
                        $" Du kommer få betala en straffavgift på {fee.ToString("N0")}kr.", "Återbesök hos biblioteket");

                    player.Money -= fee;
                    player.BookLoan = false;
                }
            }
        }


        /// <summary>
        /// Vad som händer om man hamnar på borgmästarerutan.
        /// </summary>
        private void SquareMayor()
        {
            //Om man inte redan är borgmästare.
            if (!player.Mayor)
            {
                MessageBox.Show("Grattis du har blivit borgmästare. Detta innebär en löneökning med 10%.", "Borgmästare");
            }
            //Om man redan är borgmästare.
            else
            {
                MessageBox.Show("Du är redan borgmästare och behåller din löneökning på 10%.", "Borgmästare");
            }

            //Går igenom alla spelare i listan bland deltagare.
            foreach (var playerInList in players)
            {
                //Om något namn matchar med den aktuelle spelarens namn.
                if (playerInList.Name == player.Name)
                {
                    player.Mayor = true; //Sätt till borgmästare.
                }
                else
                {
                    player.Mayor = false; //Det kan bara finnas en borgmästare, så alla andra blir inte borgmästare.
                }
            }
        }
        #endregion


        #region Metoder som har med klasskapande att göra
        /// <summary>
        /// Skapar en ny tillfällig spelare.
        /// </summary>
        /// <returns>Spelaren vars tur det är, från listan med alla spelare som är med i spelet.</returns>
        private Player GetPlayer(int playerTurn)
        {
            return players[playerTurn];
        }


        /// <summary>
        /// Skapar en spelare som äger gatan som den aktuelle spelaren hamnat på.
        /// </summary>
        private Player GetStreetOwner()
        {
            foreach (var player in players)
            {
                if (player.Name == allStreets.StreetOwner)
                {
                    return player;
                }
            }
            return null;
        }


        /// <summary>
        /// Sparar ner den tillfälliga informationen från den Spelare som skpats
        /// till den Spelaren som ligger i listan med alla spelare som är med.
        /// </summary>
        private void SavePlayerInformation()
        {
            players[playerTurn].BoardIndex = player.BoardIndex;
            players[playerTurn].Money = player.Money;
            players[playerTurn].InJail = player.InJail;
            players[playerTurn].TurnsLeft = player.TurnsLeft;
            players[playerTurn].BookLoan = player.BookLoan;
            players[playerTurn].BookLoanTurns = player.BookLoanTurns;
            players[playerTurn].Salary = player.Salary;
            players[playerTurn].Mayor = player.Mayor;
        }


        /// <summary>
        /// Sparar ner den nya informationen till rätt spelare.
        /// </summary>
        private void SaveStreetOwnerInformtion()
        {
            foreach (var player in players)
            {
                if (player.Name == streetOwner.Name)
                {
                    player.Money = streetOwner.Money;
                }
            }
        }
        #endregion


        #region Små metoder som inte behöver göras så mycket mer med - PlayerTurn - RollDice - OneLap - PayRent - BuyStreet - CheckBookLoan - ChangeButtonColor
        /// <summary>
        /// Ändrar bakgrundsfärgen på rutan som all information om spelaren finns i.
        /// </summary>
        private void ChangePlayerInfoBackground()
        {
            for (int i = 0; i < players.Count; i++)
            {
                Grid grid = (Grid)this.FindName($"gridPlayer{i + 1}");

                switch (players[i].PlayerColor)
                {
                    case Colors.Blue:
                        grid.Background = new SolidColorBrush(Color.FromRgb(64, 64, 255));
                        break;
                    case Colors.Red:
                        grid.Background = new SolidColorBrush(Color.FromRgb(255, 64, 64));
                        break;
                    case Colors.Yellow:
                        break;
                    case Colors.Green:
                        break;
                    default:
                        break;
                }

            }

            
            
        }


        /// <summary>
        /// Ändrar vilken spelares tur det är.
        /// </summary>
        private void PlayerTurn()
        {
            playerTurn++;

            if (playerTurn == players.Count)
            {
                playerTurn = 0;
            }
        }


        /// <summary>
        /// Kasta tärningen.
        /// </summary>
        private void RollDice()
        {
            Dice dice = new Dice();
            numberOfMoves = Convert.ToInt32(txtFejkTärning.Text); //Används för "fusktärningen".

            //Dessa är för den riktiga tärningen.
            //numberOfMoves = dice.RollDice();
            //lblDice.Content = numberOfMoves;
        }


        /// <summary>
        /// Vad som händer när man gått ett varv runt spelplanan.
        /// </summary>
        private void OneLap()
        {
            //Man får sin lön om man inte är borgmästare.
            if (!player.Mayor)
            {
                player.Money += player.Salary;
            }
            //Annars får man sin lön med 10% påslag.
            else
            {
                player.Money += (int)(player.Salary * 1.1);
            }
        }


        /// <summary>
        /// Betala hyra till någon annan.
        /// </summary>
        private void PayRent()
        {
            //Spelaren som förlorar pengar.
            player.Money -= allStreets.StreetPrice;

            //Spelaren som ska få pengar.
            streetOwner.Money += allStreets.StreetPrice;
        }


        /// <summary>
        /// Det som händer när man köper en gata.
        /// </summary>
        private void BuyStreet()
        {
            allStreets.BuyStreet(new Street(allStreets.ForSaleStreetName, player.Name, allStreets.ForSaleStreetPrice, player.BoardIndex)); //Lägger till gatan i listan bland köpta gator.
            player.Money -= allStreets.ForSaleStreetPrice; //Spelaren som köper gatan förlorar så mycket pengar som gatan kostar.

            //Skapar ett bildobjekt för att kunna styra vilken gata som ska få en annan byggnadsbild på sig.
            Image building = (Image)this.FindName($"imgStreet{players[playerTurn].BoardIndex}");
            building.Source = new BitmapImage(new Uri($"sign{players[playerTurn].PlayerColor}.png", UriKind.Relative));
        }


        /// <summary>
        /// Kollar om man går förbi biblioteksrutan.
        /// </summary>
        private void CheckBookLoan()
        {
            if (player.BookLoan && player.BoardIndex == 5)
            {
                player.BookLoanTurns -= 1;
            }
        }


        /// <summary>
        /// Ändrar färgen på "Kasta-knappen beroende på vilken spelares tur det är."
        /// </summary>
        private void ChangeButtonColor()
        {
            switch (players[playerTurn].PlayerColor)
            {
                case Colors.Blue:
                    btnMove.Background = new SolidColorBrush(Color.FromRgb(64, 64, 255));
                    break;
                case Colors.Red:
                    btnMove.Background = new SolidColorBrush(Color.FromRgb(255, 64, 64));
                    break;
                case Colors.Yellow:
                    break;
                case Colors.Green:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
