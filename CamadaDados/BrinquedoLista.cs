using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaNegocios;

namespace CamadaDados
{
    public class BrinquedoLista : IRepositorio
    {
        private List<Brinquedo> _brinquedos;

        public BrinquedoLista()
        {
            _brinquedos = new List<Brinquedo>();
        }

        public List<Brinquedo> Listar()
        {
            return _brinquedos;
        }

        public Brinquedo BuscarPorId(Guid id)
        {
            return _brinquedos.FirstOrDefault(b => b.Id == id);
        }

        public void Incluir(Brinquedo brinquedo)
        {
            _brinquedos.Add(brinquedo);
        }

        public void Alterar(Brinquedo brinquedo)
        {
            var brinquedoExistente = BuscarPorId(brinquedo.Id);
            if (brinquedoExistente != null)
            {
                brinquedoExistente.Nome = brinquedo.Nome;
                brinquedoExistente.DataFabricacao = brinquedo.DataFabricacao;
                brinquedoExistente.FaixaEtaria = brinquedo.FaixaEtaria;
                brinquedoExistente.Disponivel = brinquedo.Disponivel;
                brinquedoExistente.Preco = brinquedo.Preco;
            }
        }

        public void Excluir(Guid id)
        {
            var brinquedoExistente = BuscarPorId(id);
            if (brinquedoExistente != null)
            {
                _brinquedos.Remove(brinquedoExistente);
            }
        }

        public List<Brinquedo> Pesquisar(string termo)
        {
            return _brinquedos.Where(b => b.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}