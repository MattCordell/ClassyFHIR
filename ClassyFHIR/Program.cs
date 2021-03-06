﻿using System;
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
        static List<Bucket> bucketList;
        static FHIRTerminologyServer terminologyClient;
        static ClassyFHIR_Tools tools = new ClassyFHIR_Tools();   


        static void Main(string[] args)
        {
            terminologyClient = new FHIRTerminologyServer(ServerEndpoint);
            bucketList = new List<Bucket>(CreateBucketList());
            var problemList = new string[] { "heart murmur","oral candida", "mouth ulcer", "open femoral fracture", "depressive anxiety", "unilateral palsy" };

            //TO DO validate bucket definitions            
            tools.StringMetricHeader();

            foreach (var problem in problemList)
            {
                AnalyseProblem(problem);
            }

                   


            string foo = "lung hematoma";            
            string bar = "lung haematoma";
           
            //new ClassyFHIR_Tools().CompareStringMetrics(foo, bar);

            Console.WriteLine("Done");
            Console.ReadKey();

        }

        private static void AnalyseProblem(string problem)
        {
            Console.WriteLine("Problem {0} : ", problem);

            foreach (var bucket in bucketList)
            {
                var searchResult = terminologyClient.GetFirstResultFromValueSetExpansion(bucket.definition, problem);
                var numOfResults = searchResult.Expansion.Total;

                if (numOfResults > 0)
                {
                    var topCode = searchResult.Expansion.Contains.FirstOrDefault().Code;
                    //var bestTerm = searchResult.Expansion.Contains.FirstOrDefault().Display;
                    var synonyms = terminologyClient.LookUpCode(topCode).GetSynonyms();
                    foreach (var synonym in synonyms)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", bucket.name, numOfResults, string.Join("\t", tools.CalculateStringMetric(problem, synonym)), synonym);
                    }

                    
                }
                else
                {
                    Console.WriteLine("{0}  {1}", bucket.name, numOfResults);
                }
            }
        }

//Bucketname Results consine damerau jaccard jarodwinkler    levenshtein lcs mlcs ng  nl osa qg sd
//Inflammatory(NEC)  0
//Urogenital(excluding infection)    0
//All Respiratory problems(including infections) 0
//Infectious disease(NEC)    0
//Mental illness	0
//Neurological disorder	0
//Cardiovascular problems(excluding Neoplasms)   42	0.083	1	0.2	0.056	1	2	0.083	0.125	0.083	1	2	0.111
//Neoplasms and hamartomas	0
//Orthopedic problems(NEC)   0



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