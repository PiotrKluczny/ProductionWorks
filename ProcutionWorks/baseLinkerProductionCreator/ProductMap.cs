using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLinkerProductionCreator
{
    class ProductMap : ClassMap<ImageCSV>
    {
        public ProductMap()
        { 
            Map(m => m.Images).ConvertUsing(row =>
         {
          var result = new List<Image>();
          var headers = row.Context.HeaderRecord;
          var rowId = Guid.NewGuid();
          for (var i = 3; i < headers.Length; i++)
          {
              result.Add(
                  new Image
                  {
                      Url = row.GetField(i),
                      Ean = row.GetField(1),
                      Name = row.GetField(0),
                      Id = Guid.NewGuid(),
                      RowId = rowId
                  }); 
          }
          return result;
      });
        }
    }
}
