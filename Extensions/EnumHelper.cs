using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Fields;

namespace TA.Playwright.Extensions
{
    public class EnumHelper
    {
        public static Enum GetRandomEnumValue(Type t)
        {
            if (t.IsEnum)
            {
                return Enum.GetValues(t) // get values from Type provided
                .OfType<Enum>() // casts to Enum
                .OrderBy(e => Guid.NewGuid()) // mess with order of results
                .FirstOrDefault()!;            // take first item in result
            }
            else
            {
                throw new Exception("Inputted enum is not an enum!");
            }
        }

        public static string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? value.ToString();
        }
    }
}