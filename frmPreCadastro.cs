using System;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;

namespace SistemaOptometrico
{
    public partial class frmPreCadastro : Form
    {
        private Conexao conexao = new Conexao();

        public frmPreCadastro()
        {
            InitializeComponent();
            // Configurando o MaskedTextBox para formato de data
            txtNASCIMENTO.Mask = "00/00/0000";
            txtNASCIMENTO.ValidatingType = typeof(DateTime);
        }

        private void ClearFields()
        {
            txtNOMECOMPLETO.Clear();
            txtCPF.Clear();
            txtNASCIMENTO.Clear();
            txtIDADE.Clear();
            txtWHATSAPP.Clear();
            txtRUA.Clear();
            txtCIDADE.Clear();
            txtPROFISSAO.Clear();
            cbESCOLARIDADE.SelectedIndex = -1;
            cbSEXO.SelectedIndex = -1;
        }

        public int CalcularIdade(DateTime dataNascimento)
        {
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - dataNascimento.Year;

            if (dataNascimento > hoje.AddYears(-idade)) idade--;

            return idade;
        }

        private void btnSALVAR_Click(object sender, EventArgs e)
        {
            // Coleta de dados do formulário
            string nomeCompleto = txtNOMECOMPLETO.Text;
            string cpf = txtCPF.Text;
            DateTime nascimento;
            if (!DateTime.TryParseExact(txtNASCIMENTO.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out nascimento))
            {
                MessageBox.Show("Data de nascimento inválida. Por favor, insira no formato dd/MM/yyyy.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idade = int.Parse(txtIDADE.Text); // Certifique-se de que seja um número válido
            string whatsapp = txtWHATSAPP.Text;
            string rua = txtRUA.Text;
            string bairro = txtBAIRRO.Text;
            string cidade = txtCIDADE.Text;
            string profissao = txtPROFISSAO.Text;
            string escolaridade = cbESCOLARIDADE.SelectedItem.ToString();
            string sexo = cbSEXO.SelectedItem.ToString();

            // Criação da conexão e chamada do método de inserção
            Conexao conexao = new Conexao();
            try
            {
                conexao.InserirPreCadastro(nomeCompleto, cpf, nascimento, idade, whatsapp, rua, bairro, cidade, profissao, escolaridade, sexo);
                MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpa os campos do formulário
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNASCIMENTO_TextChanged(object sender, EventArgs e)
        {
            DateTime dataNascimento;

            // Verifica se a data é válida
            if (DateTime.TryParse(txtNASCIMENTO.Text, out dataNascimento))
            {
                int idade = CalcularIdade(dataNascimento);
                txtIDADE.Text = idade.ToString();
            }
            else
            {
                txtIDADE.Text = "Data inválida";
            }
        }
    }
}
