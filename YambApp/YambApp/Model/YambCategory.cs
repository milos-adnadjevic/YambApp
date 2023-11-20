using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Model
{
    public class YambCategory
    {
        public string CategoryName { get; set; }
        public string Down { get; set; }
        public string Free { get; set; }
        public string Up { get; set; }
        public string Lock { get; set; }
        //public int Player1Score { get; set; }
        //public int Player2Score { get; set; }

        public YambCategory(string categoryName)
        {
            CategoryName = categoryName;

            //Player1Score = -1;  // -1 indicates the category is not scored yet
           // Player2Score = -1;  // -1 indicates the category is not scored yet
        }

        public YambCategory(string categoryName, string up, string free, string down, string @lock) 
        {
            Down = down;
            Free = free;
            Up = up;
            Lock = @lock;
        }

        public YambCategory() { }
    }
}
