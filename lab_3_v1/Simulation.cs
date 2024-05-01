using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_v1
{
    public static class Simulation
    {
        public static async Task StartRace(List<Horse> horses)
        {
            var tasks = horses.Select(horse => horse.SimulateRace());
            await Task.WhenAll(tasks);
        }
    }

}
