using pr14.classs;
using pr14.Pags;
using pr14.WINDOWS;
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
            Base.DB = new Entities2();
            ClassFrame.frame = fMain;
            Admin = false;
            ClassFrame.frame.Navigate(new Servislist(Admin));
        }

       

        private void tbLoginAdmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Admin == false)
            {
                Admin loginAdminWindow = new Admin();
                loginAdminWindow.ShowDialog();
                if (Admin == true)
                {
                    ClassFrame.frame.Navigate(new Servislist(Admin));
                }
            }
            else
            {
                if (MessageBox.Show("Вы уверены что хотите выйти из режима администратора?", "Системное сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Admin = false;
                    MessageBox.Show("Режим администратора выключен");
                    ClassFrame.frame.Navigate(new Servislist(Admin));
                }
            }
        }
    }
}
