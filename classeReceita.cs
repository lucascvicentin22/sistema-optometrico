using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.IO.Image;
using iText.Layout.Properties;
using MySqlConnector;
using System.Diagnostics;
using iText.Layout.Borders;

namespace SistemaOptometrico
{
    public class ClasseReceita
    {
        // Método principal para gerar receita
        

        public void GerarReceita(string titulo, int idExame, bool isReceita)
        {
            var informacoes = ObterInformacoesExame(idExame);

            (string EsfOd, string EsfOe, string CilOd, string CilOe, string EixoOd, string EixoOe, string AvOd, string AvOe, string AvAdd, string rxfinaladd)? dadosReceita = null;

            if (isReceita)
            {
                dadosReceita = ObterDadosReceita(idExame);
            }

            string caminhoImagemFundo = @"C:\Users\lucas\OneDrive\Área de Trabalho\SistemaOptometrico\IMGFUNDORECEITA.png";

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(memoryStream))
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    iText.Kernel.Geom.PageSize pageSize = iText.Kernel.Geom.PageSize.A5;
                    pdf.SetDefaultPageSize(pageSize);

                    using (Document document = new Document(pdf))
                    {
                        ImageData imageData = ImageDataFactory.Create(caminhoImagemFundo);
                        var image = new Image(imageData);

                        image.SetFixedPosition(0, 0);
                        image.ScaleAbsolute(pageSize.GetWidth(), pageSize.GetHeight());
                        document.Add(image);

                        var header = new Paragraph(titulo)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(isReceita ? 16 : 20);
                        document.Add(header);

                        document.Add(new Paragraph($"Nome Completo: {informacoes.nome_completo}")
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(10)
                            .SetMarginTop(5));
                        document.Add(new Paragraph($"Data do Exame: {informacoes.data_do_exame:dd/MM/yyyy}")
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(10)
                            .SetMarginTop(5));

                        if (isReceita && dadosReceita.HasValue)
                        {
                            var receita = dadosReceita.Value;

                            var tabela = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 }))
                                .SetWidth(UnitValue.CreatePercentValue(100))
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetMarginTop(10)
                                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                            tabela.AddHeaderCell(new Cell().Add(new Paragraph("")));
                            tabela.AddHeaderCell(new Cell().Add(new Paragraph("ESF")));
                            tabela.AddHeaderCell(new Cell().Add(new Paragraph("CIL")));
                            tabela.AddHeaderCell(new Cell().Add(new Paragraph("EIXO")));
                            tabela.AddHeaderCell(new Cell().Add(new Paragraph("A / V")));

