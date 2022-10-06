using RocketAI.AI;
using RocketAI.Utils;
using RocketAI.UX;
using RocketAI.Vehicle;
using RocketAI.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RocketAI.Simulation
{
    /// <summary>
    /// Class to manage our ML controlled rockets.
    /// </summary>
    internal class RocketSimulator
    {
        /// <summary>
        /// This is the generation of the neural networks. Each time a mutation occurs it increases.
        /// </summary>
        private float Generation = 0;

        /// <summary>
        /// This is the canvas, we are drawing the rockets to.
        /// </summary>
        private readonly PictureBox? Canvas;

        /// <summary>
        /// Timer used to move the rocket.
        /// </summary>
        readonly System.Windows.Forms.Timer timerRocketMove = new();

        /// <summary>
        /// Track the rockets we added.
        /// </summary>
        private readonly Dictionary<int, Rocket> rockets = new();

        /// <summary>
        /// Width of the canvas, to save accessing it repeatedly.
        /// </summary>
        private readonly int width;

        /// <summary>
        /// Height of the canvas, to save accessing it repeatedly.
        /// </summary>
        private readonly int height;

        /// <summary>
        /// Number of generations we silently compute.
        /// </summary>
        private static int s_silentGenerationsRemaining;

        /// <summary>
        /// When this reaches zero, a mutation of the neural network occurs.
        /// </summary>
        private static float s_movesUntilNextMutation = 1000;

        /// <summary>
        /// How many moves between each mutation
        /// </summary>
        private static float s_movesPerMutation = 1000;

        /// <summary>
        /// Set to true to ensure it exits quiet mode.
        /// </summary>
        private static bool s_stopNow = false;

        /// <summary>
        /// Wind strength is variable throughout the flight, buckle up your belt. 
        /// </summary>
        internal static float s_windStrength = 0;

        /// <summary>
        /// Line for the "windicator" that shows strength and direction in the form of an arrow.
        /// </summary>
        readonly static Pen s_penWindIndicator = new(Color.FromArgb(30, 255, 255, 255));


        /// <summary>
        /// Constructor, attaches the emulation to a canvas.
        /// </summary>
        /// <param name="canvas"></param>
        internal RocketSimulator(PictureBox canvas)
        {
            Canvas = canvas;

            width = canvas.Width;
            height = canvas.Height;
        }

        /// <summary>
        /// Initialises everything, and rockets are unleashed
        /// </summary>
        internal void IgniteRockets()
        {
            // timer moves the rocket
            timerRocketMove.Stop(); // this could still crash, if it's busy when we re-ignite
            timerRocketMove.Interval = 10; // ms
            timerRocketMove.Tick += TimerRocketMove_Tick;

            // windicator is dotted with arrow
            s_penWindIndicator.Width = 20;
            s_penWindIndicator.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            s_penWindIndicator.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;

            // data that affects when a mutation occurs
            s_movesUntilNextMutation = AISettings.s_movesUntilFirstMutation;
            s_silentGenerationsRemaining = AISettings.s_generationsToTrainBeforeShowingVisuals;
            s_movesPerMutation = s_movesUntilNextMutation;

            // this initialises the neural network (enough for the number of rockets)
            InitialiseTheNeuralNetworksForTheRockets();

            // create a rocket, one per NN
            InitialiseRockets();

            // this starts the moving.
            timerRocketMove.Start();
        }
        
        /// <summary>
        /// Pauses/unpauses.
        /// </summary>
        internal void PauseUnpause()
        {
            timerRocketMove.Enabled = !timerRocketMove.Enabled;
        }

        /// <summary>
        /// Forces the rockets AI to mutate.
        /// </summary>
        internal void ForceMutate()
        {
            timerRocketMove.Stop();
            
            MutateRockets(out _);

            s_movesPerMutation *= AISettings.s_pctIncreaseBetweenMutations;
            s_movesUntilNextMutation = s_movesPerMutation;
            s_movesPerMutation=s_movesUntilNextMutation.Clamp(0, 10000);

            // after mutation, we recreate the rockets
            InitialiseRockets();
            timerRocketMove.Start();
        }

        /// <summary>
        /// Steps thru the speeds.
        /// </summary>
        internal void StepThroughSpeeds()
        {
            var newInterval = timerRocketMove.Interval switch
            {
                10 => 50,
                50 => 250,
                250 => 500,
                500 => 1000,
                _ => 10,
            };

            timerRocketMove.Interval = newInterval;
        }

        /// <summary>
        /// In quiet mode, it doesn't animate rockets.
        /// </summary>
        internal static void ToggleQuietMode()
        {
            if (s_silentGenerationsRemaining > 0) s_silentGenerationsRemaining = 0; else s_silentGenerationsRemaining = 1000;
        }

        /// <summary>
        /// This empties our rocket dictionary, and recreates them.
        /// We use it during mutation to put the rockets back to the start
        /// and ensure no data they contain persists past mutation.
        /// </summary>
        private void InitialiseRockets()
        {
            rockets.Clear();

            for (int i = 0; i < AISettings.s_numberOfRocketsToCreate; i++)
            {
                rockets.Add(i, new(i));
            }
        }

        /// <summary>
        /// Each time a tick occurs, we move the rocket. It also switches to quiet mode, whilst remembering
        /// to "DoEvents()" frequently enough to stop the UI locking up (this is running on the UI thread).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerRocketMove_Tick(object? sender, EventArgs e)
        {
            if (s_silentGenerationsRemaining > 0)
            {
                // if in "silent" training, we avoid a timer to achieve better throughput.
                // i.e. with timer, we move once every 10ms (or whatever it is set to), where as a fixed loop
                // means we move as quick as possible and rattle through mutations.
                ContinuouslyMoveRocketsWithoutUpdatingUIToImprovePerformance();
                return;
            }

            bool atLeastOneRemaining = MoveRockets();

            DrawRockets(null);

            if (atLeastOneRemaining && --s_movesUntilNextMutation >= 0) return;

            timerRocketMove.Stop();
            MutateRockets(out List<int> best);

            // draw circles around the best
            DrawRockets(best);
            ShowSafelyLandedCount();
            Application.DoEvents();
            Thread.Sleep(2000);

            timerRocketMove.Start();

            ExtendMutationTimeOnResetRockets();
        }


        /// <summary>
        /// Shows how many rockets landed safely
        /// </summary>
        private void ShowSafelyLandedCount()
        {
            if (Canvas is null || Canvas.Image is null) throw new Exception("The simulator runs on a canvas, why is it null?");

            Bitmap bitmapCanvas = new(Canvas.Image); // start with our pre-drawn image and overlay rockets. we "clone" image, as Dispose will kill original
            using Graphics g = Graphics.FromImage(bitmapCanvas);

            // quality graphics
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            int landed = 0;
            foreach(int i in rockets.Keys) { if (rockets[i].HasLandedSafely) ++ landed; }

            string label = $"Rockets landed safely: {landed}";
            
            using Font f = new("Arial", 28);
            SizeF size = g.MeasureString(label, f);
            g.DrawString(label, f, Brushes.White, width / 2 - size.Width / 2, height / 2 - size.Height / 2);

            if (Canvas is null||Canvas.Image is null) return; // should NEVER happen, but just in case

            Canvas.Image?.Dispose();
            Canvas.Image = bitmapCanvas;

            Application.DoEvents(); // allow canvas change to paint
        }

        /// <summary>
        /// Extends time between mutations.
        /// </summary>
        private void ExtendMutationTimeOnResetRockets()
        {
            s_movesPerMutation *= AISettings.s_pctIncreaseBetweenMutations;
            s_movesPerMutation = s_movesPerMutation.Clamp(0, 10000);
            s_movesUntilNextMutation = s_movesPerMutation;

            InitialiseRockets();
        }

        /// <summary>
        /// Quiet mode. 
        /// ROckets are moved, and mutated when they are all eliminated (rule breaking or run out of fuel).
        /// </summary>
        /// <returns></returns>
        private void ContinuouslyMoveRocketsWithoutUpdatingUIToImprovePerformance()
        {
            timerRocketMove.Stop(); // we are running in a high-speed loop, not relying on the timer

            // whilst in quiet mode
            while (s_silentGenerationsRemaining > 0)
            {
                if (s_stopNow) return;
                bool atLeastOneRemaining = MoveRockets();

                if (s_movesUntilNextMutation % 100 == 0) Application.DoEvents(); // yield frequently enough to avoid UI locking up

                if (atLeastOneRemaining && --s_movesUntilNextMutation >= 0) continue; // not time to mutate?

                // time to mutate

                MutateRockets(out _); // "best" is out parameter. We don't care which is best, because we're not drawing

                // extend time between mutation
                ExtendMutationTimeOnResetRockets();
                
                --s_silentGenerationsRemaining;

                ShowGeneration(); // in the center of the screen, so user knows we're doing "something"
            }

            timerRocketMove.Start(); // we are no longer in quiet mode, rely on timer to move
        }

        /// <summary>
        /// Stops "quiet"/"silent" mode immediately.
        /// </summary>
        internal static void StopImmediately()
        {
            s_stopNow = true;
        }

        /// <summary>
        /// Paint the scenery without rockets, and add "Generation xxx" to the middle
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ShowGeneration()
        {
            // we use the pre-drawn image behind each "frame". We draw it once and cache.;
            Bitmap? scenery = WorldBackground.GetScenery();

            if (scenery is null) throw new Exception("Scenery must be initialised before use.");

            Bitmap bitmapCanvas = new(scenery); // start with our pre-drawn image and overlay rockets. we "clone" image, as Dispose will kill original
            using Graphics g = Graphics.FromImage(bitmapCanvas);
           
            // quality graphics
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            string label = $"Generation {Generation}";
            using Font f = new("Arial", 28);
            SizeF size = g.MeasureString(label, f);
            g.DrawString(label, f, Brushes.White, width / 2 - size.Width / 2, height / 2 - size.Height / 2);

            if (Canvas is null) return; // should NEVER happen, but just in case

            Canvas.Image?.Dispose();
            Canvas.Image = bitmapCanvas;

            Application.DoEvents(); // allow canvas change to paint
        }

        /// <summary>
        /// Initialises the neural network (one per rocket).
        /// </summary>
        internal static void InitialiseTheNeuralNetworksForTheRockets()
        {
            NeuralNetwork.s_networks.Clear();

            for (int rocketId = 0; rocketId < AISettings.s_numberOfRocketsToCreate; rocketId++)
            {
                _ = new NeuralNetwork(rocketId, NeuralNetwork.Layers);
            }
        }

        /// <summary>
        /// Replaces rockets with better ones (with slight mutation)
        /// </summary>
        /// <param name="best">List of id's of top rockets.</param>
        private void MutateRockets(out List<int> best)
        {
            ++Generation;

            // update networks fitness for each rocket
            foreach (int id in rockets.Keys) rockets[id].UpdateFitness();

            NeuralNetwork.SortNetworkByFitness(); // largest "fitness" (best performing) goes to the bottom

            // sorting is great but index no longer matches the "id".
            // this is because the sort swaps but this misaligns id with the entry            
            List<NeuralNetwork> n = new();
            foreach (int n2 in NeuralNetwork.s_networks.Keys) n.Add(NeuralNetwork.s_networks[n2]);

            NeuralNetwork[] array = n.ToArray();

            best = new List<int>();

            for (int bestkey = 0; bestkey < 5; bestkey++)
            {
                best.Add(n[NeuralNetwork.s_networks.Keys.Count - bestkey - 1].Id);
            }

            if (AISettings.s_mutate50pct)
            {
                // replace the 50% worse offenders with the best, then mutate them.
                // we do this by copying top half (lowest fitness) with top half.
                for (int worstNeuralNetworkIndex = 0; worstNeuralNetworkIndex < AISettings.s_numberOfRocketsToCreate / 2; worstNeuralNetworkIndex++)
                {
                    // 50..100 (in 100 neural networks) are in the top performing
                    int neuralNetworkToCloneFromIndex = worstNeuralNetworkIndex + AISettings.s_numberOfRocketsToCreate / 2; // +50% -> top 50% 
                    
                    NeuralNetwork.CopyFromTo(array[neuralNetworkToCloneFromIndex], array[worstNeuralNetworkIndex]); // copy
                    array[worstNeuralNetworkIndex].Mutate(25, 0.5F); // mutate       
                }
            }
            else
            {
                int NumberToPreserve = (int) ((4 + Generation / 200 - 4 > AISettings.s_numberOfRocketsToCreate) ? AISettings.s_numberOfRocketsToCreate - 2 : 4 + (Generation / 200));
             
                int offset = 0;

                for (int worstNeuralNetworkIndex = 0; worstNeuralNetworkIndex < AISettings.s_numberOfRocketsToCreate - NumberToPreserve; worstNeuralNetworkIndex++)
                {
                    // 50..100 (in 100 neural networks) are in the top performing
                    int neuralNetworkToCloneFromIndex = AISettings.s_numberOfRocketsToCreate - 1 - offset; // top rocket

                    NeuralNetwork.CopyFromTo(array[neuralNetworkToCloneFromIndex], array[worstNeuralNetworkIndex]); // copy

                    offset++;
                    if (offset >= NumberToPreserve) offset = 0;

                    //if (killRatio[array[worstNeuralNetworkIndex].Id] > 0) Debugger.Break();
                    array[worstNeuralNetworkIndex].Mutate(1, 0.1F * 20 * 1 / (Generation / 500)); // mutate
                }
            }

            // unsort, restoring the order of rocket to neural network i.e [x]=id of "x".
            Dictionary<int, NeuralNetwork> unsortedNetworksDictionary = new();

            for (int rocketIndex = 0; rocketIndex < AISettings.s_numberOfRocketsToCreate; rocketIndex++)
            {
                var neuralNetwork = NeuralNetwork.s_networks[rocketIndex];

                unsortedNetworksDictionary[neuralNetwork.Id] = neuralNetwork;
            }

        
            NeuralNetwork.s_networks = unsortedNetworksDictionary;
        }

        /// <summary>
        /// Moves the rockets either using "parallel" or serial. 
        /// </summary>
        private bool MoveRockets()
        {
            s_windStrength = AISettings.s_variableWindStrengthAndDirection ? AISettings.s_maxWindStrength * ((float)RandomNumberGenerator.GetInt32(0, 100000) - 50000f) / 50000f : AISettings.s_maxWindStrength;

            if (WorldSettings.s_useParallelMove)
            {
                // this should run much faster (multi-threading). Particularly good if AI is lots of neurons
                Parallel.ForEach(rockets.Keys, id =>
                {
                    Rocket r = rockets[id];

                    if (!r.IsEliminated)
                    {
                        r.Move();
                        r.DetectElimination();
                    }
                });
            }
            else
            {
                // serial mode, not parallel. Easier to debug rockets, but slower (sequential)
                foreach (var id in rockets.Keys)
                {
                    Rocket r = rockets[id];

                    if (!r.IsEliminated)
                    {
                        r.Move();
                        r.DetectElimination();
                    }
                }
            }

            // we return true if there are rockets not eliminated that can be moved
            foreach (var id in rockets.Keys)
            {
                Rocket r = rockets[id];

                if (!r.IsEliminated && !r.HasLandedSafely) return true;                
            }

            return false;
        }

        /// <summary>
        /// Draws all the rockets on a pre-cached background.
        /// </summary>
        private void DrawRockets(List<int>? best)
        {
            if (Canvas is null) throw new Exception("Canvas must be initialised before use.");

            // we use the pre-drawn image behind each "frame". We draw it once and cache.;
            Bitmap? scenery = WorldBackground.GetScenery();

            if (scenery is null) throw new Exception("Scenery must be initialised before use.");

            Bitmap bitmapCanvas = new(scenery); // start with our pre-drawn image and overlay rockets

            using Graphics g = Graphics.FromImage(bitmapCanvas);

            // quality graphics
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.DrawString("[P]ause | [F]ailure | [S]low | [Q]uiet | [M]utate", new Font("Arial", 8), Brushes.White, 5, 5);

            DrawWindIndicator(g);

            // draw each rocket. "best" is used to circle the best.
            foreach (int id in rockets.Keys)
            {
                Vehicle.UX.RocketRenderer.Draw(rockets[id], g, height, best); // draws a box with velocity arrows and booster flame.
            }

            g.Flush();

            // switch last image with this one.
            Canvas.Image?.Dispose();
            Canvas.Image = bitmapCanvas;
        }

        /// <summary>
        /// Draws the "windicator", showing a line of how strong the wind is (as it can be random).
        /// </summary>
        /// <param name="locOfABM"></param>
        /// <param name="g"></param>
        /// <param name="windStrength"></param>
        private void DrawWindIndicator(Graphics g)
        {
            if (s_windStrength == 0) return;

            float xWind, yWind;

            float angleOfWindInRadians = 0;
            Point pt = new(0, height / 2)
            {
                // wind is from an edge (because it is constant)            
                X = s_windStrength * Math.Cos(angleOfWindInRadians) > 0 ? 0 : width // place on the logical edge depending on direction
            };

            xWind = (float)(130 * s_windStrength * Math.Cos(angleOfWindInRadians) + pt.X);
            yWind = (float)(130 * s_windStrength * Math.Sin(angleOfWindInRadians) + pt.Y);

            g.DrawLine(s_penWindIndicator, pt, new Point((int)(xWind), (int)yWind));
        }
    }
}
