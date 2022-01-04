using System;
using System.Collections.Generic;
using System.Text;

namespace S28Maker.Services
{
    static class MonthRenderer
    {
        public static readonly string[] MonthNames = { "сент/март", "окт/апр", "нояб/май", "дек/июнь","янв/июль", "февр/авг" };

        private const int StartMonth = 9;

        public static int GetMonthPos(int month)
        {
            return (month + StartMonth) % MonthNames.Length;
        }

        public static string GetMonthName(int month)
        {
            return MonthNames[GetMonthPos(month)];
        }
    }
}
