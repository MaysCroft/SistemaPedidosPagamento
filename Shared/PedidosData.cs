using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shared
{
    public class PedidosData
    {
        public int Id { get; set; }
        public DateTime Data_Pedido { get; set; }
        public string Nome_Cliente { get; set; }
        public string Doc_Cliente { get; set; }
        public string Itens { get; set; }
        public double Valor { get; set; }
        public string Status { get; set; }
        public string Pagamento { get; set; }
    }
}
