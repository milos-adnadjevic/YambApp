using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Service
{
    public interface IRandomDiceGenerator
    {

        //random generate 5 numbers for dices values
        List<Dices> RollDices();
      
    }
}
