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
        Models.NotaNegociacaoModel _minha_nota;
        public NotaNegociacaoService(Models.NotaNegociacaoModel nota)
        {
            _minha_nota = nota;
        }

        public void ExtractValuesFromNotaString()
        {
            Cabeçalho();
            NegociosRealizados();
        }

        private void NegociosRealizados()
        {   
            var dados_operacao = Regex.Match(_minha_nota.TextFromFile, "q negociação.+(resumo dos negócios)").Value;
            dados_operacao = Regex.Match(dados_operacao, "(?<=c ).+").Value;
            while (dados_operacao.Contains("  "))
                dados_operacao = dados_operacao.Replace("  ", " ");

            var operacoes = Regex.Split(dados_operacao, "(?<=[0-9] )[cd]{1} ")
                .Where(a => a.Trim().StartsWith("1"))
                .ToList();

            operacoes.ForEach(operacao => Operacacao(operacao));
        }

        private void Operacacao(string operacao)
        {
            operacao = Regex.Replace(operacao, "\\.", string.Empty);

            var cv = Regex.Match(operacao, "(?<= )[cv]{1}(?= )").Value;

            var tipo_mercado = "fracionario";
            var padrao_temp = $"(?<={tipo_mercado} ).+?(?= [0-9]+ [0-9]+,)";
            var especificacao_titulo = Regex.Match(operacao, padrao_temp).Value;

            padrao_temp = $"(?<={especificacao_titulo}).+";
            var valores = Regex.Match(operacao, padrao_temp).Value;
            var matches = Regex.Matches(valores, "(?<= )[0-9,\\.]+");

            var quantidade = Convert.ToInt32(matches[0].Value);
            var preco = Convert.ToDecimal(matches[1].Value);
            var valor = Convert.ToDecimal(matches[2].Value);

            _minha_nota.NegociosRealizados.Add(
                new Models.NegociosRealizadosModel()
                {
                    CV = cv,
                    Titulo = especificacao_titulo,
                    Quantidade = quantidade,
                    Preço = preco,
                    Valor = valor
                });
        }

        private void Cabeçalho()
        {
            var info_nota = Regex.Match(_minha_nota.TextFromFile, "data pregão .+?(clear)").Value;

            var numero_nota = Regex.Match(info_nota, "data pregão [0-9\\/\\.]+").Value;
            numero_nota = Regex.Match(numero_nota, "[0-9\\/\\.]+").Value;
            _minha_nota.NumeroNota = numero_nota;

            var data_pregao = Regex.Match(info_nota, "[0-9]{2}/[0-9]{2}/[0-9]{4}").Value;
            _minha_nota.Pregao = Convert.ToDateTime(data_pregao);
        }
    }
}
