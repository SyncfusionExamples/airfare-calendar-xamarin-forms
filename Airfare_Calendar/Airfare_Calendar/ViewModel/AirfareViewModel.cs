using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Syncfusion.SfBusyIndicator.XForms;
using Xamarin.Forms;

namespace Airfare_Calendar
{
    public class AirfareViewModel : INotifyPropertyChanged
    {
        private int id;
        private string fare;
        private string airline;
        private string plane;
        private Color color; 
        private DateTime? date = null;
        private ObservableCollection<string> fares;
        private ObservableCollection<int> ids;
        private SfBusyIndicator BusyIndicator;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the airline id.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// Gets or sets the calendar date.
        /// </summary>
        public DateTime? Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
                OnPropertyChanged("Date");
            }
        }

        /// <summary>
        /// Gets or sets the airfare.
        /// </summary>
        public string Fare
        {
            get
            {
                return this.fare;
            }
            set
            {
                this.fare = value;
                OnPropertyChanged("Fare");
            }
        }

        /// <summary>
        /// Gets or sets the airline name.
        /// </summary>
        public string Airline
        {
            get
            {
                return this.airline;
            }
            set
            {
                this.airline = value;
                OnPropertyChanged("Airline");
            }
        }

        /// <summary>
        /// Gets or sets the airline image.
        /// </summary>
        public string Plane
        {
            get
            {
                return this.plane;
            }
            set
            {
                this.plane = value;
                OnPropertyChanged("Plane");
            }
        }

        /// <summary>
        /// Gets or sets the airline color.
        /// </summary>
        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                OnPropertyChanged("Color");
            }
        }

        /// <summary>
        /// Gets or sets the collection of airfare.
        /// </summary>
        public ObservableCollection<string> Fares
        {
            get
            {
                return this.fares;
            }
            set
            {
                this.fares = value;
                OnPropertyChanged("Fares");
            }
        }

        /// <summary>
        /// Gets or sets the collection of airline id.
        /// </summary>
        public ObservableCollection<int> Ids
        {
            get
            {
                return this.ids;
            }
            set
            {
                this.ids = value;
                OnPropertyChanged("Ids");
            }
        }

        public AirfareViewModel(SfBusyIndicator busyIndicator)
        {
            this.BusyIndicator = busyIndicator;
            this.AppendAirfareData();
        }
 
        /// <summary>
        /// Updated the cheapest airfare based on the listed airlines.
        /// </summary>
        public async void UpdateAirfareData(DateTime dateTime)
        {
            this.Date = dateTime;
            if (this.BusyIndicator != null && this.BusyIndicator.IsVisible)
            {
                await System.Threading.Tasks.Task.Delay(1000);
                this.BusyIndicator.IsVisible = false;
            }

            var fareIndex = dateTime.Date.Day;
            if (fareIndex >= this.Fares.Count)
            {
                fareIndex = this.Fares.Count - 1;
            }

            this.Fare = this.Fares[fareIndex];

            var id = (int)dateTime.Date.Day % this.Ids.Count;
            if (id >= this.Ids.Count)
            {
                id = this.Ids.Count-1;
            }

            this.Id = this.Ids[id];
            this.Airline = "Airline" + this.Id.ToString();
            this.Plane = "O";
            this.Color = this.GetPlaneColor(this.Id);
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

        private void AppendAirfareData()
        {
            this.Fares = new ObservableCollection<string>();
            this.Ids = new ObservableCollection<int>();

            this.Ids.Add(1);
            this.Ids.Add(2);
            this.Ids.Add(3);
            this.Ids.Add(4);

            this.Fares.Add("$134.50");
            this.Fares.Add("$305.00");
            this.Fares.Add("$152.66");
            this.Fares.Add("$267.09");
            this.Fares.Add("$189.20");
            this.Fares.Add("$212.10");
            this.Fares.Add("$350.50");
            this.Fares.Add("$222.39");
            this.Fares.Add("$238.83");
            this.Fares.Add("$147.27");
            this.Fares.Add("$115.43");
            this.Fares.Add("$198.06");
            this.Fares.Add("$189.83");
            this.Fares.Add("$110.71");
            this.Fares.Add("$152.10");
            this.Fares.Add("$199.62");
            this.Fares.Add("$146.15");
            this.Fares.Add("$237.04");
            this.Fares.Add("$101.72");
            this.Fares.Add("$266.69");
            this.Fares.Add("$332.48");
            this.Fares.Add("$256.77");
            this.Fares.Add("$449.68");
            this.Fares.Add("$100.17");
            this.Fares.Add("$153.31");
            this.Fares.Add("$249.92");
            this.Fares.Add("$254.59");
            this.Fares.Add("$107.18");
            this.Fares.Add("$219.04");
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
