using Microsoft.Win32;
using pr14.classs;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ChangePicturePage.xaml
    /// </summary>
    public partial class ChangePicturePage : Page
    {
        Service service;
        public ChangePicturePage(Service service)
        {
            InitializeComponent();
            this.service = service;
            tbNameService.Text = "Услуга: " + service.Title;
            lvListImages.ItemsSource = Base.DB.ServicePhoto.Where(x => x.ServiceID == service.ID).ToList();
        }

        private void btnAddPicture_Click(object sender, RoutedEventArgs e)
        {
            string path;
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            path = OFD.FileName;
            if (path != null)
            {
                string newFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\image\\" + System.IO.Path.GetFileName(path);
                if (!File.Exists(newFilePath)) 
                {
                    File.Copy(path, newFilePath);
                }
                else
                {
                    MessageBox.Show("Такая картинка уже есть!");
                }
                ServicePhoto servicePhoto = new ServicePhoto();
                servicePhoto.ServiceID = service.ID;
                servicePhoto.PhotoPath = newFilePath;
                Base.DB.ServicePhoto.Add(servicePhoto);
                Base.DB.SaveChanges();
                ClassFrame.frame.Navigate(new ChangePicturePage(service));
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new Servislist(true));
        }
    }
}
