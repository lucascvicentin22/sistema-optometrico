namespace SistemaOptometrico
{
    partial class frmConsultorio
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWHATSAPP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCIDADE = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtENDERECO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNOME = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnEDITAR = new System.Windows.Forms.Button();
            this.btnAPAGAR = new System.Windows.Forms.Button();
            this.salvar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtWHATSAPP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCIDADE);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtENDERECO);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNOME);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CADASTRO DO CONSULTÓRIO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(104, 123);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(67, 20);
            this.txtID.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "WHATSAPP";
            // 
            // txtWHATSAPP
            // 
            this.txtWHATSAPP.Location = new System.Drawing.Point(104, 97);
            this.txtWHATSAPP.Name = "txtWHATSAPP";
            this.txtWHATSAPP.Size = new System.Drawing.Size(271, 20);
            this.txtWHATSAPP.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "CIDADE";
            // 
            // txtCIDADE
            // 
            this.txtCIDADE.Location = new System.Drawing.Point(104, 71);
            this.txtCIDADE.Name = "txtCIDADE";
            this.txtCIDADE.Size = new System.Drawing.Size(271, 20);
            this.txtCIDADE.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ENDEREÇO";
            // 
            // txtENDERECO
            // 
            this.txtENDERECO.Location = new System.Drawing.Point(104, 45);
            this.txtENDERECO.Name = "txtENDERECO";
            this.txtENDERECO.Size = new System.Drawing.Size(271, 20);
            this.txtENDERECO.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "NOME";
            // 
            // txtNOME
            // 
            this.txtNOME.Location = new System.Drawing.Point(104, 19);
            this.txtNOME.Name = "txtNOME";
            this.txtNOME.Size = new System.Drawing.Size(271, 20);
            this.txtNOME.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 161);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(527, 159);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnEDITAR
            // 
            this.btnEDITAR.Location = new System.Drawing.Point(230, 326);
            this.btnEDITAR.Name = "btnEDITAR";
            this.btnEDITAR.Size = new System.Drawing.Size(112, 36);
            this.btnEDITAR.TabIndex = 3;
            this.btnEDITAR.Text = "EDITAR";
            this.btnEDITAR.UseVisualStyleBackColor = true;
            this.btnEDITAR.Click += new System.EventHandler(this.btnEDITAR_Click);
            // 
            // btnAPAGAR
            // 
            this.btnAPAGAR.Location = new System.Drawing.Point(345, 326);
            this.btnAPAGAR.Name = "btnAPAGAR";
            this.btnAPAGAR.Size = new System.Drawing.Size(112, 36);
            this.btnAPAGAR.TabIndex = 4;
            this.btnAPAGAR.Text = "APAGAR";
            this.btnAPAGAR.UseVisualStyleBackColor = true;
            this.btnAPAGAR.Click += new System.EventHandler(this.btnAPAGAR_Click);
            // 
            // salvar
            // 
            this.salvar.Location = new System.Drawing.Point(115, 326);
            this.salvar.Name = "salvar";
            this.salvar.Size = new System.Drawing.Size(112, 36);
            this.salvar.TabIndex = 6;
            this.salvar.Text = "SALVAR";
            this.salvar.UseVisualStyleBackColor = true;
            this.salvar.Click += new System.EventHandler(this.salvar_Click);
            // 
            // frmConsultorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 379);
            this.Controls.Add(this.salvar);
            this.Controls.Add(this.btnAPAGAR);
            this.Controls.Add(this.btnEDITAR);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CADASTRO DE CONSULTÓRIO";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWHATSAPP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCIDADE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtENDERECO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNOME;
        private System.Windows.Forms.Button btnEDITAR;
        private System.Windows.Forms.Button btnAPAGAR;
        private System.Windows.Forms.Button salvar;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label6;
    }
}