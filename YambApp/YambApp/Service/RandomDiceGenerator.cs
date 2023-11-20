using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Service
{
    public class RandomDiceGenerator : IRandomDiceGenerator
    {
        public RandomDiceGenerator()
        {
        }

        //random generate 5 numbers for dices values
        public List<Dices> RollDices()
        {
            var r = new Random();
            var dices = new List<Dices>(5);
            for (int i = 0; i < 5; i++)
            {

                int rand = r.Next(1, 7);

                dices.Add(new Dices(rand));

                Console.WriteLine(dices[i].Value);
            }
            return dices;


        }
    }
}
