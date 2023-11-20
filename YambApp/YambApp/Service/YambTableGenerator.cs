using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;
using YambApp.Repository;

namespace YambApp.Service
{
    public class YambTableGenerator:IYambTableGenerator
    {
        IYambRepository _yambRepository;
        public YambTableGenerator()
        {
            _yambRepository = new YambRepository();
        }



        public List<YambCategory> GetRows()
        {
            return _yambRepository.GetRows();
        }
    }
}
