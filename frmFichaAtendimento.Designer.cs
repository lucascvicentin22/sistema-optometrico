namespace SistemaOptometrico
{
    partial class frmFichaAtendimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFichaAtendimento));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPesquisar = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnNovoCadastro = new System.Windows.Forms.Button();
            this.btnJaTemCadastro = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtSEXO = new System.Windows.Forms.TextBox();
            this.txtESCOLARIDADE = new System.Windows.Forms.TextBox();
            this.txtNASCIMENTO = new System.Windows.Forms.MaskedTextBox();
            this.btnWhatsapp = new System.Windows.Forms.Button();
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
            this.txtIDCLIENTE = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnATUALIZARBANCO = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPesquisar);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 47);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pesquisa";
            // 
            // txtPesquisar
            // 
            this.txtPesquisar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPesquisar.Location = new System.Drawing.Point(7, 21);
            this.txtPesquisar.Name = "txtPesquisar";
            this.txtPesquisar.Size = new System.Drawing.Size(303, 20);
            this.txtPesquisar.TabIndex = 0;
            this.txtPesquisar.TextChanged += new System.EventHandler(this.txtPesquisar_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(667, 226);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnATUALIZARBANCO);
            this.groupBox2.Controls.Add(this.btnSalvar);
            this.groupBox2.Controls.Add(this.btnNovoCadastro);
            this.groupBox2.Controls.Add(this.btnJaTemCadastro);
            this.groupBox2.Location = new System.Drawing.Point(692, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(141, 432);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(10, 224);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(117, 89);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "SALVAR\r\nDADOS";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnNovoCadastro
            // 
            this.btnNovoCadastro.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnNovoCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovoCadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovoCadastro.ForeColor = System.Drawing.Color.White;
            this.btnNovoCadastro.Location = new System.Drawing.Point(10, 34);
            this.btnNovoCadastro.Name = "btnNovoCadastro";
            this.btnNovoCadastro.Size = new System.Drawing.Size(117, 89);
            this.btnNovoCadastro.TabIndex = 1;
            this.btnNovoCadastro.Text = "NOVO\r\nCADASTRO\r\n";
            this.btnNovoCadastro.UseVisualStyleBackColor = false;
            this.btnNovoCadastro.Click += new System.EventHandler(this.btnNovoCadastro_Click_1);
            // 
            // btnJaTemCadastro
            // 
            this.btnJaTemCadastro.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnJaTemCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJaTemCadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJaTemCadastro.ForeColor = System.Drawing.Color.White;
            this.btnJaTemCadastro.Location = new System.Drawing.Point(10, 129);
            this.btnJaTemCadastro.Name = "btnJaTemCadastro";
            this.btnJaTemCadastro.Size = new System.Drawing.Size(117, 89);
            this.btnJaTemCadastro.TabIndex = 2;
            this.btnJaTemCadastro.Text = "JÁ TEM\r\nCADASTRO\r\n";
            this.btnJaTemCadastro.UseVisualStyleBackColor = false;
            this.btnJaTemCadastro.Click += new System.EventHandler(this.btnJaTemCadastro_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(13, 67);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(673, 245);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TODOS PACIENTES CADASTRADOS";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtSEXO);
            this.groupBox4.Controls.Add(this.txtESCOLARIDADE);
            this.groupBox4.Controls.Add(this.txtNASCIMENTO);
            this.groupBox4.Controls.Add(this.btnWhatsapp);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtPROFISSAO);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtCIDADE);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtBAIRRO);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtRUA);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtWHATSAPP);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtIDADE);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtCPF);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtNOMECOMPLETO);
            this.groupBox4.Controls.Add(this.txtIDCLIENTE);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(13, 316);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(670, 128);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Dados do Cadastro";
            // 
            // txtSEXO
            // 
            this.txtSEXO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSEXO.Location = new System.Drawing.Point(548, 98);
            this.txtSEXO.Name = "txtSEXO";
            this.txtSEXO.Size = new System.Drawing.Size(91, 20);
            this.txtSEXO.TabIndex = 27;
            // 
            // txtESCOLARIDADE
            // 
            this.txtESCOLARIDADE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtESCOLARIDADE.Location = new System.Drawing.Point(383, 99);
            this.txtESCOLARIDADE.Name = "txtESCOLARIDADE";
            this.txtESCOLARIDADE.Size = new System.Drawing.Size(122, 20);
            this.txtESCOLARIDADE.TabIndex = 26;
            // 
            // txtNASCIMENTO
            // 
            this.txtNASCIMENTO.Location = new System.Drawing.Point(208, 46);
            this.txtNASCIMENTO.Mask = "00/00/0000";
            this.txtNASCIMENTO.Name = "txtNASCIMENTO";
            this.txtNASCIMENTO.Size = new System.Drawing.Size(100, 20);
            this.txtNASCIMENTO.TabIndex = 25;
            this.txtNASCIMENTO.ValidatingType = typeof(System.DateTime);
            // 
            // btnWhatsapp
            // 
            this.btnWhatsapp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWhatsapp.BackgroundImage")));
            this.btnWhatsapp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWhatsapp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWhatsapp.FlatAppearance.BorderSize = 0;
            this.btnWhatsapp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWhatsapp.Location = new System.Drawing.Point(590, 33);
            this.btnWhatsapp.Name = "btnWhatsapp";
            this.btnWhatsapp.Size = new System.Drawing.Size(49, 44);
            this.btnWhatsapp.TabIndex = 24;
            this.btnWhatsapp.UseVisualStyleBackColor = true;
            this.btnWhatsapp.Click += new System.EventHandler(this.btnWhatsapp_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(511, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "SEXO";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(291, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "ESCOLARIDADE";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "PROFISSÃO";
            // 
            // txtPROFISSAO
            // 
            this.txtPROFISSAO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPROFISSAO.Location = new System.Drawing.Point(97, 97);
            this.txtPROFISSAO.Name = "txtPROFISSAO";
            this.txtPROFISSAO.Size = new System.Drawing.Size(190, 20);
            this.txtPROFISSAO.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(490, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "CIDADE";
            // 
            // txtCIDADE
            // 
            this.txtCIDADE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCIDADE.Location = new System.Drawing.Point(539, 72);
            this.txtCIDADE.Name = "txtCIDADE";
            this.txtCIDADE.Size = new System.Drawing.Size(100, 20);
            this.txtCIDADE.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(333, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "BAIRRO";
            // 
            // txtBAIRRO
            // 
            this.txtBAIRRO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBAIRRO.Location = new System.Drawing.Point(383, 72);
            this.txtBAIRRO.Name = "txtBAIRRO";
            this.txtBAIRRO.Size = new System.Drawing.Size(100, 20);
            this.txtBAIRRO.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(101, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "RUA";
            // 
            // txtRUA
            // 
            this.txtRUA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRUA.Location = new System.Drawing.Point(132, 71);
            this.txtRUA.Name = "txtRUA";
            this.txtRUA.Size = new System.Drawing.Size(190, 20);
            this.txtRUA.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(415, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "WHATSAPP";
            // 
            // txtWHATSAPP
            // 
            this.txtWHATSAPP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWHATSAPP.Location = new System.Drawing.Point(484, 46);
            this.txtWHATSAPP.Name = "txtWHATSAPP";
            this.txtWHATSAPP.Size = new System.Drawing.Size(100, 20);
            this.txtWHATSAPP.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(316, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "IDADE";
            // 
            // txtIDADE
            // 
            this.txtIDADE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIDADE.Location = new System.Drawing.Point(358, 46);
            this.txtIDADE.Name = "txtIDADE";
            this.txtIDADE.Size = new System.Drawing.Size(40, 20);
            this.txtIDADE.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "NASCIMENTO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(455, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "CPF";
            // 
            // txtCPF
            // 
            this.txtCPF.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCPF.Location = new System.Drawing.Point(484, 20);
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Size = new System.Drawing.Size(100, 20);
            this.txtCPF.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "NOME COMPLETO";
            // 
            // txtNOMECOMPLETO
            // 
            this.txtNOMECOMPLETO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNOMECOMPLETO.Location = new System.Drawing.Point(208, 19);
            this.txtNOMECOMPLETO.Name = "txtNOMECOMPLETO";
            this.txtNOMECOMPLETO.Size = new System.Drawing.Size(236, 20);
            this.txtNOMECOMPLETO.TabIndex = 1;
            // 
            // txtIDCLIENTE
            // 
            this.txtIDCLIENTE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIDCLIENTE.Enabled = false;
            this.txtIDCLIENTE.Location = new System.Drawing.Point(45, 20);
            this.txtIDCLIENTE.Name = "txtIDCLIENTE";
            this.txtIDCLIENTE.Size = new System.Drawing.Size(49, 20);
            this.txtIDCLIENTE.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // btnATUALIZARBANCO
            // 
            this.btnATUALIZARBANCO.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnATUALIZARBANCO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnATUALIZARBANCO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnATUALIZARBANCO.ForeColor = System.Drawing.Color.White;
            this.btnATUALIZARBANCO.Location = new System.Drawing.Point(10, 319);
            this.btnATUALIZARBANCO.Name = "btnATUALIZARBANCO";
            this.btnATUALIZARBANCO.Size = new System.Drawing.Size(117, 89);
            this.btnATUALIZARBANCO.TabIndex = 3;
            this.btnATUALIZARBANCO.Text = "ATUALIZAR\r\nBANCO";
            this.btnATUALIZARBANCO.UseVisualStyleBackColor = false;
            this.btnATUALIZARBANCO.Click += new System.EventHandler(this.btnATUALIZARBANCO_Click);
            // 
            // frmFichaAtendimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 456);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFichaAtendimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ficha de Atendimento Optometrico";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnJaTemCadastro;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnNovoCadastro;
        private System.Windows.Forms.TextBox txtPesquisar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNOMECOMPLETO;
        private System.Windows.Forms.TextBox txtIDCLIENTE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCPF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIDADE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBAIRRO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRUA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWHATSAPP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPROFISSAO;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCIDADE;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnWhatsapp;
        private System.Windows.Forms.MaskedTextBox txtNASCIMENTO;
        private System.Windows.Forms.TextBox txtSEXO;
        private System.Windows.Forms.TextBox txtESCOLARIDADE;
        private System.Windows.Forms.Button btnATUALIZARBANCO;
    }
}