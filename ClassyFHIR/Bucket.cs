using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassyFHIR
{
    //Bucket class defines an aggregation
    //name,code,criteria
    class Bucket
    {
        public string name { get; }
        public string code { get; }
        public string definition { get; }

        public Bucket(string name, string code, string definition)
        {
            this.name = name;
            this.code = code;
            this.definition = definition;
        }

        public Bucket(string nameOrCode, string definition)
        {
            this.name = nameOrCode;
            this.code = nameOrCode;
            this.definition = definition;
        }
    }
}
