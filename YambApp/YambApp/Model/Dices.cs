using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Model
{
    public class Dices
    {
        public int Value;
        public bool Holded;

     
        public Dices() { 
        
        }
        public Dices(int value) 
        { Value = value; Holded = false; }

    }
}
