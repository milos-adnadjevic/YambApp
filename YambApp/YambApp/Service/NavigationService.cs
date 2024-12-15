using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YambApp.Service
{
    public class NavigationService : INavigationService
    {
        private readonly IDictionary<string, Type> _pageMappings;

        public NavigationService()
        {
            _pageMappings = new Dictionary<string, Type>
        {
            { "Login", typeof(LoginWindow) },
            {"Home" ,typeof(StartGameWindow) },
            {"Registration",typeof(RegistrationWindow) },
            { "Game", typeof(MainWindow) },
            { "Scoreboard", typeof(PlayersScoresWindow) }



        };
        }

        public void NavigateTo(string pageKey, Window currentWindow=null)
        {
            if (_pageMappings.ContainsKey(pageKey))
            {
                var pageType = _pageMappings[pageKey];
                var page = (Window)Activator.CreateInstance(pageType);
                page.Show();

                currentWindow?.Close();
            }
        }
    }
}
