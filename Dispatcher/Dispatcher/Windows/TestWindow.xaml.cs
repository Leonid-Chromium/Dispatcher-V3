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
using System.Windows.Shapes;

namespace Dispatcher.Windows
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Process proc = Process.GetProcessesByName("Dispatcher")[0];
            Trace.WriteLine($"ID: {proc.Id}");

            ProcessThreadCollection processThreads = proc.Threads;

            foreach (ProcessThread thread in processThreads)
            {
                Trace.WriteLine($"ThreadId: {thread.Id}  StartTime: {thread.StartTime}");
            }

            ProcessModuleCollection modules = proc.Modules;

            foreach (ProcessModule module in modules)
            {
                Trace.WriteLine($"Name: {module.ModuleName}  MemorySize: {module.ModuleMemorySize}");
            }
        }
    }
}
