using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YambApp.Service
{
    public interface INavigationService
    {
        void NavigateTo(string pageKey,Window currentWindow);
    }
}
