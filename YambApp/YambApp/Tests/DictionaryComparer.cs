using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Tests
{
    public class DictionaryComparer
    {
        public  bool AreDictionariesEqual(Dictionary<Dictionary<int, int>, int> expected, Dictionary<Dictionary<int, int>, int> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }



            foreach (Dictionary<int, int> key in expected.Keys)
            {
                bool flag = true;
                foreach (Dictionary<int, int> key2 in actual.Keys)
                {
                    if (AreSmallDicionariesEqual(key,key2))
                    {
                        flag = false;
                        
                        if (!expected[key].Equals(actual[key2]))
                        {
                            return false;
                        }
                    }
                }
                if (flag)
                {
                    return false;
                }

            }

            return true;

        }


        private bool AreSmallDicionariesEqual(Dictionary<int,int> expected, Dictionary<int, int> actual)
        {

            if (expected.Count != actual.Count)
            {
                return false;
            }

            foreach (var key in expected.Keys)
            {
                if (!actual.ContainsKey(key) || actual[key] != expected[key])
                {
                    return false;
                }
            }

            return true;


        }

    }
}

