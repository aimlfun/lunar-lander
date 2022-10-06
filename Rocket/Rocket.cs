using RocketAI.AI;
using RocketAI.Simulation;
using RocketAI.Utils;
using RocketAI.UX;
using RocketAI.Vehicle.UX;
using RocketAI.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RocketAI.Vehicle
{
    /// <summary>
    /// Represents an AI rocket.
    /// </summary>
    internal class Rocket
    {        
        /// <summary>
        /// Unique identifier for this rocket. Associated with the neural network having this ID
        /// </summary>
        private readonly int id = 0;

        /// <summary>
        /// Contains a classs that can draw a rocket.
        /// </summary>
        internal RocketRenderer Renderer = new();

        /// <summary>
        /// Where the rocket is located relative to the world coordinates.
        /// </summary>
        private PointF location = new();

        /// <summary>
        /// Stores the angle the rocket is pointing
        /// </summary>
        private double anglePointingDegrees = 0;

        /// <summary>
        /// Stores the vertical velocity.
        /// </summary>
        private double verticalVelocity = 0;

        /// <summary>
        /// Stores the lateral (horizontal) velocity.
        /// </summary>
        private double lateralVelocity = 0;

        /// <summary>
        /// Angle the thrust is applied at.
        /// </summary>
        private double offsetAngleOfThrustInDegrees = 0;

        /// <summary>
        /// How much the fuel the rocket starts with.
        /// </summary>
        private readonly double initialFuelKg; // kg

        /// <summary>
        /// How much the fuel is remaining.
        /// </summary>
        private double fuelRemainingKg; // kg

        /// <summary>
        /// Acceleration upwards or downwards (if rocket is pointing upwards).
        /// </summary>
        private double verticalAcceleration = 0;

        /// <summary>
        /// Acceleration horizontally (if rocket is pointing upwards).
        /// </summary>
        private double lateralAcceleration = 0;

        /// <summary>
        /// Amount of force from igniting the thruster.
        /// </summary>
        private double burnForce = 0;

        /// <summary>
        /// Time (used in v=u+at*t/2, where t is dt).
        /// </summary>
        private readonly double dt = 0.1;

        /// <summary>
        /// Indicates the rocket is eliminated if true.
        /// </summary>
        private bool isEliminated = false;

        /// <summary>
        /// Reason why the rocket was eliminated.
        /// </summary>
        private string eliminatedBecauseOf = "";

        /// <summary>
        /// Each rocket has a "colour". That colour is in this brush.
        /// </summary>
        internal SolidBrush ColouredBrushToPaintShip;

        /// <summary>
        /// Used to hide failures after a fixed period.
        /// </summary>
        private int eliminationCount = 0;

        /// <summary>
        /// This is the heading of the rocket.
        /// </summary>
        internal float targetBase = -1;

        #region SETTER / GETTERS
        /// <summary>
        /// Returns the rockets unique identifier.
        /// </summary>
        internal int Id
        {
            get { return id; }
        }

        /// <summary>
        /// Setter/Getter for the altitude of the rocket.
        /// </summary>
        /// <returns></returns>
        internal float Altitude
        {
            get { return location.Y; }
            set { location.Y = value; }
        }

        /// <summary>
        /// Setter/Getter for the location of the rocket.
        /// </summary>
        /// <returns></returns>
        internal float PositionX
        {
            get { return location.X; }
            set { location.X = value; }
        }

        /// <summary>
        /// Returns the angle of the rocket.
        /// </summary>
        internal double AnglePointingDegrees
        {
            get { return anglePointingDegrees; }
        }

        /// <summary>
        /// Returns the vertical velocity.
        /// </summary>
        internal double VerticalVelocity
        {
            get { return verticalVelocity; }
        }

        /// <summary>
        /// Returns the lateral velocity
        /// </summary>
        internal double LateralVelocity
        {
            get { return lateralVelocity; }
        }

        /// <summary>
        /// Returns the angle of the thrust vectoring gimbal.
        /// </summary>
        internal double OffsetAngleOfThrustInDegrees
        {
            get { return offsetAngleOfThrustInDegrees; }
        }

        /// <summary>
        /// Returns %age fuel used. 0..1 = 0-100%R
        /// </summary>
        internal double PctFuelUsed
        {
            get
            {
                return fuelRemainingKg / initialFuelKg;
            }
        }

        /// <summary>
        /// Returns the burn force.
        /// </summary>
        internal double BurnForce
        {
            get => burnForce;         
        }
        
        /// <summary>
        /// Setter/Getter for elimination reason.
        /// </summary>
        internal string EliminatedBecauseOf
        {
            get
            {
                return eliminatedBecauseOf;
            }

            set
            {
                eliminatedBecauseOf = value;
                isEliminated = true;
                eliminationCount = 100;
            }
        }

        /// <summary>
        /// Returns how many rockets were eliminated
        /// </summary>
        internal int EliminationCount
        {
            get { return eliminationCount; }
            set {  eliminationCount = value; }
        }
#endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <returns></returns>
        internal Rocket(int rocketId)
        {
            id = rocketId;

            Altitude = 1800; // 300;
            PositionX = id * 15 + 130; // so they are spread horizontally
            anglePointingDegrees = 90;
            verticalVelocity = 0;

            fuelRemainingKg = RocketSettings.s_FuelMassKg;
            initialFuelKg = fuelRemainingKg;

            ColouredBrushToPaintShip = new(Color.FromArgb((20+(id * 971)) % 255, (id * 113) % 255, (id * 211) % 255)); // random colour

            // choose a random number from the list of bases.
            if (AISettings.s_fixedBases)
            {
                targetBase = WorldBackground.GetBaseOffset(RandomNumberGenerator.GetInt32(0, 6));
            }
        }

        /// <summary>
        /// Returns the total mass (ship + fuel).
        /// </summary>
        /// <returns></returns>
        internal double TotalMass
        {
            get { return RocketSettings.s_massOfRocketKg + fuelRemainingKg; }
        }

        /// <summary>
        /// Detects if the rocket is going rather fast downwards.
        /// If this is violated, don't go in the rocket!
        /// </summary>
        /// <returns></returns>
        internal bool ComingInHot()
        {
            return (verticalVelocity < -5); // m/s
        }

        /// <summary>
        /// What speed is deemed *too fast*. We allow it to plummet fairly fast,
        /// until it gets close to the ground.
        /// </summary>
        /// <returns></returns>
        private bool Overspeed()
        {
            return (verticalVelocity < -30); // m/s
        }

        /// <summary>
        /// Detect horizontal movement that will cause problems. 
        /// In reality a human can only take 9g, and not sustained
        /// </summary>
        /// <returns></returns>
        private bool TooMuchLateralGForce()
        {
            return (Math.Abs(lateralAcceleration) > 15); // g
        }

        /// <summary>
        /// Detect side ways velocity.
        /// </summary>
        /// <returns></returns>
        private bool TooMuchLateralVelocity()
        {
            return (Math.Abs(lateralVelocity) > 30);
        }

        /// <summary>
        /// Detect if the AI should be eliminated, e.g. because it overspeeds, or goes out into space.
        /// </summary>
        internal void DetectElimination()
        {
            // upwards not downwards
            if (Altitude > RocketSettings.s_maxHeight || (Altitude < 1000 && verticalVelocity >10))
            {
                EliminatedBecauseOf = "up";
                return;
            }

            // to fast generally
            if (Overspeed())
            {
                EliminatedBecauseOf = "vel";
                return;
            }

            // too much lateral movement
            if (TooMuchLateralGForce() || TooMuchLateralVelocity())
            {
                EliminatedBecauseOf = "g";
                return;
            }

            if(fuelRemainingKg <= 0)
            {
                EliminatedBecauseOf = "fuel";
                return;
            }

            // too fast hitting floor
            if (Altitude < 1 && ComingInHot())
            {
                EliminatedBecauseOf = "> 5m/s";
                return;
            }

            // off screen
            if (PositionX < -100 || PositionX > 2000)
            {
                EliminatedBecauseOf = "o/s";
                return;
            }
        }

        /// <summary>
        /// Rates the neural network for landing the space craft.
        /// </summary>
        internal void UpdateFitness()
        {
            if (HasLandedSafely)
            {
                AISettings.s_mutate50pct = false;
                NeuralNetwork.s_networks[id].Fitness = 1000000F + (10F + (float)verticalVelocity) - Math.Abs((float)anglePointingDegrees) + 20 - Math.Abs(targetBase - location.X); // 0 degrees = top points, slower landing better
                return;
            }

            // up it goes into space...
            if (Altitude > RocketSettings.s_maxHeight)
            {
                NeuralNetwork.s_networks[id].Fitness = -int.MaxValue; // upwards!
                return;
            }

            float x = Math.Abs((float)verticalVelocity);

            if (x > 50) x = 50;

            // broken basic rules
            if ((Math.Abs(lateralVelocity) > 20)
                || (verticalVelocity > -0.5)
                || (verticalVelocity > 0)
                || (Math.Abs(lateralVelocity)>20)
                || EliminatedBecauseOf == "offs"
                || EliminatedBecauseOf == "up"
                || EliminatedBecauseOf == "fuel"
                || (RocketSettings.s_maxHeight - Altitude < 500))
            {
                NeuralNetwork.s_networks[id].Fitness = -int.MaxValue;
                return;
            }

            if (x > 0)
            {
                float altitudePoints = (float)RocketSettings.s_maxHeight - (float)Altitude;
 
                if (Altitude < 10)
                {
                    // punish for too much gforce
                    // punish for too much velocity
                    // punish for being too far from target base
                    NeuralNetwork.s_networks[id].Fitness = 1000000F / (TooMuchLateralGForce() ? 2 : 1) - 1000 + (10F + (float)verticalVelocity) - Math.Abs((float)anglePointingDegrees) - Math.Abs(targetBase-location.X)*100;
                }

                NeuralNetwork.s_networks[id].Fitness = altitudePoints * (float)0.5F / 
                                                           (float)x * ((verticalVelocity < -20) ? 0.5F : 1) + (Altitude < 1000 ? 2000-Math.Abs(targetBase - location.X) : 0);
            }
            else
                NeuralNetwork.s_networks[id].Fitness = -999;
        }

        /// <summary>
        /// Determines if the spaceship has landed safely (not too fast).
        /// </summary>
        internal bool HasLandedSafely
        {
            get
            {
                return (Altitude < 1 && !ComingInHot());
            }
        }

        /// <summary>
        /// Computes the acceleration from the thrust based on the burn force and angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="force"></param>
        internal void ApplyThrust(double angle, double force)
        {
            if (fuelRemainingKg < 0) return;

            // a= f/m
            double a = force / TotalMass;

            angle += (angle < 0 ? 360 : 0) - (angle >= 360 ? 360 : 0);

            double angleInRadians = Math.PI * angle / 180;
            double cos = Math.Cos(angleInRadians);
            double sin = Math.Sin(angleInRadians);

            // acceleration in up/down and lateral direction based on angle
            verticalAcceleration += a * cos;
            lateralAcceleration += a * sin;

            fuelRemainingKg -= force / 150000; // burns fuel
        }

        /// <summary>
        /// Apply fluid dynamics for the air-resistance.
        /// Air is thinner higher up, needs scaling based on altitude so we approximate that too.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="Fupwards"></param>
        /// <param name="Flateral"></param>
        internal void ComputeDrag(double angle, out double Fupwards, out double Flateral)
        {
            double adjustmentBecauseAirIsThinnerAtAltitude = (1 - ((Altitude / RocketSettings.s_maxHeight)/4)); // 1 at bottom close to 0 at altitude.

            // F[d]=1/2*[density of fluid] x v^2 * C[drag coefficient] * A
            Fupwards = adjustmentBecauseAirIsThinnerAtAltitude * Math.Abs(Math.Cos(Math.PI * angle / 180) * (0.5F * WorldSettings.s_airResistance * verticalVelocity * verticalVelocity * WorldSettings.s_Cdrag * Math.PI / 4 * 0.5 * 0.5)); // 0.5=d2
            Flateral = adjustmentBecauseAirIsThinnerAtAltitude * Math.Abs(0.5F * WorldSettings.s_airResistance * lateralVelocity * lateralVelocity * WorldSettings.s_Cdrag * Math.PI / 4 * 0.5 * 0.5); // 0.5=d2
        }

        /// <summary>
        /// What an engine does to keep the rocket in the sky (burn fuel).
        /// </summary>
        /// <param name="force"></param>
        internal void Burn(double force)
        {
            if (fuelRemainingKg < 0) return; // no fuel, no burn

            burnForce += force;
        }

        /// <summary>
        /// Setter/Getter for whether rocket is eliminated or not.
        /// </summary>
        internal bool IsEliminated
        {
            get
            {
                return isEliminated;
            }

            set
            {
                if (value == IsEliminated) return;
                UpdateFitness();

                eliminationCount = 200;
                isEliminated = value;
            }
        }

        /// <summary>
        /// Moves the rocket (applying velocity, and acceleration via AI).
        /// </summary>
        internal void Move()
        {
            if (HasLandedSafely)
            {
                return; // no move, it's landed
            }

            if (isEliminated) return; // no move it's crashed

            // INPUT: xaccel, yaccel, xspeed, yspeed, angle, alt
            // OUTPUT: angle, thrust
            double[] outputFromNeuralNetwork;
            double[] neuralNetworkInput = GetInputsToLandRocketSafely();

            // ask the neural to use the input and decide what to do with the rocket
            outputFromNeuralNetwork = NeuralNetwork.s_networks[id].FeedForward(neuralNetworkInput); // process inputs

            /*           T  f=ma
             *          /|\
             *           |
             *          ___                                                     Angle           _  angle of thrust
             *         /   \      v0                                            /|\          |  /|
             *         |___|      |                                              |           | /
             *         /   \      |            |                                 |           |/  theta
             *           |       \|/          \|/ vf = 0.5ms
             *          \|/
             *          mg
             *
             *          F = m(a[thrust])-mg
             *          T = m(a+g)
             */

            burnForce = (1 + outputFromNeuralNetwork[0]) * (RocketSettings.s_thrusterKg * WorldSettings.s_gravity) * AISettings.s_AIburnAmplifier;

            if (AISettings.s_failures.ContainsKey("burnForce"))
            {
                if (AISettings.s_failures["burnForce"] == 1) burnForce *= 0.7f; // reduced burn

                if (AISettings.s_failures["burnForce"] == 2) burnForce = RandomNumberGenerator.GetInt32(0, 5) < 2 ? 0 : burnForce;// intermittent not firing

                if (fuelRemainingKg < 1 || burnForce < 0) burnForce = 0; // no burn;
            }

            // adjustment of angle
            offsetAngleOfThrustInDegrees = (outputFromNeuralNetwork[1] * AISettings.s_AIthrusterNozzleAmplifier).Clamp(-45, 45); //degrees

            if (AISettings.s_failures.ContainsKey("offsetAngleOfThrustInDegrees"))
            {
                if (AISettings.s_failures["offsetAngleOfThrustInDegrees"] == 1) offsetAngleOfThrustInDegrees = offsetAngleOfThrustInDegrees.Clamp(-27,23); // reduced travel imbalanced
                if (AISettings.s_failures["offsetAngleOfThrustInDegrees"] == 2 && RandomNumberGenerator.GetInt32(0, 10) < 1) offsetAngleOfThrustInDegrees = 0; // intermittent, no angle
            }

            // acceleration is computed each time, you cannot keep adding to it.
            // push with "wind"
            lateralAcceleration = RocketSimulator.s_windStrength; // simple wind pushes the rocket sideways for an added challenge
            verticalAcceleration = 0;

            // thrust alters lateral and vertical acceleratopm
            if (burnForce > 0)
            {
                ApplyThrust(anglePointingDegrees - offsetAngleOfThrustInDegrees, burnForce);
                anglePointingDegrees += 3 * offsetAngleOfThrustInDegrees; // completely ignores inertia/momentum
            }

            double ForceDownwards = TotalMass * WorldSettings.s_gravity;

            // a = f/m
            verticalAcceleration -= ForceDownwards / TotalMass;

            ComputeDrag(anglePointingDegrees, out double Fupwards, out double Flateral);

            // a = f/m, applying our air resistance drag
            verticalAcceleration -= -Fupwards * Math.Sign(verticalAcceleration) / TotalMass;
            lateralAcceleration -= -Flateral * Math.Sign(lateralAcceleration) / TotalMass;

            // s = ut + 1/2 a*t*t
            location.X += (float)(lateralVelocity * dt + lateralAcceleration * dt * dt / 2.0f);
            location.Y += (float)(verticalVelocity * dt + verticalAcceleration * dt * dt / 2.0f);

            // v = u + at;
            verticalVelocity += verticalAcceleration * dt;
            lateralVelocity += lateralAcceleration * dt;
        }

        /// <summary>
        /// Returns a random float -0.1..+0.1
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        private static float Random10Pct(float max)
        {
            return max*((float)RandomNumberGenerator.GetInt32(0, 100000) - 50000f) / 10000; // 10%
        }

        /// <summary>
        /// These are the inputs to the neural network to enable it to land safely.
        /// </summary>
        /// <returns></returns>
        private double[] GetInputsToLandRocketSafely()
        {
            // 0 degrees is UP, we want it facing UP. If we don't do this, the rockets tumble.
            // -2...+2, represent 8 to -8 degrees
            double controlFeedAngleOfRocket = 2 - anglePointingDegrees.Clamp(-8, 8) / 4;
           
            if (AISettings.s_failures.ContainsKey("controlFeedAngleOfRocket"))
            {
                if (AISettings.s_failures["controlFeedAngleOfRocket"] == 1) controlFeedAngleOfRocket = 0; // no reading
                if (AISettings.s_failures["controlFeedAngleOfRocket"] == 2) controlFeedAngleOfRocket = (controlFeedAngleOfRocket + Random10Pct(0.2f)).Clamp(-2,2); // 1% inaccuracy
            }

            // 0 alt = 2, maxHeight - 1
            double controlFeedHeight = 2 - (Altitude / RocketSettings.s_maxHeight); // inform it of the altitude           
            
            if (AISettings.s_failures.ContainsKey("controlFeedHeight"))
            {
                if (AISettings.s_failures["controlFeedHeight"] == 1) controlFeedHeight = 0; // no reading
                if (AISettings.s_failures["controlFeedHeight"] == 2) controlFeedHeight = (controlFeedHeight + Random10Pct(2)).Clamp(0,2); // inaccuracy
            }

            double controlLateralAcceleration = 2 - lateralAcceleration.Clamp(-10, 0) / 5;
            
            if (AISettings.s_failures.ContainsKey("controlLateralAcceleration"))
            {
                if (AISettings.s_failures["controlLateralAcceleration"] == 1) controlLateralAcceleration = 0; // no reading
                if (AISettings.s_failures["controlLateralAcceleration"] == 2) controlLateralAcceleration = (controlLateralAcceleration + Random10Pct(2)).Clamp(0,2); // inaccuracy
            }

            
            double controlVerticalAcceleration = 2 - verticalAcceleration.Clamp(-6, 0) / 3;
            if (AISettings.s_failures.ContainsKey("controlVerticalAcceleration"))
            {
                if (AISettings.s_failures["controlVerticalAcceleration"] == 1) controlVerticalAcceleration = 0; // no reading
                if (AISettings.s_failures["controlVerticalAcceleration"] == 2) controlVerticalAcceleration = (controlVerticalAcceleration + Random10Pct(2)).Clamp(0,2); // inaccuracy
            }

            double controlLateralVelocity = 2 - lateralVelocity.Clamp(-30, 30) / 15;
            
            if (AISettings.s_failures.ContainsKey("controlLateralVelocity"))
            {
                if (AISettings.s_failures["controlLateralVelocity"] == 1) controlLateralVelocity = 0; // no reading
                if (AISettings.s_failures["controlLateralVelocity"] == 2) controlLateralVelocity = (controlLateralVelocity + Random10Pct(2)).Clamp(0,2); // inaccuracy
            }

            double controlVerticalVelocity = -2 * verticalVelocity.Clamp(-3, 1) / 3;
            
            if (AISettings.s_failures.ContainsKey("controlVerticalVelocity"))
            {
                if (AISettings.s_failures["controlVerticalVelocity"] == 1) controlVerticalVelocity = 0; // no reading
                if (AISettings.s_failures["controlVerticalVelocity"] == 2) controlVerticalVelocity = (controlVerticalVelocity + Random10Pct(2)).Clamp(-2, 2); // inaccuracy
            }

            double controlMoveToBase = AISettings.s_fixedBases ? 2 - (targetBase - location.X).Clamp(-20, 20) / 20 : 0; // make it aim for the base

            double[] neuralNetworkInput = { controlLateralAcceleration,
                                            controlVerticalAcceleration,
                                            controlFeedAngleOfRocket,
                                            controlLateralVelocity,
                                            controlVerticalVelocity,
                                            controlFeedHeight,
                                            controlMoveToBase };
            return neuralNetworkInput;
        }
    }
}