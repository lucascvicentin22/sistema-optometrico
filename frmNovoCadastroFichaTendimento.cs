using System;
using System.Data;
using System.Windows.Forms;
using iTextParagraph = iText.Layout.Element.Paragraph;
using iTextSharpParagraph = iTextSharp.text.Paragraph;
// Alias para desambiguar PageSize
using iTextPageSize = iText.Kernel.Geom.PageSize;
using System.ComponentModel.Design;
using System.Globalization;
using iText.StyledXmlParser.Jsoup.Nodes;
using SistemaOptometrico;
using System.Collections.Generic;
using MySqlConnector;

namespace SistemaOptometrico

{
    public partial class frmNovoCadastroFichaTendimento : Form
    {
        private ContextMenuStrip contextMenuStrip1;
        private int idExameSelecionado = -1; // -1 ou outro valor que indique que nenhum exame foi selecionado ainda
        private Conexao conexao = new Conexao();
        public frmNovoCadastroFichaTendimento()
        {
            InitializeComponent();

            cbNome.Items.Clear();
            cbEndereco.Items.Clear();
            txtCidadeConsultorio.Clear();
            txtIdConsultorio.Clear();

            CarregarNomesConsultorios();


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
            gerarAtestadoMenuItem});
            // Associar o ContextMenuStrip à DataGridView
            dataGridView1.ContextMenuStrip = contextMenuStrip1;

