using Btg.Helpers;
using Btg.Models;
using Btg.Services;
using Btg.Views;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Btg.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IClienteService _clienteService;
        private readonly IServiceProvider _serviceProvider;

        public ObservableCollection<Cliente> Clientes { get; }

        public ICommand AdicionarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }

        public MainViewModel(IClienteService clienteService, IServiceProvider serviceProvider)
        {
            _clienteService = clienteService;
            _serviceProvider = serviceProvider;

            Clientes = clienteService.ObterTodos();

            AdicionarCommand = new Command(AdicionarCliente);
            EditarCommand = new Command<Cliente>(EditarCliente);
            ExcluirCommand = new Command<Cliente>(ExcluirCliente);
        }

        private async void AdicionarCliente()
        {
            var formPage = _serviceProvider.GetRequiredService<ClienteFormPage>();

            if (formPage.BindingContext is ClienteFormViewModel vm)
            {
                WindowHelper.OpenCenteredWindow(formPage, "Novo Cliente");
                var resultado = await vm.FormularioConcluido.Task;

                if (resultado)
                {
                    // Força a UI a atualizar
                    var cliente = vm.Cliente;
                    if (!Clientes.Contains(cliente))
                        Clientes.Add(cliente);
                }
            }
        }


        private async void EditarCliente(Cliente clienteSelecionado)
        {
            var viewModel = new ClienteFormViewModel(_clienteService, clienteSelecionado);
            var formPage = new ClienteFormPage(viewModel);

            WindowHelper.OpenCenteredWindow(formPage, "Editar Cliente");
            var resultado = await viewModel.FormularioConcluido.Task;
        }


        private async void ExcluirCliente(Cliente cliente)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "Deseja excluir este cliente?", "Sim", "Não");
            if (confirm)
            {
                _clienteService.Remover(cliente);
            }
        }
    }
}
