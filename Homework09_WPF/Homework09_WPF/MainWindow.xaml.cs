using Homework09_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Homework09_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DateTime Date { get; set; } = DateTime.Today;
        public HttpClient HttpClient = new HttpClient();
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();


        public MainWindow()
        {
            HttpClient.BaseAddress = new Uri("http://localhost:5000/");
            DataContext = this;
            InitializeComponent();

            GetAllCars();
        }

        public async void GetAllCars()
        {
            var response = await HttpClient.GetAsync("api/cars/");
            JsonSerializer.Deserialize<List<Car>>(await response.Content.ReadAsStringAsync()).ForEach(c => Cars.Add(c));
        }

        public async void GetCarsFromDate()
        {
            Cars.Clear();

            var response = await HttpClient.GetAsync("api/cars/avail/" + Date.Day + "." + Date.Month + "." + Date.Year);
            JsonSerializer.Deserialize<List<Car>>(await response.Content.ReadAsStringAsync()).ForEach(c => Cars.Add(c));
        }

        public async void BookCarForDate(BookDate curDate, int id)
        {
            var content = new StringContent(JsonSerializer.Serialize(curDate), Encoding.UTF8, "application/json");
            await HttpClient.PutAsync("api/cars/" + id + "/book", content);
            GetCarsFromDate();
        }

        private void ShowAvail(object sender, RoutedEventArgs e)
        {
            GetCarsFromDate();
        }

        private void BookCar(object sender, RoutedEventArgs e)
        {
            int id = (int)(((Button)sender).Tag);
            BookDate curDate = new BookDate();
            curDate.Date = Date;

            BookCarForDate(curDate, id);
        }
    }
}
