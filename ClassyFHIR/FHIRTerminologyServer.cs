using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;

namespace ClassyFHIR
{
    public class FHIRTerminologyServer
    {
        public string Endpoint;
        private FhirClient client;

        private Parameters.ParameterComponent valuesetIdParameter;
        private Parameters.ParameterComponent filterParameter;
        private Parameters.ParameterComponent countParameter;
        private Parameters.ParameterComponent systemParameter;
        private Parameters.ParameterComponent codeParameter;



        public FHIRTerminologyServer(string endpoint)
        {
            Endpoint = endpoint;
            client = new FhirClient(endpoint);

            valuesetIdParameter = new Parameters.ParameterComponent { Name = "identifier"};
            filterParameter = new Parameters.ParameterComponent { Name = "filter" };
            countParameter = new Parameters.ParameterComponent { Name = "limit" };
            systemParameter = new Parameters.ParameterComponent { Name = "system" };
            codeParameter = new Parameters.ParameterComponent { Name = "code" };

            //Parameters.ParameterComponent limit = new Parameters.ParameterComponent();

        }

        public ValueSet ExpandValueSet(string valueSetID)
        {
            valuesetIdParameter.Value = new FhirUri(valueSetID);
            
            var parameters = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                    {
                        valuesetIdParameter,
                    }
            };

            return (ValueSet)client.TypeOperation<ValueSet>("expand", parameters);                       
        }

        public ValueSet FilterExpandValueSet(string valueSetID, string searchFilter)
        {
            valuesetIdParameter.Value = new FhirUri(valueSetID);
            filterParameter.Value = new FhirString(searchFilter);

            var parameters = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                    {
                        valuesetIdParameter,
                        filterParameter
                    }
            };

            return (ValueSet)client.TypeOperation<ValueSet>("expand", parameters);            
        }

        public ValueSet GetFirstResultFromValueSetExpansion(string valueSetID, string searchFilter)
        {
            valuesetIdParameter.Value = new FhirUri(valueSetID);
            filterParameter.Value = new FhirString(searchFilter);
            countParameter.Value = new FhirString("1");

            var parameters = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                    {
                        valuesetIdParameter,
                        filterParameter,
                        countParameter
                    }
            };

            return (ValueSet)client.TypeOperation<ValueSet>("expand", parameters);            
        }

        public Parameters LookUpCode(string id)
        {
            //codeParameter.Value = new FhirString(id);
            //systemParameter.Value = new FhirUri("http://snomed.info/sct");

            var parameters = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                    {
                        codeParameter,
                        systemParameter
                    }
            };

            return client.ConceptLookup(new Code(id), new FhirUri("http://snomed.info/sct"));

            //return (Parameters)client.TypeOperation<ValueSet>("lookup", parameters);
        }



    }
}
