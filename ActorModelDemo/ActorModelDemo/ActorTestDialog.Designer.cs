namespace ActorModelDemo
{
    partial class ActorTestDialog
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
            this.components = new System.ComponentModel.Container();
            this.c_ButtonDisableTimer = new System.Windows.Forms.Button();
            this.c_ButtonEnableTimer = new System.Windows.Forms.Button();
            this.c_ButtonStopFile = new System.Windows.Forms.Button();
            this.c_ButtonStartFile = new System.Windows.Forms.Button();
            this.c_TextBoxResults = new System.Windows.Forms.TextBox();
            this.c_TimerMessageTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // c_ButtonDisableTimer
            // 
            this.c_ButtonDisableTimer.AutoSize = true;
            this.c_ButtonDisableTimer.Enabled = false;
            this.c_ButtonDisableTimer.Location = new System.Drawing.Point(153, 25);
            this.c_ButtonDisableTimer.Name = "c_ButtonDisableTimer";
            this.c_ButtonDisableTimer.Size = new System.Drawing.Size(117, 33);
            this.c_ButtonDisableTimer.TabIndex = 9;
            this.c_ButtonDisableTimer.Text = "Stop Timer";
            this.c_ButtonDisableTimer.UseVisualStyleBackColor = true;
            this.c_ButtonDisableTimer.Click += new System.EventHandler(this.c_ButtonDisableTimer_Click);
            // 
            // c_ButtonEnableTimer
            // 
            this.c_ButtonEnableTimer.AutoSize = true;
            this.c_ButtonEnableTimer.Location = new System.Drawing.Point(15, 25);
            this.c_ButtonEnableTimer.Name = "c_ButtonEnableTimer";
            this.c_ButtonEnableTimer.Size = new System.Drawing.Size(114, 33);
            this.c_ButtonEnableTimer.TabIndex = 8;
            this.c_ButtonEnableTimer.Text = "Start Timer";
            this.c_ButtonEnableTimer.UseVisualStyleBackColor = true;
            this.c_ButtonEnableTimer.Click += new System.EventHandler(this.c_ButtonEnableTimer_Click);
            // 
            // c_ButtonStopFile
            // 
            this.c_ButtonStopFile.AutoSize = true;
            this.c_ButtonStopFile.Enabled = false;
            this.c_ButtonStopFile.Location = new System.Drawing.Point(153, 90);
            this.c_ButtonStopFile.Name = "c_ButtonStopFile";
            this.c_ButtonStopFile.Size = new System.Drawing.Size(99, 36);
            this.c_ButtonStopFile.TabIndex = 7;
            this.c_ButtonStopFile.Text = "Stop File";
            this.c_ButtonStopFile.UseVisualStyleBackColor = true;
            this.c_ButtonStopFile.Click += new System.EventHandler(this.c_ButtonStopFile_Click);
            // 
            // c_ButtonStartFile
            // 
            this.c_ButtonStartFile.AutoSize = true;
            this.c_ButtonStartFile.Enabled = false;
            this.c_ButtonStartFile.Location = new System.Drawing.Point(15, 90);
            this.c_ButtonStartFile.Name = "c_ButtonStartFile";
            this.c_ButtonStartFile.Size = new System.Drawing.Size(99, 36);
            this.c_ButtonStartFile.TabIndex = 6;
            this.c_ButtonStartFile.Text = "Start File";
            this.c_ButtonStartFile.UseVisualStyleBackColor = true;
            this.c_ButtonStartFile.Click += new System.EventHandler(this.c_ButtonStartFile_Click);
            // 
            // c_TextBoxResults
            // 
            this.c_TextBoxResults.Location = new System.Drawing.Point(15, 151);
            this.c_TextBoxResults.Multiline = true;
            this.c_TextBoxResults.Name = "c_TextBoxResults";
            this.c_TextBoxResults.Size = new System.Drawing.Size(255, 89);
            this.c_TextBoxResults.TabIndex = 5;
            // 
            // c_TimerMessageTimer
            // 
            this.c_TimerMessageTimer.Tick += new System.EventHandler(this.c_TimerMessageTimer_Tick);
            // 
            // ActorTestDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 266);
            this.Controls.Add(this.c_ButtonDisableTimer);
            this.Controls.Add(this.c_ButtonEnableTimer);
            this.Controls.Add(this.c_ButtonStopFile);
            this.Controls.Add(this.c_ButtonStartFile);
            this.Controls.Add(this.c_TextBoxResults);
            this.Name = "ActorTestDialog";
            this.Text = "ActorTestDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button c_ButtonDisableTimer;
        private System.Windows.Forms.Button c_ButtonEnableTimer;
        private System.Windows.Forms.Button c_ButtonStopFile;
        private System.Windows.Forms.Button c_ButtonStartFile;
        private System.Windows.Forms.TextBox c_TextBoxResults;
        private System.Windows.Forms.Timer c_TimerMessageTimer;
    }
}