using RocketAI.AI;
using RocketAI.Simulation;
using RocketAI.UX;
using System.Diagnostics;
using System.Security.Cryptography;

namespace RocketAI
{
    public partial class MainForm : Form
    {
        RocketSimulator? rocketSimulator;

        FormFailures? formFailures;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On load, we show the screen paint the background and ignite the rockets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Show(); // maximises screen so width/height are correct

            // creates the background scenery
            WorldBackground.Draw(CanvasImage.Width, CanvasImage.Height);

#pragma warning disable CS8604 // Possible null reference argument. Scenery is always populated, just not in the constructor.
            Bitmap bitmapCanvas = new(WorldBackground.GetScenery());
#pragma warning restore CS8604 // Possible null reference argument.
            CanvasImage.Image = bitmapCanvas;

            // create an emulator for the rockets.
            rocketSimulator = new RocketSimulator(CanvasImage);

            // starts the "learning"
            rocketSimulator.IgniteRockets();
        }
                  
        /// <summary>
        /// Ensures the rockets stop even when in quiet mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RocketSimulator.StopImmediately();
        }

        /// <summary>
        /// Handle keys that control the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                    // "Q" quiet mode (learns quicker)
                    RocketSimulator.ToggleQuietMode();
                    break;

                case Keys.P:
                    // "P" pauses the timer (and what's happening)
                    rocketSimulator?.PauseUnpause();
                    break;

                case Keys.S:
                    // "S" rotate thru slow modes (fps)
                    rocketSimulator?.StepThroughSpeeds();
                    break;

                case Keys.F:
                    // "F" show failure panel
                    PopupTheFailureControlPanel();
                    break;

                case Keys.M:
                    // "M" forces mutation of the neural networks.
                    rocketSimulator?.ForceMutate();
                    break;
            }
        }

        /// <summary>
        /// Show a modal dialog to set/reset failures.
        /// </summary>
        private void PopupTheFailureControlPanel()
        {
            formFailures ??= new();
            
            formFailures.ShowDialog();
            
            formFailures.Hide();
        }
    }
}