using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Miljardär
{
    /// <summary>
    /// Interaction logic for Setup.xaml
    /// </summary>
    public partial class Setup : Window
    {
        Colors colorsPlayer1; //Spelare 1 färg
        Colors colorsPlayer2; //Spelare 2 färg
        List<Participant> participants = new List<Participant>(); //Lista som lagrar alla som deltar i spelet.

        public Setup()
        {
            InitializeComponent();
            //Förmarkerar vissa färger.
            radioPlayer1Red.IsChecked = true;
            radioPlayer2Blue.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            participants.Add(new Participant(txtPlayer1Name.Text, Convert.ToInt32(txtPlayer1Money.Text), colorsPlayer1));
            participants.Add(new Participant(txtPlayer2Name.Text, Convert.ToInt32(txtPlayer2Money.Text), colorsPlayer2));
            MainWindow mainWindow = new MainWindow(participants, participants.Count);
            mainWindow.Show();
            this.Close();
        }

        #region Radioknappar
        private void radioPlayer1Red_Checked(object sender, RoutedEventArgs e)
        {
            colorsPlayer1 = Colors.Red;

            if (radioPlayer2Red.IsChecked == true)
            {
                radioPlayer2Blue.IsChecked = true;
            }
        }

        private void radioPlayer1Blue_Checked(object sender, RoutedEventArgs e)
        {
            colorsPlayer1 = Colors.Blue;

            if (radioPlayer2Blue.IsChecked == true)
            {
                radioPlayer2Red.IsChecked = true;
            }
        }

        private void radioPlayer2Red_Checked(object sender, RoutedEventArgs e)
        {
            colorsPlayer2 = Colors.Red;

            if (radioPlayer1Red.IsChecked == true)
            {
                radioPlayer1Blue.IsChecked = true;
            }
        }

        private void radioPlayer2Blue_Checked(object sender, RoutedEventArgs e)
        {
            colorsPlayer2 = Colors.Blue;

            if (radioPlayer1Blue.IsChecked == true)
            {
                radioPlayer1Red.IsChecked = true;
            }
        }
        #endregion
    }
}
