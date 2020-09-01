using BaseLinkerProductionCreator;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml.XPath;

namespace baseLinkerProductionCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var import = Import();

            using (var conxtex = new ApplicationDbContext())
            {
                conxtex.Products.AddRange(import);
                conxtex.SaveChanges();
            }

        }

        private static string path = "C:\\LocalRepository\\Test1New.csv";

        static IEnumerable<Image> Import()
        {
            using (var reader = new StreamReader(path))
            {
                using (var csvR = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csvR.Configuration.RegisterClassMap<ProductMap>();
                    csvR.Configuration.Delimiter = ";";

                    var csv = csvR.GetRecords<ImageCSV>().ToList();

                    var coll = new List<Image>();
                    var result = new List<Image>();

                    List<ImageP> ts = new List<ImageP>();

                    foreach (var item in csv)
                    {
                        var firstFiltrOfValue = item.Images.Where(x => !String.IsNullOrWhiteSpace(x.Url))
                            .Where(x => !x.Name.ToUpper().Contains("ZESTAW"))
                           .Select(x => new ImageP { Ean = x.Ean, Name = x.Name, RowId = x.RowId, Url = x.Url });

                        //tworzenie kolekcji ktora juz nie posiada pustyk url i zestawow w nazwie i dodana do listy ImageP ktora posiada tylko 3 zmienne
                        ts.AddRange(firstFiltrOfValue);
                        ;

                        var secondFiltrOfValue = item.Images.Where(x => !String.IsNullOrWhiteSpace(x.Url))
                            .Where(x => !x.Name.ToUpper().Contains("ZESTAW"))
                           .Select(x => new Image { Ean = x.Ean, Name = x.Name, Url = x.Url, RowId = x.RowId });
                        //tworzenie kolekcji ktora juz nie posiada pustyk url i zestawow w nazwie i dodane jest do listy Image ktora posiada 4 zmienne
                        coll.AddRange(secondFiltrOfValue);
                        ;

                    }
                    // wyodrebnienie z listy ts wszystkich unikatowych EAN-ow 
                    var distinctEan = ts.Where(x => !String.IsNullOrWhiteSpace(x.Ean)).Select(x => x.Ean).Distinct().ToList();

                    var groupByEan = ts.GroupBy(x => new { x.Ean, x.RowId })
                    .Select((x, y) => new { x.Key.Ean, x.Key.RowId, c = x.Count() }).ToList();

                    foreach (var ean in distinctEan)
                    {

                        var finalVersionOfValues = groupByEan.Where(x => x.Ean == ean)
                            .Where(x => x.c == groupByEan.Where(q => q.Ean == ean).Select(q => q.c).Max())
                            .First();

                        //zmienna ktora wyodrebnia wszystkie pierwse EAN-y
                        var row = ts.Where(x => x.RowId == finalVersionOfValues.RowId)
                            .First();

                        // zmiena gdzie przypisujemy nazwe tego EAN-u
                        var name = row.Name;
                        var rowId = row.RowId;
                        // zmienna z ktorej wydobywamy konkretny ean i konkretne ime i RowId 
                        var toSave = coll
                            .Where(x => x.Ean == ean && x.Name == name)
                            .Where(x => x.RowId == rowId)
                            .ToList();

                        result.AddRange(toSave);
                    }
                    ;
                    return result;

                }
            }
        }
    }

    public class ImageP
    {
        public string Ean { get; set; }
        public string Name { get; set; }
        public Guid RowId { get; set; }
        public string Url { get; internal set; }
    }
}
