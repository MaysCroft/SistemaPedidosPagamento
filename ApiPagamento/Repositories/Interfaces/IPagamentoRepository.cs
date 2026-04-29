using Shared;

namespace ApiPagamento.Repositories.Interfaces
{
    public class IPagamentoRepository
    {
        Task<List<PedidosData>> GetAll();
        Task<PedidosData> GetById(int id);
        Task Add(PedidosData pagamento);
        Task Update(PedidosData pagamento);
        Task Delete(int id);
    }
}
