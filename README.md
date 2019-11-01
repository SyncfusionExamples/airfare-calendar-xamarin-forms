# Designing Airfare Calendar – Xamarin
## Overview
While booking the flight tickets, the most generic thing is to find out the cheapest fares among the listed airlines in a calendar. Syncfusion [Xamarin.Forms calendar](https://www.syncfusion.com/xamarin-ui-controls/xamarin-calendar) with its rich feature set allows user to design Airfare calendar using data binding and template support.

![Airfare calendar xamarin UWP](https://github.com/SyncfusionExamples/airfare-calendar-xamarin-forms/blob/master/Airfare%20xamarin.png)

In this blog post, we will discuss how to design Airfare calendar and display the cheapest airfares among the listed airlines using Syncfusion Xamarin.Forms calendar component. If you are new to the calendar control, please read the [Getting Started](https://help.syncfusion.com/xamarin/sfcalendar/getting-started) article in the calendar documentation before proceeding.

## Creating view model
Calendar is a MVVM-friendly control with complete data-binding support, this allows you to bind the real-time data from other sources. Create a view model **AirfareViewModel** with the required properties like Date, Fare, Airline and Id. Now, append the data, for demo purposes we are generating the random data. 

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
                                    <Label Text="{Binding Date.Day}" Grid.Row="0" Margin="1" 
                                           VerticalTextAlignment="Start" BackgroundColor="Transparent">
                                        <Label.FontSize>
                                            <OnPlatform x:TypeArguments="x:Double">
                                                <On Platform="iOS" Value="14" />
                                                <On Platform="Android" Value="14" />
                                                <On Platform="UWP" Value="20" />
                                            </OnPlatform>
                                        </Label.FontSize>
                                    </Label>
                                        <Label Text="{Binding Fare}" Grid.Row="1" TextColor="#2A8A94" 
                                               FontAttributes="Bold" HorizontalTextAlignment="Start" 
                                               VerticalTextAlignment="Center" BackgroundColor="Transparent">
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
                                        <Label Text="{Binding Plane}" Margin="1,0,0,0" TextColor="{Binding Color}" Grid.Column="0" 
                                               VerticalOptions="Center" HorizontalOptions="Center" 
                                               HorizontalTextAlignment="Center" VerticalTextAlignment="Center">
                                            <Label.FontFamily>
                                                <OnPlatform x:TypeArguments="x:String">
                                                    <On Platform="iOS" Value="airlines" />
                                                    <On Platform="Android" Value="airlines.ttf#airlines" />
                                                    <On Platform="UWP" Value="Assets/airlines.ttf#airlines" />
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
                                        <Label Text="{Binding Airline}" Grid.Column="1" VerticalOptions="FillAndExpand" 
                                               HorizontalOptions="FillAndExpand" HorizontalTextAlignment="Start" VerticalTextAlignment="Center">
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

                <syncfusion:SfCalendar.BindingContext>
                    <local:AirfareViewModel/>
                </syncfusion:SfCalendar.BindingContext>
            </syncfusion:SfCalendar> 
      </Grid>
    </ContentPage.Content>
    </ContentPage>

Create a behavior class **CalendarPageBehavior**, initialize the view model with calendar date and set the binding context for each month cell in calendar using **OnMonthCellLoaded** event.

    public class CalendarPageBehavior : Behavior<ContentPage>
    {
        private SfCalendar calendar; 
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.calendar = bindable.FindByName<SfCalendar>("calendar");
            this.calendar.MinDate = DateTime.Now;
            this.WireEvents(); 
        }

        private void WireEvents()
        { 
            calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded; 
        }
          
        private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        { 
           e.CellBindingContext= (calendar.BindingContext as AirfareViewModel)?.UpdateAirfareData(e.Date); 
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            this.UnWireEvents();
        }

        private void UnWireEvents()
        { 
            calendar.OnMonthCellLoaded -= Calendar_OnMonthCellLoaded; 
        }
    }

Now, calendar control is configured with an application to design and display the cheapest airfares among the listed airlines. Just running the sample with the previous steps will render a scheduler with appointments.

## Conclusion
In this blog post, we’ve discussed about designing Airfare calendar using Xamarin Forms calendar. You can also check out our project samples in this GitHub repository. Feel free to try out this sample and share your feedback or questions in the comments section. You can also contact us through our [support forum](https://www.syncfusion.com/forums), [Direct-Trac](https://www.syncfusion.com/support/directtrac/), or [feedback portal](https://www.syncfusion.com/feedback/). We are happy to assist you.

## References
https://www.syncfusion.com/blogs/post/designing-airfare-calendar-using-pure-javascript-scheduler2.aspx
https://help.syncfusion.com/xamarin/sfcalendar/how-to#how-to-customize-month-view-cell-using-a-template

