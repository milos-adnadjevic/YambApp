using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Service
{
    public interface ISumRules
    {
        int SumDices(List<int> dices);
        int SumOnes(List<int> dices);

        int SumTwos(List<int> dices);

        int SumThrees(List<int> dices);

        int SumFours(List<int> dices);

        int SumFives(List<int> dices);

        int SumSixes(List<int> dices);
        int SumYamb(List<int> dices, bool gameWithBonus);

        int SumPocker(List<int> dices, bool gameWithBonus);

        int SumFullHouse(List<int> dices, bool gameWithBonus);

        int SumStraight(List<int> dices, bool gameWithBonus);
    }
}