                            tabela.AddCell(new Cell().Add(new Paragraph("OD")));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.EsfOd)));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.CilOd)));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.EixoOd)));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.AvOd)));

                            tabela.AddCell(new Cell().Add(new Paragraph("OE")));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.EsfOe)));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.CilOe)));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.EixoOe)));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.AvOe)));

                            tabela.AddCell(new Cell().Add(new Paragraph("ADIÇÃO")));
                            tabela.AddCell(new Cell().Add(new Paragraph(receita.rxfinaladd)));
                            tabela.AddCell(new Cell().Add(new Paragraph("")));
                            tabela.AddCell(new Cell().Add(new Paragraph("")));
                            tabela.AddCell(new Cell().Add(new Paragraph("")));

                            document.Add(tabela);
                            // Adicionar OBS
                            document.Add(new Paragraph("OBS:")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10)
                                .SetMarginTop(10));
                            document.Add(new Paragraph("______________________________________________________________")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10)
                                .SetMarginTop(5));
                            document.Add(new Paragraph("______________________________________________________________")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10)
                                .SetMarginTop(5));
                        }

                        // Adicionar rodapé
                        var rodape = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 2, 1 }))
                            .SetWidth(UnitValue.CreatePercentValue(100))
                            .SetFixedPosition(15, 30, pageSize.GetWidth());

                        // Adicionar célula OPTOMETRISTA
                        rodape.AddCell(new Cell(1, 2)
                            .Add(new Paragraph("________________________")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10))
                            .Add(new Paragraph("OPTOMETRISTA")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10))
                            .SetBorder(Border.NO_BORDER));

                        // Adicionar célula RETORNO
                        rodape.AddCell(new Cell(1, 2)
                            .Add(new Paragraph("RETORNO: _____/_____")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(10))
                            .SetBorder(Border.NO_BORDER));

                        // Adicionar célula Consultório
                        rodape.AddCell(new Cell(1, 4)
                            .Add(new Paragraph($"Consultório: {informacoes.nome_consultorio}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10))
                            .Add(new Paragraph($"Endereço: {informacoes.endereco}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10))
                            .Add(new Paragraph($"Cidade: {informacoes.cidade}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(10))
                            .SetBorder(Border.NO_BORDER));

                        // Adicionar célula com texto centralizado e espaço acima
                        var cellRodape = new Cell(1, 4)
                            .Add(new Paragraph()
                                .Add(new Text("Retorno e conferência de óculos no prazo de até 30 dias.")
                                    .SetBold()
                                    .SetFontSize(10))
                                .Add(new Text("\nApós, será cobrado nova consulta.")
                                    .SetBold()
                                    .SetFontSize(10))
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetMarginTop(10)) // Espaço acima da célula
                            .SetBorder(Border.NO_BORDER) // Remove a borda da célula
                            .SetPadding(0)
                            .SetMargin(0);

                        // Adicionar a célula ao rodapé
                        rodape.AddCell(cellRodape);

                        // Adicionar rodapé ao documento
                        document.Add(rodape);
                    }
                }

                string caminhoArquivoTemporario = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DocumentoOptometrico.pdf");
                File.WriteAllBytes(caminhoArquivoTemporario, memoryStream.ToArray());

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                    Arguments = $"\"{caminhoArquivoTemporario}\"",
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            }
        }

        // Método fictício para obter as informações do exame
        private dynamic ObterInformacoesExame(int idExame)
        {
            using (var conexao = new Conexao().GetConnection())
            {
                conexao.Open();
                string query = @"SELECT e.nome_completo, e.data_do_exame, c.nome AS nome_consultorio, 
                                c.endereco, c.cidade 
                         FROM tb_exames e 
                         JOIN tb_consultorio c ON e.id_consultorio = c.id_consultorio 
                         WHERE e.id_exame = @idExame";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@idExame", idExame);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new
                            {
                                nome_completo = reader.GetString("nome_completo"),
                                data_do_exame = reader.GetDateTime("data_do_exame"),
                                nome_consultorio = reader.GetString("nome_consultorio"),
                                endereco = reader.GetString("endereco"),
                                cidade = reader.GetString("cidade")
                            };
                        }
                    }
                }
            }

            return null;
        }

        private (string EsfOd, string EsfOe, string CilOd, string CilOe, string EixoOd, string EixoOe, string AvOd, string AvOe, string AvAdd, string rxfinaladd) ObterDadosReceita(int idExame)
        {
            string esfOd = "Não disponível", esfOe = "Não disponível", cilOd = "Não disponível", cilOe = "Não disponível";
            string eixoOd = "Não disponível", eixoOe = "Não disponível", avOd = "Não disponível", avOe = "Não disponível";
            string avAdd = "Não disponível", rxfinaladd = "Não disponível";

            using (var conexao = new Conexao().GetConnection())
            {
                string query = @"SELECT rx_final_esf_od, rx_final_esf_oe, rx_final_cil_od, rx_final_cil_oe, 
                         rx_final_eixo_od, rx_final_eixo_oe, rx_final_av_od, rx_final_av_oe, rx_final_av_add, rx_final_add
                         FROM tb_exames
                         WHERE id_exame = @idExame";

                using (var command = new MySqlCommand(query, conexao))
                {
                    command.Parameters.AddWithValue("@idExame", idExame);

                    try
                    {
                        conexao.Open();
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
