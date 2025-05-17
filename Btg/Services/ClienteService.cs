using Btg.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btg.Services
{
    public class ClienteService : IClienteService
    {
        private ObservableCollection<Cliente> _clientes = new();

        public ObservableCollection<Cliente> ObterTodos() => _clientes;

        public void Adicionar(Cliente cliente) => _clientes.Add(cliente);

        public void Atualizar(Cliente cliente)
        {
            var clienteExistente = _clientes.FirstOrDefault(c => ReferenceEquals(c, cliente));
            if (clienteExistente != null)
            {
                clienteExistente.Name = cliente.Name;
                clienteExistente.Lastname = cliente.Lastname;
                clienteExistente.Age = cliente.Age;
                clienteExistente.Address = cliente.Address;
            }
        }

        public void Remover(Cliente cliente) => _clientes.Remove(cliente);


    }
}
