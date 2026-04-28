using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace PedidoInterface.ViewModels
{
    internal class MainViewModel
    {
        public ObservableCollection<PedidosData> Pagamentos { get; set; }
        public ICommand CarregarPagamentosCommand { get; }
        public MainViewModel()
        {
            Pagamentos = new ObservableCollection<PedidosData>();

            CarregarPagamentosCommand = new RelayCommand(CarregarPagamentos);
        }

        private async void CarregarPagamentos()
        {
            var http = new HttpClient();

            var dados = await http.GetFromJsonAsync<List<PedidosData>>(
                "http://localhost:5000/api/pedidos");

            Pagamentos.Clear();

            foreach (var item in dados)
            {
                Pagamentos.Add(item);
            }
        }
    }
}
