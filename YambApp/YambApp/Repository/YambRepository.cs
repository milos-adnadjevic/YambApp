using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Repository
{
    public class YambRepository:IYambRepository
    {

        List<YambCategory> yambCategories = new List<YambCategory>
        {
                new YambCategory("Ones"),
                new YambCategory("Twos"),
                new YambCategory("Threes"),
                new YambCategory("Fours"),
                new YambCategory("Fives"),
                new YambCategory("Sixes"),
                new YambCategory("Max"),
                new YambCategory("Min"),
                new YambCategory("Straight"),
                new YambCategory("Full House"),
                new YambCategory("Poker"),
                new YambCategory("Yamb")
        };
        public YambRepository() { }

        public List<YambCategory> GetRows()
        {
            return yambCategories;
        }

        
      
        
    }
}
