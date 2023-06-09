namespace Snake
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblGameOver = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.lblScoreAI = new System.Windows.Forms.Label();
            this.PictureBoxAI = new System.Windows.Forms.PictureBox();
            this.lblGameOverAI = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxAI)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.Location = new System.Drawing.Point(13, 13);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(35, 13);
            this.lblGameOver.TabIndex = 0;
            this.lblGameOver.Text = "label1";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(826, 13);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(35, 13);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "label2";
            // 
            // PictureBox
            // 
            this.PictureBox.BackColor = System.Drawing.Color.Black;
            this.PictureBox.Location = new System.Drawing.Point(16, 79);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(845, 395);
            this.PictureBox.TabIndex = 2;
            this.PictureBox.TabStop = false;
            // 
            // lblScoreAI
            // 
            this.lblScoreAI.AutoSize = true;
            this.lblScoreAI.Location = new System.Drawing.Point(826, 516);
            this.lblScoreAI.Name = "lblScoreAI";
            this.lblScoreAI.Size = new System.Drawing.Size(35, 13);
            this.lblScoreAI.TabIndex = 3;
            this.lblScoreAI.Text = "label1";
            // 
            // PictureBoxAI
            // 
            this.PictureBoxAI.BackColor = System.Drawing.Color.Black;
            this.PictureBoxAI.Location = new System.Drawing.Point(16, 566);
            this.PictureBoxAI.Name = "PictureBoxAI";
            this.PictureBoxAI.Size = new System.Drawing.Size(845, 395);
            this.PictureBoxAI.TabIndex = 4;
            this.PictureBoxAI.TabStop = false;
            // 
            // lblGameOverAI
            // 
            this.lblGameOverAI.AutoSize = true;
            this.lblGameOverAI.Location = new System.Drawing.Point(13, 497);
            this.lblGameOverAI.Name = "lblGameOverAI";
            this.lblGameOverAI.Size = new System.Drawing.Size(35, 13);
            this.lblGameOverAI.TabIndex = 5;
            this.lblGameOverAI.Text = "label1";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(748, 535);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(13, 13);
            this.lblTime.TabIndex = 6;
            this.lblTime.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 973);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblGameOverAI);
            this.Controls.Add(this.PictureBoxAI);
            this.Controls.Add(this.lblScoreAI);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblGameOver);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxAI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Label lblScoreAI;
        private System.Windows.Forms.PictureBox PictureBoxAI;
        private System.Windows.Forms.Label lblGameOverAI;
        private System.Windows.Forms.Label lblTime;
    }
}

