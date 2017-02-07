namespace smashgg_to_liquipedia
{
    partial class Form1
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
            this.richTextBoxEntrants = new System.Windows.Forms.RichTextBox();
            this.buttonGetBracket = new System.Windows.Forms.Button();
            this.richTextBoxLpOutput = new System.Windows.Forms.RichTextBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // richTextBoxEntrants
            // 
            this.richTextBoxEntrants.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEntrants.Location = new System.Drawing.Point(12, 15);
            this.richTextBoxEntrants.Name = "richTextBoxEntrants";
            this.richTextBoxEntrants.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxEntrants.Size = new System.Drawing.Size(691, 86);
            this.richTextBoxEntrants.TabIndex = 0;
            this.richTextBoxEntrants.Text = "";
            // 
            // buttonGetBracket
            // 
            this.buttonGetBracket.Location = new System.Drawing.Point(709, 107);
            this.buttonGetBracket.Name = "buttonGetBracket";
            this.buttonGetBracket.Size = new System.Drawing.Size(75, 23);
            this.buttonGetBracket.TabIndex = 2;
            this.buttonGetBracket.Text = "Get Bracket";
            this.buttonGetBracket.UseVisualStyleBackColor = true;
            this.buttonGetBracket.Click += new System.EventHandler(this.buttonGetBracket_Click);
            // 
            // richTextBoxLpOutput
            // 
            this.richTextBoxLpOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLpOutput.Location = new System.Drawing.Point(12, 369);
            this.richTextBoxLpOutput.Name = "richTextBoxLpOutput";
            this.richTextBoxLpOutput.Size = new System.Drawing.Size(691, 99);
            this.richTextBoxLpOutput.TabIndex = 0;
            this.richTextBoxLpOutput.Text = "";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(709, 368);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(225, 161);
            this.richTextBoxLog.TabIndex = 4;
            this.richTextBoxLog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(706, 348);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Log:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(709, 535);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(225, 23);
            this.progressBar.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 566);
            this.Controls.Add(this.buttonGetBracket);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.richTextBoxLpOutput);
            this.Controls.Add(this.richTextBoxEntrants);
            this.Name = "Form1";
            this.Text = "Smash.gg to Liquipedia";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxEntrants;
        private System.Windows.Forms.Button buttonGetBracket;
        private System.Windows.Forms.RichTextBox richTextBoxLpOutput;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