            // Eventos de clique para cada opção do menu
            gerarFichaMenuItem.Click += new EventHandler(GerarFichaMenuItem_Click);
            gerarReceitaMenuItem.Click += new EventHandler(GerarReceitaMenuItem_Click);
            gerarAtestadoMenuItem.Click += new EventHandler(GerarAtestadoMenuItem_Click);
        }
        private void GerarFichaMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void GerarAtestadoMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void GerarReceitaMenuItem_Click(object sender, EventArgs e)
        {
            // Verifique se um exame foi selecionado
            if (idExameSelecionado != -1)
            {
                ClasseReceita receita = new ClasseReceita();
                receita.GerarReceita("RECEITA OPTOMÉTRICA", idExameSelecionado, true);
            }
            else
            {
                MessageBox.Show("Por favor, selecione um exame.");
            }
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
                        Conexao conexao = new Conexao();
                        string queryCliente = @"
                    SELECT p.nome_completo, p.nascimento, p.idade, p.profissao, p.escolaridade, 
                           e.id_consultorio, c.nome AS nome_consultorio, c.endereco AS endereco_consultorio 
                    FROM tb_precadastro p
                    LEFT JOIN tb_exames e ON p.id_cliente = e.id_cliente
                    LEFT JOIN tb_consultorio c ON e.id_consultorio = c.id_consultorio
                    WHERE p.id_cliente = @id_cliente";

                        MySqlParameter[] parametros = new MySqlParameter[]
                        {
                    new MySqlParameter("@id_cliente", idCliente)
                        };

                        DataTable clienteData = conexao.BuscarDados(queryCliente, parametros);

                        if (clienteData.Rows.Count > 0)
                        {
                            DataRow reader = clienteData.Rows[0];

                            // Preencher campos do cliente
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

                            // Preencher as ComboBox do consultório
                            cbNome.Text = reader["nome_consultorio"].ToString();
                            cbEndereco.Text = reader["endereco_consultorio"].ToString();
                        }
                        else
                        {
                            LimparCamposCliente(); // Se não encontrar o cliente
                            MessageBox.Show("Cliente não encontrado.");
                        }

                        // Buscar os exames do cliente
                        BuscarExamesPorCliente(idCliente);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao buscar dados do cliente: " + ex.Message);
                    }
                }
                else
                {
                    LimparCamposCliente(); // Se o ID não for válido
                    MessageBox.Show("ID do cliente inválido.");
                }
            }
            else
            {
                LimparCamposCliente(); // Se o campo estiver vazio
            }
        }
        private void LimparCamposCliente()
        {
            txtNOMECOMPLETO.Text = string.Empty;
            txtNASCIMENTO.Text = string.Empty;
            txtIDADE.Text = string.Empty;
            txtPROFISSAO.Text = string.Empty;
            txtESCOLARIDADE.Text = string.Empty;
            cbNome.Text = string.Empty;
            cbEndereco.Text = string.Empty;

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

                // Buscar o id_consultorio com base no nome e endereço selecionados
                //string queryConsultorio = "SELECT id_consultorio FROM tb_consultorio WHERE nome = @nome AND endereco = @endereco";
                MySqlConnector.MySqlParameter[] parametrosConsultorio = {
                new MySqlConnector.MySqlParameter("@nome", cbNome.SelectedItem?.ToString() ?? string.Empty),
                new MySqlConnector.MySqlParameter("@endereco", cbEndereco.SelectedItem?.ToString() ?? string.Empty)};

                string query = "INSERT INTO tb_exames (" +
                "cefaleia_frontal, cefaleia_temporal, dor_ocular, hiperemia, prurido, ardencia, lacrimejamento, exame_rotina, renovar_oculos, moscas_volantes, " +
                "motivos, historico_depressao, historico_ansiedade, historico_cardiaco, historico_tireoide, historico_enxaqueca, historico_labirintite, historico_hipertensao, historico_diabete, historico_pessoal_outros, " +
                "cirurgia_sim, cirurgia_nao, cirurgia_quais, usa_oculos_sim, usa_oculos_nao, ultima_consulta, usa_medicamento_sim, usa_medicamento_nao, usa_med_quais_depressao, usa_med_quais_ansiedade, usa_med_quais_cardiaco, usa_med_quais_tireoide, usa_med_quais_enxaqueca, usa_med_quais_labirintite, usa_med_quais_hipertensao, usa_med_quais_diabete, usa_med_quais_outros, " +
                "familiar_glaucoma, familiar_diabete, familiar_hipertensao, rx_uso_esf_od, rx_uso_esf_oe, rx_uso_cil_od, rx_uso_cil_oe, rx_uso_eixo_od, rx_uso_eixo_oe, rx_uso_av_od, rx_uso_av_oe, rx_uso_av_add, rx_uso_add, " +
                "sem_long_od, sem_long_oe, sem_perto_od, sem_perto_oe, sem_pin_od, sem_pin_oe, sem_pin_add, sem_add, " +
                "dina_esf_od, dina_esf_oe, dina_cil_od, dina_cil_oe, dina_eixo_od, dina_eixo_oe, esta_esf_od, esta_esf_oe, esta_cil_od, esta_cil_oe, esta_eixo_od, esta_eixo_oe, " +
                "rx_final_esf_od, rx_final_esf_oe, rx_final_cil_od, rx_final_cil_oe, rx_final_eixo_od, rx_final_eixo_oe, rx_final_av_od, rx_final_av_oe, rx_final_av_add, rx_final_add, " +
                "id_cliente, data_do_exame, nome_completo, nascimento, idade, profissao, escolaridade, nome_consultorio, endereco_consultorio, cidade_consultorio, id_consultorio" +
                ") VALUES (" +
                "@cefaleia_frontal, @cefaleia_temporal, @dor_ocular, @hiperemia, @prurido, @ardencia, @lacrimejamento, @exame_rotina, @renovar_oculos, @moscas_volantes, " +
                "@motivos, @historico_depressao, @historico_ansiedade, @historico_cardiaco, @historico_tireoide, @historico_enxaqueca, @historico_labirintite, @historico_hipertensao, @historico_diabete, @historico_pessoal_outros, " +
                "@cirurgia_sim, @cirurgia_nao, @cirurgia_quais, @usa_oculos_sim, @usa_oculos_nao, @ultima_consulta, @usa_medicamento_sim, @usa_medicamento_nao, @usa_med_quais_depressao, @usa_med_quais_ansiedade, @usa_med_quais_cardiaco, @usa_med_quais_tireoide, @usa_med_quais_enxaqueca, @usa_med_quais_labirintite, @usa_med_quais_hipertensao, @usa_med_quais_diabete, @usa_med_quais_outros, " +
                "@familiar_glaucoma, @familiar_diabete, @familiar_hipertensao, @rx_uso_esf_od, @rx_uso_esf_oe, @rx_uso_cil_od, @rx_uso_cil_oe, @rx_uso_eixo_od, @rx_uso_eixo_oe, @rx_uso_av_od, @rx_uso_av_oe, @rx_uso_av_add, @rx_uso_add, " +
                "@sem_long_od, @sem_long_oe, @sem_perto_od, @sem_perto_oe, @sem_pin_od, @sem_pin_oe, @sem_pin_add, @sem_add, " +
                "@dina_esf_od, @dina_esf_oe, @dina_cil_od, @dina_cil_oe, @dina_eixo_od, @dina_eixo_oe, @esta_esf_od, @esta_esf_oe, @esta_cil_od, @esta_cil_oe, @esta_eixo_od, @esta_eixo_oe, " +
                "@rx_final_esf_od, @rx_final_esf_oe, @rx_final_cil_od, @rx_final_cil_oe, @rx_final_eixo_od, @rx_final_eixo_oe, @rx_final_av_od, @rx_final_av_oe, @rx_final_av_add, @rx_final_add, " +
                "@id_cliente, '" + dataParaMySQL + "', @nome_completo, '" + dataParaMySQL + "', @idade, @profissao, @escolaridade, @nome_consultorio, @endereco_consultorio, @cidade_consultorio,@id_consultorio" +
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
            new MySqlConnector.MySqlParameter("@escolaridade", txtESCOLARIDADE.Text),
            new MySqlConnector.MySqlParameter("@nome_consultorio", cbNome.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@endereco_consultorio", cbEndereco.SelectedItem?.ToString() ?? string.Empty),
            new MySqlConnector.MySqlParameter("@cidade_consultorio", txtCidadeConsultorio.Text),
            new MySqlConnector.MySqlParameter("@id_consultorio", txtIdConsultorio.Text)
            };

                // Executando a query
                conexao.ExecuteQuery(query, parametros);
                MessageBox.Show("Registro salvo com sucesso!");
                AtualizarDadosDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar os dados: " + ex.Message);
            }
        }
        private void AtualizarDadosDataGridView()
        {
            // Suponha que você tenha uma maneira de obter o idCliente, por exemplo, de um ComboBox ou TextBox
            int idCliente = ObterIdCliente(); // Implementar este método para obter o ID do cliente desejado

            // Chama o método para atualizar o DataGridView com o idCliente
            AtualizarDataGridView(idCliente);
        }
        private void AtualizarDataGridView(int idCliente)
        {
            try
            {
                // Criar uma instância da classe Conexao
                Conexao conexao = new Conexao();

                // Usar o método ObterExamesPorCliente para buscar os dados
                DataTable dataTable = conexao.ObterExamesPorCliente(idCliente);

                dataGridView1.DataSource = dataTable;

                // Ajuste das colunas
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Renomear colunas
                dataGridView1.Columns["id_cliente"].HeaderText = "CLIENTE";
                dataGridView1.Columns["nome_completo"].HeaderText = "NOME COMPLETO";
                dataGridView1.Columns["data_do_exame"].HeaderText = "DATA DOS EXAMES";
                dataGridView1.Columns["nome_consultorio"].HeaderText = "CONSULTÓRIO";
                dataGridView1.Columns["endereco"].HeaderText = "ENDEREÇO";
                dataGridView1.Columns["cidade"].HeaderText = "CIDADE";

                // Ajustar largura das colunas
                dataGridView1.Columns["id_cliente"].Width = 50;
                dataGridView1.Columns["nome_completo"].Width = 300;
                dataGridView1.Columns["data_do_exame"].Width = 130;
                dataGridView1.Columns["nome_consultorio"].Width = 200;
                dataGridView1.Columns["endereco"].Width = 200;
                dataGridView1.Columns["cidade"].Width = 150;

                // Tornar a coluna id_exame visível se necessário, ou ocultá-la se não for necessária
                dataGridView1.Columns["id_exame"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o DataGridView: " + ex.Message);
            }
        }
        private int ObterIdCliente()
        {
            int idCliente = 0; // Valor padrão se não encontrado

            // Obtém o valor do TextBox e remove espaços em branco
            string inputIdCliente = txtIDCLIENTE.Text.Trim();

            // Verifica se o input não está vazio
            if (!string.IsNullOrEmpty(inputIdCliente))
            {
                try
                {
                    // Criar uma instância da classe Conexao
                    Conexao conexao = new Conexao();

                    // Chamar o método ObterIdCliente da classe Conexao
                    idCliente = conexao.ObterIdCliente(int.Parse(inputIdCliente));

                    // Verifica se o cliente foi encontrado
                    if (idCliente == 0)
                    {
                        MessageBox.Show("Cliente não encontrado.");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Por favor, insira um ID de cliente válido.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar ID do cliente: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira um ID de cliente válido.");
            }

            return idCliente; // Retorna o ID do cliente encontrado ou 0 se não encontrado
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
        private void BuscarExamesPorCliente(int idCliente)
        {
            try
            {
                // Criar uma instância da classe Conexao
                Conexao conexao = new Conexao();

                // Usar o método ObterExamesPorCliente para buscar os dados
                DataTable dataTable = conexao.ObterExamesPorCliente(idCliente);

                dataGridView1.DataSource = dataTable;

                // Ajuste das colunas
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Renomear colunas
                dataGridView1.Columns["id_cliente"].HeaderText = "CLIENTE";
                dataGridView1.Columns["nome_completo"].HeaderText = "NOME COMPLETO";
                dataGridView1.Columns["data_do_exame"].HeaderText = "DATA DOS EXAMES";
                dataGridView1.Columns["nome_consultorio"].HeaderText = "CONSULTÓRIO";
                dataGridView1.Columns["endereco"].HeaderText = "ENDEREÇO";
                dataGridView1.Columns["cidade"].HeaderText = "CIDADE";

                // Ajustar largura das colunas
                dataGridView1.Columns["id_cliente"].Width = 50;
                dataGridView1.Columns["nome_completo"].Width = 300;
                dataGridView1.Columns["data_do_exame"].Width = 130;
                dataGridView1.Columns["nome_consultorio"].Width = 200;
                dataGridView1.Columns["endereco"].Width = 200;
                dataGridView1.Columns["cidade"].Width = 150;

                // Tornar a coluna id_exame visível se necessário, ou ocultá-la se não for necessária
                dataGridView1.Columns["id_exame"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar exames: " + ex.Message);
            }
        }

        private DateTime dataExameSelecionado;
        private void LoadDataGridView()
        {
            try
            {
                // Criar uma instância da classe Conexao
                Conexao conexao = new Conexao();

                // Usar o método ObterExames para buscar os dados
                DataTable dataTable = conexao.ObterExames();

                // Atribuir o DataTable ao DataGridView
                dataGridView1.DataSource = dataTable;

                // Ajuste das colunas
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Renomear colunas, se necessário
                dataGridView1.Columns["id_exame"].HeaderText = "ID EXAME";
                dataGridView1.Columns["data_do_exame"].HeaderText = "DATA DO EXAME";
                dataGridView1.Columns["id_consultorio"].HeaderText = "ID CONSULTÓRIO";
                dataGridView1.Columns["nome_consultorio"].HeaderText = "NOME CONSULTÓRIO";
                dataGridView1.Columns["endereco_consultorio"].HeaderText = "ENDEREÇO CONSULTÓRIO";
                dataGridView1.Columns["cidade_consultorio"].HeaderText = "CIDADE CONSULTÓRIO";

                // Ajustar largura das colunas
                dataGridView1.Columns["id_exame"].Width = 50;
                dataGridView1.Columns["data_do_exame"].Width = 130;
                dataGridView1.Columns["id_consultorio"].Width = 50;
                dataGridView1.Columns["nome_consultorio"].Width = 200;
                dataGridView1.Columns["endereco_consultorio"].Width = 200;
                dataGridView1.Columns["cidade_consultorio"].Width = 150;

                // Ocultar a coluna id_exame se não for necessária
                dataGridView1.Columns["id_exame"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }
        private void CarregarNomesConsultorios()
        {
            try
            {
                // Criar uma instância da classe Conexao
                Conexao conexao = new Conexao();

                // Usar o método para obter os nomes dos consultórios
                DataTable dataTable = conexao.ObterNomesConsultorios();

                // Limpar os itens existentes no ComboBox
                cbNome.Items.Clear();

                // Adicionar os nomes ao ComboBox
                foreach (DataRow row in dataTable.Rows)
                {
                    cbNome.Items.Add(row["nome"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar nomes dos consultórios: " + ex.Message);
            }
        }
        private void txtIdConsultorio_TextChanged(object sender, EventArgs e)
        {

        }
        private void cbNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nomeConsultorio = cbNome.SelectedItem?.ToString() ?? string.Empty;

            if (!string.IsNullOrEmpty(nomeConsultorio))
            {
                // Criar uma instância da classe Conexao
                Conexao conexao = new Conexao();

                // Usar o método para obter dados do consultório
                DataTable dataTable = conexao.ObterDadosConsultorio(nomeConsultorio);

                cbEndereco.Items.Clear();

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    // Preenche o txtIdConsultorio com o id_consultorio
                    txtIdConsultorio.Text = row["id_consultorio"].ToString();

                    // Adiciona o endereço no cbEndereco
                    foreach (DataRow r in dataTable.Rows)
                    {
                        cbEndereco.Items.Add(r["endereco"].ToString());
                    }

                    // Preenche a cidade no campo txtCidadeConsultorio
                    txtCidadeConsultorio.Text = row["cidade"].ToString();

                    // Seleciona o primeiro endereço encontrado (caso haja mais de um)
                    if (cbEndereco.Items.Count > 0)
                    {
                        cbEndereco.SelectedIndex = 0;
                    }
                }
            }
        }
        private void cbEndereco_SelectedIndexChanged(object sender, EventArgs e)
        {
            string enderecoConsultorio = cbEndereco.SelectedItem?.ToString() ?? string.Empty;
            string nomeConsultorio = cbNome.SelectedItem?.ToString() ?? string.Empty;

            if (!string.IsNullOrEmpty(enderecoConsultorio) && !string.IsNullOrEmpty(nomeConsultorio))
            {
                // Criar uma instância da classe Conexao
                Conexao conexao = new Conexao();

                // Usar o método para obter dados do consultório
                DataTable dataTable = conexao.ObterDadosConsultorioPorNomeEEndereco(nomeConsultorio, enderecoConsultorio);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    // Preenche o id_consultorio
                    txtIdConsultorio.Text = row["id_consultorio"].ToString();

                    // Preenche a cidade no campo txtCidadeConsultorio
                    txtCidadeConsultorio.Text = row["cidade"].ToString();
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifique se a célula clicada é válida (ignorar se for o cabeçalho)
            if (e.RowIndex >= 0)
            {
                // Obtenha a linha atual
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Capture o valor do id_exame e a data_do_exame
                idExameSelecionado = Convert.ToInt32(row.Cells["id_exame"].Value);
                dataExameSelecionado = Convert.ToDateTime(row.Cells["data_do_exame"].Value);

            }
        }
    }
}
