using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CamadaNegocios;


namespace CamadaDados
{
    public class BrinquedoArquivo : IRepositorio
    {
        private string arquivo = "brinquedos.txt";

        public List<Brinquedo> Listar()
        {
            if (!File.Exists(arquivo))
            {
                return new List<Brinquedo>();
            }

            return File.ReadAllLines(arquivo)
                       .Select(line => BrinquedoFromCsv(line))
                       .ToList();
        }

        public Brinquedo BuscarPorId(Guid id)
        {
            return Listar().FirstOrDefault(b => b.Id == id);
        }

        public void Incluir(Brinquedo brinquedo)
        {
            var brinquedos = Listar();
            brinquedos.Add(brinquedo);
            SalvarNoArquivo(brinquedos);
        }

        public void Alterar(Brinquedo brinquedo)
        {
            var brinquedos = Listar();
            var index = brinquedos.FindIndex(b => b.Id == brinquedo.Id);
            if (index != -1)
            {
                brinquedos[index] = brinquedo;
                SalvarNoArquivo(brinquedos);
            }
        }

        public void Excluir(Guid id)
        {
            var brinquedos = Listar();
            brinquedos.RemoveAll(b => b.Id == id);
            SalvarNoArquivo(brinquedos);
        }

        public List<Brinquedo> Pesquisar(string nome)
        {
            return Listar().Where(b => b.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private Brinquedo BrinquedoFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            if (values.Length != 6)
            {
                throw new ArgumentException("Formato de linha inválido no arquivo de brinquedos.");
            }

            return new Brinquedo
            {
                Id = Guid.Parse(values[0]),
                Nome = values[1],
                DataFabricacao = DateTime.Parse(values[2]),
                FaixaEtaria = int.Parse(values[3]),
                Disponivel = bool.Parse(values[4]),
                Preco = double.Parse(values[5])
            };
        }

        private void SalvarNoArquivo(List<Brinquedo> brinquedos)
        {
            var csvLines = brinquedos.Select(brinquedo =>
                $"{brinquedo.Id};{brinquedo.Nome};{brinquedo.DataFabricacao:dd-MM-yyyy};{brinquedo.FaixaEtaria};{brinquedo.Disponivel};{brinquedo.Preco}");

            File.WriteAllLines(arquivo, csvLines);
        }
    }
}