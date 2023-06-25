using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_ProcessamentoDados_CEE
{
    internal class RegistroSolicitacaoCartao
    {
        public int Tipo { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public int IdTransacao { get; set; }
        public int AgenciaConta { get; set; }
        public long NumeroConta { get; set; }
        public string? CpfCliente { get; set; }
        public string? NomeCliente { get; set; }
        public string? NomeCartao { get; set; }
        public int DiaVencimento { get; set; }
        public string? SenhaNumerica { get; set; }
    }
}
