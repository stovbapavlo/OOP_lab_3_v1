using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace lab_3_v1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Horse selectedHorse;
        private double betAmount;
        private double Balance;

        private List<Horse> horses;
        private Horse winner;
        private BettingSystem bettingSystem;

        private DispatcherTimer raceTimer;
        private Random random = new Random();

        private List<RaceResult> raceResults;



        public MainWindow()
        {
            InitializeComponent();
            InitializeHorses();
            HorseComboBox.ItemsSource = horses;
            bettingSystem = new BettingSystem(horses);

            raceResults = new List<RaceResult>();

            UpdateBalance();

            random = new Random();
            raceTimer = new DispatcherTimer();
            raceTimer.Interval = TimeSpan.FromSeconds(1);
            raceTimer.Tick += RaceTimer_Tick;
        }

        private void InitializeHorses()
        {
            horses = new List<Horse>
            {
                new Horse("Horse 1", Colors.Red, GetRandomSpeed()),
                new Horse("Horse 2", Colors.Blue, GetRandomSpeed()),
                new Horse("Horse 3", Colors.Green, GetRandomSpeed()),
                new Horse("Horse 4", Colors.Yellow, GetRandomSpeed())
            };
            HorsesListBox.ItemsSource = horses;
        }
        private double GetRandomSpeed()
        {
            return random.Next(10, 31); 
        }
        private void RaceTimer_Tick(object sender, EventArgs e)
        {
            bool allHorsesFinished = true;
            double raceTime = raceTimer.Interval.TotalSeconds;

            foreach (var horse in horses)
            {
                horse.Distance += horse.Speed;

                if (horse.Distance >= RaceTrack.TotalLength)
                {
                    if (winner == null)
                    {
                        winner = horse;
                    }
                    horse.HandleFinish(raceTime);
                }
                else
                {
                    allHorsesFinished = false;
                }
            }

            if (allHorsesFinished)
            {
                raceTimer.Stop();
                UpdateRaceResults();
                bettingSystem.ProcessBets(winner);
            }

            HorsesListBox.Items.Refresh();
        }




        private void StartRaceButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var horse in horses)
            {
                horse.Reset(); // Скидаємо попередні дані кожного коня перед новим забігом
            }

            if (raceTimer.IsEnabled)
            {
                MessageBox.Show("Race is already in progress.");
                return;
            }

            raceTimer.Start(); // Запускаємо таймер гонки
           

        }

        private void UpdateRaceResults()
        {
            Horse1ResultTextBlock.Text = $"{horses[0].Name}: Race Time: {horses[0].RaceTime} seconds";
            Horse2ResultTextBlock.Text = $"{horses[1].Name}: Race Time: {horses[1].RaceTime} seconds";
            Horse3ResultTextBlock.Text = $"{horses[2].Name}: Race Time: {horses[2].RaceTime} seconds";
            Horse4ResultTextBlock.Text = $"{horses[3].Name}: Race Time: {horses[3].RaceTime} seconds";

            if (winner != null)
            {
                RaceResultsTextBlock.Text = "Winner: " + winner.Name + ", Race Time: " + winner.RaceTime + " seconds";
            }
        }



        private void PlaceBetButton_Click(object sender, RoutedEventArgs e)
        {
            if (HorseComboBox.SelectedItem != null && !string.IsNullOrEmpty(BetAmountTextBox.Text) && double.TryParse(BetAmountTextBox.Text, out double betAmount))
            {
                if (betAmount <= 0)
                {
                    MessageBox.Show("Please enter a positive bet amount.");
                    return;
                }
                if (bettingSystem != null)
                {
                    var selectedHorse = (Horse)HorseComboBox.SelectedItem;
                    if (bettingSystem.PlaceBet(selectedHorse, betAmount))
                    {
                        // Успішно встановлено ставку
                        UpdateBettingUI();
                        UpdateBalance();
                    }
                    else
                    {
                        // Не вдалося встановити ставку (недостатньо коштів)
                    }
                }
                else
                {
                    MessageBox.Show("Betting system is not initialized.");
                }
            }
            else
            {
                MessageBox.Show("Please select a horse and enter a valid bet amount.");
            }
        }


        private void UpdateBalance()
        {
            double balance = bettingSystem.GetBalance();
            BalanceTextBlock.Text = "Balance: $" + balance.ToString("0.00"); 
        }



        private void BetAmountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(BetAmountTextBox.Text, out double betAmount))
            {
                BetAmountTextBox.Text = ""; 
            }
        }


        private void UpdateBettingUI()
        {
            
        }
    }
}