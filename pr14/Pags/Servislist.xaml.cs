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
    /// Логика взаимодействия для Servislist.xaml
    /// </summary>
    public partial class Servislist : Page
    {
        bool Admin;
        public Servislist(bool Admin)
        {
            InitializeComponent();
            this.Admin = Admin;
            lvListService.ItemsSource = Base.DB.Service.ToList();
            tbCurrentCount.Text = Convert.ToString(lvListService.Items.Count);
            tbAllCount.Text = Convert.ToString(Base.DB.Service.ToList().Count);
            cbDiscount.SelectedIndex = 0;
        }

        private void tbDiscount_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if (textBlock.Uid != null)
            {
                textBlock.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock.Visibility = Visibility.Hidden;
            }
        }
        public void Filter()
        {
            List<Service> services = new List<Service>();
            services = Base.DB.Service.ToList();

            if (!string.IsNullOrWhiteSpace(tbSearchName.Text))  
            {
                services = services.Where(x => x.Title.ToLower().Contains(tbSearchName.Text.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(tbSearchDescription.Text))  
            {

                List<Service> trl = services.Where(x => x.Description != null).ToList();
                if (trl.Count > 0)
                {

                    services = trl.Where(x => x.Description.ToLower().Contains(tbSearchDescription.Text.ToLower())).ToList();

                }
                else
                {
                    MessageBox.Show("Нет описания");
                    tbSearchDescription.Text = "";
                }
            }

            switch (cbDiscount.SelectedIndex)
            {

                case 0:
                    services = services.ToList();
                    break;
                case 1:
                    services = services.Where(x => x.Discount >= 0 && x.Discount < 0.05).ToList();
                    break;
                case 2:
                    services = services.Where(x => x.Discount >= 0.05 && x.Discount < 0.15).ToList();
                    break;
                case 3:
                    services = services.Where(x => x.Discount >= 0.15 && x.Discount < 0.3).ToList();
                    break;
                case 4:
                    services = services.Where(x => x.Discount >= 0.3 && x.Discount < 0.7).ToList();
                    break;
                case 5:
                    services = services.Where(x => x.Discount >= 0.7 && x.Discount < 1).ToList();
                    break;
            }

            switch (cbSorting.SelectedIndex)
            {
                case 1:
                    {
                        services.Sort((x, y) => x.CurrentPrice.CompareTo(y.CurrentPrice));
                    }
                    break;
                case 2:
                    {
                        services.Sort((x, y) => x.CurrentPrice.CompareTo(y.CurrentPrice));
                        services.Reverse();
                    }
                    break;
            }

            lvListService.ItemsSource = services;
            if (services.Count == 0)
            {
                MessageBox.Show("Нет записей");
                tbAllCount.Text = Base.DB.Service.ToList().Count + "/" + Base.DB.Service.ToList().Count;
                tbSearchName.Text = "";
                tbSearchDescription.Text = "";
                cbSorting.SelectedIndex = 0;
                cbDiscount.SelectedIndex = 0;

            }
            tbAllCount.Text = services.Count + "/" + Base.DB.Service.ToList().Count;

        }
        private void tbSearchName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void tbOldPrice_Loaded_1(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if (textBlock.Uid != null)
            {
                textBlock.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void tbSearchName_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void tbSearchDescription_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void cbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnChangeService_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnChangeService = sender as Button;
            if (Admin)
            {
                btnChangeService.Visibility = Visibility.Visible;
            }
            else
            {
                btnChangeService.Visibility = Visibility.Collapsed;
            }
        }

        private void btnDeleteService_Loaded(object sender, RoutedEventArgs e)
        {
            Button buttonDeleteService = sender as Button;
            if (Admin)
            {
                buttonDeleteService.Visibility = Visibility.Visible;
            }
            else
            {
                buttonDeleteService.Visibility = Visibility.Collapsed;
            }
        }
        private bool getProverkaInfoAboutService(int index) 
        {
            foreach (ClientService clientService in Base.DB.ClientService.ToList())
            {
                if (clientService.ServiceID == index)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = Base.DB.Service.FirstOrDefault(x => x.ID == index);
            if (getProverkaInfoAboutService(index))
            {
                MessageBox.Show("Удаление услуги из базы данных запрещено, так как на неё есть информация о записях на услуги!!!");
                return;
            }
            if (MessageBox.Show("Вы уверены что хотите удалить услугу: " + service.Title + "?", "Системное сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                foreach (ServicePhoto servicePhoto in Base.DB.ServicePhoto.ToList()) 
                {
                    if (servicePhoto.ServiceID == index)
                    {
                        Base.DB.ServicePhoto.Remove(servicePhoto);
                    }
                }
                Base.DB.Service.Remove(service);
                Base.DB.SaveChanges();
                ClassFrame.frame.Navigate(new Servislist(Admin));
            }
        }

        private void btnEnterService_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnEnterService = (Button)sender;
            if (Admin)
            {
                btnEnterService.Visibility = Visibility.Visible;
            }
            else
            {
                btnEnterService.Visibility = Visibility.Collapsed;
            }
        }
        private void btnChangeService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = Base.DB.Service.FirstOrDefault(x => x.ID == index);
            ClassFrame.frame.Navigate(new AddService(service));
        }

        private void btnChangePictures_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnChangePictures = sender as Button;
            if (Admin)
            {
                btnChangePictures.Visibility = Visibility.Visible;
            }
            else
            {
                btnChangePictures.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new AddService());
        }

        private void btnChangePictures_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = Base.DB.Service.FirstOrDefault(x => x.ID == index);
            ClassFrame.frame.Navigate(new ChangePicturePage(service));
        }

        private void btnEnterService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = Base.DB.Service.FirstOrDefault(x => x.ID == index);
            ClassFrame.frame.Navigate(new signingUpForServicePage(service));
        }

        private void btnUpcomingEntries_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new PageUPC());
        }

        private void btnUpcomingEntries_Loaded(object sender, RoutedEventArgs e)
        {
            if (Admin)
            {
                btnUpcomingEntries.Visibility = Visibility.Visible;
            }
            else
            {
                btnUpcomingEntries.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdd_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
