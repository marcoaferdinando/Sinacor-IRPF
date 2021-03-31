using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_sinacor.Models
{
    public class NotaNegociacaoModel
    {
        public string File { get; set; }
        public string TextFile { get; set; }

        public DateTime Pregao { get; set; }
        public List<Models.NegociosRealizadosModel> NegociosRealizados { get; set; }
        public Models.ResumoDosNegociosModel ResumoDosNegocios { get; set; }
        public Models.ResumoFinanceiroModel ResumoFinanceiro { get; set; }
    }
}
