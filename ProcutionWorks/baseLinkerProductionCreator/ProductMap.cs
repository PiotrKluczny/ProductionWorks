using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLinkerProductionCreator
{
    class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Map(x => x.Name).Index(0);
            Map(x => x.Ean).Index(1);
            Map(x => x.Photo).Index(3);
        }
    }
}
