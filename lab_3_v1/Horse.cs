using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace lab_3_v1
{
    public class Horse
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }
        public double RaceTime { get; set; }
        public double Speed { get; private set; }
        private int CurrentPosition { get; set; }
        private int CurrentAnimationFrame { get; set; }
        private WriteableBitmap[] AnimationFrames { get; set; }
        public bool IsWinner { get; set; }
        public double Distance { get; set; }

        public double FinishTime { get; private set; }
        public string FinishTimeString { get; private set; }

        public Horse(string name, Color color, double speed)
        {
            Name = name;
            Color = color;
            IsWinner = false;
            Distance = 0;
            Speed = speed;

        }
        public void Run()
        {
            Distance += (int)Speed; 
        }
        public void Reset()
        {
            IsWinner = false;
            Distance = 0;
        }

        public async Task ChangeAcceleration(double newAcceleration)
        {
            // Implement acceleration change logic
        }

        public async Task SimulateRace()
        {
            while (CurrentPosition < RaceTrack.TotalLength)
            {
                await Task.Delay(TimeSpan.FromSeconds(1)); 
                MoveOnTrack();
                UpdateAnimationFrame(); 
                UpdateRaceTable(); 
            }
        }
        public void HandleFinish(double raceTime)
        {
            FinishTime = raceTime;
            FinishTimeString = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            IsWinner = true; // Позначимо коня як переможця
        }

        private void MoveOnTrack()
        {
            // Implement logic to move the horse on the track based on its speed
        }

        private void UpdateAnimationFrame()
        {
            // Update the current animation frame based on the horse's movement
        }

        private void UpdateRaceTable()
        {
            // Update the race table with the current position, time, etc. of the horse
        }
    }   
}
