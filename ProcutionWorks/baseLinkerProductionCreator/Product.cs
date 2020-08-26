using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLinkerProductionCreator
{
    class Product
    {
        public Guid Id { get; set; }
        public string Ean { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
}
