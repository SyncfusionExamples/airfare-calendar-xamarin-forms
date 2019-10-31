# Designing Airfare Calendar – Xamarin
## Overview
While booking the flight tickets, the most generic thing is to find out the cheapest fares among the listed airlines in a calendar. Syncfusion [Xamarin.Forms calendar](https://www.syncfusion.com/xamarin-ui-controls/xamarin-calendar) with its rich feature set allows user to design Airfare calendar using data binding and template support.

![Airfare calendar xamarin UWP](https://github.com/SyncfusionExamples/airfare-calendar-xamarin-forms/blob/master/Airfare%20xamarin.png)

In this blog post, we will discuss how to design Airfare calendar and display the cheapest airfares among the listed airlines using Syncfusion Xamarin.Forms calendar component. If you are new to the calendar control, please read the [Getting Started](https://help.syncfusion.com/xamarin/sfcalendar/getting-started) article in the calendar documentation before proceeding.

## Creating view model
Calendar is a MVVM-friendly control with complete data-binding support, this allows you to bind the real-time data from other sources. Create a view model **AirfareViewModel** with the required properties like Date, Fare, Airline and Id. Now, append the data, for demo purposes we are generating the random data. 

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
        public event PropertyChangedEventHandler PropertyChanged;
        public SfBusyIndicator BusyIndicator;

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

## Designing airfare calendar
In Xamarin,  you can customize the presentation of data and its interaction using [DataTemplate](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/templates/data-templates/) and [data-binding](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/) support. Customize the calendar month cell to display the airfare, airway name and image based on the airline ID in each cell by using the CellTemplate property.  Bind the data appended in the view model using the properties of AirfareViewModel.

<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:Airfare_Calendar"
             xmlns:syncfusion="clr-namespace:Syncfusion.SfCalendar.XForms;assembly=Syncfusion.SfCalendar.XForms"
             xmlns:busyindicator="clr-namespace:Syncfusion.SfBusyIndicator.XForms;assembly=Syncfusion.SfBusyIndicator.XForms"
             x:Class="Airfare_Calendar.CalendarPage">
    
    <ContentPage.Behaviors>
        <local:CalendarPageBehavior/>
    </ContentPage.Behaviors>
    <ContentPage.Content>
        <Grid>
          <syncfusion:SfCalendar x:Name="calendar" EnableDatesInPast="False"
                                ShowLeadingAndTrailingDays="false" 
                                ShowYearView="False"  
                                VerticalOptions="FillAndExpand" 
                                HorizontalOptions="FillAndExpand" 
                                BackgroundColor="White"> 
            <syncfusion:SfCalendar.MonthViewSettings>
                    <syncfusion:MonthViewSettings HeaderBackgroundColor="#2A8A94"  
                                                  HeaderTextColor="White" CellGridOptions="Both"
                                              DayHeaderBackgroundColor="#2A8A94" 
                                                  DateSelectionColor="Transparent"
                                              TodaySelectionBackgroundColor="Transparent"
                                              DayHeaderTextColor="White">
                    <syncfusion:MonthViewSettings.CellTemplate>
                            <DataTemplate>
                                <Grid BackgroundColor="White" RowSpacing="0" ColumnSpacing="0">
                                <Grid.RowDefinitions>
                                    <RowDefinition Height="0.3*"/>
                                    <RowDefinition Height="0.4*"/>
                                    <RowDefinition Height="0.3*"/>
                                </Grid.RowDefinitions>

                                    <Label Text="{Binding Date.Day}" Grid.Row="0" Margin="1" VerticalTextAlignment="Start" BackgroundColor="Transparent">
                                        <Label.FontSize>
                                            <OnPlatform x:TypeArguments="x:Double">
                                                <On Platform="iOS" Value="14" />
                                                <On Platform="Android" Value="14" />
                                                <On Platform="UWP" Value="20" />
                                            </OnPlatform>
                                        </Label.FontSize>
                                    </Label>
                                        <Label Text="{Binding Fare}" Grid.Row="1" TextColor="#2A8A94" FontAttributes="Bold" HorizontalTextAlignment="Start" VerticalTextAlignment="Center" BackgroundColor="Transparent">
                                        <Label.FontSize>
                                            <OnPlatform x:TypeArguments="x:Double">
                                                <On Platform="iOS" Value="12" />
                                                <On Platform="Android" Value="12" />
                                                <On Platform="UWP" Value="22" />
                                            </OnPlatform>
                                        </Label.FontSize>
                                    </Label>

                                    <Grid Grid.Row="2" BackgroundColor="Transparent">
                                        <Grid.ColumnDefinitions>
                                            <ColumnDefinition Width="0.3*"/>
                                            <ColumnDefinition Width="0.7*"/>
                                        </Grid.ColumnDefinitions>

                                        <Label Text="{Binding Plane}" TextColor="{Binding Color}" Grid.Column="0" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" HorizontalTextAlignment="Center" VerticalTextAlignment="Center">
                                            <Label.FontFamily>
                                                <OnPlatform x:TypeArguments="x:String">
                                                    <On Platform="iOS" Value="aircraft" />
                                                    <On Platform="Android" Value="aircraft.ttf#aircraft" />
                                                    <On Platform="UWP" Value="Assets/aircraft.ttf#aircraft" />
                                                </OnPlatform>
                                            </Label.FontFamily>
                                            <Label.FontSize>
                                                <OnPlatform x:TypeArguments="x:Double">
                                                    <On Platform="iOS" Value="12" />
                                                    <On Platform="Android" Value="12" />
                                                    <On Platform="UWP" Value="22" />
                                                </OnPlatform>
                                            </Label.FontSize>
                                        </Label>
                                        <Label Text="{Binding Airline}" Grid.Column="1" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" HorizontalTextAlignment="Start" VerticalTextAlignment="Center">
                                            <Label.FontSize>
                                                <OnPlatform x:TypeArguments="x:Double">
                                                    <On Platform="iOS" Value="8" />
                                                    <On Platform="Android" Value="8" />
                                                    <On Platform="UWP" Value="16" />
                                                </OnPlatform>
                                            </Label.FontSize>
                                        </Label>
                                    </Grid>
                                </Grid>
                            </DataTemplate>
                    </syncfusion:MonthViewSettings.CellTemplate>
                </syncfusion:MonthViewSettings>
            </syncfusion:SfCalendar.MonthViewSettings>
        </syncfusion:SfCalendar>
          <busyindicator:SfBusyIndicator x:Name="busyindicator" Duration="3" ViewBoxHeight="100" ViewBoxWidth="100" AnimationType="SingleCircle" TextColor="#2A8A94" IsBusy="True"/>
      </Grid> 
    </ContentPage.Content> 
</ContentPage>

Create a behavior class **CalendarPageBehavior**, initialize the view model with calendar date and set the binding context for each month cell in calendar using **OnMonthCellLoaded** event.

    public class CalendarPageBehavior : Behavior<ContentPage>
    {
        private SfCalendar calendar;
        private SfBusyIndicator busyIndicator; 
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.calendar = bindable.FindByName<SfCalendar>("calendar");
            this.busyIndicator = bindable.FindByName<SfBusyIndicator>("busyindicator");
            this.WireEvents(); 
            this.calendar.MinDate = DateTime.Now; 
        }

        private void WireEvents()
        {
            calendar.MonthChanged += Calendar_MonthChanged;
            calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded; 
        }

        private void Calendar_MonthChanged(object sender, MonthChangedEventArgs e)
        {
            if (this.busyIndicator.IsVisible)
            {
                return;
            }

            this.busyIndicator.IsVisible = true;
        }

        private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        {
            AirfareViewModel viewModel = new AirfareViewModel(busyIndicator);
            viewModel.UpdateAirfareData(e.Date);
            e.CellBindingContext = viewModel;
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            this.UnWireEvents();
        }

        private void UnWireEvents()
        {
            calendar.MonthChanged -= Calendar_MonthChanged;
            calendar.OnMonthCellLoaded -= Calendar_OnMonthCellLoaded; 
        }
    }

Now, calendar control is configured with an application to design and display the cheapest airfares among the listed airlines. Just running the sample with the previous steps will render a scheduler with appointments.

## Conclusion
In this blog post, we’ve discussed about designing Airfare calendar using Xamarin Forms calendar. You can also check out our project samples in this GitHub repository. Feel free to try out this sample and share your feedback or questions in the comments section. You can also contact us through our [support forum](https://www.syncfusion.com/forums), [Direct-Trac](https://www.syncfusion.com/support/directtrac/), or [feedback portal](https://www.syncfusion.com/feedback/). We are happy to assist you.

## References
https://www.syncfusion.com/blogs/post/designing-airfare-calendar-using-pure-javascript-scheduler2.aspx
https://help.syncfusion.com/xamarin/sfcalendar/how-to#how-to-customize-month-view-cell-using-a-template

