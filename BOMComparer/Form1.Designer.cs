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
            this.mbLBL = new System.Windows.Forms.Label();
            this.nbLBL = new System.Windows.Forms.Label();
            this.saveBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(626, 640);
            this.dataGridView1.TabIndex = 0;
            // 
            // importmbBTN
            // 
            this.importmbBTN.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.importmbBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importmbBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.importmbBTN.Location = new System.Drawing.Point(1231, 42);
            this.importmbBTN.Name = "importmbBTN";
            this.importmbBTN.Size = new System.Drawing.Size(169, 69);
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
            this.compareBTN.Location = new System.Drawing.Point(1231, 344);
            this.compareBTN.Name = "compareBTN";
            this.compareBTN.Size = new System.Drawing.Size(169, 40);
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
            this.importnbBTN.Location = new System.Drawing.Point(1231, 117);
            this.importnbBTN.Name = "importnbBTN";
            this.importnbBTN.Size = new System.Drawing.Size(169, 72);
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
            this.buildBTN.Location = new System.Drawing.Point(1231, 298);
            this.buildBTN.Name = "buildBTN";
            this.buildBTN.Size = new System.Drawing.Size(169, 35);
            this.buildBTN.TabIndex = 10;
            this.buildBTN.Text = "Build";
            this.buildBTN.UseVisualStyleBackColor = false;
            this.buildBTN.Click += new System.EventHandler(this.buildBTN_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(644, 42);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(581, 640);
            this.dataGridView2.TabIndex = 11;
            // 
            // mbLBL
            // 
            this.mbLBL.AutoSize = true;
            this.mbLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbLBL.Location = new System.Drawing.Point(12, 14);
            this.mbLBL.Name = "mbLBL";
            this.mbLBL.Size = new System.Drawing.Size(149, 25);
            this.mbLBL.TabIndex = 12;
            this.mbLBL.Text = "Master BOM:";
            // 
            // nbLBL
            // 
            this.nbLBL.AutoSize = true;
            this.nbLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbLBL.Location = new System.Drawing.Point(644, 14);
            this.nbLBL.Name = "nbLBL";
            this.nbLBL.Size = new System.Drawing.Size(122, 25);
            this.nbLBL.TabIndex = 13;
            this.nbLBL.Text = "New BOM:";
            // 
            // saveBTN
            // 
            this.saveBTN.BackColor = System.Drawing.Color.LavenderBlush;
            this.saveBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.saveBTN.Location = new System.Drawing.Point(1231, 464);
            this.saveBTN.Name = "saveBTN";
            this.saveBTN.Size = new System.Drawing.Size(169, 72);
            this.saveBTN.TabIndex = 15;
            this.saveBTN.Text = "Save Changes";
            this.saveBTN.UseVisualStyleBackColor = false;
            this.saveBTN.Click += new System.EventHandler(this.saveBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1419, 687);
            this.Controls.Add(this.saveBTN);
            this.Controls.Add(this.nbLBL);
            this.Controls.Add(this.mbLBL);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.buildBTN);
            this.Controls.Add(this.importnbBTN);
            this.Controls.Add(this.compareBTN);
            this.Controls.Add(this.importmbBTN);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "BOMComparer v1.01";
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
        private System.Windows.Forms.Label mbLBL;
        private System.Windows.Forms.Label nbLBL;
        private System.Windows.Forms.Button saveBTN;
    }
}

