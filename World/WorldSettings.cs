using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketAI.World
{
    /// <summary>
    /// Settings that relate to the world.
    /// </summary>
    internal static class WorldSettings
    {
        /// <summary>
        /// If true, it uses Parallel.Foreach(). This is faster, and with more complicate neural networks
        /// enables it to scale better
        /// </summary>
        internal static bool s_useParallelMove = true;

        /// <summary>
        /// Gravity.
        /// </summary>
        internal static double s_gravity = 9.81F; // ms^2

        /// <summary>
        /// Air resistance amount.
        /// </summary>
        internal static double s_airResistance = 1.225F;  // kg / m^2

        /// <summary>
        /// Coefficient of drag for the rocket. Guesstimate.
        /// </summary>
        internal static double s_Cdrag = 0.7F;

    }
}