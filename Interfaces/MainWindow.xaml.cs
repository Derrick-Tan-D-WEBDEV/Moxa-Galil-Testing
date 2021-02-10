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

namespace Interfaces
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MXIO MXIO = new MXIO();
        DispatcherTimer tm;

        public MainWindow()
        {
            
            InitializeComponent();
            Grid InputGrid = Input_Grid;
            
            //load input button
            for (int i = 0; i <= 15; i++)
            {
                CreateAButton(InputGrid, i, Color.FromArgb(255, 186, 19, 0), Color.FromArgb(0, 186, 19, 0));
            }

            Grid OutputGrid = Output_Grid;
            //load input button
            for (int i = 0; i <= 15; i++)
            {
                CreateAButton(OutputGrid, i, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(0, 186, 19, 0), "O");
            }
            initializeMoxa();

        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            uint ReturnInputValue = MXIO.ReadMoxaInputAllBytes();
            var grid_input = Input_Grid;
            loopinput_output(ReturnInputValue, grid_input,"Input");

            uint ReturnOutputValue = MXIO.ReadMoxaOutputAllBytes();
            var grid_output = Output_Grid;
            loopinput_output(ReturnOutputValue, grid_output,"Output");


        }

        private static void loopinput_output(uint ReturnValue, Grid grid_output, String type)
        {
            if (type == "Input")
            {
                var loop_int = 0;
                foreach (Button btn in grid_output.Children)
                {
                    btn.Background = (ReturnValue & (0x0001 << loop_int)) > 0 ? Brushes.LightGreen : Brushes.DarkGreen;
                    loop_int += 1;
                }
            }
            else if(type == "Output")
            {
                var loop_int = 0;
                foreach (Button btn in grid_output.Children)
                {
                    btn.Background = (ReturnValue & (0x0001 << loop_int)) > 0 ? Brushes.LightCoral : Brushes.DarkRed;
                    loop_int += 1;
                }
            }


        }

        private void output_click(object sender, RoutedEventArgs e)
        {
            String name = (sender as Button).Name;
            writeInStatusInfoBlock("Info", "Output I/O Triggered on Button(" + name + ")");
            
            var no = Int32.Parse(name.Replace("DO", ""));
            bool state = MXIO.ReadMoxaOutput(no) ? true : false;
            MXIO.WriteMoxaOutput(no, !state);
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
            Environment.Exit(0);
        }

        private void OpenGalil(object sender, RoutedEventArgs e)
        {
            GalilControl galil_control = new GalilControl();
            galil_control.Show();
        }

        private void CreateAButton(Grid grid, int i, Color color, Color border_color, String IO  = null)
        {
            if (i == 1)
            {
                Button btn = new Button();
                btn.Height = 50;
                btn.Width = 50;
                btn.Content = i.ToString();
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.Background = new SolidColorBrush(color);
                btn.BorderBrush = new SolidColorBrush(border_color);
                
                if(IO == "O")
                {
                    btn.Name = "DO" + i;
                    btn.Click += new RoutedEventHandler(output_click);
                }
                    

                Grid.SetColumn(btn, i);
                grid.Children.Add(btn);
            }
            else
            {
                Button btn = new Button();
                btn.Height = 50;
                btn.Width = 50;
                btn.Content = i.ToString();
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.Background = new SolidColorBrush(color);
                btn.BorderBrush = new SolidColorBrush(border_color);
                if (IO == "O")
                {
                    btn.Name = "DO" + i;
                    btn.Click += new RoutedEventHandler(output_click);
                }

                Grid.SetColumn(btn, i);
                grid.Children.Add(btn);
            }

            ((MainWindow)System.Windows.Application.Current.MainWindow).UpdateLayout();
        }

        public void writeInStatusInfoBlock(String type,String text)
        {
            //statusInfo.Text += text + Environment.NewLine;
            //ScrollViewer scrollViewer = statusInfoScroll;
            //scrollViewer.ScrollToBottom();
            if (type == "Error")
            {
                Run run = new Run(type + ": " + text + "\r\n");
                run.Foreground = Brushes.Red;
                statusInfo.Inlines.Add(run);
                ScrollViewer scrollViewer = statusInfoScroll;
                scrollViewer.ScrollToBottom();
            }
            else if (type == "Info")
            {
                Run run = new Run(type + ": " + text + "\r\n");
                run.Foreground = Brushes.Blue;
                statusInfo.Inlines.Add(run);
                ScrollViewer scrollViewer = statusInfoScroll;
                scrollViewer.ScrollToBottom();

            }

        }

        public void initializeMoxa()
        {
            Image plc_img = PLC_img;
            writeInStatusInfoBlock("Info", "Intializing Moxa...");
            try
            {
                MXIO.InitMoxaIO();

                lblConnection.Text = "Connected";
                lblConnection.Foreground = Brushes.LimeGreen;
                plc_img.Opacity = 1;
                tm = new DispatcherTimer();
                tm.Tick += Tm_Tick;
                tm.Start();
                writeInStatusInfoBlock("Info", "Connected to Moxa");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ex.Message + "Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                lblConnection.Text = "Disconnected";
                lblConnection.Foreground = Brushes.Red;
                plc_img.Opacity = 0.2;
                writeInStatusInfoBlock("Error", "Error connecting to Moxa");

                IpSettings ipSettigs = new IpSettings();
                ipSettigs.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                ipSettigs.Topmost = true;
                ipSettigs.Show();
            }
        }
    }
}
