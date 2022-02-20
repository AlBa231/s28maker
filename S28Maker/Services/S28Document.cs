using S28Maker.Models;
using System;
using System.Collections.Generic;

namespace S28Maker.Services
{
    public abstract class S28Document: IS28Document
    {

        public static S28Document Current { get; set;}

        public IReadOnlyCollection<PublicationName> PublicationRows { get; protected set; }

        public IS28MonthColumn CurrentMonth => Monthes[MonthRenderer.GetMonthPos(MonthNumberBeforeCurrent)];

        public IList<IS28MonthColumn> Monthes {get; protected set; }
        private int MonthNumberBeforeCurrent => DateTime.Today.AddMonths(-1).Month;

        public IS28MonthColumn GetMonth(int pos)
        {
            return Monthes[pos];
        }

        public abstract void Close();
    }
}