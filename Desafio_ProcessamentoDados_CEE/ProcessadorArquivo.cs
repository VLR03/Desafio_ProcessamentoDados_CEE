using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_ProcessamentoDados_CEE
{
    internal class ProcessadorArquivo
    {
        public void ProcessarArquivo(string caminhoArquivo)
        {
            string nomeArquivo = Path.GetFileNameWithoutExtension(caminhoArquivo);
            string tipoArquivo = nomeArquivo.Substring(0, 4);
            string dataArquivoStr = nomeArquivo.Substring(4, 8);
            DateTime dataArquivo = DateTime.ParseExact(dataArquivoStr, "yyyyMMdd", CultureInfo.InvariantCulture);
            string sequencialStr = nomeArquivo.Substring(12);
            int sequencial = int.Parse(sequencialStr);

            // Verificar o tipo de arquivo e executar ação correspondente
            if (tipoArquivo == "CARD")
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                // Processar cada linha do arquivo
                foreach (string linha in linhas)
                {
                    // Verificar o tipo de registro e executar ação correspondente
                    int tipoRegistro = int.Parse(linha.Substring(0, 2));
                    switch (tipoRegistro)
                    {
                        case 0:
                            ProcessarRegistroHeader(nomeArquivo, dataArquivo, tipoRegistro, linha);

                            break;
                        case 1:
                            ProcessarRegistroSolicitacaoCartao(nomeArquivo, dataArquivo);

                            break;
                        default:
                            Console.WriteLine("Tipo de registro desconhecido.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Arquivo inválido: " + nomeArquivo);
            }
        }

        private void ProcessarRegistroHeader(string nomeArquivo, DateTime dataArquivo, int tipoRegistro, string linha)
        {
            RegistroHeader header = new RegistroHeader();
            header.Tipo = tipoRegistro;
            header.DataArquivo = dataArquivo;
            header.CodigoRemetente = int.Parse(linha.Substring(10));

            if (header.DataArquivo.Date != dataArquivo.Date)
            {
                Console.WriteLine($"A data do registro de header não corresponde à data do nome do arquivo: {nomeArquivo}");
            }
        }

        private void ProcessarRegistroSolicitacaoCartao(string nomeArquivo, DateTime dataArquivo)
        {
            RegistroSolicitacaoCartao solicitacaoCartao = new RegistroSolicitacaoCartao();
            solicitacaoCartao.Tipo = int.Parse(nomeArquivo.Substring(0, 2));
            solicitacaoCartao.DataSolicitacao = dataArquivo;
            solicitacaoCartao.IdTransacao = int.Parse(nomeArquivo.Substring(10, 6));
            solicitacaoCartao.AgenciaConta = int.Parse(nomeArquivo.Substring(16, 4));
            solicitacaoCartao.NumeroConta = long.Parse(nomeArquivo.Substring(20, 12));
            solicitacaoCartao.CpfCliente = nomeArquivo.Substring(32, 11);
            solicitacaoCartao.NomeCliente = nomeArquivo.Substring(43, 40).Trim();
            solicitacaoCartao.NomeCartao = nomeArquivo.Substring(83, 40).Trim();
            solicitacaoCartao.DiaVencimento = int.Parse(nomeArquivo.Substring(123, 2));
            solicitacaoCartao.SenhaNumerica = nomeArquivo.Substring(125, 8);

            // Realizar ações desejadas com o registro de solicitação de cartão
            // ...
        }
    }
}
