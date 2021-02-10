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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Interfaces
{
    /// <summary>
    /// Interaction logic for GalilControl.xaml
    /// </summary>
    public partial class GalilControl : Window
    {
        DispatcherTimer tm;
        GalilFunctions galilFunctions;
        public GalilControl()
        {
            InitializeComponent();

            galilFunctions = new GalilFunctions();
            galilFunctions.GalilInit();
            tm = new DispatcherTimer();
            tm.Tick += new System.EventHandler(DisplayPositionXYZ);
            tm.Interval = new TimeSpan(0, 0, 0);
            tm.Start();
        }
        private void DragApplications(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseApplicaitons(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DisplayPositionXYZ(object sender, EventArgs e)
        {
            var xpos = galilFunctions.getPosition(0);
            xpos_curr.Content = xpos;
            var ypos = galilFunctions.getPosition(1);
            ypos_curr.Content = ypos;
            var zpos = galilFunctions.getPosition(2);
            zpos_curr.Content = zpos;
        }

        private void HomeXYZ(object sender, EventArgs e)
        {
            galilFunctions.homingXYZ();
        }

        private void HomeX(object sender, EventArgs e)
        {
            galilFunctions.homing(0);
        }

        private void HomeY(object sender, EventArgs e)
        {
            galilFunctions.homing(1);
        }

        private void HomeZ(object sender, EventArgs e)
        {
            galilFunctions.homing(2);
        }

        private void StopX(object sender, EventArgs e)
        {
            galilFunctions.stop(0);
        }

        private void JogXF(object sender, EventArgs e)
        {
            galilFunctions.jog(0,0);
        }

        private void JogXR(object sender, EventArgs e)
        {
            galilFunctions.jog(0, 1);
        }
    }
}
