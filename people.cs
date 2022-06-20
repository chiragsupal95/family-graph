using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LINQtoCSV;

namespace FamilyGraph
{
    [Serializable]
    class people
    {
        [CsvColumn(FieldIndex = 1)]
        public string name { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string email { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string age { get; set; }
    }
}
