using System.Windows.Forms;
using System.Drawing;

namespace AudioRecorderForms
{
    partial class AudioRecorderForm
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
            startButton = new Button();
            stopButton = new Button();
            richTextBox1 = new RichTextBox();
            translateButton = new Button();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(12, 12);
            startButton.Name = "startButton";
            startButton.Size = new Size(164, 23);
            startButton.TabIndex = 0;
            startButton.Text = "Start Recording";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(182, 12);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(193, 23);
            stopButton.TabIndex = 1;
            stopButton.Text = "Stop Recording";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 41);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(776, 397);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // translateButton
            // 
            translateButton.Location = new Point(381, 12);
            translateButton.Name = "translateButton";
            translateButton.Size = new Size(171, 23);
            translateButton.TabIndex = 3;
            translateButton.Text = "Traduzir";
            translateButton.UseVisualStyleBackColor = true;
            translateButton.Click += translateButton_Click;
            // 
            // AudioRecorderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(translateButton);
            Controls.Add(richTextBox1);
            Controls.Add(stopButton);
            Controls.Add(startButton);
            Name = "AudioRecorderForm";
            Text = "AudioRecorderForm";
            ResumeLayout(false);
        }

        #endregion

        private Button startButton;
        private Button stopButton;
        private RichTextBox richTextBox1;
        private Button translateButton;
    }
}