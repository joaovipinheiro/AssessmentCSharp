using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaNegocios;

namespace CamadaDados
{
    public interface IRepositorio
    {
        List<Brinquedo> Listar();
        Brinquedo BuscarPorId(Guid id);
        void Incluir(Brinquedo brinquedo);
        void Alterar(Brinquedo brinquedo);
        void Excluir(Guid id);
        List<Brinquedo> Pesquisar(string nome);
    }
}