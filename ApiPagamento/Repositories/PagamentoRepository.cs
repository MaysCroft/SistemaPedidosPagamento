using ApiPagamento.Data;
using ApiPagamento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace ApiPagamento.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        /// <summary>
        /// AppDbContext - Contexto do banco de dados 
        /// Responsável por gerenciar a conexão com o banco de dados e fornecer acesso
        /// às tabelas e entidades do banco de dados
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor da classe - Recebe o contexto do banco de dados
        /// </summary>
        /// <param name="context"></param>
        public PagamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PedidosData>> GetAll()
            => await _context.Pagamentos.ToListAsync();

        /// <summary>
        /// GetById - Responsável por retornar uma pagamento 
        /// específico do banco de dados, com base no ID fornecido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PedidosData> GetById(int id)
            => await _context.Pagamentos.FindAsync(id);

        /// <summary>
        /// Add é responsável por adicionar um novo
        /// pagamento ao banco de dados
        /// </summary>
        /// <param name="pagamento"></param>
        /// <returns></returns>
        public async Task Add(PedidosData pagamento)
        {
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update é responsável por atualizar um 
        /// pagamento existente no banco de dados
        /// </summary>
        /// <param name="pagamento"></param>
        /// <returns></returns>
        public async Task Update(PedidosData pagamento)
        {
            var existente = await _context.Pagamentos.FindAsync(pagamento.Id);
            // Coloca um aviso genérico para não aparecer nenhuma informação importante
            if (existente == null)
                throw new Exception("Produto não encontrado!");

            // Atualiza os valores do objeto existente com os do novo objeto
            _context.Entry(existente).CurrentValues.SetValues(pagamento);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete é responsável por excluir um 
        /// pagamento do banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var p = await GetById(id);
            _context.Pagamentos.Remove(p);
            await _context.SaveChangesAsync();
        }
    }
}
