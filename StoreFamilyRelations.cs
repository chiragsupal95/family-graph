using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyGraph
{
    class StoreFamilyRelations
    {
        private string person;
        private string relative;
        private string relation;

        public StoreFamilyRelations(string person, string relative, string relation)
        {
            this.person = person;
            this.relative = relative;
            this.relation = relation;
        }

        public string Person
        {
            get { return person; }
            set { person = value; }
        }

        public string Relative
        {
            get { return relative; }
            set { relative = value; }
        }

        public string Relation
        {
            get { return relation; }
            set { relation = value; }
        }
    }
}
