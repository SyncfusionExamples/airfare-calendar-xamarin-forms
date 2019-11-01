using System; 
using System.Collections.ObjectModel;
using System.ComponentModel; 
using Xamarin.Forms;

namespace Airfare_Calendar
{
    /// <summary>
    /// Airfare View Model
    /// </summary>
    public class AirfareViewModel : INotifyPropertyChanged
    { 
        private ObservableCollection<AirFare> airFareData;

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// AirFare Data of Flight fare stored and retrieved.
        /// </summary>
        public ObservableCollection<AirFare> AirFareData  
        {
            get
            {
                return this.airFareData;
            }
            set
            {
                this.airFareData = value;
                OnPropertyChanged("AirFareData");
            }
        }

        /// <summary>
        /// Airfare View Model
        /// </summary>
        public AirfareViewModel()
        {
            AirFareData = new ObservableCollection<AirFare>(); 
            this.GetAirFareData();
        }

        /// <summary>
        /// Updated the cheapest airfare based on the listed airlines.
        /// </summary>
        public AirFare UpdateAirfareData(DateTime dateTime)
        {
            var fareData = this.AirFareData[dateTime.Date.DayOfYear % (AirFareData.Count - 1)];
            fareData.Date = dateTime;
            return fareData;
        }

        private Color GetPlaneColor(int airlineId)
        {
            if (airlineId == 1)
                return Color.Blue;
            else if (airlineId == 2)
                return Color.Green;
            else
                return Color.Red;
        }

        private void GetAirFareData()
        {
            var Fares = new ObservableCollection<string>();
            var Ids = new ObservableCollection<int>(); 
            FetchFareData(out Fares, out Ids);
            //// Adding 100 flight fare data
            for (int i = 0; i < 100; i++)
            {
                var airfare = new AirFare(); 
                airfare.Fare = Fares[i % (Fares.Count - 1)]; 
                var id = i % Ids.Count;
                if (id >= Ids.Count)
                {
                    id = Ids.Count - 1;
                }

                id = Ids[id];
                airfare.Airline = "Airline" + id.ToString();
                airfare.Plane = "\ue700";
                airfare.Color = GetPlaneColor(id);
                this.AirFareData.Add(airfare);
            }
        }

        private void FetchFareData(out ObservableCollection<string> Fares, out ObservableCollection<int> Ids)
        {
            Ids = new ObservableCollection<int>
            {
                1,
                2,
                3,
                4
            };

            Fares = new ObservableCollection<string>();
            Fares.Add("$134.50");
            Fares.Add("$305.00");
            Fares.Add("$152.66");
            Fares.Add("$267.09");
            Fares.Add("$189.20");
            Fares.Add("$212.10");
            Fares.Add("$350.50");
            Fares.Add("$222.39");
            Fares.Add("$238.83");
            Fares.Add("$147.27");
            Fares.Add("$115.43");
            Fares.Add("$198.06");
            Fares.Add("$189.83");
            Fares.Add("$110.71");
            Fares.Add("$152.10");
            Fares.Add("$199.62");
            Fares.Add("$146.15");
            Fares.Add("$237.04");
            Fares.Add("$100.17");
            Fares.Add("$101.72");
            Fares.Add("$266.69");
            Fares.Add("$332.48");
            Fares.Add("$256.77");
            Fares.Add("$449.68");
            Fares.Add("$100.17");
            Fares.Add("$153.31");
            Fares.Add("$249.92");
            Fares.Add("$254.59");
            Fares.Add("$332.48");
            Fares.Add("$256.77");
            Fares.Add("$449.68");
            Fares.Add("$107.18");
            Fares.Add("$219.04");
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}