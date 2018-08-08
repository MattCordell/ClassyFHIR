using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using ClassyFHIR;
using F23.StringSimilarity;


namespace ConsoleApplication1
{    
    class Program
    {
        const string ServerEndpoint = "https://ontoserver.csiro.au/stu3-latest";
        const string vs = "http://snomed.info/sct?fhir_vs=ecl/<284666000";
        const string ecl = "http://snomed.info/sct?fhir_vs=ecl/";



        static void Main(string[] args)
        {
            var bucketList = new List<Bucket>(CreateBucketList());

            //validate bucket definitions
                      
            var terminologyClient = new FHIRTerminologyServer(ServerEndpoint);

            foreach (var item in bucketList)
            {
                var searchResult = terminologyClient.GetFirstResultFromValueSetExpansion(ecl+item.definition, "heart");
                var numOfResults = searchResult.Expansion.Total;
                //var topCode = searchResult.Expansion.Contains.FirstOrDefault().Code;

                var bestTerm = searchResult.Expansion.Contains.FirstOrDefault().Display;
                Console.WriteLine("{0} : {1} results, Best match = {2}",item.name,numOfResults,bestTerm);
            }



            //var synonyms = terminologyClient.LookUpCode(topCode).GetSynonyms();

            //foreach (var s in synonyms)
            //{
            //    Console.WriteLine(s);
            //}

            string foo = "lung hematoma";            
            string bar = "lung haematoma";
           
            //new ClassyFHIR_Tools().CompareStringMetrics(foo, bar);

            Console.WriteLine("Done");
            Console.ReadKey();

        }

        private static List<Bucket> CreateBucketList()
        {
            var _bucketList = new List<Bucket>();
            
            //128139000| Inflammatory disorder |
            _bucketList.Add(new Bucket("Inflammatory (NEC)", "Inflam", "<<128139000 MINUS (<< 118238000 OR << 106048009 OR << 40733004 OR << 74732009 OR <<118940003 OR << 106063007 OR << 399981008 )"));
            //118238000| Urogenital finding(finding)|
            _bucketList.Add(new Bucket("Urogenital (excluding infection)", "Urog", "<<118238000 MINUS (<<40733004)"));          
            //106048009|Respiratory finding (finding)|
            _bucketList.Add(new Bucket("All Respiratory problems (including infections)", "Respiratory", "<<106048009"));
            //40733004| Infectious disease(disorder)|
            _bucketList.Add(new Bucket("Infectious disease (NEC)", "Infect", "<<40733004 MINUS ( << 106048009 OR << 74732009 OR <<118940003  OR << 106063007 OR << 399981008 OR << 106028002)"));
            
            //74732009| Mental disorder(disorder)|    
            _bucketList.Add(new Bucket("Mental illness", "Mental", "<<74732009 MINUS (<<118940003)"));
            //118940003| Disorder of nervous system (disorder)|
            _bucketList.Add(new Bucket("Neurological disorder", "Neurolo", "<<118940003"));

            //106063007| Cardiovascular finding(finding)|
            _bucketList.Add(new Bucket("Cardiovascular problems (excluding Neoplasms)", "Cardio", "<<106063007 MINUS (<<399981008)"));

            //399981008| Neoplasm and / or hamartoma(disorder) |
            _bucketList.Add(new Bucket("Neoplasms and hamartomas", "NeoHarma", "<<399981008"));
            //106028002|Musculoskeletal finding (finding)|
            _bucketList.Add(new Bucket("Orthopedic problems (NEC)", "Orthoped", "<<106028002 MINUS ( << 128139000 OR << 118238000 OR << 106048009 OR << 40733004 OR << 74732009 OR <<118940003 OR << 106063007 OR << 399981008 )"));

            // << 128139000 OR << 118238000 OR << 106048009 OR << 40733004 OR << 74732009 OR <<118940003 OR <<118940003 OR << 106063007 OR << 399981008 OR << 106028002

            return _bucketList;
        }
    }
}