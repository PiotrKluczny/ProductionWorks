using BaseLinkerProductionCreator;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace baseLinkerProductionCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var import = Import();
            
            using (var conxtex = new AplicationDbContext())
            {
                conxtex.Products.AddRange(import);
                conxtex.SaveChanges();
            }

        }

        private static string path = "C:\\LocalRepository\\ProcutionWorks\\Test.csv";

        static IEnumerable<Product> Import()
        {
            using (var reader = new StreamReader(path))
            {
                using (var csvR = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csvR.Configuration.RegisterClassMap<ProductMap>();
                    csvR.Configuration.Delimiter = ";";

                    return csvR.GetRecords<Product>().ToList(); ;
                }
            }
        }
    }
}
