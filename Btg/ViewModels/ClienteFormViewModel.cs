using Btg.Models;
using Btg.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Btg.Messages;

namespace Btg.ViewModels
{
    public class ClienteFormViewModel : INotifyPropertyChanged
    {
        private readonly IClienteService _clienteService;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get => _cliente;
            set
            {
                _cliente = value;
                OnPropertyChanged(nameof(Cliente));
            }
        }

        private Cliente _clienteOriginal;

        private string _idadeTexto;
        public string IdadeTexto
        {
            get => _idadeTexto;
            set
            {
                _idadeTexto = value;
                OnPropertyChanged(nameof(IdadeTexto));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public TaskCompletionSource<bool> FormularioConcluido { get; } = new();

        // Construtor para novo cliente
        public ClienteFormViewModel(IClienteService clienteService)
        {
            _clienteService = clienteService;
            Cliente = new Cliente(); // novo cliente
            IdadeTexto = string.Empty;

            SalvarCommand = new Command(Salvar);
            CancelarCommand = new Command(Cancelar);
        }

        // Construtor para edição
        public ClienteFormViewModel(IClienteService clienteService, Cliente clienteExistente)
        {
            _clienteService = clienteService;
            _clienteOriginal = clienteExistente;

            // Trabalha com uma cópia temporária
            Cliente = new Cliente
            {
                Name = clienteExistente.Name,
                Lastname = clienteExistente.Lastname,
                Age = clienteExistente.Age,
                Address = clienteExistente.Address
            };

            IdadeTexto = Cliente.Age.ToString();

            SalvarCommand = new Command(Salvar);
            CancelarCommand = new Command(Cancelar);
        }

        private async void Salvar()
        {
            if (!Validar())
                return;

            // Se for uma edição
            if (_clienteOriginal != null)
            {
                _clienteOriginal.Name = Cliente.Name;
                _clienteOriginal.Lastname = Cliente.Lastname;
                _clienteOriginal.Age = Cliente.Age;
                _clienteOriginal.Address = Cliente.Address;
            }
            else
            {
                // Novo cliente
                _clienteService.Adicionar(Cliente);
            }

            FormularioConcluido.TrySetResult(true);
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage());
        }

        private async void Cancelar()
        {
            FormularioConcluido.TrySetResult(false);
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage());
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(Cliente.Name))
            {
                ErrorMessage = "Nome é obrigatório.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Cliente.Lastname))
            {
                ErrorMessage = "Sobrenome é obrigatório.";
                return false;
            }

            if (!int.TryParse(IdadeTexto, out int idade) || idade <= 0)
            {
                ErrorMessage = "Digite uma idade válida (somente números inteiros).";
                return false;
            }

            Cliente.Age = idade;

            if (string.IsNullOrWhiteSpace(Cliente.Address))
            {
                ErrorMessage = "Endereço é obrigatório.";
                return false;
            }

            ErrorMessage = null;
            return true;
        }

        public void SetCliente(Cliente cliente)
        {
            Cliente = cliente ?? new Cliente();
            IdadeTexto = Cliente.Age.ToString();
            ErrorMessage = string.Empty;
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
