using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LevelBackup
{
    /// <summary>
    /// Interaction logic for LandingWPF.xaml
    /// </summary>
    public partial class LandingWPF : UserControl
    {
        public Action OnManageLevels;
        public Action OnManageConfigs;
        public Action OnManageBehaviours;

        public LandingWPF()
        {
            InitializeComponent();
        }
        public void SetVersionInfo(string version)
        {
            VersionText.Content = "Version " + version;
        }

        private void ManageLevels(object sender, RoutedEventArgs e)
        {
            OnManageLevels?.Invoke();
        }
        private void ManageConfigs(object sender, RoutedEventArgs e)
        {
            OnManageConfigs?.Invoke();
        }
        private void ManageBehaviours(object sender, RoutedEventArgs e)
        {
            OnManageBehaviours?.Invoke();
        }
    }
}
