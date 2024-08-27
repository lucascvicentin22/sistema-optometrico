using iText.Kernel.Pdf;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Image;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iTextSharp.text;
using iTextParagraph = iText.Layout.Element.Paragraph;
using iTextSharpParagraph = iTextSharp.text.Paragraph;
// Alias para desambiguar PageSize
using iTextPageSize = iText.Kernel.Geom.PageSize;
using System.ComponentModel.Design;
using System.Globalization;
using iText.StyledXmlParser.Jsoup.Nodes;
using SistemaOptometrico;


namespace SistemaOptometrico

{
    
    public partial class frmNovoCadastroFichaTendimento : Form
    {
        private ContextMenuStrip contextMenuStrip1;
        private string connectionString = "Server=localhost;Database=db_clinica;User ID=root;Password=2707;";
        
        public frmNovoCadastroFichaTendimento()
        {
            InitializeComponent();

            txtDATADOEXAME.Mask = "00/00/0000";
            txtDATADOEXAME.ValidatingType = typeof(DateTime);

            txtNASCIMENTO.Mask = "00/00/0000";
            txtNASCIMENTO.ValidatingType = typeof(DateTime);

            // Criação das opções do menu de contexto
            ToolStripMenuItem gerarFichaMenuItem = new ToolStripMenuItem("GERAR FICHA DE ATENDIMENTO");
            ToolStripMenuItem gerarReceitaMenuItem = new ToolStripMenuItem("GERAR RECEITA");
            ToolStripMenuItem gerarAtestadoMenuItem = new ToolStripMenuItem("GERAR ATESTADO");

            // Criação e configuração do ContextMenuStrip
            contextMenuStrip1 = new ContextMenuStrip();
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            gerarFichaMenuItem,
            gerarReceitaMenuItem,
            gerarAtestadoMenuItem
        });

            // Associar o ContextMenuStrip à DataGridView
            dataGridView1.ContextMenuStrip = contextMenuStrip1;

