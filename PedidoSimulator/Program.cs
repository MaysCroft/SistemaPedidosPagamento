using System.Net.Http.Json;
using Shared;

var http = new HttpClient();
int index = 0;

while (true)
{
    var statuspedido = new List<string> { "Pendente", "Processando", "Enviado", "Entregue", "Cancelado" };
    var formapagamento = new List<string> { "Dinheiro", "Cartão de Crédito", "Cartão de Débito", "Boleto", "Pix", "Transferência Bancária" };
    var statuspagamento = new List<string> { "Aguardando", "Pago", "Recusado", "Estornado" };

    var pagamento = new PedidosData
    {
        Id = index,
        Data_Pedido = DateTime.Now,
        Nome_Cliente = $"Cliente {index}",
        Doc_Cliente = $"Documento {index}",
        Produto = $"Item {index}, Item {index + 1}",
        Quantidade = $"{index + 1}",
        Valor = 100.0 + index,
        StatusPedido = $"{statuspedido[new Random().Next(statuspedido.Count)]}",
        FormaPagamento = $"{formapagamento[new Random().Next(formapagamento.Count)]}",
        StatusPagamento = $"{statuspagamento[new Random().Next(statuspagamento.Count)]}"
    };

    var response = await http.PostAsJsonAsync(
        "http://localhost:5000/api/v1/pedidos", pagamento);

    if (!response.IsSuccessStatusCode)
    {
        var erro = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Erro ao enviar pedido: {response.StatusCode} - {erro}");
    }
    else
    {
        Console.WriteLine("Pedido enviado com sucesso!");
    }

    await Task.Delay(2000);
    index++;
}