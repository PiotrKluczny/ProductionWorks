using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseLinkerProductionCreator
{
    class Image
    {
        public Guid Id { get; set; }
        public string Ean { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Base64 { get; set; }
        [NotMapped]
        public Guid RowId { get; set; }
    }
    class ImageCSV
    {
        public List<Image> Images { get; set; }
    }
}


