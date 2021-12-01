using AdminPanel.Models;
using Microsoft.Maps.MapControl.WPF;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using UserPanel.Commands;
using UserPanel.Models;
using UserPanel.Services;
using UserPanel.Stores;
using UserPanel.Views;

namespace UserPanel.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class NewMapViewModel : BaseViewModel
    {
        public NewMapViewModel(NavigationStore store)
        {
            SignOut = new UpdateViewCommand<LoginViewModel>(store, () => new LoginViewModel(store));
            Centerr = new Location(40.409264, 49.867092);
            PushPinLocations = new List<Location>(3);
            Locations = new LocationCollection();

            Timer = new DispatcherTimer();
            Timer2 = new DispatcherTimer();
            Timer2.Interval = new TimeSpan(0, 0, 1);
            Timer2.Tick += Timer2_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            directory = directory.Replace("UserPanel", "AdminPanel");
            Prices price = JsonSaveService<Prices>.Load($@"{directory}\pricing");
            if (price == null)
                price = new Prices();

            PricePerKm = price.PricePerKm;

            GoCommand = new RelayCommand(a =>
            {
                OrderButtonIsEnable = true;
                Locations.Clear();
                if (IsVisiblePin1 == Visibility.Visible)
                {
                    Centerr = GetRouteService.GetRoute(FromLocation, ToLocation, Locations);
                    if (Locations.Count != 0)
                    {
                        From = Locations[0].ToString();
                        To = Locations[Locations.Count - 1].ToString();
                    }
                }
                else
                {
                    Centerr = GetRouteService.GetRoute(From, To, Locations);
                }
                if (Locations.Count > 0)
                {
                    GeoCoordinate ge = new GeoCoordinate(Locations[0].Latitude, Locations[0].Longitude);
                    Distance = (ge.GetDistanceTo(new GeoCoordinate(Locations[Locations.Count - 1].Latitude, Locations[Locations.Count - 1].Longitude)) / 1000).ToString();
                    Double dist = Double.Parse(Distance);
                    Distance = dist.ToString("0.##") + "  km";
                    Price = (dist * PricePerKm).ToString("0.##");
                    if (float.Parse(Price) < 1.6f)
                        Price = "1.6";
                }
            },

            b => !string.IsNullOrWhiteSpace(FromLocation) && !string.IsNullOrWhiteSpace(ToLocation));
            ApplyCommand = new RelayCommand(a => Apply());
            HistoryCommand = new UpdateViewCommand<HistoryViewModel>(store, () => new HistoryViewModel(store));
            Rating = new UpdateViewCommand<RatingViewModel>(store, () => new RatingViewModel(store));
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (1 < Locations.Count)
            {
                Locations.Remove(Locations.Last());
                TaxiLocation = Locations.Last().Latitude + ", " + Locations.Last().Longitude;

                GeoCoordinate ge = new GeoCoordinate(Locations[0].Latitude, Locations[0].Longitude);
                Distance = (ge.GetDistanceTo(new GeoCoordinate(Locations[Locations.Count - 1].Latitude, Locations[Locations.Count - 1].Longitude)) / 100).ToString();
                Double dist = Double.Parse(Distance);
                Distance = dist.ToString("0.##") + "  km";
            }
            else
            {
                Timer2.Stop();
                System.Windows.MessageBox.Show("Roat Successfuly");
                IsVisiblePin1 = Visibility.Visible;
                TaxiVisible = Visibility.Hidden;
                neise history = new neise(From,To,DateTime.Now.ToLongTimeString(),Price);
                HistoryViews HIST = new HistoryViews();
                HIST.neise = history;
                Rating.Execute(null);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (1 < Locations.Count)
            {
                Locations.Remove(Locations.Last());
                TaxiLocation = Locations.Last().Latitude + ", " + Locations.Last().Longitude;

                GeoCoordinate ge = new GeoCoordinate(Locations[0].Latitude, Locations[0].Longitude);
                Distance = (ge.GetDistanceTo(new GeoCoordinate(Locations[Locations.Count - 1].Latitude, Locations[Locations.Count - 1].Longitude)) / 100).ToString();
                Double dist = Double.Parse(Distance);
                Distance = dist.ToString("0.##") + "  km";
            }
            else
            {
                Timer.Stop();
                System.Windows.MessageBox.Show("You reach your destination");
                Apply2();
            }
        }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public string Price { get; set; }

        public RelayCommand GoCommand { get; set; }

        public RelayCommand ApplyCommand { get; set; }

        public ICommand SignOut { get; set; }

        public ICommand Rating { get; set; }

        public bool GoButtonIsEnable { get; set; } = true;

        public bool OrderButtonIsEnable { get; set; } = false;

        public string From { get; set; }

        public string To { get; set; }


        public string Distance { get; set; }

        public ICommand HistoryCommand { get; set; }


        private float PricePerKm;


        private LocationCollection _locations;

        public LocationCollection Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged("Locations");
            }
        }


        private List<Location> _pushPinLocations;

        public List<Location> PushPinLocations
        {
            get => _pushPinLocations;
            set
            {
                _pushPinLocations = value;
                OnPropertyChanged("PushPinLocations");
            }
        }



        private Location _centerr;

        public Location Centerr
        {
            get => _centerr;
            set
            {
                _centerr = value;
                OnPropertyChanged("Centerr");
            }
        }

        public Visibility IsVisiblePin1 { get; set; } = Visibility.Visible;

        public Visibility IsVisiblePin2 { get; set; } = Visibility.Visible;

        public string TaxiLocation { get; set; }

        public Visibility TaxiVisible { get; set; } = Visibility.Hidden;


        DispatcherTimer Timer;


        DispatcherTimer Timer2;

        private Driver _driver;


        public static User Usr { get; set; }

        public static List<User> Users { get; set; }

        public Driver driver
        {
            get => _driver;
            set
            {
                _driver = value;
                OnPropertyChanged("driver");
            }
        }
        private List<Driver> Drivers;
        public void Apply()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            directory = directory.Replace("UserPanel", "AdminPanel");
            List<Driver> Drivers = JsonSaveService<List<Driver>>.Load($@"{directory}\driver");
            List<Location> TaxiLocations = Drivers.Select(d => d.LastLocation).ToList();
            TaxiLocation = FindTaxiService.TaxiLocation(new Location(double.Parse(From.Split(',')[0]), double.Parse(From.Split(',')[1])), TaxiLocations).ToString();
            var Taxsistname = Drivers.Find(f => f.LastLocation.Latitude.ToString() == TaxiLocation);
            Driver driver = FindTaxiService.NearDriver(new Location(double.Parse(From.Split(',')[0]), double.Parse(From.Split(',')[1])), TaxiLocations);
            System.Windows.Forms.MessageBox.Show($"{driver.Name} {driver.Surname}\n Car Model: {driver.Car.Model}\n Car Color:{driver.Car.Color}");
            if (TaxiLocation != null)
            {
                Locations.Clear();
                GetRouteService.GetRoute(From, TaxiLocation, Locations);
                TaxiLocation2 = TaxiLocation;

                TaxiVisible = Visibility.Visible;
                IsVisiblePin2 = Visibility.Visible;
                IsVisiblePin2 = Visibility.Hidden;

                Timer.Start();
            }
            else
            {
                MessageBox.Show("Taxi is not found");
                return;
            }
        }
        public string TaxiLocation2 { get; set; }

        public void Apply2()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            directory = directory.Replace("UserPanel", "AdminPanel");
            List<Driver> Drivers = JsonSaveService<List<Driver>>.Load($@"{directory}\driver");
            List<Location> TaxiLocations = Drivers.Select(d => d.LastLocation).ToList();
            TaxiLocation = FindTaxiService.TaxiLocation(new Location(double.Parse(To.Split(',')[0]), double.Parse(To.Split(',')[1])), TaxiLocations).ToString();
            if (TaxiLocation != null)
            {
                Locations.Clear();
                GetRouteService.GetRoute(To, From, Locations);


                TaxiVisible = Visibility.Visible;
                IsVisiblePin2 = Visibility.Visible;
                IsVisiblePin1 = Visibility.Hidden;

                Timer2.Start();
            }
            else
            {
                MessageBox.Show("Taxi is not found");
                return;
            }
        }


    }


}
