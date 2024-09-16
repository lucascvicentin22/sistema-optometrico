namespace SistemaOptometrico
{
    partial class frmPreCadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreCadastro));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSEXO = new System.Windows.Forms.ComboBox();
            this.cbESCOLARIDADE = new System.Windows.Forms.ComboBox();
            this.txtNASCIMENTO = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPROFISSAO = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCIDADE = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBAIRRO = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRUA = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWHATSAPP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIDADE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCPF = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNOMECOMPLETO = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSALVAR = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSEXO);
            this.groupBox1.Controls.Add(this.cbESCOLARIDADE);
            this.groupBox1.Controls.Add(this.txtNASCIMENTO);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtPROFISSAO);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtCIDADE);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBAIRRO);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtRUA);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtWHATSAPP);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtIDADE);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCPF);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNOMECOMPLETO);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DADOS PESSOAIS";
            // 
            // cbSEXO
            // 
            this.cbSEXO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSEXO.FormattingEnabled = true;
            this.cbSEXO.Items.AddRange(new object[] {
            "FEMININO",
            "MASCULINO"});
            this.cbSEXO.Location = new System.Drawing.Point(553, 97);
            this.cbSEXO.Name = "cbSEXO";
            this.cbSEXO.Size = new System.Drawing.Size(86, 21);
            this.cbSEXO.TabIndex = 49;
            // 
            // cbESCOLARIDADE
            // 
            this.cbESCOLARIDADE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbESCOLARIDADE.FormattingEnabled = true;
            this.cbESCOLARIDADE.Items.AddRange(new object[] {
            "FUNDAMENTAL",
            "MÉDIO",
            "SUPERIOR",
            "PÓS GRADUADO",
            "MESTRADO",
            "DOUTORADO"});
            this.cbESCOLARIDADE.Location = new System.Drawing.Point(388, 98);
            this.cbESCOLARIDADE.Name = "cbESCOLARIDADE";
            this.cbESCOLARIDADE.Size = new System.Drawing.Size(121, 21);
            this.cbESCOLARIDADE.TabIndex = 48;
            // 
            // txtNASCIMENTO
            // 
            this.txtNASCIMENTO.Location = new System.Drawing.Point(214, 46);
            this.txtNASCIMENTO.Mask = "00/00/0000";
            this.txtNASCIMENTO.Name = "txtNASCIMENTO";
            this.txtNASCIMENTO.Size = new System.Drawing.Size(100, 20);
            this.txtNASCIMENTO.TabIndex = 47;
            this.txtNASCIMENTO.ValidatingType = typeof(System.DateTime);
            this.txtNASCIMENTO.TextChanged += new System.EventHandler(this.txtNASCIMENTO_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(517, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "SEXO";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(297, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "ESCOLARIDADE";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 44;
            this.label10.Text = "PROFISSÃO";
            // 
            // txtPROFISSAO
            // 
            this.txtPROFISSAO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPROFISSAO.Location = new System.Drawing.Point(103, 97);
            this.txtPROFISSAO.Name = "txtPROFISSAO";
            this.txtPROFISSAO.Size = new System.Drawing.Size(190, 20);
            this.txtPROFISSAO.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(496, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "CIDADE";
            // 
            // txtCIDADE
            // 
            this.txtCIDADE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCIDADE.Location = new System.Drawing.Point(545, 72);
            this.txtCIDADE.Name = "txtCIDADE";
            this.txtCIDADE.Size = new System.Drawing.Size(100, 20);
            this.txtCIDADE.TabIndex = 37;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(339, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "BAIRRO";
            // 
            // txtBAIRRO
            // 
            this.txtBAIRRO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBAIRRO.Location = new System.Drawing.Point(389, 72);
            this.txtBAIRRO.Name = "txtBAIRRO";
            this.txtBAIRRO.Size = new System.Drawing.Size(100, 20);
            this.txtBAIRRO.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "RUA";
            // 
            // txtRUA
            // 
            this.txtRUA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRUA.Location = new System.Drawing.Point(70, 71);
            this.txtRUA.Name = "txtRUA";
            this.txtRUA.Size = new System.Drawing.Size(258, 20);
            this.txtRUA.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(421, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "WHATSAPP";
            // 
            // txtWHATSAPP
            // 
            this.txtWHATSAPP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWHATSAPP.Location = new System.Drawing.Point(490, 46);
            this.txtWHATSAPP.Name = "txtWHATSAPP";
            this.txtWHATSAPP.Size = new System.Drawing.Size(100, 20);
            this.txtWHATSAPP.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(322, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "IDADE";
            // 
            // txtIDADE
            // 
            this.txtIDADE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIDADE.Location = new System.Drawing.Point(364, 46);
            this.txtIDADE.Name = "txtIDADE";
            this.txtIDADE.Size = new System.Drawing.Size(40, 20);
            this.txtIDADE.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "NASCIMENTO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(461, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "CPF";
            // 
            // txtCPF
            // 
            this.txtCPF.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCPF.Location = new System.Drawing.Point(490, 20);
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Size = new System.Drawing.Size(100, 20);
            this.txtCPF.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "NOME COMPLETO";
            // 
            // txtNOMECOMPLETO
            // 
            this.txtNOMECOMPLETO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNOMECOMPLETO.Location = new System.Drawing.Point(214, 19);
            this.txtNOMECOMPLETO.Name = "txtNOMECOMPLETO";
            this.txtNOMECOMPLETO.Size = new System.Drawing.Size(236, 20);
            this.txtNOMECOMPLETO.TabIndex = 28;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSALVAR);
            this.groupBox2.Location = new System.Drawing.Point(13, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(654, 74);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnSALVAR
            // 
            this.btnSALVAR.Location = new System.Drawing.Point(249, 19);
            this.btnSALVAR.Name = "btnSALVAR";
            this.btnSALVAR.Size = new System.Drawing.Size(155, 39);
            this.btnSALVAR.TabIndex = 19;
            this.btnSALVAR.Text = "SALVAR";
            this.btnSALVAR.UseVisualStyleBackColor = true;
            this.btnSALVAR.Click += new System.EventHandler(this.btnSALVAR_Click);
            // 
            // frmPreCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 235);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPreCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CADASTRO DE PERFIL DO CLIENTE";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSALVAR;
        private System.Windows.Forms.MaskedTextBox txtNASCIMENTO;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPROFISSAO;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCIDADE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBAIRRO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRUA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWHATSAPP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIDADE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCPF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNOMECOMPLETO;
        private System.Windows.Forms.ComboBox cbSEXO;
        private System.Windows.Forms.ComboBox cbESCOLARIDADE;
    }
}