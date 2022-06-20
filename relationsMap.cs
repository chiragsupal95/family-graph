using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LINQtoCSV;
namespace FamilyGraph
{
    [Serializable]
    class relationsMap
    {
       // [CsvColumn(FieldIndex = 1)]
        public string name { get; set; }
       // [CsvColumn(FieldIndex = 2)]
        public string relativeName { get; set; }
       // [CsvColumn(FieldIndex = 3)]
        public string relation { get; set; }

        public List<string> storeRelations = new List<string>();
    }
    
}
