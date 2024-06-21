namespace Glitonea.Mvvm.Converters;

using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Utilities;

public class EnumDescriptionConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            throw new ArgumentNullException(
                nameof(value),
                "Value cannot be null."
            );
        }

        if (!value.GetType().IsEnum)
        {
            throw new ArgumentException(
                "Value must be an enum.",
                nameof(value)
            );
        }

        return ((Enum)value).ToDescription();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            throw new ArgumentNullException(
                nameof(value),
                "Value cannot be null."
            );
        }
            
        if (value is EnumDescription enumDescription)
        {
            return enumDescription.Value;
        }
        throw new ArgumentException("ConvertBack:EnumDescription must be an enum.");
    }
}