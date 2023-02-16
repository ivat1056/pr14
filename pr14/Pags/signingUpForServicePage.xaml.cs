using pr14.classs;
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

namespace pr14.Pags
{
    /// <summary>
    /// Логика взаимодействия для signingUpForServicePage.xaml
    /// </summary>
    public partial class signingUpForServicePage : Page
    {
        Service service;
        public signingUpForServicePage(Service service)
        {
            InitializeComponent();
            this.service = service;
            tbNameService.Text = "Услуга: " + service.Title;
            tbDurationInMinute.Text = "Длительность услуги: " + service.DurationInMinute + " минут";
            cbClient.ItemsSource = Base.DB.Client.ToList();
            cbClient.SelectedValuePath = "ID";
            cbClient.DisplayMemberPath = "FIO";
        }
    }
}
