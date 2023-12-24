using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.ViewModels
{
    public class SettingsViewModel
    {
        public SettingsViewModel()
        {
            LivelloLogItems = new ObservableCollection<string>
            {
                "OK",
                "Ciao"
            };



        }
        #region -- ------------ Property
        public ObservableCollection<string> LivelloLogItems { get; set; }
        public string TestoCentrale { get; set; } = "Da questa pagina è possibile gestire tutte le impostazioni della serra in modo da personalizzare il più possibile l'esperienza";
        #endregion
    }
}
