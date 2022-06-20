using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LINQtoCSV;

namespace FamilyGraph
{
    [Serializable]
    class relations
    {
        [CsvColumn(FieldIndex = 1)]
        public string email { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string relationship { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string emailOfRelative { get; set; }
    }
}
