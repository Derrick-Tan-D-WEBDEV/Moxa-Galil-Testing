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
using MoxaIO_Lib;

namespace Interfaces
{
    /// <summary>
    /// Interaction logic for IpSettings.xaml
    /// </summary>
    public partial class IpSettings : Window
    {
        MXIO MXIO = new MXIO();
        public IpSettings()
        {
            InitializeComponent();
            input_ipaddress_textbox.Text = MXIO.getInputIpAddress();
            output_ipaddress_textbox.Text = MXIO.getOutputIpAddress();

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

        private void SaveIpAddress(object sender, RoutedEventArgs e)
        {
            if (input_ipaddress_textbox.Text != "" && output_ipaddress_textbox.Text != "")
            {
                MXIO.setInputIpAddress(input_ipaddress_textbox.Text);
                MXIO.setOutputIpAddress(output_ipaddress_textbox.Text);

                
                (Application.Current.MainWindow as MainWindow).writeInStatusInfoBlock("Info", "Input IP Address set to : " + input_ipaddress_textbox.Text);
                (Application.Current.MainWindow as MainWindow).writeInStatusInfoBlock("Info", "Output IP Address set to : " + output_ipaddress_textbox.Text);
                this.Close();
                (Application.Current.MainWindow as MainWindow).initializeMoxa();
                
            }
            else
            {
                MessageBox.Show("Please input all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }


}
