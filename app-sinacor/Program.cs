using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace app_sinacor
{
    class Program
    {
        private static readonly string _notasFolder
            = @"C:\Users\Marquinho\Downloads\Notas de Negociação";

        private static List<Models.NotaNegociacaoModel> _minhas_notas = new List<Models.NotaNegociacaoModel>();

        static void Main(string[] args)
        {
            GetStringsFromFiles();
            GetValuesFromNota();
        }

        private static void GetValuesFromNota()
        {
            _minhas_notas
                .ForEach(nota => 
                    new Services.NotaNegociacaoService(nota)
                        .ExtractValuesFromNotaString());
        }

        private static void GetStringsFromFiles()
        {
            Directory.GetFiles(_notasFolder)
               .ToList()
               .ForEach(file =>
                   _minhas_notas.Add(
                       new Models.NotaNegociacaoModel()
                       {
                           File = file,
                           TextFromFile =
                           Services.PdfService.ExtractTextFromPdf(file)
                       }));
        }
    }
}
