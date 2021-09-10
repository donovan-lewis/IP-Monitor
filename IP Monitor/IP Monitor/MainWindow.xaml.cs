using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Net.NetworkInformation;
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;

// To Do 
// ------------
// Try and make images dynamic so that we can support more devices clearly (Inserting any IP or URL will work but it will always show the pi image)
// Add custom names so we can display not just the ip but also the name of the device most of this is implemented in the UI just need to actually write the code

namespace IP_Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 15); // Send the ping every 15 seconds
        }

        private void OpenFIle_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "CSV Files|*.csv";
            open.FilterIndex = 0;

            open.FileOk += Open_FileOk;
            open.ShowDialog();
        }

        private void Open_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenFileDialog open = sender as OpenFileDialog;

            Path.Text = open.FileName;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (Path.Text.Trim() == "")
            {
                MessageBox.Show("No path selected. Either input the path to your file or click the ... button to get a file explorer", "ERROR - [No Path]", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(Path.Text);
            }
            catch
            {
                MessageBox.Show("Unable to read file please verify the file is not open anywhere else or in use by another application", "Error - [File Read]", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int count = 1;

            Machines.Children.Clear();
            foreach(string ip in lines[0].Split(','))
            {
                GenerateGrid("", ip, count);
                count++;
            }

            Start.Content = "Stop";
            Start.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B2FF0000"));
            Start.Click += Stop_Click;

            PingMachine();

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            PingMachine();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Machines.Children.Clear();
            Start.Content = "Start";
            Start.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B200FF00"));
            Start.Click += Start_Click;
            timer.Stop();
        }

        private void PingMachine()
        {
            // The actual ping object
            Ping ping = new Ping();

            // What data will be in the packet and get the ascii bytes of that data
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Argument meaning
            // How many devices we can go through, Data packet cannot be fragmented 
            PingOptions options = new PingOptions(128, true);

            // Timeout
            int timeout = 1000;

            IEnumerable<Grid> grids = Machines.Children.OfType<Grid>();

            foreach(Grid g in grids)
            {
                if (Machines.Children.OfType<Grid>().ToArray().Length > 0)
                {
                    try
                    {
                        PingReply reply = ping.Send(g.Children.OfType<TextBlock>().ToArray()[1].Text, timeout, buffer, options);

                        // Set the GUI
                        if (reply.Status == IPStatus.Success)
                            g.Children.OfType<Image>().ToArray()[1].Source = new BitmapImage(new Uri("/On.png", UriKind.Relative));
                        else
                            g.Children.OfType<Image>().ToArray()[1].Source = new BitmapImage(new Uri("/Off.png", UriKind.Relative));

                        logBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss:ffffff}: Sent ping to {g.Children.OfType<TextBlock>().ToArray()[1].Text} with a result of {(reply.Status == IPStatus.Success ? "Success" : "Fail")}\n");
                        logBox.ScrollToEnd();
                    }
                    catch
                    {
                        g.Children.OfType<Image>().LastOrDefault().Source = new BitmapImage(new Uri("/Off.png", UriKind.Relative));
                        logBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss:ffffff}: Sent ping to {g.Children.OfType<TextBlock>().ToArray()[1].Text} with a result of Fail\n");
                        logBox.ScrollToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Generates the UI Elements for the machines area of the display
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ip"></param>
        /// <param name="even"></param>
        private void GenerateGrid(string name, string ip, int even)
        {
            Grid g = new Grid();
            g.Height = 100;
            g.Background = even % 2 == 0 ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9B9B9B")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCFCFCF"));

            Image pi = new Image();
            BitmapImage b = new BitmapImage(new Uri("/motherboard-pi-4-1024x701.png", UriKind.Relative));
            pi.Source = b;

            pi.Width = 120;
            pi.HorizontalAlignment = HorizontalAlignment.Left;
            pi.Stretch = Stretch.Uniform;
            pi.Margin = new Thickness(0, 0, 0, 10);

            g.Children.Add(pi);

            TextBlock n = new TextBlock();
            n.Text = name;
            n.TextWrapping = TextWrapping.Wrap;
            n.VerticalAlignment = VerticalAlignment.Top;
            n.HorizontalAlignment = HorizontalAlignment.Center;
            n.FontSize = 20;
            n.Margin = new Thickness(0, 18, 0, 0);

            g.Children.Add(n);

            TextBlock i = new TextBlock();
            i.Text = ip;
            i.TextWrapping = TextWrapping.Wrap;
            i.VerticalAlignment = VerticalAlignment.Top;
            i.HorizontalAlignment = HorizontalAlignment.Center;
            i.FontSize = 20;
            i.Margin = new Thickness(0, 62, 0, 0);

            g.Children.Add(i);

            Image up = new Image();
            up.Source = new BitmapImage(new Uri("/On.png", UriKind.Relative));
            up.Width = 57;
            up.HorizontalAlignment = HorizontalAlignment.Right;
            up.Stretch = Stretch.Uniform;
            up.Margin = new Thickness(0, 22, 10, 22);

            g.Children.Add(up);

            Machines.Children.Add(g);
        }
    }
}
