using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using ClassyFHIR;


namespace ConsoleApplication1
{    
    class Program
    {
        const string ServerEndpoint = "https://ontoserver.csiro.au/stu3-latest";
        const string vs = "http://snomed.info/sct?fhir_vs=ecl/<284666000";

        static void Main(string[] args)
        {

            var terminologyClient = new FHIRTerminologyServer(ServerEndpoint);
            var s = terminologyClient.ExpandValueSet(vs);

            Console.WriteLine(s.Expansion.Contains.FirstOrDefault().Display);

            Console.WriteLine("Done");
            Console.ReadKey();

        }

    }
}