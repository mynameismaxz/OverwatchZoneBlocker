using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace OverwatchZoneBlocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FetchDataFromServer();
            WriteDownListIP();
        }
        
        private void FetchDataFromServer()
        {
            throw new NotImplementedException();
        }

        private void WriteDownListIP()
        {
            throw new NotImplementedException();
        }
        
        private void BtnBlock_Click(object sender, RoutedEventArgs e)
        {
            BtnBlockEvent();
        }

        private void BtnBlock_Copy_Click(object sender, RoutedEventArgs e)
        {
            BtnUnblockEvent();
        }

        // TODO: make function to doing on button
        private void BtnBlockEvent()
        {
            try
            {
                // todo: make arguments of commands prompt to loop data from server.
                // loop firewall command :  for /f %i in (ips.txt) do echo netsh advfirewall firewall add rule name="Block %i" dir=in protocol=any action=block remoteip=%i
                var proc = new Process {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = "/c netsh advfirewall firewall add rule name=\"IP Block\" dir=in interface=any action=block remoteip=192.169.0.10/32",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    Dispatcher.Invoke(()=> {
                        textBox.Text += line + "\n";
                    });
                }              
            } catch (Exception e)
            {
                Console.WriteLine("Error : " + e.ToString());
            }
            MessageBox.Show("Btn Block Event!");
        }

        private void BtnUnblockEvent()
        {
            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = "/c netsh advfirewall reset",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    Dispatcher.Invoke(() => {
                        textBox.Text += line + "\n";
                    });
                }
                //Process.Start("cmd", "/c netsh advfirewall reset");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unblock Error : " + e.ToString());
            }
            MessageBox.Show("Btn Unblock Event!");
        }
    }
}
