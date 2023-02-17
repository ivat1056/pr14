﻿using pr14.classs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            cbClient.SelectedIndex = 0;
            cbClient.SelectedValuePath = "ID";
            cbClient.DisplayMemberPath = "FIO";
        }

        private bool getProverkaData(string date) 
        {
            try
            {
                Convert.ToDateTime(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool getProverkaTime(string time) 
        {
            Regex regex = new Regex("^(([0,1][0-9])|(2[0-3])):[0-5][0-9]$");
            bool a = regex.IsMatch(time);
            return a;
        }
        private void showDateAndTime() 
        {
            if (getProverkaData(dpData.Text))
            {
                if (getProverkaTime(tbTime.Text))
                {
                    tbErrorDate.Visibility = Visibility.Collapsed;
                    spDateAndTime.Visibility = Visibility.Visible;
                    DateTime dateTime = dpData.SelectedDate.Value;
                    dateTime = dateTime.Add(new TimeSpan(Convert.ToInt32(tbTime.Text.Substring(0, 2)), Convert.ToInt32(tbTime.Text.Substring(3, 2)), 0));
                    dateTime = dateTime.AddSeconds(service.DurationInSeconds);
                    tbDateEndTime.Text = dateTime.ToString("dd MMMM yyyy HH:mm");
                }
                else
                {
                    tbErrorDate.Visibility = Visibility.Visible;
                    spDateAndTime.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                tbErrorDate.Visibility = Visibility.Visible;
                spDateAndTime.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbClient.SelectedIndex == -1)
            {
                MessageBox.Show("Клиент для записи на услугу не выбран");
                return;
            }
            if (getProverkaData(dpData.Text))
            {
                if (getProverkaTime(tbTime.Text))
                {
                    try
                    {
                        DateTime dateTime = dpData.SelectedDate.Value;
                        dateTime = dateTime.Add(new TimeSpan(Convert.ToInt32(tbTime.Text.Substring(0, 2)), Convert.ToInt32(tbTime.Text.Substring(3, 2)), 0)); // Дата с временем
                        ClientService clientService = new ClientService();
                        clientService.ClientID = Convert.ToInt32(cbClient.SelectedValue);
                        clientService.ServiceID = service.ID;
                        clientService.StartTime = dateTime;
                        Base.DB.ClientService.Add(clientService);
                        Base.DB.SaveChanges();
                        MessageBox.Show("Клиент успешно записан на услугу");
                        ClassFrame.frame.Navigate(new Servislist(true));
                    }
                    catch
                    {
                        MessageBox.Show("При записи клиента на услугу возникла ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Время указано не корректно");
                }
            }
            else
            {
                MessageBox.Show("Дата не выбрана");
            }
        }

        private void dpData_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            showDateAndTime();
        }
    }
}
