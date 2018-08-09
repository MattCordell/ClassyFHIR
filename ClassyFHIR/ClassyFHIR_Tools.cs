using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassyFHIR
{
    class ClassyFHIR_Tools
    {
        public void CompareStringMetrics(string s1, string s2)
        {

            var consine = new F23.StringSimilarity.Cosine().Distance(s1, s2);
            var damerau = new F23.StringSimilarity.Damerau().Distance(s1, s2);
            var jaccard = new F23.StringSimilarity.Jaccard().Distance(s1, s2);
            var jarodwinkler = new F23.StringSimilarity.JaroWinkler().Distance(s1, s2);
            var levenshtein = new F23.StringSimilarity.Levenshtein().Distance(s1, s2);

            var lcs = new F23.StringSimilarity.LongestCommonSubsequence().Distance(s1, s2);
            var mlcs = new F23.StringSimilarity.MetricLCS().Distance(s1, s2);
            var ng = new F23.StringSimilarity.NGram().Distance(s1, s2);
            var nl = new F23.StringSimilarity.NormalizedLevenshtein().Distance(s1, s2);
            var osa = new F23.StringSimilarity.OptimalStringAlignment().Distance(s1, s2);
            var qg = new F23.StringSimilarity.QGram().Distance(s1, s2);
            //var ng = new F23.StringSimilarity.ShingleBased
            var sd = new F23.StringSimilarity.SorensenDice().Distance(s1, s2);
            //var wl = new F23.StringSimilarity.WeightedLevenshtein().Distance

            Console.WriteLine("consine score = {0}", consine.ToString());
            Console.WriteLine("damerau score = {0}", damerau.ToString());
            Console.WriteLine("jaccard score = {0}", jaccard.ToString());
            Console.WriteLine("jarodwinkler score = {0}", jarodwinkler.ToString());
            Console.WriteLine("levenshtein score = {0}", levenshtein.ToString());

            Console.WriteLine("lcs score = {0}", lcs.ToString());
            Console.WriteLine("mlcs score = {0}", mlcs.ToString());
            Console.WriteLine("ng score = {0}", ng.ToString());
            Console.WriteLine("nl score = {0}", nl.ToString());
            Console.WriteLine("osa score = {0}", osa.ToString());
            Console.WriteLine("qg score = {0}", qg.ToString());
            //Console.WriteLine("ng score = {0}",ng.ToString());
            Console.WriteLine("sd score = {0}", sd.ToString());
            //Console.WriteLine("wl score = {0}",wl.ToString());
        }

        public void StringMetricHeader()
        {
            Console.WriteLine(@"bucketname   Results consine   damerau jaccard jarodwinkler    levenshtein   lcs mlcs    ng  nl  osa qg  sd");
        }

        public string[] CalculateStringMetric(string s1, string s2)
        {
            var results = new string[12];
            
            results[0] = Math.Round(new F23.StringSimilarity.Cosine().Distance(s1, s2),3).ToString();
            results[1] = Math.Round(new F23.StringSimilarity.Damerau().Distance(s1, s2),3).ToString();
            results[2] = Math.Round(new F23.StringSimilarity.Jaccard().Distance(s1, s2),3).ToString();
            results[3] = Math.Round(new F23.StringSimilarity.JaroWinkler().Distance(s1, s2),3).ToString();
            results[4] = Math.Round(new F23.StringSimilarity.Levenshtein().Distance(s1, s2),3).ToString();

            results[5] = Math.Round(new F23.StringSimilarity.LongestCommonSubsequence().Distance(s1, s2),3).ToString();
            results[6] = Math.Round(new F23.StringSimilarity.MetricLCS().Distance(s1, s2),3).ToString();
            results[7] = Math.Round(new F23.StringSimilarity.NGram().Distance(s1, s2),3).ToString();
            results[8] = Math.Round(new F23.StringSimilarity.NormalizedLevenshtein().Distance(s1, s2),3).ToString();
            results[9] = Math.Round(new F23.StringSimilarity.OptimalStringAlignment().Distance(s1, s2),3).ToString();
            results[10] = Math.Round(new F23.StringSimilarity.QGram().Distance(s1, s2),3).ToString();
            results[11] = Math.Round(new F23.StringSimilarity.SorensenDice().Distance(s1, s2),3).ToString();

            return results;
        }

    }
}
