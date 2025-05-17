using Btg.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btg.Services
{
    public interface IClienteService
    {
        ObservableCollection<Cliente> ObterTodos();
        void Adicionar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Remover(Cliente cliente);

    }
}
