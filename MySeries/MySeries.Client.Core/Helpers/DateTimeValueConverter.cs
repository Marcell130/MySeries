using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace MySeries.Client.Core.Helpers
{
    public class DateTimeValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert( DateTime value, Type targetType, object parameter, CultureInfo cultureInfo )
        {
            var dateString = value.ToString( "MM.dd." );
            if (value == DateTime.Today.AddDays( -1 ))
            {
                dateString = "Yesterday";
            }
            else if (value == DateTime.Today)
            {
                dateString = "Today";
            }
            else if (value == DateTime.Today.AddDays( 1 ))
            {
                dateString = "Tomorrow";
            }
            else if (value > DateTime.Today && value <= DateTime.Today.AddDays( 7 ))
            {
                dateString = value.DayOfWeek.ToString();
            }
            else if (value.Year != DateTime.Today.Year)
            {
                dateString = value.ToString( "yyyy.MM.dd." );
            }

            return dateString;
        }
    }
}
