using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketAI.Vehicle
{
    
    /// <summary>
    /// Settings for the rocket.
    /// </summary>
    internal static class RocketSettings
    {        
        /// <summary>
        /// Height of the space ship.
        /// </summary>
        internal static double s_maxHeight = 2000; // metres

        /// <summary>
        /// This is the amount of fuel (weight wise) the rocket starts with.
        /// </summary>
        internal static double s_FuelMassKg = 10624; // kg 

        /// <summary>
        /// Mass of the rocket.
        /// </summary>
        internal static double s_massOfRocketKg = 4479; // kg = 220,000lb

        /// <summary>
        /// Amount of thrust.
        /// </summary>
        internal static double s_thrusterKg = 15103; // kg
    }
}
