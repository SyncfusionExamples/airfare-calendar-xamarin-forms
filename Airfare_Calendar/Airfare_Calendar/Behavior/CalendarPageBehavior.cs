using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.SfCalendar.XForms;
using System;
using Xamarin.Forms;

namespace Airfare_Calendar
{
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
            calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded; 
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
            calendar.OnMonthCellLoaded -= Calendar_OnMonthCellLoaded; 
        }
    }
}
