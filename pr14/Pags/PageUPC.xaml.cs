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
using System.Windows.Threading;

namespace pr14.Pags
{
    /// <summary>
    /// Логика взаимодействия для PageUPC.xaml
    /// </summary>
    public partial class PageUPC : Page
    {
        public PageUPC()
        {
            InitializeComponent();
            loadedData();
        }
        private void loadedData()
        {
            List<ClientService> clientServices = Base.DB.ClientService.ToList();
            clientServices = clientServices.Where(x => x.StartTime >= DateTime.Now).ToList(); 
            DateTime endDateTime = DateTime.Today.AddDays(2).AddTicks(-1); 
            clientServices = clientServices.Where(x => x.StartTime < endDateTime).ToList(); 
            clientServices.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            lvListClientService.ItemsSource = clientServices;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new Servislist(true));
        }
        private void dtTicker(object sender, EventArgs e)
        {
            loadedData();
        }
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(30);
            dispatcherTimer.Tick += dtTicker;
            dispatcherTimer.Start();
        }
    }
}
