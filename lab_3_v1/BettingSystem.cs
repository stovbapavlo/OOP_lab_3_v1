using System;
using System.Collections.Generic;
using System.Windows;

namespace lab_3_v1
{
    public class BettingSystem
    {
        private Horse selectedHorse;
        private double betAmount;
        private List<Horse> horses;
        private Dictionary<Horse, double> Odds;
        public double Balance { get; private set; }

        public BettingSystem(List<Horse> horses)
        {
            this.horses = horses;
            InitializeOdds();
            Balance = 1000;
        }

        public double GetBalance()
        {
            return Balance;
        }

        public bool PlaceBet(Horse horse, double amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;

                
                selectedHorse = horse;
                betAmount = amount;

                return true; 
            }
            else
            {
                MessageBox.Show("Not enough balance to place this bet.");
                return false; 
            }
        }

        public void ProcessBets(Horse winner)
        {
            if (selectedHorse != null && winner != null && selectedHorse == winner)
            {
                double winnings = betAmount * Odds[selectedHorse]; 
                Balance += winnings; 
                MessageBox.Show($"Congratulations! Your bet on {winner.Name} won. You've won ${winnings}.");
            }
            else
            {
                MessageBox.Show("Sorry, your bet did not win.");
            }
        }

        private void InitializeOdds()
        {
            Odds = new Dictionary<Horse, double>();
            foreach (var horse in horses)
            {
                Random random = new Random();
                double odds = random.NextDouble() * (5 - 1) + 1;
                Odds.Add(horse, odds);
            }
        }

        public void UpdateOdds()
        {
        }
    }
}
