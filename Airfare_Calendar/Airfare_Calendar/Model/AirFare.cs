using System; 
using System.Collections.ObjectModel;
using System.ComponentModel; 
using Xamarin.Forms;

namespace Airfare_Calendar 
{

    public class AirFare : INotifyPropertyChanged
    {
        private string fare;
        private string airline;
        private string plane;
        private Color color;
        private DateTime? date = null;

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
               
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
