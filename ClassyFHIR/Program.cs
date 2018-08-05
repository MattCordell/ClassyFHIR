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

            //var terminologyClient = new FHIRTerminologyServer(ServerEndpoint);
            //var s = terminologyClient.ExpandValueSet(vs);
            //var c = terminologyClient.LookUpCode("396620009");

            ////Console.WriteLine(s.Expansion.Contains.FirstOrDefault().Display);
            ////Console.WriteLine(c.Parameter.Where(p => p.Name.Equals("designation")).FirstOrDefault().Part.Where(p => p.Name.Equals("value")).ToString());
            //var designations = c.Parameter.Where(p => p.Name.Equals("designation"));

            //var x = designations.Where(p => p.Name == "value");


            //var synonyms = new List<string>();

            //foreach (var designation in x)
            //{
            //    Console.WriteLine(designation.Part);


            //}

            string foo = "haematoma lung";
            //string bar = "kidney haematoma";
            string bar = "lung haematoma";
            //string bar = "lung infection";

            var calculator = new ScoreCalculator();

            var score = calculator.ComputeLevenshteinDistance(foo, bar);

            Console.WriteLine("Score {0}", score.ToString());
            
            

            //Console.WriteLine(c.Parameter.Where(p => p.Name.Equals("designation")).Where(part => part.Name.Equals("value")));

            Console.WriteLine("Done");
            Console.ReadKey();

        }

    }
}