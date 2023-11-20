using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YambApp.ViewModel
{

        public class EventTuple
        {
            public object Sender { get; set; }
            public MouseButtonEventArgs MouseEventArgs { get; set; }
        }
    
}
