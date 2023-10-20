using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;
using YambApp.Repository;
using YambApp.Service;

namespace YambApp.Controller
{
    public class YambController
    {
        public YambController() { }

        public YambService yambService = new YambService();


        public List<YambCategory> GetRows()
        {
            return yambService.GetRows();
        }

        public List<Dices> RollDices()
        {
            return yambService.RollDices();
        }

        public int SumOnes(List<int> dices)
        {
            return yambService.SumOnes(dices);
        }

        public int SumTwos(List<int> dices)
        {
            return yambService.SumTwos(dices);
        }

        public int SumThrees(List<int> dices)
        {
            return yambService.SumThrees(dices);
        }

        public int SumFours(List<int> dices)
        {
            return yambService.SumFours(dices);
        }

        public int SumFives(List<int> dices)
        {
            return yambService.SumFives(dices);
        }
        public int SumSixes(List<int> dices)
        {
            return yambService.SumSixes(dices);
        }
        public int SumYamb(List<int> dices, bool gameWithBonus)
        {
            return yambService.SumYamb(dices, gameWithBonus);
        }

        public int SumPocker(List<int> dices, bool gameWithBonus)
        {
            return yambService.SumPocker(dices,  gameWithBonus);
        }

        public int SumFullHouse(List<int> dices, bool gameWithBonus)
        {
            return yambService.SumFullHouse(dices, gameWithBonus);
        }

        public int SumStraight(List<int> dices, bool gameWithBonus)
        {
            return yambService.SumStraight(dices, gameWithBonus);
        }

        public Dictionary<Dictionary<int, int>, int> BestChoice(Dictionary<int, List<int>> freeFieldsIndexes, List<int> dices,bool gameWithBonus)
        {
            return yambService.BestChoice(freeFieldsIndexes, dices,gameWithBonus);
        }
    }
}
