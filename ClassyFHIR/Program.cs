using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RestSharp;
using Newtonsoft.Json;

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            const string Endpoint = "https://ontoserver.csiro.au/stu3-latest";
            var client = new FhirClient(Endpoint);

            //Approach using the more versatile .TypeOperation();
            var parameters = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                    {
                        new Parameters.ParameterComponent
                        {
                            Name = "identifier",
                            Value = new FhirUri($"http://snomed.info/sct?fhir_vs=refset/1072351000168102")
                        },
                        new Parameters.ParameterComponent
                        {
                            Name = "filter",
                            Value = new FhirString("gluco")
                        }
                    }
            };

            var result = (ValueSet)client.TypeOperation<ValueSet>("expand", parameters);


            Console.WriteLine(result.Expansion.Contains.FirstOrDefault().Display);

            Console.WriteLine("Done");
            Console.ReadKey();

        }
    }
}