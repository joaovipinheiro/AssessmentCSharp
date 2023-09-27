using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocios
{
    public class Brinquedo
    {
        public string Nome { get; set; }
        public Guid Id { get; set; }
        public DateTime DataFabricacao { get; set; }
        public int FaixaEtaria { get; set; }
        public double Preco { get; set; }
        public bool Disponivel { get; set; }

        public Brinquedo()
        {
            Id = Guid.NewGuid();
        }

        public string CalcTempoEstoque()
        {
            TimeSpan tempoEstoque = DateTime.Now - DataFabricacao;
            return $"Tempo em estoque: {tempoEstoque.Days} dias";
        }
    }
}