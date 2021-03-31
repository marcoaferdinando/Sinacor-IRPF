using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace app_sinacor.Services
{
    public class NotaNegociacaoService
    {
        Models.NotaNegociacaoModel _nota;
        public NotaNegociacaoService(Models.NotaNegociacaoModel nota)
        {
            _nota = nota;
        }

        public void ExtractValuesFromNotaString()
        {
            DataPregao();
        }

        private void DataPregao()
        {
            var info_nota = Regex.Match(_nota.TextFromFile, "data pregão .+?(clear)").Value;

            var numero_nota = Regex.Match(info_nota, "data pregão [0-9\\/\\.]+").Value;
            numero_nota = Regex.Match(numero_nota, "[0-9\\/\\.]+").Value;

            var data_pregao = Regex.Match(info_nota, "[0-9]{2}/[0-9]{2}/[0-9]{4}").Value;
        }
    }
}
