using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Service
{
    public class SumRules:ISumRules
    {
        //get sum of all dices
        public int SumDices(List<int> dices)
        {
            int sum = 0;
            foreach (int dice in dices) sum = sum + dice;
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
            if ((dices[0] == dices[1] && dices[2] == dices[4] && dices[0] != dices[4]) || (dices[0] == dices[2] && dices[3] == dices[4] && dices[0] != dices[4]))
            {
                if (gameWithBonus) sumFullHouse = SumDices(dices) + 30;
                else sumFullHouse = SumDices(dices);
            }
            return sumFullHouse;
        }

        // find if combination of straight is yamb and get points for that
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dices"></param>
        /// <param name="gameWithBonus"></param>
        /// <returns></returns>
        public int SumStraight(List<int> dices, bool gameWithBonus)
        {
            int sumStraight = 0;
            if (dices[0] == dices[1] - 1 && dices[1] == dices[2] - 1 && dices[2] == dices[3] - 1 && dices[3] == dices[4] - 1)
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
    }
}
