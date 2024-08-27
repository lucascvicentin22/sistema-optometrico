using System;
using System.IO; // Para System.IO.Path
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom; // Para PageSize
using iText.IO.Image;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using iText.Layout.Borders; // Para Border

namespace SistemaOptometrico
{
    public class ClasseReceita
    {
        private void SomeMethod()
        {
            // Criar uma instância da classe ClasseReceita
            ClasseReceita receita = new ClasseReceita();

            // Chamar o método GerarReceita
            receita.GerarReceita("Título da Receita", 123, true);
        }

        private string connectionString = "Server=localhost;Database=db_clinica;User ID=root;Password=2707;";

        public void GerarReceita(string titulo, int idExame, bool isReceita)
        {
            // Obter informações do exame
            var informacoes = ObterInformacoesExame(idExame);

            // Obter dados da receita (se necessário)
            (string EsfOd, string EsfOe, string CilOd, string CilOe, string EixoOd, string EixoOe, string AvOd, string AvOe, string AvAdd, string rxfinaladd)? dadosReceita = null;

            if (isReceita)
            {
                dadosReceita = ObterDadosReceita(idExame);
            }

            // Caminho para a imagem de fundo
            string caminhoImagemFundo = @"C:\Users\lucas\OneDrive\Área de Trabalho\SistemaOptometrico\IMGFUNDORECEITA.png";

            // Criação do PDF na memória
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(memoryStream))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        // Configurar tamanho da página
                        iText.Kernel.Geom.PageSize pageSize = isReceita ? iText.Kernel.Geom.PageSize.A5 : iText.Kernel.Geom.PageSize.A4;
                        pdf.SetDefaultPageSize(pageSize);

                        using (Document document = new Document(pdf))
                        {
                            // Carregar a imagem de fundo
                            ImageData imageData = ImageDataFactory.Create(caminhoImagemFundo);
                            var image = new Image(imageData);

                            // Ajustar a imagem para o tamanho da página
                            image.SetFixedPosition(0, 0);
                            image.ScaleAbsolute(pageSize.GetWidth(), pageSize.GetHeight());

                            // Adicionar a imagem ao documento
                            document.Add(image);

                            // Adicionar título
                            var header = new Paragraph(titulo)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(isReceita ? 16 : 20);
                            document.Add(header);

                            // Adicionar informações do exame
                            document.Add(new Paragraph($"Nome Completo: {informacoes.NomeCompleto}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10)
                                .SetMarginTop(5));
                            document.Add(new Paragraph($"Data do Exame: {informacoes.DataDoExame:dd/MM/yyyy}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10)
                                .SetMarginTop(5));

                            if (isReceita && dadosReceita.HasValue)
                            {
                                var receita = dadosReceita.Value; // Descompactar a tupla

                                // Criar a tabela para receita
                                var tabela = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 }))
                                    .SetWidth(UnitValue.CreatePercentValue(100))
                                    .SetTextAlignment(TextAlignment.CENTER)
                                    .SetMarginTop(10);

                                // Adicionar cabeçalho da tabela
                                tabela.AddHeaderCell("")
                                      .AddHeaderCell("ESF")
                                      .AddHeaderCell("CIL")
                                      .AddHeaderCell("EIXO")
                                      .AddHeaderCell("A / V");

                                // Adicionar linhas à tabela
                                tabela.AddCell("OD")
                                      .AddCell(receita.EsfOd)
                                      .AddCell(receita.CilOd)
                                      .AddCell(receita.EixoOd)
                                      .AddCell(receita.AvOd);

                                tabela.AddCell("OE")
                                      .AddCell(receita.EsfOe)
                                      .AddCell(receita.CilOe)
                                      .AddCell(receita.EixoOe)
                                      .AddCell(receita.AvOe);

                                tabela.AddCell("ADIÇÃO")
                                      .AddCell(receita.rxfinaladd)
                                      .AddCell("")
                                      .AddCell("")
                                      .AddCell(receita.AvAdd);

                                document.Add(tabela);
                            }
                            else
                            {
                                // Adicionar outras informações ou tabelas para a ficha de atendimento
                            }

