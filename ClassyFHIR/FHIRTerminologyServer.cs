using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;

namespace ClassyFHIR
{
    class FHIRTerminologyServer
    {
        public string Endpoint = "https://ontoserver.csiro.au/stu3-latest";
        private FhirClient client;



    public FHIRTerminologyServer(string endpoint)
        {
            Endpoint = endpoint;
            client = new FhirClient(endpoint);
            //Parameters.ParameterComponent limit = new Parameters.ParameterComponent();
            
        }

        public ValueSet ExpandValueSet(string valueSetID)
        {

            //Approach using the more versatile .TypeOperation();
            var parameters = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                    {
                        new Parameters.ParameterComponent
                        {
                            Name = "identifier",
                            Value = new FhirUri($"http://snomed.info/sct?fhir_vs=ecl/<284666000")
                        },
                        new Parameters.ParameterComponent
                        {
                            Name = "filter",
                            Value = new FhirString("mega")
                        }
                    }
            };

            var result = (ValueSet)client.TypeOperation<ValueSet>("expand", parameters);


            Console.WriteLine(result.Expansion.Contains.FirstOrDefault().Display);

            return result;
        }

        public ValueSet FilterExpandValueSet(string valueSetID, string searchFilter)
        {
            throw new NotImplementedException();
        }

        public ValueSet GetFirstResultFromValueSetExpansion(string valueSetID, string searchFilter)
        {
            throw new NotImplementedException();
        }



    }
}
