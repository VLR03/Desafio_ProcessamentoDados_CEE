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
            ProcessadorArquivo processador = new ProcessadorArquivo();
            processador.ProcessarArquivo(arquivo);
        }

        Console.WriteLine("Processamento concluído. Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }
}
