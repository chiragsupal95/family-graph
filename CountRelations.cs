using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyGraph
{
    public class CountRelations
    {
        private string name;
        private int count;
        

        public CountRelations(string name, int count)
        {
            this.name = name;
            this.count = count;
            
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        
    }
}
