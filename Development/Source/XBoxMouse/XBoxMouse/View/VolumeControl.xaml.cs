using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XBoxMouse.ViewModel;

namespace XBoxMouse.View
{
    /// <summary>
    /// Interaction logic for VolumeControl.xaml
    /// </summary>
    public partial class VolumeControl : Window
    {
        
        public VolumeControl()
        {
            InitializeComponent();
            Opacity = 0;

            VolumeControlModel model = new VolumeControlModel();
            DataContext = model;
            model.View = this;

        }
    }
}
