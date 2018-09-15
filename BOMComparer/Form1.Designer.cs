namespace BOMComparer
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.importmbBTN = new System.Windows.Forms.Button();
            this.compareBTN = new System.Windows.Forms.Button();
            this.importnbBTN = new System.Windows.Forms.Button();
            this.buildBTN = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.bnameTB = new System.Windows.Forms.TextBox();
            this.outputBTN = new System.Windows.Forms.Button();
            this.dirtb = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 92);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(760, 590);
            this.dataGridView1.TabIndex = 0;
            // 
            // importmbBTN
            // 
            this.importmbBTN.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.importmbBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importmbBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.importmbBTN.Location = new System.Drawing.Point(17, 11);
            this.importmbBTN.Name = "importmbBTN";
            this.importmbBTN.Size = new System.Drawing.Size(301, 41);
            this.importmbBTN.TabIndex = 6;
            this.importmbBTN.Text = "Import Master BOM";
            this.importmbBTN.UseVisualStyleBackColor = false;
            this.importmbBTN.Click += new System.EventHandler(this.importmbBTN_Click);
            // 
            // compareBTN
            // 
            this.compareBTN.BackColor = System.Drawing.Color.LightYellow;
            this.compareBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compareBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.compareBTN.Location = new System.Drawing.Point(1308, 46);
            this.compareBTN.Name = "compareBTN";
            this.compareBTN.Size = new System.Drawing.Size(197, 40);
            this.compareBTN.TabIndex = 8;
            this.compareBTN.Text = "Compare";
            this.compareBTN.UseVisualStyleBackColor = false;
            this.compareBTN.Click += new System.EventHandler(this.compareBTN_Click);
            // 
            // importnbBTN
            // 
            this.importnbBTN.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.importnbBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importnbBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.importnbBTN.Location = new System.Drawing.Point(324, 11);
            this.importnbBTN.Name = "importnbBTN";
            this.importnbBTN.Size = new System.Drawing.Size(280, 41);
            this.importnbBTN.TabIndex = 9;
            this.importnbBTN.Text = "Import New BOM";
            this.importnbBTN.UseVisualStyleBackColor = false;
            this.importnbBTN.Click += new System.EventHandler(this.importnbBTN_Click);
            // 
            // buildBTN
            // 
            this.buildBTN.BackColor = System.Drawing.Color.PeachPuff;
            this.buildBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buildBTN.Location = new System.Drawing.Point(1308, 5);
            this.buildBTN.Name = "buildBTN";
            this.buildBTN.Size = new System.Drawing.Size(197, 35);
            this.buildBTN.TabIndex = 10;
            this.buildBTN.Text = "Column Mapping";
            this.buildBTN.UseVisualStyleBackColor = false;
            this.buildBTN.Click += new System.EventHandler(this.buildBTN_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView2.Location = new System.Drawing.Point(778, 92);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(735, 590);
            this.dataGridView2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(610, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 24);
            this.label1.TabIndex = 16;
            this.label1.Text = "Board Name:";
            // 
            // bnameTB
            // 
            this.bnameTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnameTB.Location = new System.Drawing.Point(614, 38);
            this.bnameTB.Name = "bnameTB";
            this.bnameTB.Size = new System.Drawing.Size(117, 24);
            this.bnameTB.TabIndex = 17;
            // 
            // outputBTN
            // 
            this.outputBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputBTN.Location = new System.Drawing.Point(751, 5);
            this.outputBTN.Name = "outputBTN";
            this.outputBTN.Size = new System.Drawing.Size(122, 35);
            this.outputBTN.TabIndex = 18;
            this.outputBTN.Text = "Output Folder";
            this.outputBTN.UseVisualStyleBackColor = true;
            this.outputBTN.Click += new System.EventHandler(this.outputBTN_Click);
            // 
            // dirtb
            // 
            this.dirtb.Location = new System.Drawing.Point(751, 42);
            this.dirtb.Name = "dirtb";
            this.dirtb.ReadOnly = true;
            this.dirtb.Size = new System.Drawing.Size(354, 20);
            this.dirtb.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1517, 687);
            this.Controls.Add(this.dirtb);
            this.Controls.Add(this.outputBTN);
            this.Controls.Add(this.bnameTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.buildBTN);
            this.Controls.Add(this.importnbBTN);
            this.Controls.Add(this.compareBTN);
            this.Controls.Add(this.importmbBTN);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "BOMComparer v1.03";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button importmbBTN;
        private System.Windows.Forms.Button compareBTN;
        private System.Windows.Forms.Button importnbBTN;
        private System.Windows.Forms.Button buildBTN;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox bnameTB;
        private System.Windows.Forms.Button outputBTN;
        private System.Windows.Forms.TextBox dirtb;
    }
}

