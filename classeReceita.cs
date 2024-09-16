using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.IO.Image;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using iText.Layout.Borders;

namespace SistemaOptometrico
{
    public class ClasseReceita
    {
        private string connectionString = "Server=localhost;Database=db_clinica;User ID=root;Password=2707;";

        // Método principal para gerar receita
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
                        iText.Kernel.Geom.PageSize pageSize = isReceita ? iText.Kernel.Geom.PageSize.A5 : iText.Kernel.Geom.PageSize.A5;
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
                            document.Add(new Paragraph($"Nome Completo: {informacoes.nome_completo}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10)
                                .SetMarginTop(5));
                            document.Add(new Paragraph($"Data do Exame: {informacoes.data_do_exame:dd/MM/yyyy}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10)
                                .SetMarginTop(5));

                            // Adicionar informações da receita
                            if (isReceita && dadosReceita.HasValue)
                            {
                                var receita = dadosReceita.Value;

                                // Criar a tabela com 5 colunas
                                var tabela = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 }))
                                    .SetWidth(UnitValue.CreatePercentValue(100))  // Define a largura total da tabela
                                    .SetTextAlignment(TextAlignment.CENTER)  // Alinha o texto das células ao centro
                                    .SetMarginTop(10);  // Define a margem superior da tabela

                                // Adicionar cabeçalhos
                                tabela.AddHeaderCell("").AddHeaderCell("ESF").AddHeaderCell("CIL").AddHeaderCell("EIXO").AddHeaderCell("A / V");

                                // Preencher OD e OE
                                tabela.AddCell("OD").AddCell(receita.EsfOd).AddCell(receita.CilOd).AddCell(receita.EixoOd).AddCell(receita.AvOd);
                                tabela.AddCell("OE").AddCell(receita.EsfOe).AddCell(receita.CilOe).AddCell(receita.EixoOe).AddCell(receita.AvOe);

                                // Adição
                                tabela.AddCell("ADIÇÃO").AddCell(receita.rxfinaladd).AddCell("").AddCell("").AddCell(receita.AvAdd);

                                // Adicionar a tabela ao documento com alinhamento centralizado
                                document.Add(new Paragraph()  // Adiciona um parágrafo vazio para ajustar a margem superior
                                    .SetMarginTop(80));  // Define a margem superior para o parágrafo vazio

                                // Adicionar a tabela ao documento
                                document.Add(tabela);
                            }


                            // Adicionar rodapé
                            var rodape = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 1 }))
                                .SetWidth(UnitValue.CreatePercentValue(100))
                                .SetFixedPosition(15, 30, pageSize.GetWidth());

                            // Adicionar célula com texto "OPTOMETRISTA" e linha para assinatura
                            var cellOptometrista = new Cell(1, 3) // Célula que ocupa as 3 colunas
                                .Add(new Paragraph("OPTOMETRISTA: ________________________")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10))
                                .SetBorder(Border.NO_BORDER)
                                .SetHeight(25); // Ajusta a altura da célula para garantir que haja espaço

                            rodape.AddCell(cellOptometrista);

                            // Adicionar célula com texto centralizado e espaço acima
                            var cellRodape = new Cell(1, 3) // Célula que ocupa as 3 colunas
                                .Add(new Paragraph("Retorno e conferência de óculos no prazo de até 30 dias. Após, será cobrado nova consulta.")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(9)
                                .SetMarginTop(10)) // Adiciona margem superior para o espaço
                                .SetBorder(Border.NO_BORDER);

                            rodape.AddCell(cellRodape);

                            document.Add(rodape);


                        }
                    }
                }

                // Salvar o PDF em um arquivo temporário
                string caminhoArquivoTemporario = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DocumentoOptometrico.pdf");
                File.WriteAllBytes(caminhoArquivoTemporario, memoryStream.ToArray());

                // Abrir o PDF no Chrome
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                    Arguments = $"\"{caminhoArquivoTemporario}\"",
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            }
        }

        // Método para obter os dados da receita
        private (string EsfOd, string EsfOe, string CilOd, string CilOe, string EixoOd, string EixoOe, string AvOd, string AvOe, string AvAdd, string rxfinaladd) ObterDadosReceita(int idExame)
        {
            string esfOd = "Não disponível", esfOe = "Não disponível", cilOd = "Não disponível", cilOe = "Não disponível";
            string eixoOd = "Não disponível", eixoOe = "Não disponível", avOd = "Não disponível", avOe = "Não disponível";
            string avAdd = "Não disponível", rxfinaladd = "Não disponível";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT rx_final_esf_od, rx_final_esf_oe, rx_final_cil_od, rx_final_cil_oe, 
                         rx_final_eixo_od, rx_final_eixo_oe, rx_final_av_od, rx_final_av_oe, rx_final_av_add, rx_final_add
                         FROM tb_exames
                         WHERE id_exame = @idExame";

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
                                esfOd = reader["rx_final_esf_od"]?.ToString() ?? esfOd;
                                esfOe = reader["rx_final_esf_oe"]?.ToString() ?? esfOe;
                                cilOd = reader["rx_final_cil_od"]?.ToString() ?? cilOd;
                                cilOe = reader["rx_final_cil_oe"]?.ToString() ?? cilOe;
                                eixoOd = reader["rx_final_eixo_od"]?.ToString() ?? eixoOd;
                                eixoOe = reader["rx_final_eixo_oe"]?.ToString() ?? eixoOe;
                                avOd = reader["rx_final_av_od"]?.ToString() ?? avOd;
                                avOe = reader["rx_final_av_oe"]?.ToString() ?? avOe;
                                avAdd = reader["rx_final_av_add"]?.ToString() ?? avAdd;
                                rxfinaladd = reader["rx_final_add"]?.ToString() ?? rxfinaladd;
                            }
                            else
                            {
                                throw new Exception($"Nenhum dado encontrado para o id_exame: {idExame}");
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

        // Método fictício para obter as informações do exame (precisa ser implementado)
        private dynamic ObterInformacoesExame(int idExame)
        {
            string nomeCompleto = "Não disponível";
            DateTime? dataDoExame = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT nome_completo, data_do_exame
                         FROM tb_exames
                         WHERE id_exame = @idExame";

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
                                nomeCompleto = reader["nome_completo"]?.ToString() ?? nomeCompleto;
                                dataDoExame = reader["data_do_exame"] as DateTime?;
                            }
                            else
                            {
                                throw new Exception($"Nenhum dado encontrado para o id_exame: {idExame}");
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

            return new { nome_completo = nomeCompleto, data_do_exame = dataDoExame };
        }


    }
}
