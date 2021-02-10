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
using System.Windows.Threading;
using MoxaIO_Lib;

namespace Counter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MXIO MXIO = new MXIO();
        DispatcherTimer tm;
        int Counter = 0;
        bool Press = false;
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // Connecting input & output card
                MXIO.InitMoxaIO();

                lblConnection.Content = "Connected";
                lblConnection.Background = Brushes.Lime;

                tm = new DispatcherTimer();
                //tm.Interval = 10;
                TimeSpan ts = tm.Interval;
                MessageBox.Show(ts.TotalMilliseconds.ToString());
                tm.Tick += Tm_Tick;
                tm.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ex.Message);

                lblConnection.Content = "Disconnected";
                lblConnection.Background = Brushes.Red;
            }
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            if (MXIO.ReadMoxaInput(0) && !Press)
            {
                Counter++;
                lblCounter.Content = Counter.ToString("0000");
                Press = true;
            }
            else if (MXIO.ReadMoxaInput(1) && !Press)
            {
                Counter--;
                lblCounter.Content = Counter.ToString("0000");
                Press = true;
            }
            else if (!MXIO.ReadMoxaInput(0) && !MXIO.ReadMoxaInput(1))
                Press = false;

            if(Counter >= 10)
            {
                MXIO.WriteMoxaOutput(0, false);
                MXIO.WriteMoxaOutput(1, true);
            }
            else if(Counter <= 0)
            {
                MXIO.WriteMoxaOutput(0, true);
                MXIO.WriteMoxaOutput(1, false);
            }
            else
            {
                MXIO.WriteMoxaOutput(0, false);
                MXIO.WriteMoxaOutput(1, false);
            }
        }
    }
}
