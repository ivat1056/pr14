using pr14.classs;
using pr14.Pags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace pr14
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool Admin;
        public MainWindow()
        {

            InitializeComponent();
            Base.DB = new Entities();
            ClassFrame.frame = fMain;
            Admin = false;
            ClassFrame.frame.Navigate(new Servislist(Admin));
        }
    }
}
