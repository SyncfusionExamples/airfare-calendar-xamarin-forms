using Syncfusion.SfCalendar.XForms;
using System;
using Xamarin.Forms;

namespace Airfare_Calendar
{
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
}
