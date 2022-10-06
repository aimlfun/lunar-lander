namespace RocketAI
{
    partial class FormFailures
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonThrustFailureIntermittentThrust = new System.Windows.Forms.RadioButton();
            this.radioButtonThrustFailureReducedThrust = new System.Windows.Forms.RadioButton();
            this.radioButtonThrustFailureNone = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonThrustVectorFailureIntermittentTravel = new System.Windows.Forms.RadioButton();
            this.radioButtonThrustVectorFailureRestrictedTravel = new System.Windows.Forms.RadioButton();
            this.radioButtonThrustVectorFailureNone = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonAltitudeSensorFailureInaccurate = new System.Windows.Forms.RadioButton();
            this.radioButtonAltitudeSensorFailureNoReading = new System.Windows.Forms.RadioButton();
            this.radioButtonAltitudeSensorFailureNone = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonVertVelSensorFailureInaccurate = new System.Windows.Forms.RadioButton();
            this.radioButtonVertVelSensorFailureNoReading = new System.Windows.Forms.RadioButton();
            this.radioButtonVertVelSensorFailureNone = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButtonHorizVelSensorFailureInaccurate = new System.Windows.Forms.RadioButton();
            this.radioButtonHorizVelSensorFailureNoReading = new System.Windows.Forms.RadioButton();
            this.radioButtonHorizVelSensorFailureNone = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonHorizAccelSensorFailureInaccurate = new System.Windows.Forms.RadioButton();
            this.radioButtonHorizAccelSensorFailureNoReading = new System.Windows.Forms.RadioButton();
            this.radioButtonHorizAccelSensorFailureNone = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radioButtonVertAccelSensorFailureInaccurate = new System.Windows.Forms.RadioButton();
            this.radioButtonVertAccelSensorFailureNoReading = new System.Windows.Forms.RadioButton();
            this.radioButtonVertAccelSensorFailureNone = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioButtonAngleSensorFailureInaccurate = new System.Windows.Forms.RadioButton();
            this.radioButtonAngleSensorFailureNoReading = new System.Windows.Forms.RadioButton();
            this.radioButtonAngleSensorFailureNone = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonThrustFailureIntermittentThrust);
            this.groupBox1.Controls.Add(this.radioButtonThrustFailureReducedThrust);
            this.groupBox1.Controls.Add(this.radioButtonThrustFailureNone);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 113);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thrust Failure";
            // 
            // radioButtonThrustFailureIntermittentThrust
            // 
            this.radioButtonThrustFailureIntermittentThrust.AutoSize = true;
            this.radioButtonThrustFailureIntermittentThrust.Location = new System.Drawing.Point(22, 81);
            this.radioButtonThrustFailureIntermittentThrust.Name = "radioButtonThrustFailureIntermittentThrust";
            this.radioButtonThrustFailureIntermittentThrust.Size = new System.Drawing.Size(124, 19);
            this.radioButtonThrustFailureIntermittentThrust.TabIndex = 2;
            this.radioButtonThrustFailureIntermittentThrust.Text = "Intermittent Thrust";
            this.radioButtonThrustFailureIntermittentThrust.UseVisualStyleBackColor = true;
            this.radioButtonThrustFailureIntermittentThrust.CheckedChanged += new System.EventHandler(this.radioButtonThrustFailureIntermittentThrust_CheckedChanged);
            // 
            // radioButtonThrustFailureReducedThrust
            // 
            this.radioButtonThrustFailureReducedThrust.AutoSize = true;
            this.radioButtonThrustFailureReducedThrust.Location = new System.Drawing.Point(22, 56);
            this.radioButtonThrustFailureReducedThrust.Name = "radioButtonThrustFailureReducedThrust";
            this.radioButtonThrustFailureReducedThrust.Size = new System.Drawing.Size(107, 19);
            this.radioButtonThrustFailureReducedThrust.TabIndex = 1;
            this.radioButtonThrustFailureReducedThrust.Text = "Reduced Thrust";
            this.radioButtonThrustFailureReducedThrust.UseVisualStyleBackColor = true;
            this.radioButtonThrustFailureReducedThrust.CheckedChanged += new System.EventHandler(this.radioButtonThrustFailureReducedThrust_CheckedChanged);
            // 
            // radioButtonThrustFailureNone
            // 
            this.radioButtonThrustFailureNone.AutoSize = true;
            this.radioButtonThrustFailureNone.Checked = true;
            this.radioButtonThrustFailureNone.Location = new System.Drawing.Point(22, 31);
            this.radioButtonThrustFailureNone.Name = "radioButtonThrustFailureNone";
            this.radioButtonThrustFailureNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonThrustFailureNone.TabIndex = 0;
            this.radioButtonThrustFailureNone.TabStop = true;
            this.radioButtonThrustFailureNone.Text = "None";
            this.radioButtonThrustFailureNone.UseVisualStyleBackColor = true;
            this.radioButtonThrustFailureNone.CheckedChanged += new System.EventHandler(this.radioButtonThrustFailureNone_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonThrustVectorFailureIntermittentTravel);
            this.groupBox2.Controls.Add(this.radioButtonThrustVectorFailureRestrictedTravel);
            this.groupBox2.Controls.Add(this.radioButtonThrustVectorFailureNone);
            this.groupBox2.Location = new System.Drawing.Point(13, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 109);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thrust Vector Failure";
            // 
            // radioButtonThrustVectorFailureIntermittentTravel
            // 
            this.radioButtonThrustVectorFailureIntermittentTravel.AutoSize = true;
            this.radioButtonThrustVectorFailureIntermittentTravel.Location = new System.Drawing.Point(24, 81);
            this.radioButtonThrustVectorFailureIntermittentTravel.Name = "radioButtonThrustVectorFailureIntermittentTravel";
            this.radioButtonThrustVectorFailureIntermittentTravel.Size = new System.Drawing.Size(121, 19);
            this.radioButtonThrustVectorFailureIntermittentTravel.TabIndex = 2;
            this.radioButtonThrustVectorFailureIntermittentTravel.Text = "Intermittent Travel";
            this.radioButtonThrustVectorFailureIntermittentTravel.UseVisualStyleBackColor = true;
            this.radioButtonThrustVectorFailureIntermittentTravel.CheckedChanged += new System.EventHandler(this.radioButtonThrustVectorFailureIntermittentTravel_CheckedChanged);
            // 
            // radioButtonThrustVectorFailureRestrictedTravel
            // 
            this.radioButtonThrustVectorFailureRestrictedTravel.AutoSize = true;
            this.radioButtonThrustVectorFailureRestrictedTravel.Location = new System.Drawing.Point(24, 56);
            this.radioButtonThrustVectorFailureRestrictedTravel.Name = "radioButtonThrustVectorFailureRestrictedTravel";
            this.radioButtonThrustVectorFailureRestrictedTravel.Size = new System.Drawing.Size(110, 19);
            this.radioButtonThrustVectorFailureRestrictedTravel.TabIndex = 1;
            this.radioButtonThrustVectorFailureRestrictedTravel.Text = "Restricted Travel";
            this.radioButtonThrustVectorFailureRestrictedTravel.UseVisualStyleBackColor = true;
            this.radioButtonThrustVectorFailureRestrictedTravel.CheckedChanged += new System.EventHandler(this.radioButtonThrustVectorFailureRestrictedTravel_CheckedChanged);
            // 
            // radioButtonThrustVectorFailureNone
            // 
            this.radioButtonThrustVectorFailureNone.AutoSize = true;
            this.radioButtonThrustVectorFailureNone.Checked = true;
            this.radioButtonThrustVectorFailureNone.Location = new System.Drawing.Point(24, 31);
            this.radioButtonThrustVectorFailureNone.Name = "radioButtonThrustVectorFailureNone";
            this.radioButtonThrustVectorFailureNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonThrustVectorFailureNone.TabIndex = 0;
            this.radioButtonThrustVectorFailureNone.TabStop = true;
            this.radioButtonThrustVectorFailureNone.Text = "None";
            this.radioButtonThrustVectorFailureNone.UseVisualStyleBackColor = true;
            this.radioButtonThrustVectorFailureNone.CheckedChanged += new System.EventHandler(this.radioButtonThrustVectorFailureNone_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonAltitudeSensorFailureInaccurate);
            this.groupBox3.Controls.Add(this.radioButtonAltitudeSensorFailureNoReading);
            this.groupBox3.Controls.Add(this.radioButtonAltitudeSensorFailureNone);
            this.groupBox3.Location = new System.Drawing.Point(200, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(166, 113);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Altitude Sensor Failure";
            // 
            // radioButtonAltitudeSensorFailureInaccurate
            // 
            this.radioButtonAltitudeSensorFailureInaccurate.AutoSize = true;
            this.radioButtonAltitudeSensorFailureInaccurate.Location = new System.Drawing.Point(24, 81);
            this.radioButtonAltitudeSensorFailureInaccurate.Name = "radioButtonAltitudeSensorFailureInaccurate";
            this.radioButtonAltitudeSensorFailureInaccurate.Size = new System.Drawing.Size(80, 19);
            this.radioButtonAltitudeSensorFailureInaccurate.TabIndex = 2;
            this.radioButtonAltitudeSensorFailureInaccurate.Text = "Inaccurate";
            this.radioButtonAltitudeSensorFailureInaccurate.UseVisualStyleBackColor = true;
            this.radioButtonAltitudeSensorFailureInaccurate.CheckedChanged += new System.EventHandler(this.radioButtonAltitudeSensorFailureInaccurate_CheckedChanged);
            // 
            // radioButtonAltitudeSensorFailureNoReading
            // 
            this.radioButtonAltitudeSensorFailureNoReading.AutoSize = true;
            this.radioButtonAltitudeSensorFailureNoReading.Location = new System.Drawing.Point(24, 56);
            this.radioButtonAltitudeSensorFailureNoReading.Name = "radioButtonAltitudeSensorFailureNoReading";
            this.radioButtonAltitudeSensorFailureNoReading.Size = new System.Drawing.Size(87, 19);
            this.radioButtonAltitudeSensorFailureNoReading.TabIndex = 1;
            this.radioButtonAltitudeSensorFailureNoReading.Text = "No Reading";
            this.radioButtonAltitudeSensorFailureNoReading.UseVisualStyleBackColor = true;
            this.radioButtonAltitudeSensorFailureNoReading.CheckedChanged += new System.EventHandler(this.radioButtonAltitudeSensorFailureNoReading_CheckedChanged);
            // 
            // radioButtonAltitudeSensorFailureNone
            // 
            this.radioButtonAltitudeSensorFailureNone.AutoSize = true;
            this.radioButtonAltitudeSensorFailureNone.Checked = true;
            this.radioButtonAltitudeSensorFailureNone.Location = new System.Drawing.Point(24, 31);
            this.radioButtonAltitudeSensorFailureNone.Name = "radioButtonAltitudeSensorFailureNone";
            this.radioButtonAltitudeSensorFailureNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonAltitudeSensorFailureNone.TabIndex = 0;
            this.radioButtonAltitudeSensorFailureNone.TabStop = true;
            this.radioButtonAltitudeSensorFailureNone.Text = "None";
            this.radioButtonAltitudeSensorFailureNone.UseVisualStyleBackColor = true;
            this.radioButtonAltitudeSensorFailureNone.CheckedChanged += new System.EventHandler(this.radioButtonAltitudeSensorFailureNone_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonVertVelSensorFailureInaccurate);
            this.groupBox4.Controls.Add(this.radioButtonVertVelSensorFailureNoReading);
            this.groupBox4.Controls.Add(this.radioButtonVertVelSensorFailureNone);
            this.groupBox4.Location = new System.Drawing.Point(387, 145);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(182, 109);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Vert. Velocity Sensor Failure";
            // 
            // radioButtonVertVelSensorFailureInaccurate
            // 
            this.radioButtonVertVelSensorFailureInaccurate.AutoSize = true;
            this.radioButtonVertVelSensorFailureInaccurate.Location = new System.Drawing.Point(24, 81);
            this.radioButtonVertVelSensorFailureInaccurate.Name = "radioButtonVertVelSensorFailureInaccurate";
            this.radioButtonVertVelSensorFailureInaccurate.Size = new System.Drawing.Size(80, 19);
            this.radioButtonVertVelSensorFailureInaccurate.TabIndex = 2;
            this.radioButtonVertVelSensorFailureInaccurate.Text = "Inaccurate";
            this.radioButtonVertVelSensorFailureInaccurate.UseVisualStyleBackColor = true;
            this.radioButtonVertVelSensorFailureInaccurate.CheckedChanged += new System.EventHandler(this.radioButtonVertVelSensorFailureInaccurate_CheckedChanged);
            // 
            // radioButtonVertVelSensorFailureNoReading
            // 
            this.radioButtonVertVelSensorFailureNoReading.AutoSize = true;
            this.radioButtonVertVelSensorFailureNoReading.Location = new System.Drawing.Point(24, 56);
            this.radioButtonVertVelSensorFailureNoReading.Name = "radioButtonVertVelSensorFailureNoReading";
            this.radioButtonVertVelSensorFailureNoReading.Size = new System.Drawing.Size(87, 19);
            this.radioButtonVertVelSensorFailureNoReading.TabIndex = 1;
            this.radioButtonVertVelSensorFailureNoReading.Text = "No Reading";
            this.radioButtonVertVelSensorFailureNoReading.UseVisualStyleBackColor = true;
            this.radioButtonVertVelSensorFailureNoReading.CheckedChanged += new System.EventHandler(this.radioButtonVertVelSensorFailureNoReading_CheckedChanged);
            // 
            // radioButtonVertVelSensorFailureNone
            // 
            this.radioButtonVertVelSensorFailureNone.AutoSize = true;
            this.radioButtonVertVelSensorFailureNone.Checked = true;
            this.radioButtonVertVelSensorFailureNone.Location = new System.Drawing.Point(24, 31);
            this.radioButtonVertVelSensorFailureNone.Name = "radioButtonVertVelSensorFailureNone";
            this.radioButtonVertVelSensorFailureNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonVertVelSensorFailureNone.TabIndex = 0;
            this.radioButtonVertVelSensorFailureNone.TabStop = true;
            this.radioButtonVertVelSensorFailureNone.Text = "None";
            this.radioButtonVertVelSensorFailureNone.UseVisualStyleBackColor = true;
            this.radioButtonVertVelSensorFailureNone.CheckedChanged += new System.EventHandler(this.radioButtonVertVelSensorFailureNone_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButtonHorizVelSensorFailureInaccurate);
            this.groupBox5.Controls.Add(this.radioButtonHorizVelSensorFailureNoReading);
            this.groupBox5.Controls.Add(this.radioButtonHorizVelSensorFailureNone);
            this.groupBox5.Location = new System.Drawing.Point(387, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(182, 113);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Horiz. Velocity Sensor Failure";
            // 
            // radioButtonHorizVelSensorFailureInaccurate
            // 
            this.radioButtonHorizVelSensorFailureInaccurate.AutoSize = true;
            this.radioButtonHorizVelSensorFailureInaccurate.Location = new System.Drawing.Point(24, 81);
            this.radioButtonHorizVelSensorFailureInaccurate.Name = "radioButtonHorizVelSensorFailureInaccurate";
            this.radioButtonHorizVelSensorFailureInaccurate.Size = new System.Drawing.Size(80, 19);
            this.radioButtonHorizVelSensorFailureInaccurate.TabIndex = 2;
            this.radioButtonHorizVelSensorFailureInaccurate.Text = "Inaccurate";
            this.radioButtonHorizVelSensorFailureInaccurate.UseVisualStyleBackColor = true;
            this.radioButtonHorizVelSensorFailureInaccurate.CheckedChanged += new System.EventHandler(this.radioButtonHorizVelSensorFailureInaccurate_CheckedChanged);
            // 
            // radioButtonHorizVelSensorFailureNoReading
            // 
            this.radioButtonHorizVelSensorFailureNoReading.AutoSize = true;
            this.radioButtonHorizVelSensorFailureNoReading.Location = new System.Drawing.Point(24, 56);
            this.radioButtonHorizVelSensorFailureNoReading.Name = "radioButtonHorizVelSensorFailureNoReading";
            this.radioButtonHorizVelSensorFailureNoReading.Size = new System.Drawing.Size(87, 19);
            this.radioButtonHorizVelSensorFailureNoReading.TabIndex = 1;
            this.radioButtonHorizVelSensorFailureNoReading.Text = "No Reading";
            this.radioButtonHorizVelSensorFailureNoReading.UseVisualStyleBackColor = true;
            this.radioButtonHorizVelSensorFailureNoReading.CheckedChanged += new System.EventHandler(this.radioButtonHorizVelSensorFailureNoReading_CheckedChanged);
            // 
            // radioButtonHorizVelSensorFailureNone
            // 
            this.radioButtonHorizVelSensorFailureNone.AutoSize = true;
            this.radioButtonHorizVelSensorFailureNone.Checked = true;
            this.radioButtonHorizVelSensorFailureNone.Location = new System.Drawing.Point(24, 31);
            this.radioButtonHorizVelSensorFailureNone.Name = "radioButtonHorizVelSensorFailureNone";
            this.radioButtonHorizVelSensorFailureNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonHorizVelSensorFailureNone.TabIndex = 0;
            this.radioButtonHorizVelSensorFailureNone.TabStop = true;
            this.radioButtonHorizVelSensorFailureNone.Text = "None";
            this.radioButtonHorizVelSensorFailureNone.UseVisualStyleBackColor = true;
            this.radioButtonHorizVelSensorFailureNone.CheckedChanged += new System.EventHandler(this.radioButtonHorizVelSensorFailureNone_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButtonHorizAccelSensorFailureInaccurate);
            this.groupBox6.Controls.Add(this.radioButtonHorizAccelSensorFailureNoReading);
            this.groupBox6.Controls.Add(this.radioButtonHorizAccelSensorFailureNone);
            this.groupBox6.Location = new System.Drawing.Point(590, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(182, 113);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Horiz. Accel Sensor Failure";
            // 
            // radioButtonHorizAccelSensorFailureInaccurate
            // 
            this.radioButtonHorizAccelSensorFailureInaccurate.AutoSize = true;
            this.radioButtonHorizAccelSensorFailureInaccurate.Location = new System.Drawing.Point(24, 81);
            this.radioButtonHorizAccelSensorFailureInaccurate.Name = "radioButtonHorizAccelSensorFailureInaccurate";
            this.radioButtonHorizAccelSensorFailureInaccurate.Size = new System.Drawing.Size(80, 19);
            this.radioButtonHorizAccelSensorFailureInaccurate.TabIndex = 2;
            this.radioButtonHorizAccelSensorFailureInaccurate.Text = "Inaccurate";
            this.radioButtonHorizAccelSensorFailureInaccurate.UseVisualStyleBackColor = true;
            this.radioButtonHorizAccelSensorFailureInaccurate.CheckedChanged += new System.EventHandler(this.radioButtonHorizAccelSensorFailureInaccurate_CheckedChanged);
            // 
            // radioButtonHorizAccelSensorFailureNoReading
            // 
            this.radioButtonHorizAccelSensorFailureNoReading.AutoSize = true;
            this.radioButtonHorizAccelSensorFailureNoReading.Location = new System.Drawing.Point(24, 56);
            this.radioButtonHorizAccelSensorFailureNoReading.Name = "radioButtonHorizAccelSensorFailureNoReading";
            this.radioButtonHorizAccelSensorFailureNoReading.Size = new System.Drawing.Size(87, 19);
            this.radioButtonHorizAccelSensorFailureNoReading.TabIndex = 1;
            this.radioButtonHorizAccelSensorFailureNoReading.Text = "No Reading";
            this.radioButtonHorizAccelSensorFailureNoReading.UseVisualStyleBackColor = true;
            this.radioButtonHorizAccelSensorFailureNoReading.CheckedChanged += new System.EventHandler(this.radioButtonHorizAccelSensorFailureNoReading_CheckedChanged);
            // 
            // radioButtonHorizAccelSensorFailureNone
            // 
            this.radioButtonHorizAccelSensorFailureNone.AutoSize = true;
            this.radioButtonHorizAccelSensorFailureNone.Checked = true;
            this.radioButtonHorizAccelSensorFailureNone.Location = new System.Drawing.Point(24, 31);
            this.radioButtonHorizAccelSensorFailureNone.Name = "radioButtonHorizAccelSensorFailureNone";
            this.radioButtonHorizAccelSensorFailureNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonHorizAccelSensorFailureNone.TabIndex = 0;
            this.radioButtonHorizAccelSensorFailureNone.TabStop = true;
            this.radioButtonHorizAccelSensorFailureNone.Text = "None";
            this.radioButtonHorizAccelSensorFailureNone.UseVisualStyleBackColor = true;
            this.radioButtonHorizAccelSensorFailureNone.CheckedChanged += new System.EventHandler(this.radioButtonHorizAccelSensorFailureNone_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.radioButtonVertAccelSensorFailureInaccurate);
            this.groupBox7.Controls.Add(this.radioButtonVertAccelSensorFailureNoReading);
            this.groupBox7.Controls.Add(this.radioButtonVertAccelSensorFailureNone);
            this.groupBox7.Location = new System.Drawing.Point(590, 145);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(182, 109);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Vert. Accel Sensor Failure";
            // 
            // radioButtonVertAccelSensorFailureInaccurate
            // 
            this.radioButtonVertAccelSensorFailureInaccurate.AutoSize = true;
            this.radioButtonVertAccelSensorFailureInaccurate.Location = new System.Drawing.Point(24, 81);
            this.radioButtonVertAccelSensorFailureInaccurate.Name = "radioButtonVertAccelSensorFailureInaccurate";
            this.radioButtonVertAccelSensorFailureInaccurate.Size = new System.Drawing.Size(80, 19);
            this.radioButtonVertAccelSensorFailureInaccurate.TabIndex = 2;
            this.radioButtonVertAccelSensorFailureInaccurate.Text = "Inaccurate";
            this.radioButtonVertAccelSensorFailureInaccurate.UseVisualStyleBackColor = true;
            this.radioButtonVertAccelSensorFailureInaccurate.CheckedChanged += new System.EventHandler(this.radioButtonVertAccelSensorFailureInaccurate_CheckedChanged);
            // 
            // radioButtonVertAccelSensorFailureNoReading
            // 
            this.radioButtonVertAccelSensorFailureNoReading.AutoSize = true;
            this.radioButtonVertAccelSensorFailureNoReading.Location = new System.Drawing.Point(24, 56);
            this.radioButtonVertAccelSensorFailureNoReading.Name = "radioButtonVertAccelSensorFailureNoReading";
            this.radioButtonVertAccelSensorFailureNoReading.Size = new System.Drawing.Size(87, 19);
            this.radioButtonVertAccelSensorFailureNoReading.TabIndex = 1;
            this.radioButtonVertAccelSensorFailureNoReading.Text = "No Reading";
            this.radioButtonVertAccelSensorFailureNoReading.UseVisualStyleBackColor = true;
            this.radioButtonVertAccelSensorFailureNoReading.CheckedChanged += new System.EventHandler(this.radioButtonVertAccelSensorFailureNoReading_CheckedChanged);
            // 
            // radioButtonVertAccelSensorFailureNone
            // 
            this.radioButtonVertAccelSensorFailureNone.AutoSize = true;
            this.radioButtonVertAccelSensorFailureNone.Checked = true;
            this.radioButtonVertAccelSensorFailureNone.Location = new System.Drawing.Point(24, 31);
            this.radioButtonVertAccelSensorFailureNone.Name = "radioButtonVertAccelSensorFailureNone";
            this.radioButtonVertAccelSensorFailureNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonVertAccelSensorFailureNone.TabIndex = 0;
            this.radioButtonVertAccelSensorFailureNone.TabStop = true;
            this.radioButtonVertAccelSensorFailureNone.Text = "None";
            this.radioButtonVertAccelSensorFailureNone.UseVisualStyleBackColor = true;
            this.radioButtonVertAccelSensorFailureNone.CheckedChanged += new System.EventHandler(this.radioButtonVertAccelSensorFailureNone_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButtonAngleSensorFailureInaccurate);
            this.groupBox8.Controls.Add(this.radioButtonAngleSensorFailureNoReading);
            this.groupBox8.Controls.Add(this.radioButtonAngleSensorFailureNone);
            this.groupBox8.Location = new System.Drawing.Point(200, 145);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(166, 113);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Angle Sensor Failure";
            // 
            // radioButtonAngleSensorFailureInaccurate
            // 
            this.radioButtonAngleSensorFailureInaccurate.AutoSize = true;
            this.radioButtonAngleSensorFailureInaccurate.Location = new System.Drawing.Point(24, 81);
            this.radioButtonAngleSensorFailureInaccurate.Name = "radioButtonAngleSensorFailureInaccurate";
            this.radioButtonAngleSensorFailureInaccurate.Size = new System.Drawing.Size(80, 19);
            this.radioButtonAngleSensorFailureInaccurate.TabIndex = 2;
            this.radioButtonAngleSensorFailureInaccurate.Text = "Inaccurate";
            this.radioButtonAngleSensorFailureInaccurate.UseVisualStyleBackColor = true;
            this.radioButtonAngleSensorFailureInaccurate.CheckedChanged += new System.EventHandler(this.radioButtonAngleSensorFailureInaccurate_CheckedChanged);
            // 
            // radioButtonAngleSensorFailureNoReading
            // 
            this.radioButtonAngleSensorFailureNoReading.AutoSize = true;
            this.radioButtonAngleSensorFailureNoReading.Location = new System.Drawing.Point(24, 56);
            this.radioButtonAngleSensorFailureNoReading.Name = "radioButtonAngleSensorFailureNoReading";
            this.radioButtonAngleSensorFailureNoReading.Size = new System.Drawing.Size(87, 19);
            this.radioButtonAngleSensorFailureNoReading.TabIndex = 1;
            this.radioButtonAngleSensorFailureNoReading.Text = "No Reading";
            this.radioButtonAngleSensorFailureNoReading.UseVisualStyleBackColor = true;
            this.radioButtonAngleSensorFailureNoReading.CheckedChanged += new System.EventHandler(this.radioButtonAngleSensorFailureNoReading_CheckedChanged);
            // 
            // radioButtonAngleSensorFailureNone
            // 
            this.radioButtonAngleSensorFailureNone.AutoSize = true;
            this.radioButtonAngleSensorFailureNone.Checked = true;
            this.radioButtonAngleSensorFailureNone.Location = new System.Drawing.Point(24, 31);
            this.radioButtonAngleSensorFailureNone.Name = "radioButtonAngleSensorFailureNone";
            this.radioButtonAngleSensorFailureNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonAngleSensorFailureNone.TabIndex = 0;
            this.radioButtonAngleSensorFailureNone.TabStop = true;
            this.radioButtonAngleSensorFailureNone.Text = "None";
            this.radioButtonAngleSensorFailureNone.UseVisualStyleBackColor = true;
            this.radioButtonAngleSensorFailureNone.CheckedChanged += new System.EventHandler(this.radioButtonAngleSensorFailureNone_CheckedChanged);
            // 
            // FormFailures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 268);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFailures";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Failure Control Panel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton radioButtonThrustFailureIntermittentThrust;
        private RadioButton radioButtonThrustFailureReducedThrust;
        private RadioButton radioButtonThrustFailureNone;
        private GroupBox groupBox2;
        private RadioButton radioButtonThrustVectorFailureIntermittentTravel;
        private RadioButton radioButtonThrustVectorFailureRestrictedTravel;
        private RadioButton radioButtonThrustVectorFailureNone;
        private GroupBox groupBox3;
        private RadioButton radioButtonAltitudeSensorFailureInaccurate;
        private RadioButton radioButtonAltitudeSensorFailureNoReading;
        private RadioButton radioButtonAltitudeSensorFailureNone;
        private GroupBox groupBox4;
        private RadioButton radioButtonVertVelSensorFailureInaccurate;
        private RadioButton radioButtonVertVelSensorFailureNoReading;
        private RadioButton radioButtonVertVelSensorFailureNone;
        private GroupBox groupBox5;
        private RadioButton radioButtonHorizVelSensorFailureInaccurate;
        private RadioButton radioButtonHorizVelSensorFailureNoReading;
        private RadioButton radioButtonHorizVelSensorFailureNone;
        private GroupBox groupBox6;
        private RadioButton radioButtonHorizAccelSensorFailureInaccurate;
        private RadioButton radioButtonHorizAccelSensorFailureNoReading;
        private RadioButton radioButtonHorizAccelSensorFailureNone;
        private GroupBox groupBox7;
        private RadioButton radioButtonVertAccelSensorFailureInaccurate;
        private RadioButton radioButtonVertAccelSensorFailureNoReading;
        private RadioButton radioButtonVertAccelSensorFailureNone;
        private GroupBox groupBox8;
        private RadioButton radioButtonAngleSensorFailureInaccurate;
        private RadioButton radioButtonAngleSensorFailureNoReading;
        private RadioButton radioButtonAngleSensorFailureNone;
    }
}