                            // Adicionar rodapé
                            var rodape = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 1 }))
                                .SetWidth(UnitValue.CreatePercentValue(100))
                                .SetFixedPosition(15, 30, pageSize.GetWidth());

                            rodape.AddCell(new Cell().Add(new Paragraph("OPTOMETRISTA: ________________________")
                                .SetBorder(Border.NO_BORDER)
                                .SetTextAlignment(TextAlignment.LEFT)));
                            document.Add(rodape);
                        }
                    }
                }

                // Salvar o PDF em um arquivo temporário
                string caminhoArquivoTemporario = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DocumentoOptometrico.pdf");
                File.WriteAllBytes(caminhoArquivoTemporario, memoryStream.ToArray());

                // Caminho para o executável do Google Chrome
                string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                // Abrir o PDF no Chrome
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = chromePath,
                    Arguments = $"\"{caminhoArquivoTemporario}\"",
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            }
        }


        private (string NomeCompleto, DateTime DataDoExame) ObterInformacoesExame(int idExame)
        {
            string nomeCompleto = "Nome não disponível";
            DateTime dataDoExame = DateTime.MinValue;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT nome_completo, data_do_exame
                         FROM tb_exames
                         WHERE id_exames = @idExame";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idExame", idExame);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nomeCompleto = reader["nome_completo"] != DBNull.Value ? reader["nome_completo"].ToString() : nomeCompleto;

                                if (reader["data_do_exame"] != DBNull.Value)
                                {
                                    if (DateTime.TryParse(reader["data_do_exame"].ToString(), out DateTime tempData))
                                    {
                                        dataDoExame = tempData;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Erro ao converter data_do_exame para DateTime.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Campo data_do_exame está nulo.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Nenhum registro encontrado para o id_exame: {idExame}");
                                throw new Exception($"Nenhum registro encontrado para o id_exame: {idExame}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao obter informações do exame: " + ex.Message);
                        throw;
                    }
                }
            }

            return (nomeCompleto, dataDoExame);
        }


        private (string EsfOd, string EsfOe, string CilOd, string CilOe, string EixoOd, string EixoOe, string AvOd, string AvOe, string AvAdd, string rxfinaladd) ObterDadosReceita(int idExame)
        {
            string esfOd = "Não disponível";
            string esfOe = "Não disponível";
            string cilOd = "Não disponível";
            string cilOe = "Não disponível";
            string eixoOd = "Não disponível";
            string eixoOe = "Não disponível";
            string avOd = "Não disponível";
            string avOe = "Não disponível";
            string avAdd = "Não disponível";
            string rxfinaladd = "Não disponível";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT rx_final_esf_od, rx_final_esf_oe, rx_final_cil_od, rx_final_cil_oe, 
                         rx_final_eixo_od, rx_final_eixo_oe, rx_final_av_od, rx_final_av_oe, rx_final_av_add, rx_final_add
                         FROM tb_exames
                         WHERE id_exames = @idExame";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idExame", idExame);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                esfOd = reader["rx_final_esf_od"] != DBNull.Value ? reader["rx_final_esf_od"].ToString() : esfOd;
                                esfOe = reader["rx_final_esf_oe"] != DBNull.Value ? reader["rx_final_esf_oe"].ToString() : esfOe;
                                cilOd = reader["rx_final_cil_od"] != DBNull.Value ? reader["rx_final_cil_od"].ToString() : cilOd;
                                cilOe = reader["rx_final_cil_oe"] != DBNull.Value ? reader["rx_final_cil_oe"].ToString() : cilOe;
                                eixoOd = reader["rx_final_eixo_od"] != DBNull.Value ? reader["rx_final_eixo_od"].ToString() : eixoOd;
                                eixoOe = reader["rx_final_eixo_oe"] != DBNull.Value ? reader["rx_final_eixo_oe"].ToString() : eixoOe;
                                avOd = reader["rx_final_av_od"] != DBNull.Value ? reader["rx_final_av_od"].ToString() : avOd;
                                avOe = reader["rx_final_av_oe"] != DBNull.Value ? reader["rx_final_av_oe"].ToString() : avOe;
                                avAdd = reader["rx_final_av_add"] != DBNull.Value ? reader["rx_final_av_add"].ToString() : avAdd;
                                rxfinaladd = reader["rx_final_add"] != DBNull.Value ? reader["rx_final_add"].ToString() : rxfinaladd;
                            }
                            else
                            {
                                Console.WriteLine($"Nenhum dado de receita encontrado para o id_exame: {idExame}");
                                throw new Exception($"Nenhum dado de receita encontrado para o id_exame: {idExame}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao obter dados da receita: " + ex.Message);
                        throw;
                    }
                }
            }

            return (esfOd, esfOe, cilOd, cilOe, eixoOd, eixoOe, avOd, avOe, avAdd, rxfinaladd);
        }

    }
}
