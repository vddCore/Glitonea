using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Glitonea.Utilities;

namespace Glitonea
{
    public static partial class Extensions
    {
        public static EnumDescription ToDescription(this Enum value)
        {
            string? description;
            string? hint = null;

            var attributes = value!
                .GetType()!
                .GetField(value.ToString())!
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Any())
            {
                description = (attributes.First() as DescriptionAttribute)?.Description;
            }
            else
            {
                var textInfo = CultureInfo.CurrentCulture.TextInfo;
                description = textInfo.ToTitleCase(textInfo.ToLower(value.ToString().Replace("_", " ")));
            }

            var hintSeparatorPosition = description!.IndexOf(';');
            if (hintSeparatorPosition > 0)
            {
                hint = description.Substring(hintSeparatorPosition + 1);
                description = description.Substring(0, hintSeparatorPosition);
            }

            return new(value) { Description = description, Hint = hint };
        }
    }
}