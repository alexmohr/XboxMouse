using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Configuration;
using XBoxMouse.Properties;

namespace XBoxMouse.ViewModel
{
    class ShellViewModel
    {
        private ConfigurationManager _configManager;

        public void Init()
        {
            try
            {
                _configManager = new ConfigurationManager();
                _configManager.ReadConfig(Settings.Default.StartupConfiguration);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to read configuration file!", "Config invalid", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }
    }
}
