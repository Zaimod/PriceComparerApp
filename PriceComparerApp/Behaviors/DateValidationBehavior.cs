using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PriceComparerApp.Behaviors
{
    public class DateValidationBehavior : Behavior<DatePicker>
    {
        protected override void OnAttachedTo(DatePicker datePicker)
        {
            datePicker.DateSelected += Datepicker_DateSelected;
            base.OnAttachedTo(datePicker);
        }

        private void Datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime value = e.NewDate;
            int year = DateTime.Now.Year;
            int selyear = value.Year;
            int result = selyear - year;
            bool isValid = false;
            if (result <= 100 && result > 0)
            {
                isValid = true;
            }
           ((DatePicker)sender).BackgroundColor = isValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(DatePicker datepicker)
        {
            datepicker.DateSelected -= Datepicker_DateSelected;
            base.OnDetachingFrom(datepicker);
        }
    }
}
