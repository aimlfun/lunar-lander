using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketAI.AI
{
    /// <summary>
    /// AI related settings.
    /// </summary>
    internal static class AISettings
    {
        /// <summary>
        /// If true, it draws bases and tries to get the rockets to land on them rather than "somewhere".
        /// </summary>
        internal static bool s_fixedBases = true;

        /// <summary>
        /// How many rockets to start with.
        /// </summary>
        internal static int s_numberOfRocketsToCreate = 100;

        /// <summary>
        /// How many moves before the first mutation occurs.
        /// </summary>
        internal static float s_movesUntilFirstMutation = 1000;

        /// <summary>
        /// How many generations to compute in the background, before showing the user.
        /// </summary>
        internal static int s_generationsToTrainBeforeShowingVisuals = 100;

        /// <summary>
        /// Scaling to increase length of time between mutations.
        /// </summary>
        internal static float s_pctIncreaseBetweenMutations = 1.1F;

        /// <summary>
        /// How much any directional thruster is amplified.
        /// </summary>
        internal static double s_AIthrusterNozzleAmplifier = 2F; // multiplier also used: 0.2 

        /// <summary>
        /// How much the AI "burn" gets amplified.
        /// </summary>
        internal static double s_AIburnAmplifier = 1.2F; // multiplier

        /// <summary>
        /// If true, we mutate half the rockets.
        /// If false, we clone all from the best and mutate each.
        /// </summary>
        internal static bool s_mutate50pct = true;

        /// <summary>
        /// When failures are set, this contains the failure. <failure-type,setting> where setting is 0 meaning off. 1/2 depend on setting.
        /// </summary>
        internal static Dictionary<string, int> s_failures = new();

        /// <summary>
        /// When not zero, the wind blows the rockets. Sign indicates direction, -ve is right to left.
        /// </summary>
        internal static float s_maxWindStrength = 0f;
        
        /// <summary>
        /// Whether the wind is random or constant.
        /// </summary>
        internal static bool s_variableWindStrengthAndDirection = true;
    }
}
