﻿using Microsoft.Win32;
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
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Page
    {
        Service service; 
        bool flagUpdate = false; 
        string path;
        public AddService() 
        {
            InitializeComponent();
            path = null;
            imMainImagePath.Source = new BitmapImage(new Uri("..\\image\\picture.png", UriKind.Relative));
        }

        public AddService(Service service) 
        {
            InitializeComponent();
            this.service = service;
            flagUpdate = true;
            tbIndex.Visibility = Visibility.Visible;
            tbIndex.Text = "Идентификатор: " + service.ID;
            tbHeader.Text = "Изменение услуги";
            btnAddService.Content = "Изменить услугу";
            tbName.Text = service.Title;
            tbCost.Text = Convert.ToString(service.Cost);
            tbDurationInSeconds.Text = Convert.ToString(service.DurationInSeconds);
            tbDiscount.Text = Convert.ToString(service.Discount);
            tbDescription.Text = Convert.ToString(service.Description);
            if (service.MainImagePath != null)
            {
                path = service.MainImagePath;
                imMainImagePath.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                btnDeleteImage.Visibility = Visibility.Visible;
                btnAddImage.Content = "Изменить картинку";
            }
            else
            {
                path = null;
                imMainImagePath.Source = new BitmapImage(new Uri("..\\image\\picture.png", UriKind.Relative));
            }
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Наименование услуги должно быть заполнено!");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Наименование услуги должно быть заполнено!");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbCost.Text))
            {
                MessageBox.Show("Стоимость услуги должна быть заполнена!");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbDurationInSeconds.Text))
            {
                MessageBox.Show("Продолжительность услуги должна быть заполнена!");
                return;
            }
            if (Convert.ToInt32(tbDurationInSeconds.Text) < 0)
            {
                MessageBox.Show("Длительность оказания услуги не может быть отрицательной");
                return;
            }
            if (Convert.ToDouble(tbDurationInSeconds.Text) / 60 > 240)
            {
                MessageBox.Show("Длительность оказания услуги не может быть больше 4 часов");
                return;
            }
            if (!string.IsNullOrWhiteSpace(tbDiscount.Text))
            {
                if (Convert.ToDouble(tbDiscount.Text) > 100)
                {
                    MessageBox.Show("Скидка не может быть больше 100%");
                    return;
                }
            }
            if (flagUpdate == false)
            {
                service = new Service();
                List<Service> services = Base.DB.Service.Where(x => x.Title == tbName.Text).ToList();
                if (services.Count > 0)
                {
                    MessageBox.Show("Услуга с таким наименованием уже зарегистрирована в системе");
                    return;
                }
            }
            service.Title = tbName.Text;
            service.Cost = Convert.ToDecimal(tbCost.Text);
            service.DurationInSeconds = Convert.ToInt32(tbDurationInSeconds.Text);
            if (string.IsNullOrWhiteSpace(tbDiscount.Text))
            {
                service.Discount = null;
            }
            else
            {
                service.Discount = Convert.ToDouble(tbDiscount.Text);
            }
            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                service.Description = null;
            }
            else
            {
                service.Description = tbDescription.Text;
            }
            service.MainImagePath = path;

            if (flagUpdate == false)
            {
                Base.DB.Service.Add(service);
            }
            Base.DB.SaveChanges();
            if (flagUpdate)
            {
                MessageBox.Show("Услуга успешно изменена в базе данных");
            }
            else
            {
                MessageBox.Show("Услуга успешно добавлена в базу данных");
            }
            ClassFrame.frame.Navigate(new Servislist(true));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new Servislist(true));
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            path = OFD.FileName;
            string[] arrayPath = path.Split('\\');
            path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];
            imMainImagePath.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            btnDeleteImage.Visibility = Visibility.Hidden;
            btnAddImage.Content = "Изменить картинку";
            btnDeleteImage.Visibility = Visibility.Visible;
        }

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            path = null;
            imMainImagePath.Source = new BitmapImage(new Uri("..\\image\\picture.png", UriKind.Relative));
            btnAddImage.Content = "Добавить картинку";
            btnDeleteImage.Visibility = Visibility.Hidden;
        }
    }
}