            // Eventos de clique para cada opção do menu
            gerarFichaMenuItem.Click += new EventHandler(GerarFichaMenuItem_Click);
            gerarReceitaMenuItem.Click += new EventHandler(GerarReceitaMenuItem_Click);
            gerarAtestadoMenuItem.Click += new EventHandler(GerarAtestadoMenuItem_Click);
        }

        // Métodos de evento para as opções do menu
        private void GerarFichaMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void GerarAtestadoMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void GerarReceitaMenuItem_Click(object sender, EventArgs e)
        {
            ClasseReceita receita = new ClasseReceita();
            receita.GerarReceita("Receita Optométrico", 1, true);
        }
       
        private void txtIDCLIENTE_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIDCLIENTE.Text))
            {
                int idCliente;
                if (int.TryParse(txtIDCLIENTE.Text, out idCliente))
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(connectionString);
                        {
                            string queryCliente = "SELECT nome_completo, nascimento, idade, profissao, escolaridade FROM tb_precadastro WHERE id_cliente = @id_cliente";
                            MySqlCommand commandCliente = new MySqlCommand(queryCliente, connection);
                            commandCliente.Parameters.AddWithValue("@id_cliente", idCliente);

                            connection.Open();
                            using (MySqlDataReader reader = commandCliente.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtNOMECOMPLETO.Text = reader["nome_completo"].ToString();

                                    DateTime nascimento;
                                    if (DateTime.TryParse(reader["nascimento"].ToString(), out nascimento))
                                    {
                                        txtNASCIMENTO.Text = nascimento.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        txtNASCIMENTO.Text = string.Empty;
                                    }

                                    txtIDADE.Text = reader["idade"].ToString();
                                    txtPROFISSAO.Text = reader["profissao"].ToString();
                                    txtESCOLARIDADE.Text = reader["escolaridade"].ToString();
                                }
                                else
                                {
                                    // Limpar os campos se o cliente não for encontrado
                                    LimparCamposCliente();
                                }
                            }
                            // Fechar a conexão para reutilizá-la na próxima operação
                            connection.Close();

                            // Agora, buscar os exames do cliente
                            BuscarExamesPorCliente(idCliente);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao buscar dados do cliente: " + ex.Message);
                    }
                }
                else
                {
                    // Limpar os campos se o ID não for um número válido
                    LimparCamposCliente();
                }
            }
            else
            {
                // Limpar os campos se o ID estiver vazio
                LimparCamposCliente();
            }
        }
        private void LimparCamposCliente()
        {
            txtNOMECOMPLETO.Text = string.Empty;
            txtNASCIMENTO.Text = string.Empty;
            txtIDADE.Text = string.Empty;
            txtPROFISSAO.Text = string.Empty;
            txtESCOLARIDADE.Text = string.Empty;
        }
        // Exemplo de chamada a partir de outro formulário
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idExame = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_exames"].Value);
                Console.WriteLine($"Selecionado id_exame na DataGridView: {idExame}");

                // Supondo que você tenha uma instância da classe ClasseReceita
                ClasseReceita receita = new ClasseReceita();
                receita.GerarReceita("Título do Documento", idExame, true);
            }
        }



        private void BuscarExamesPorCliente(int idCliente)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT id_exames, id_cliente, nome_completo, data_do_exame 
                         FROM tb_exames 
                         WHERE id_cliente = @idCliente
                         ORDER BY data_do_exame DESC";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    try
                    {
                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

                        // Ajuste das colunas
                        dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Renomear colunas
                        dataGridView1.Columns["id_cliente"].HeaderText = "CLIENTE";
                        dataGridView1.Columns["nome_completo"].HeaderText = "NOME COMPLETO";
                        dataGridView1.Columns["data_do_exame"].HeaderText = "DATA DOS EXAMES";

                        // Ajustar largura das colunas
                        dataGridView1.Columns["id_cliente"].Width = 50;
                        dataGridView1.Columns["nome_completo"].Width = 300;
                        dataGridView1.Columns["data_do_exame"].Width = 130;

                        // Tornar a coluna id_exames visível se necessário, ou ocultá-la se não for necessária
                        dataGridView1.Columns["id_exames"].Visible = true; // Mantenha visível se precisar, ou defina como false para ocultar
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao buscar exames: " + ex.Message);
                    }
                }
            }
        }

        private void LimparCampos()
        {
            // Limpar TextBoxes
            txtDATADOEXAME.Text = string.Empty;
            txtIDCLIENTE.Text = string.Empty;
            txtMOTIVO.Text = string.Empty;
            txtHISTPESSOAL.Text = string.Empty;
            txtRXUSOADD.Text = string.Empty;
            txtSEMADD.Text = string.Empty;
            txtRXFINALADD.Text = string.Empty;
            txtCIRURGIA.Text = string.Empty;
            txtULTIMACONSULTA.Text = string.Empty;
            txtMEDICAMENTO.Text = string.Empty;

            // Limpar ComboBoxes
            cbRXUSOESFOD.SelectedIndex = -1;
            cbRXUSOESFOE.SelectedIndex = -1;
            cbRXUSOCILOD.SelectedIndex = -1;
            cbRXUSOCILOE.SelectedIndex = -1;
            cbRXUSOEIXOOD.SelectedIndex = -1;
            cbRXUSOEIXOOE.SelectedIndex = -1;
            cbRXUSOAVOD.SelectedIndex = -1;
            cbRXUSOAVOE.SelectedIndex = -1;
            cbRXUSOAVADD.SelectedIndex = -1;
            cbSEMLONGOD.SelectedIndex = -1;
            cbSEMLONGOE.SelectedIndex = -1;
            cbSEMPERTOOD.SelectedIndex = -1;
            cbSEMPERTOOE.SelectedIndex = -1;
            cbSEMPINOD.SelectedIndex = -1;
            cbSEMPINOE.SelectedIndex = -1;
            cbSEMPINADD.SelectedIndex = -1;
            cbDINAESFOD.SelectedIndex = -1;
            cbDINAESFOE.SelectedIndex = -1;
            cbDINACILOD.SelectedIndex = -1;
            cbDINACILOE.SelectedIndex = -1;
            cbDINAEIXOOD.SelectedIndex = -1;
            cbDINAEIXOOE.SelectedIndex = -1;
            cbESTAESFOD.SelectedIndex = -1;
            cbESTAESFOE.SelectedIndex = -1;
            cbESTACILOD.SelectedIndex = -1;
            cbESTACILOE.SelectedIndex = -1;
            cbESTAEIXOOD.SelectedIndex = -1;
            cbESTAEIXOOE.SelectedIndex = -1;
            cbFINALESFOD.SelectedIndex = -1;
            cbFINALESFOE.SelectedIndex = -1;
            cbFINALCILOD.SelectedIndex = -1;
            cbFINALCILOE.SelectedIndex = -1;
            cbFINALEIXOOD.SelectedIndex = -1;
            cbFINALEIXOOE.SelectedIndex = -1;
            cbFINALAVOD.SelectedIndex = -1;
            cbFINALAVOE.SelectedIndex = -1;
            cbFINALAVADD.SelectedIndex = -1;

            // Limpar CheckBoxes
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            checkBox19.Checked = false;
            checkBox20.Checked = false;
            checkBox21.Checked = false;
            checkBox22.Checked = false;
            checkBox23.Checked = false;
            checkBox24.Checked = false;
            checkBox25.Checked = false;
            checkBox26.Checked = false;
            checkBox27.Checked = false;
            checkBox28.Checked = false;
            checkBox29.Checked = false;
            checkBox30.Checked = false;
            checkBox31.Checked = false;
            checkBox32.Checked = false;
            checkBox33.Checked = false;
            checkBox34.Checked = false;
            checkBox35.Checked = false;
        }

        private void btnSALVAR_Click(object sender, EventArgs e)
        {
            try
            {
                // Supondo que a data seja recebida no formato "dd/MM/yyyy"
                string dataInput = txtDATADOEXAME.Text; // Pegando a data de um TextBox
                string dataInput2 = txtNASCIMENTO.Text; // Pegando a data de um TextBox
                DateTime dataConvertida = DateTime.ParseExact(dataInput, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                // Convertendo a data para o formato MySQL "yyyy-MM-dd"
                string dataParaMySQL = dataConvertida.ToString("yyyy-MM-dd");

                Conexao conexao = new Conexao();
                string query = "INSERT INTO tb_exames (" +
                    "cefaleia_frontal, cefaleia_temporal, dor_ocular, hiperemia, prurido, ardencia, lacrimejamento, exame_rotina, renovar_oculos, moscas_volantes, " +
                    "motivos, historico_depressao, historico_ansiedade, historico_cardiaco, historico_tireoide, historico_enxaqueca, historico_labirintite, historico_hipertensao, historico_diabete, historico_pessoal_outros, " +
                    "cirurgia_sim, cirurgia_nao, cirurgia_quais, usa_oculos_sim, usa_oculos_nao, ultima_consulta, usa_medicamento_sim, usa_medicamento_nao, usa_med_quais_depressao, usa_med_quais_ansiedade, usa_med_quais_cardiaco, usa_med_quais_tireoide, usa_med_quais_enxaqueca, usa_med_quais_labirintite, usa_med_quais_hipertensao, usa_med_quais_diabete, usa_med_quais_outros, " +
                    "familiar_glaucoma, familiar_diabete, familiar_hipertensao, rx_uso_esf_od, rx_uso_esf_oe, rx_uso_cil_od, rx_uso_cil_oe, rx_uso_eixo_od, rx_uso_eixo_oe, rx_uso_av_od, rx_uso_av_oe, rx_uso_av_add, rx_uso_add, " +
                    "sem_long_od, sem_long_oe, sem_perto_od, sem_perto_oe, sem_pin_od, sem_pin_oe, sem_pin_add, sem_add, " +
                    "dina_esf_od, dina_esf_oe, dina_cil_od, dina_cil_oe, dina_eixo_od, dina_eixo_oe, esta_esf_od, esta_esf_oe, esta_cil_od, esta_cil_oe, esta_eixo_od, esta_eixo_oe, " +
                    "rx_final_esf_od, rx_final_esf_oe, rx_final_cil_od, rx_final_cil_oe, rx_final_eixo_od, rx_final_eixo_oe, rx_final_av_od, rx_final_av_oe, rx_final_av_add, rx_final_add, " +
                    "id_cliente, data_do_exame, nome_completo, nascimento, idade, profissao, escolaridade" +
                    ") VALUES (" +
                    "@cefaleia_frontal, @cefaleia_temporal, @dor_ocular, @hiperemia, @prurido, @ardencia, @lacrimejamento, @exame_rotina, @renovar_oculos, @moscas_volantes, " +
                    "@motivos, @historico_depressao, @historico_ansiedade, @historico_cardiaco, @historico_tireoide, @historico_enxaqueca, @historico_labirintite, @historico_hipertensao, @historico_diabete, @historico_pessoal_outros, " +
                    "@cirurgia_sim, @cirurgia_nao, @cirurgia_quais, @usa_oculos_sim, @usa_oculos_nao, @ultima_consulta, @usa_medicamento_sim, @usa_medicamento_nao, @usa_med_quais_depressao, @usa_med_quais_ansiedade, @usa_med_quais_cardiaco, @usa_med_quais_tireoide, @usa_med_quais_enxaqueca, @usa_med_quais_labirintite, @usa_med_quais_hipertensao, @usa_med_quais_diabete, @usa_med_quais_outros, " +
                    "@familiar_glaucoma, @familiar_diabete, @familiar_hipertensao, @rx_uso_esf_od, @rx_uso_esf_oe, @rx_uso_cil_od, @rx_uso_cil_oe, @rx_uso_eixo_od, @rx_uso_eixo_oe, @rx_uso_av_od, @rx_uso_av_oe, @rx_uso_av_add, @rx_uso_add, " +
                    "@sem_long_od, @sem_long_oe, @sem_perto_od, @sem_perto_oe, @sem_pin_od, @sem_pin_oe, @sem_pin_add, @sem_add, " +
                    "@dina_esf_od, @dina_esf_oe, @dina_cil_od, @dina_cil_oe, @dina_eixo_od, @dina_eixo_oe, @esta_esf_od, @esta_esf_oe, @esta_cil_od, @esta_cil_oe, @esta_eixo_od, @esta_eixo_oe, " +
                    "@rx_final_esf_od, @rx_final_esf_oe, @rx_final_cil_od, @rx_final_cil_oe, @rx_final_eixo_od, @rx_final_eixo_oe, @rx_final_av_od, @rx_final_av_oe, @rx_final_av_add, @rx_final_add, " +
                    "@id_cliente, '" + dataParaMySQL + "', @nome_completo, '" + dataParaMySQL + "', @idade, @profissao, @escolaridade" +
                    ")";

                // Adicionando parâmetros e seus respectivos valores
                MySqlConnector.MySqlParameter[] parametros = {
            new MySqlConnector.MySqlParameter("@cefaleia_frontal", checkBox1.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@cefaleia_temporal", checkBox2.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@dor_ocular", checkBox3.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@hiperemia", checkBox4.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@prurido", checkBox5.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@ardencia", checkBox6.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@lacrimejamento", checkBox7.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@exame_rotina", checkBox8.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@renovar_oculos", checkBox9.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@moscas_volantes", checkBox10.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@motivos", txtMOTIVO.Text),
            new MySqlConnector.MySqlParameter("@historico_depressao", checkBox18.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@historico_ansiedade", checkBox17.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@historico_cardiaco", checkBox16.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@historico_tireoide", checkBox15.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@historico_enxaqueca", checkBox14.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@historico_labirintite", checkBox13.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@historico_hipertensao", checkBox12.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@historico_diabete", checkBox11.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@historico_pessoal_outros", txtHISTPESSOAL.Text),
            new MySqlConnector.MySqlParameter("@cirurgia_sim", checkBox30.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@cirurgia_nao", checkBox31.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@cirurgia_quais", txtCIRURGIA.Text),
            new MySqlConnector.MySqlParameter("@usa_oculos_sim", checkBox33.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_oculos_nao", checkBox32.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@ultima_consulta", txtULTIMACONSULTA.Text),
            new MySqlConnector.MySqlParameter("@usa_medicamento_sim", checkBox35.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_medicamento_nao", checkBox34.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_depressao", checkBox19.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_ansiedade", checkBox20.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_cardiaco", checkBox21.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_tireoide", checkBox22.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_enxaqueca", checkBox23.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_labirintite", checkBox24.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_hipertensao", checkBox25.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_diabete", checkBox26.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@usa_med_quais_outros", txtMEDICAMENTO.Text),
            new MySqlConnector.MySqlParameter("@familiar_glaucoma", checkBox27.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@familiar_diabete", checkBox28.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@familiar_hipertensao", checkBox29.Checked ? 1 : 0),
            new MySqlConnector.MySqlParameter("@rx_uso_esf_od", cbRXUSOESFOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_esf_oe", cbRXUSOESFOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_cil_od", cbRXUSOCILOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_cil_oe", cbRXUSOCILOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_eixo_od", cbRXUSOEIXOOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_eixo_oe", cbRXUSOEIXOOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_av_od", cbRXUSOAVOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_av_oe", cbRXUSOAVOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_av_add", cbRXUSOAVADD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_uso_add", txtRXUSOADD.Text),
            new MySqlConnector.MySqlParameter("@sem_long_od", cbSEMLONGOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@sem_long_oe", cbSEMLONGOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@sem_perto_od", cbSEMPERTOOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@sem_perto_oe", cbSEMPERTOOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@sem_pin_od", cbSEMPINOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@sem_pin_oe", cbSEMPINOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@sem_pin_add", cbSEMPINADD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@sem_add", txtSEMADD.Text),
            new MySqlConnector.MySqlParameter("@dina_esf_od", cbDINAESFOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@dina_esf_oe", cbDINAESFOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@dina_cil_od", cbDINACILOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@dina_cil_oe", cbDINACILOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@dina_eixo_od", cbDINAEIXOOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@dina_eixo_oe", cbDINAEIXOOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@esta_esf_od", cbESTAESFOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@esta_esf_oe", cbESTAESFOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@esta_cil_od", cbESTACILOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@esta_cil_oe", cbESTACILOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@esta_eixo_od", cbESTAEIXOOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@esta_eixo_oe", cbESTAEIXOOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_esf_od", cbFINALESFOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_esf_oe", cbFINALESFOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_cil_od", cbFINALCILOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_cil_oe", cbFINALCILOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_eixo_od", cbFINALEIXOOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_eixo_oe", cbFINALEIXOOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_av_od", cbFINALAVOD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_av_oe", cbFINALAVOE.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_av_add", cbFINALAVADD.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@rx_final_add", txtRXFINALADD.Text),
            new MySqlConnector.MySqlParameter("@id_cliente", txtIDCLIENTE.Text),
            new MySqlConnector.MySqlParameter("@data_do_exame", txtDATADOEXAME.Text),
            new MySqlConnector.MySqlParameter("@nome_completo", txtNOMECOMPLETO.Text),
            new MySqlConnector.MySqlParameter("@nascimento", txtNASCIMENTO.Text),
            new MySqlConnector.MySqlParameter("@idade", txtIDADE.Text),
            new MySqlConnector.MySqlParameter("@profissao", txtPROFISSAO.Text),
            new MySqlConnector.MySqlParameter("@escolaridade", txtESCOLARIDADE.Text)
        };

                // Executando a query
                conexao.ExecuteQuery(query, parametros);
                MessageBox.Show("Registro salvo com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar os dados: " + ex.Message);
            }
        }

    }

}