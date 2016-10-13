﻿namespace smashgg_api
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
            this.textBoxURLSingles = new System.Windows.Forms.TextBox();
            this.buttonSingles = new System.Windows.Forms.Button();
            this.buttonPhaseSingles = new System.Windows.Forms.Button();
            this.radioButtonBracketSingles = new System.Windows.Forms.RadioButton();
            this.radioButtonRRSingles = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownAdvanceSingles = new System.Windows.Forms.NumericUpDown();
            this.richTextBoxWinners = new System.Windows.Forms.RichTextBox();
            this.richTextBoxLosers = new System.Windows.Forms.RichTextBox();
            this.richTextBoxLpOutput = new System.Windows.Forms.RichTextBox();
            this.buttonFill = new System.Windows.Forms.Button();
            this.buttonFillDoubles = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxWinnersSingles = new System.Windows.Forms.CheckBox();
            this.checkBoxLosersSingles = new System.Windows.Forms.CheckBox();
            this.checkBoxFillUnfinishedSingles = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSingles = new System.Windows.Forms.TabPage();
            this.buttonRegexReplace = new System.Windows.Forms.Button();
            this.tabPageDoubles = new System.Windows.Forms.TabPage();
            this.numericUpDownAdvanceDoubles = new System.Windows.Forms.NumericUpDown();
            this.radioButtonRRDoubles = new System.Windows.Forms.RadioButton();
            this.radioButtonBracketDoubles = new System.Windows.Forms.RadioButton();
            this.buttonPhaseDoubles = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxFillUnfinishedDoubles = new System.Windows.Forms.CheckBox();
            this.checkBoxLosersDoubles = new System.Windows.Forms.CheckBox();
            this.checkBoxWinnersDoubles = new System.Windows.Forms.CheckBox();
            this.buttonDoubles = new System.Windows.Forms.Button();
            this.textBoxURLDoubles = new System.Windows.Forms.TextBox();
            this.numericUpDownWinnersStart = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownWinnersEnd = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownWinnersOffset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownLosersStart = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDownLosersOffset = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownLosersEnd = new System.Windows.Forms.NumericUpDown();
            this.checkBoxLockWinners = new System.Windows.Forms.CheckBox();
            this.checkBoxLockLosers = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.checkBoxGuessFinal = new System.Windows.Forms.CheckBox();
            this.richTextBoxExRegexReplace = new RichTextBoxEx();
            this.richTextBoxExRegexFind = new RichTextBoxEx();
            this.richTextBoxExLpWinnersBracket = new RichTextBoxEx();
            this.richTextBoxExLpLosersBracket = new RichTextBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdvanceSingles)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageSingles.SuspendLayout();
            this.tabPageDoubles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdvanceDoubles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWinnersStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWinnersEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWinnersOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLosersStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLosersOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLosersEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxEntrants
            // 
            this.richTextBoxEntrants.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEntrants.Location = new System.Drawing.Point(12, 15);
            this.richTextBoxEntrants.Name = "richTextBoxEntrants";
            this.richTextBoxEntrants.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxEntrants.Size = new System.Drawing.Size(691, 130);
            this.richTextBoxEntrants.TabIndex = 0;
            this.richTextBoxEntrants.Text = "";
            // 
            // textBoxURLSingles
            // 
            this.textBoxURLSingles.Location = new System.Drawing.Point(3, 3);
            this.textBoxURLSingles.Name = "textBoxURLSingles";
            this.textBoxURLSingles.Size = new System.Drawing.Size(187, 20);
            this.textBoxURLSingles.TabIndex = 1;
            this.textBoxURLSingles.Enter += new System.EventHandler(this.textBoxURL_Enter);
            // 
            // buttonSingles
            // 
            this.buttonSingles.Location = new System.Drawing.Point(3, 29);
            this.buttonSingles.Name = "buttonSingles";
            this.buttonSingles.Size = new System.Drawing.Size(75, 23);
            this.buttonSingles.TabIndex = 2;
            this.buttonSingles.Text = "Get Singles";
            this.buttonSingles.UseVisualStyleBackColor = true;
            this.buttonSingles.Click += new System.EventHandler(this.buttonSingles_Click);
            // 
            // buttonPhaseSingles
            // 
            this.buttonPhaseSingles.Location = new System.Drawing.Point(115, 29);
            this.buttonPhaseSingles.Name = "buttonPhaseSingles";
            this.buttonPhaseSingles.Size = new System.Drawing.Size(75, 23);
            this.buttonPhaseSingles.TabIndex = 2;
            this.buttonPhaseSingles.Text = "Get Phase";
            this.buttonPhaseSingles.UseVisualStyleBackColor = true;
            this.buttonPhaseSingles.Click += new System.EventHandler(this.buttonPhaseSingles_Click);
            // 
            // radioButtonBracketSingles
            // 
            this.radioButtonBracketSingles.AutoSize = true;
            this.radioButtonBracketSingles.Checked = true;
            this.radioButtonBracketSingles.Location = new System.Drawing.Point(115, 59);
            this.radioButtonBracketSingles.Name = "radioButtonBracketSingles";
            this.radioButtonBracketSingles.Size = new System.Drawing.Size(91, 17);
            this.radioButtonBracketSingles.TabIndex = 8;
            this.radioButtonBracketSingles.TabStop = true;
            this.radioButtonBracketSingles.Text = "Bracket Pools";
            this.radioButtonBracketSingles.UseVisualStyleBackColor = true;
            // 
            // radioButtonRRSingles
            // 
            this.radioButtonRRSingles.AutoSize = true;
            this.radioButtonRRSingles.Location = new System.Drawing.Point(115, 82);
            this.radioButtonRRSingles.Name = "radioButtonRRSingles";
            this.radioButtonRRSingles.Size = new System.Drawing.Size(88, 17);
            this.radioButtonRRSingles.TabIndex = 9;
            this.radioButtonRRSingles.Text = "Round Robin";
            this.radioButtonRRSingles.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Advance";
            // 
            // numericUpDownAdvanceSingles
            // 
            this.numericUpDownAdvanceSingles.Location = new System.Drawing.Point(115, 105);
            this.numericUpDownAdvanceSingles.Name = "numericUpDownAdvanceSingles";
            this.numericUpDownAdvanceSingles.Size = new System.Drawing.Size(32, 20);
            this.numericUpDownAdvanceSingles.TabIndex = 10;
            this.numericUpDownAdvanceSingles.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // richTextBoxWinners
            // 
            this.richTextBoxWinners.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxWinners.Location = new System.Drawing.Point(12, 210);
            this.richTextBoxWinners.Name = "richTextBoxWinners";
            this.richTextBoxWinners.Size = new System.Drawing.Size(342, 151);
            this.richTextBoxWinners.TabIndex = 0;
            this.richTextBoxWinners.Text = "";
            // 
            // richTextBoxLosers
            // 
            this.richTextBoxLosers.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLosers.Location = new System.Drawing.Point(361, 210);
            this.richTextBoxLosers.Name = "richTextBoxLosers";
            this.richTextBoxLosers.Size = new System.Drawing.Size(342, 151);
            this.richTextBoxLosers.TabIndex = 0;
            this.richTextBoxLosers.Text = "";
            // 
            // richTextBoxLpOutput
            // 
            this.richTextBoxLpOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLpOutput.Location = new System.Drawing.Point(12, 524);
            this.richTextBoxLpOutput.Name = "richTextBoxLpOutput";
            this.richTextBoxLpOutput.Size = new System.Drawing.Size(691, 154);
            this.richTextBoxLpOutput.TabIndex = 0;
            this.richTextBoxLpOutput.Text = "";
            // 
            // buttonFill
            // 
            this.buttonFill.Location = new System.Drawing.Point(3, 276);
            this.buttonFill.Name = "buttonFill";
            this.buttonFill.Size = new System.Drawing.Size(75, 23);
            this.buttonFill.TabIndex = 2;
            this.buttonFill.Text = "Fill Bracket";
            this.buttonFill.UseVisualStyleBackColor = true;
            this.buttonFill.Click += new System.EventHandler(this.buttonFillSingles_Click);
            // 
            // buttonFillDoubles
            // 
            this.buttonFillDoubles.Location = new System.Drawing.Point(3, 276);
            this.buttonFillDoubles.Name = "buttonFillDoubles";
            this.buttonFillDoubles.Size = new System.Drawing.Size(75, 23);
            this.buttonFillDoubles.TabIndex = 2;
            this.buttonFillDoubles.Text = "Fill Doubles";
            this.buttonFillDoubles.UseVisualStyleBackColor = true;
            this.buttonFillDoubles.Click += new System.EventHandler(this.buttonFillDoubles_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(709, 368);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(225, 281);
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
            // checkBoxWinnersSingles
            // 
            this.checkBoxWinnersSingles.AutoSize = true;
            this.checkBoxWinnersSingles.Location = new System.Drawing.Point(3, 207);
            this.checkBoxWinnersSingles.Name = "checkBoxWinnersSingles";
            this.checkBoxWinnersSingles.Size = new System.Drawing.Size(65, 17);
            this.checkBoxWinnersSingles.TabIndex = 7;
            this.checkBoxWinnersSingles.Text = "Winners";
            this.checkBoxWinnersSingles.UseVisualStyleBackColor = true;
            // 
            // checkBoxLosersSingles
            // 
            this.checkBoxLosersSingles.AutoSize = true;
            this.checkBoxLosersSingles.Location = new System.Drawing.Point(3, 230);
            this.checkBoxLosersSingles.Name = "checkBoxLosersSingles";
            this.checkBoxLosersSingles.Size = new System.Drawing.Size(57, 17);
            this.checkBoxLosersSingles.TabIndex = 7;
            this.checkBoxLosersSingles.Text = "Losers";
            this.checkBoxLosersSingles.UseVisualStyleBackColor = true;
            // 
            // checkBoxFillUnfinishedSingles
            // 
            this.checkBoxFillUnfinishedSingles.AutoSize = true;
            this.checkBoxFillUnfinishedSingles.Location = new System.Drawing.Point(3, 253);
            this.checkBoxFillUnfinishedSingles.Name = "checkBoxFillUnfinishedSingles";
            this.checkBoxFillUnfinishedSingles.Size = new System.Drawing.Size(111, 17);
            this.checkBoxFillUnfinishedSingles.TabIndex = 7;
            this.checkBoxFillUnfinishedSingles.Text = "Fill unfinished sets";
            this.checkBoxFillUnfinishedSingles.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSingles);
            this.tabControl1.Controls.Add(this.tabPageDoubles);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(709, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(229, 349);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageSingles
            // 
            this.tabPageSingles.Controls.Add(this.textBoxURLSingles);
            this.tabPageSingles.Controls.Add(this.numericUpDownAdvanceSingles);
            this.tabPageSingles.Controls.Add(this.buttonSingles);
            this.tabPageSingles.Controls.Add(this.radioButtonRRSingles);
            this.tabPageSingles.Controls.Add(this.radioButtonBracketSingles);
            this.tabPageSingles.Controls.Add(this.buttonRegexReplace);
            this.tabPageSingles.Controls.Add(this.buttonFill);
            this.tabPageSingles.Controls.Add(this.checkBoxFillUnfinishedSingles);
            this.tabPageSingles.Controls.Add(this.checkBoxLosersSingles);
            this.tabPageSingles.Controls.Add(this.buttonPhaseSingles);
            this.tabPageSingles.Controls.Add(this.checkBoxWinnersSingles);
            this.tabPageSingles.Controls.Add(this.label5);
            this.tabPageSingles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageSingles.Location = new System.Drawing.Point(4, 34);
            this.tabPageSingles.Name = "tabPageSingles";
            this.tabPageSingles.Size = new System.Drawing.Size(221, 311);
            this.tabPageSingles.TabIndex = 0;
            this.tabPageSingles.Text = "Singles";
            this.tabPageSingles.UseVisualStyleBackColor = true;
            // 
            // buttonRegexReplace
            // 
            this.buttonRegexReplace.Location = new System.Drawing.Point(84, 276);
            this.buttonRegexReplace.Name = "buttonRegexReplace";
            this.buttonRegexReplace.Size = new System.Drawing.Size(106, 23);
            this.buttonRegexReplace.TabIndex = 2;
            this.buttonRegexReplace.Text = "Regex Replace";
            this.buttonRegexReplace.UseVisualStyleBackColor = true;
            this.buttonRegexReplace.Click += new System.EventHandler(this.buttonRegexReplace_Click);
            // 
            // tabPageDoubles
            // 
            this.tabPageDoubles.Controls.Add(this.numericUpDownAdvanceDoubles);
            this.tabPageDoubles.Controls.Add(this.radioButtonRRDoubles);
            this.tabPageDoubles.Controls.Add(this.radioButtonBracketDoubles);
            this.tabPageDoubles.Controls.Add(this.buttonPhaseDoubles);
            this.tabPageDoubles.Controls.Add(this.label6);
            this.tabPageDoubles.Controls.Add(this.checkBoxFillUnfinishedDoubles);
            this.tabPageDoubles.Controls.Add(this.checkBoxLosersDoubles);
            this.tabPageDoubles.Controls.Add(this.checkBoxWinnersDoubles);
            this.tabPageDoubles.Controls.Add(this.buttonDoubles);
            this.tabPageDoubles.Controls.Add(this.textBoxURLDoubles);
            this.tabPageDoubles.Controls.Add(this.buttonFillDoubles);
            this.tabPageDoubles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageDoubles.Location = new System.Drawing.Point(4, 34);
            this.tabPageDoubles.Name = "tabPageDoubles";
            this.tabPageDoubles.Size = new System.Drawing.Size(221, 311);
            this.tabPageDoubles.TabIndex = 0;
            this.tabPageDoubles.Text = "Doubles";
            this.tabPageDoubles.UseVisualStyleBackColor = true;
            // 
            // numericUpDownAdvanceDoubles
            // 
            this.numericUpDownAdvanceDoubles.Location = new System.Drawing.Point(115, 105);
            this.numericUpDownAdvanceDoubles.Name = "numericUpDownAdvanceDoubles";
            this.numericUpDownAdvanceDoubles.Size = new System.Drawing.Size(32, 20);
            this.numericUpDownAdvanceDoubles.TabIndex = 21;
            this.numericUpDownAdvanceDoubles.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // radioButtonRRDoubles
            // 
            this.radioButtonRRDoubles.AutoSize = true;
            this.radioButtonRRDoubles.Location = new System.Drawing.Point(115, 82);
            this.radioButtonRRDoubles.Name = "radioButtonRRDoubles";
            this.radioButtonRRDoubles.Size = new System.Drawing.Size(88, 17);
            this.radioButtonRRDoubles.TabIndex = 20;
            this.radioButtonRRDoubles.Text = "Round Robin";
            this.radioButtonRRDoubles.UseVisualStyleBackColor = true;
            // 
            // radioButtonBracketDoubles
            // 
            this.radioButtonBracketDoubles.AutoSize = true;
            this.radioButtonBracketDoubles.Checked = true;
            this.radioButtonBracketDoubles.Location = new System.Drawing.Point(115, 59);
            this.radioButtonBracketDoubles.Name = "radioButtonBracketDoubles";
            this.radioButtonBracketDoubles.Size = new System.Drawing.Size(91, 17);
            this.radioButtonBracketDoubles.TabIndex = 19;
            this.radioButtonBracketDoubles.TabStop = true;
            this.radioButtonBracketDoubles.Text = "Bracket Pools";
            this.radioButtonBracketDoubles.UseVisualStyleBackColor = true;
            // 
            // buttonPhaseDoubles
            // 
            this.buttonPhaseDoubles.Location = new System.Drawing.Point(115, 29);
            this.buttonPhaseDoubles.Name = "buttonPhaseDoubles";
            this.buttonPhaseDoubles.Size = new System.Drawing.Size(75, 23);
            this.buttonPhaseDoubles.TabIndex = 17;
            this.buttonPhaseDoubles.Text = "Get Phase";
            this.buttonPhaseDoubles.UseVisualStyleBackColor = true;
            this.buttonPhaseDoubles.Click += new System.EventHandler(this.buttonPhaseDoubles_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Advance";
            // 
            // checkBoxFillUnfinishedDoubles
            // 
            this.checkBoxFillUnfinishedDoubles.AutoSize = true;
            this.checkBoxFillUnfinishedDoubles.Location = new System.Drawing.Point(3, 253);
            this.checkBoxFillUnfinishedDoubles.Name = "checkBoxFillUnfinishedDoubles";
            this.checkBoxFillUnfinishedDoubles.Size = new System.Drawing.Size(111, 17);
            this.checkBoxFillUnfinishedDoubles.TabIndex = 14;
            this.checkBoxFillUnfinishedDoubles.Text = "Fill unfinished sets";
            this.checkBoxFillUnfinishedDoubles.UseVisualStyleBackColor = true;
            // 
            // checkBoxLosersDoubles
            // 
            this.checkBoxLosersDoubles.AutoSize = true;
            this.checkBoxLosersDoubles.Location = new System.Drawing.Point(3, 230);
            this.checkBoxLosersDoubles.Name = "checkBoxLosersDoubles";
            this.checkBoxLosersDoubles.Size = new System.Drawing.Size(57, 17);
            this.checkBoxLosersDoubles.TabIndex = 15;
            this.checkBoxLosersDoubles.Text = "Losers";
            this.checkBoxLosersDoubles.UseVisualStyleBackColor = true;
            // 
            // checkBoxWinnersDoubles
            // 
            this.checkBoxWinnersDoubles.AutoSize = true;
            this.checkBoxWinnersDoubles.Location = new System.Drawing.Point(3, 207);
            this.checkBoxWinnersDoubles.Name = "checkBoxWinnersDoubles";
            this.checkBoxWinnersDoubles.Size = new System.Drawing.Size(65, 17);
            this.checkBoxWinnersDoubles.TabIndex = 16;
            this.checkBoxWinnersDoubles.Text = "Winners";
            this.checkBoxWinnersDoubles.UseVisualStyleBackColor = true;
            // 
            // buttonDoubles
            // 
            this.buttonDoubles.Location = new System.Drawing.Point(3, 29);
            this.buttonDoubles.Name = "buttonDoubles";
            this.buttonDoubles.Size = new System.Drawing.Size(75, 23);
            this.buttonDoubles.TabIndex = 3;
            this.buttonDoubles.Text = "Get Doubles";
            this.buttonDoubles.UseVisualStyleBackColor = true;
            this.buttonDoubles.Click += new System.EventHandler(this.buttonDoubles_Click);
            // 
            // textBoxURLDoubles
            // 
            this.textBoxURLDoubles.Location = new System.Drawing.Point(3, 3);
            this.textBoxURLDoubles.Name = "textBoxURLDoubles";
            this.textBoxURLDoubles.Size = new System.Drawing.Size(187, 20);
            this.textBoxURLDoubles.TabIndex = 2;
            this.textBoxURLDoubles.Enter += new System.EventHandler(this.textBoxURL_Enter);
            // 
            // numericUpDownWinnersStart
            // 
            this.numericUpDownWinnersStart.Location = new System.Drawing.Point(64, 187);
            this.numericUpDownWinnersStart.Name = "numericUpDownWinnersStart";
            this.numericUpDownWinnersStart.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownWinnersStart.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Offset";
            // 
            // numericUpDownWinnersEnd
            // 
            this.numericUpDownWinnersEnd.Location = new System.Drawing.Point(108, 187);
            this.numericUpDownWinnersEnd.Name = "numericUpDownWinnersEnd";
            this.numericUpDownWinnersEnd.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownWinnersEnd.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "End";
            // 
            // numericUpDownWinnersOffset
            // 
            this.numericUpDownWinnersOffset.Location = new System.Drawing.Point(152, 187);
            this.numericUpDownWinnersOffset.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownWinnersOffset.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numericUpDownWinnersOffset.Name = "numericUpDownWinnersOffset";
            this.numericUpDownWinnersOffset.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownWinnersOffset.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Start";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Winners";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(358, 194);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Losers";
            // 
            // numericUpDownLosersStart
            // 
            this.numericUpDownLosersStart.Location = new System.Drawing.Point(402, 187);
            this.numericUpDownLosersStart.Name = "numericUpDownLosersStart";
            this.numericUpDownLosersStart.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownLosersStart.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(399, 170);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Start";
            // 
            // numericUpDownLosersOffset
            // 
            this.numericUpDownLosersOffset.Location = new System.Drawing.Point(490, 187);
            this.numericUpDownLosersOffset.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownLosersOffset.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numericUpDownLosersOffset.Name = "numericUpDownLosersOffset";
            this.numericUpDownLosersOffset.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownLosersOffset.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(443, 170);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "End";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(487, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Offset";
            // 
            // numericUpDownLosersEnd
            // 
            this.numericUpDownLosersEnd.Location = new System.Drawing.Point(446, 187);
            this.numericUpDownLosersEnd.Name = "numericUpDownLosersEnd";
            this.numericUpDownLosersEnd.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownLosersEnd.TabIndex = 9;
            // 
            // checkBoxLockWinners
            // 
            this.checkBoxLockWinners.AutoSize = true;
            this.checkBoxLockWinners.Location = new System.Drawing.Point(196, 188);
            this.checkBoxLockWinners.Name = "checkBoxLockWinners";
            this.checkBoxLockWinners.Size = new System.Drawing.Size(50, 17);
            this.checkBoxLockWinners.TabIndex = 14;
            this.checkBoxLockWinners.Text = "Lock";
            this.checkBoxLockWinners.UseVisualStyleBackColor = true;
            this.checkBoxLockWinners.CheckedChanged += new System.EventHandler(this.checkBoxLock_CheckedChanged);
            // 
            // checkBoxLockLosers
            // 
            this.checkBoxLockLosers.AutoSize = true;
            this.checkBoxLockLosers.Location = new System.Drawing.Point(534, 188);
            this.checkBoxLockLosers.Name = "checkBoxLockLosers";
            this.checkBoxLockLosers.Size = new System.Drawing.Size(50, 17);
            this.checkBoxLockLosers.TabIndex = 14;
            this.checkBoxLockLosers.Text = "Lock";
            this.checkBoxLockLosers.UseVisualStyleBackColor = true;
            this.checkBoxLockLosers.CheckedChanged += new System.EventHandler(this.checkBoxLock_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(709, 655);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(225, 23);
            this.progressBar.TabIndex = 15;
            // 
            // checkBoxGuessFinal
            // 
            this.checkBoxGuessFinal.AutoSize = true;
            this.checkBoxGuessFinal.Location = new System.Drawing.Point(580, 151);
            this.checkBoxGuessFinal.Name = "checkBoxGuessFinal";
            this.checkBoxGuessFinal.Size = new System.Drawing.Size(121, 17);
            this.checkBoxGuessFinal.TabIndex = 14;
            this.checkBoxGuessFinal.Text = "Guess Final Bracket";
            this.checkBoxGuessFinal.UseVisualStyleBackColor = true;
            this.checkBoxGuessFinal.CheckedChanged += new System.EventHandler(this.checkBoxLock_CheckedChanged);
            // 
            // richTextBoxExRegexReplace
            // 
            this.richTextBoxExRegexReplace.Cue = null;
            this.richTextBoxExRegexReplace.Location = new System.Drawing.Point(361, 472);
            this.richTextBoxExRegexReplace.Name = "richTextBoxExRegexReplace";
            this.richTextBoxExRegexReplace.Size = new System.Drawing.Size(342, 46);
            this.richTextBoxExRegexReplace.TabIndex = 16;
            this.richTextBoxExRegexReplace.Text = "";
            // 
            // richTextBoxExRegexFind
            // 
            this.richTextBoxExRegexFind.Cue = null;
            this.richTextBoxExRegexFind.Location = new System.Drawing.Point(12, 472);
            this.richTextBoxExRegexFind.Name = "richTextBoxExRegexFind";
            this.richTextBoxExRegexFind.Size = new System.Drawing.Size(342, 46);
            this.richTextBoxExRegexFind.TabIndex = 16;
            this.richTextBoxExRegexFind.Text = "";
            // 
            // richTextBoxExLpWinnersBracket
            // 
            this.richTextBoxExLpWinnersBracket.Cue = null;
            this.richTextBoxExLpWinnersBracket.Location = new System.Drawing.Point(13, 367);
            this.richTextBoxExLpWinnersBracket.Name = "richTextBoxExLpWinnersBracket";
            this.richTextBoxExLpWinnersBracket.Size = new System.Drawing.Size(341, 89);
            this.richTextBoxExLpWinnersBracket.TabIndex = 13;
            this.richTextBoxExLpWinnersBracket.Text = "";
            // 
            // richTextBoxExLpLosersBracket
            // 
            this.richTextBoxExLpLosersBracket.Cue = null;
            this.richTextBoxExLpLosersBracket.Location = new System.Drawing.Point(360, 368);
            this.richTextBoxExLpLosersBracket.Name = "richTextBoxExLpLosersBracket";
            this.richTextBoxExLpLosersBracket.Size = new System.Drawing.Size(341, 88);
            this.richTextBoxExLpLosersBracket.TabIndex = 13;
            this.richTextBoxExLpLosersBracket.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 690);
            this.Controls.Add(this.richTextBoxExRegexReplace);
            this.Controls.Add(this.richTextBoxExRegexFind);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.checkBoxGuessFinal);
            this.Controls.Add(this.checkBoxLockLosers);
            this.Controls.Add(this.checkBoxLockWinners);
            this.Controls.Add(this.richTextBoxExLpWinnersBracket);
            this.Controls.Add(this.richTextBoxExLpLosersBracket);
            this.Controls.Add(this.numericUpDownLosersStart);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericUpDownLosersOffset);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.numericUpDownLosersEnd);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.richTextBoxLpOutput);
            this.Controls.Add(this.richTextBoxLosers);
            this.Controls.Add(this.richTextBoxWinners);
            this.Controls.Add(this.richTextBoxEntrants);
            this.Controls.Add(this.numericUpDownWinnersStart);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownWinnersOffset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownWinnersEnd);
            this.Name = "Form1";
            this.Text = "Smash.gg to Liquipedia";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdvanceSingles)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageSingles.ResumeLayout(false);
            this.tabPageSingles.PerformLayout();
            this.tabPageDoubles.ResumeLayout(false);
            this.tabPageDoubles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdvanceDoubles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWinnersStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWinnersEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWinnersOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLosersStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLosersOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLosersEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxEntrants;
        private System.Windows.Forms.TextBox textBoxURLSingles;
        private System.Windows.Forms.Button buttonSingles;
        private System.Windows.Forms.Button buttonPhaseSingles;
        private System.Windows.Forms.RadioButton radioButtonBracketSingles;
        private System.Windows.Forms.RadioButton radioButtonRRSingles;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownAdvanceSingles;
        private System.Windows.Forms.RichTextBox richTextBoxWinners;
        private System.Windows.Forms.RichTextBox richTextBoxLosers;
        private System.Windows.Forms.RichTextBox richTextBoxLpOutput;
        private System.Windows.Forms.Button buttonFill;
        private System.Windows.Forms.Button buttonFillDoubles;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxWinnersSingles;
        private System.Windows.Forms.CheckBox checkBoxLosersSingles;
        private System.Windows.Forms.CheckBox checkBoxFillUnfinishedSingles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSingles;
        private System.Windows.Forms.NumericUpDown numericUpDownWinnersStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownWinnersEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownWinnersOffset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageDoubles;
        private System.Windows.Forms.CheckBox checkBoxFillUnfinishedDoubles;
        private System.Windows.Forms.CheckBox checkBoxLosersDoubles;
        private System.Windows.Forms.CheckBox checkBoxWinnersDoubles;
        private System.Windows.Forms.Button buttonDoubles;
        private System.Windows.Forms.TextBox textBoxURLDoubles;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownLosersStart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDownLosersOffset;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDownLosersEnd;
        private RichTextBoxEx richTextBoxExLpLosersBracket;
        private RichTextBoxEx richTextBoxExLpWinnersBracket;
        private System.Windows.Forms.CheckBox checkBoxLockWinners;
        private System.Windows.Forms.CheckBox checkBoxLockLosers;
        private System.Windows.Forms.NumericUpDown numericUpDownAdvanceDoubles;
        private System.Windows.Forms.RadioButton radioButtonRRDoubles;
        private System.Windows.Forms.RadioButton radioButtonBracketDoubles;
        private System.Windows.Forms.Button buttonPhaseDoubles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox checkBoxGuessFinal;
        private RichTextBoxEx richTextBoxExRegexFind;
        private RichTextBoxEx richTextBoxExRegexReplace;
        private System.Windows.Forms.Button buttonRegexReplace;
    }
}

