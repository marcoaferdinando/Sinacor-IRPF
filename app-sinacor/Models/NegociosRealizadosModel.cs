using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_sinacor.Models
{
    public class NegociosRealizadosModel
    {
        public string CV { get; set; }
        public string Ticker { get; set; }
        public int Quantidade { get; set; }
        public decimal Preço { get; set; }
        public decimal Total { get; set; }
        public string DC { get; set; }
    }
}
