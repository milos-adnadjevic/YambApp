using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;
using YambApp.Repository;

namespace YambApp.Service
{
    public class YambService
    {
        public YambService() { }

        YambRepository yambRepository = new YambRepository();

        public List<YambCategory> GetRows()
        {
            return yambRepository.GetRows();
        }

        //random generate 5 numbers for dices values
        public List<Dices> RollDices()
        {
            var r = new Random();
            var dices = new List<Dices>(5);
            for (int i = 0; i < 5; i++) {

                int rand = r.Next(1, 7);

                dices.Add(new Dices(rand));

                Console.WriteLine(dices[i].Value);
            }
            return dices;


        }

        //get sum of all dices
        private int SumDices(List<int> dices)
        {
            int sum = 0;
            foreach(int dice in dices) sum=sum+dice;
            return sum;
        }

        // sum only dices which value is one
        public int SumOnes(List<int> dices)
        {
            int sumOnes = 0;
            foreach (int i in dices)
            {
                if (i == 1) sumOnes = sumOnes + i;
            }
            return sumOnes;

        }


        // sum only dices which value is two
        public int SumTwos(List<int> dices)
        {
            int sumTwos = 0;
            foreach (int i in dices)
            {
                if (i == 2) sumTwos = sumTwos + i;
            }
            return sumTwos;

        }

        // sum only dices which value is three
        public int SumThrees(List<int> dices)
        {
            int sumThrees = 0;
            foreach (int i in dices)
            {
                if (i == 3) sumThrees = sumThrees + i;
            }
            return sumThrees;

        }

        // sum only dices which value is four
        public int SumFours(List<int> dices)
        {
            int sumFours = 0;
            foreach (int i in dices)
            {
                if (i == 4) sumFours = sumFours + i;
            }
            return sumFours;

        }

        // sum only dices which value is five
        public int SumFives(List<int> dices)
        {
            int sumFives = 0;
            foreach (int i in dices)
            {
                if (i == 5) sumFives = sumFives + i;
            }
            return sumFives;

        }

        // sum only dices which value is six
        public int SumSixes(List<int> dices)
        {
            int sumSixes = 0;
            foreach (int i in dices)
            {
                if (i == 6) sumSixes = sumSixes + i;
            }
            return sumSixes;

        }
        // find if combination of dices is yamb and sum it with bonus or without
        public int SumYamb(List<int> dices, bool gameWithBonus)
        {
            int sumYamb = 0;
            if (dices[0] == dices[4])
            {
                if (gameWithBonus) sumYamb = dices[0] * 5 + 50;
                else sumYamb = dices[0] * 5;
            }
            return sumYamb;
        }

        // find if combination of dices is pocker and sum it with bonus or without
        public int SumPocker(List<int> dices, bool gameWithBonus)
        {
            int sumPocker = 0;
            if (dices[0] == dices[3] || dices[1] == dices[4])
            {
                if (gameWithBonus) sumPocker = dices[3] * 4 + 40;
                else sumPocker = dices[3] * 4;
            }
            return sumPocker;
        }

        // find if combination of dices is full house and sum it with bonus or without
        public int SumFullHouse(List<int> dices, bool gameWithBonus)
        {
            int sumFullHouse = 0;
            if ((dices[0] == dices[1] && dices[2] == dices[4]) || (dices[0] == dices[2] && dices[3] == dices[4]))
            {
                if (gameWithBonus) sumFullHouse = SumDices(dices) + 30;
                else sumFullHouse = SumDices(dices);
            }
                return sumFullHouse;
        }

        // find if combination of straight is yamb and get points for that
        public int SumStraight(List<int> dices, bool gameWithBonus)
        {
            int sumStraight = 0;
            if (dices[0] == dices[1]-1 && dices[1] == dices[2]-1 && dices[2] == dices[3]-1 && dices[3] == dices[4] - 1)
            {
                if (gameWithBonus)
                {
                    if (dices[0] == 1) return sumStraight = 45;
                    if (dices[0] == 2) return sumStraight = 50;
                }
                else return sumStraight = SumDices(dices);
                
                
            }
            return sumStraight;
        }

        public Dictionary<Dictionary<int,int>,int> BestChoice(Dictionary<int,List<int>> freeFieldsIndexes, List<int> dices, bool gameWithBonus)
        {
            Dictionary<Dictionary<int, int>, int> indexesValue= new Dictionary<Dictionary<int, int>, int>();
            Dictionary<int,int> indexes = new Dictionary<int,int>();
            int value = 0;
          
            foreach (var row in freeFieldsIndexes)
            {
               

                foreach (int col in row.Value)
                {
                    indexes = new Dictionary<int, int>();
                    indexes.Add(row.Key, col);
                    if (row.Key == 0)
                    {

                        value = SumOnes(dices);

                    }
                    else if (row.Key == 1)
                    {

                        value = SumTwos(dices);
                        
                    }
                    else if (row.Key == 2)
                    {
                        
                        value = SumThrees(dices);
                        
                    }
                    else if (row.Key == 3)
                    {
                       
                        value = SumFours(dices);
                       
                    }
                    else if (row.Key == 4)
                    {
                       
                        value = SumFives(dices);
                       
                    }
                    else if (row.Key == 5)
                    {
                       
                        value = SumSixes(dices);
                       
                    }

                    else if (row.Key == 6)
                    {

                        value = SumDices(dices);

                    }
                    else if (row.Key == 7)
                    {

                        value = SumDices(dices);

                    }

                    else if (row.Key == 8)
                    {
                        
                        value = SumStraight(dices, gameWithBonus);
                       
                    }
                    else if (row.Key == 9)
                    {
                       
                        value = SumFullHouse(dices,gameWithBonus);
                      
                    }
                    else if (row.Key == 10)
                    {
                        
                        value = SumPocker(dices, gameWithBonus);
                       
                    }
                    else if (row.Key == 11)
                    {
                       
                        value = SumYamb(dices, gameWithBonus);
                        
                    }

                    indexesValue.Add(indexes, value);
                }
            }
            foreach (var j in indexesValue)
            {
                foreach (var ind in j.Key)
                {
                    Console.Write("Row:" + ind.Key + " " + "Column:" + ind.Value +" "+ "Value:"+j.Value);
                    Console.WriteLine();
                }
            }

                return indexesValue;
        }
    }
}
