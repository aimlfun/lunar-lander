using RocketAI.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketAI
{
    public partial class FormFailures : Form
    {
        public FormFailures()
        {
            InitializeComponent();

        }

        private void SetValue(string setting, int value)
        {
            // if not set already, set it
            if (!AISettings.s_failures.ContainsKey(setting))
            {
                AISettings.s_failures.Add(setting, value);
            }
            else
            {
                // update it
                AISettings.s_failures[setting] = value;
            }
        }

        private void radioButtonThrustFailureIntermittentThrust_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("burnForce", 2);
        }

        private void radioButtonThrustFailureReducedThrust_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("burnForce", 1);
        }

        private void radioButtonThrustFailureNone_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("burnForce", 0);
        }

        private void radioButtonThrustVectorFailureNone_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("offsetAngleOfThrustInDegrees", 0);
        }

        private void radioButtonThrustVectorFailureRestrictedTravel_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("offsetAngleOfThrustInDegrees", 1);
        }

        private void radioButtonThrustVectorFailureIntermittentTravel_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("offsetAngleOfThrustInDegrees", 2);
        }

        private void radioButtonAltitudeSensorFailureInaccurate_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlFeedHeight", 2);
        }

        private void radioButtonAltitudeSensorFailureNoReading_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlFeedHeight", 1);
        }

        private void radioButtonAltitudeSensorFailureNone_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlFeedHeight", 0);
        }

        private void radioButtonAngleSensorFailureInaccurate_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlFeedAngleOfRocket", 2);
        }

        private void radioButtonAngleSensorFailureNoReading_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlFeedAngleOfRocket", 1);
        }

        private void radioButtonAngleSensorFailureNone_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlFeedAngleOfRocket", 0);
        }

        private void radioButtonHorizVelSensorFailureInaccurate_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlLateralVelocity", 2);
        }

        private void radioButtonHorizVelSensorFailureNoReading_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlLateralVelocity", 1);
        }

        private void radioButtonHorizVelSensorFailureNone_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlLateralVelocity", 0);
        }

        private void radioButtonVertVelSensorFailureInaccurate_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlVerticalVelocity", 2);
        }

        private void radioButtonVertVelSensorFailureNoReading_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlVerticalVelocity", 1);
        }

        private void radioButtonVertVelSensorFailureNone_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlVerticalVelocity", 0);
        }

        private void radioButtonHorizAccelSensorFailureInaccurate_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlLateralAcceleration", 2);
        }

        private void radioButtonHorizAccelSensorFailureNoReading_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlLateralAcceleration", 1);
        }

        private void radioButtonHorizAccelSensorFailureNone_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlLateralAcceleration", 0);
        }

        private void radioButtonVertAccelSensorFailureNone_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlVerticalAcceleration", 0);
        }

        private void radioButtonVertAccelSensorFailureNoReading_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlVerticalAcceleration", 1);
        }

        private void radioButtonVertAccelSensorFailureInaccurate_CheckedChanged(object sender, EventArgs e)
        {
            SetValue("controlVerticalAcceleration", 2);
        }
    }
}
