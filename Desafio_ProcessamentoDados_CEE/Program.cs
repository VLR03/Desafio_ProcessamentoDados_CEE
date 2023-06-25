using Desafio_ProcessamentoDados_CEE;
using System.IO;
using System.Globalization;


class Program
{
    static void Main(string[] args)
    {
        string pastaArquivos = "C:\\Users\\Vitor\\source\\repos\\Desafio_ProcessamentoDados_CEE\\Desafio_ProcessamentoDados_CEE\\Arquivos\\";
        string[] arquivos = Directory.GetFiles(pastaArquivos, "*.IN");

        foreach (string arquivo in arquivos)
        {
            ProcessarArquivo(arquivo);
        }

        Console.WriteLine("Processamento concluído. Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }

    static void ProcessarArquivo(string caminhoArquivo)
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
                        RegistroHeader header = new RegistroHeader();
                        header.Tipo = tipoRegistro;
                        header.DataArquivo = dataArquivo;
                        header.CodigoRemetente = int.Parse(linha.Substring(10));

                        if (header.DataArquivo.Date != dataArquivo.Date)
                        {
                            Console.WriteLine($"A data do registro de header não corresponde à data do nome do arquivo: {nomeArquivo}");
                            // Você pode optar por ignorar esse registro ou tomar a ação necessária.
                            // Por exemplo, você pode usar um "continue" para ignorar esse registro e ir para o próximo.
                            continue;
                        }

                        // Realizar ações desejadas com o registro de header
                        // ...

                        break;
                    case 1:
                        // Processar registro tipo 1 (Solicitação de cartão)
                        RegistroSolicitacaoCartao solicitacaoCartao = new RegistroSolicitacaoCartao();
                        solicitacaoCartao.Tipo = tipoRegistro;
                        solicitacaoCartao.DataSolicitacao = DateTime.ParseExact(linha.Substring(2, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
                        solicitacaoCartao.IdTransacao = int.Parse(linha.Substring(10, 6));
                        solicitacaoCartao.AgenciaConta = int.Parse(linha.Substring(16, 4));
                        solicitacaoCartao.NumeroConta = long.Parse(linha.Substring(20, 12));
                        solicitacaoCartao.CpfCliente = linha.Substring(32, 11);
                        solicitacaoCartao.NomeCliente = linha.Substring(43, 40).Trim();
                        solicitacaoCartao.NomeCartao = linha.Substring(83, 40).Trim();
                        solicitacaoCartao.DiaVencimento = int.Parse(linha.Substring(123, 2));
                        solicitacaoCartao.SenhaNumerica = linha.Substring(125, 8);

                        // Realizar ações desejadas com o registro de solicitação de cartão
                        // ...

                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Arquivo inválido: " + nomeArquivo);
        }
    }
}
