using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Repository
{
    public interface IYambRepository
    {
        List<YambCategory> GetRows();
    }
}
