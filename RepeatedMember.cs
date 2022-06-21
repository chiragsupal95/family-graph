using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyGraph
{
    public class RepeatedMember
    {
        private string name;

        public RepeatedMember(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
