using System;
using System.Collections.Generic;
using Ical.Net;
using Ical.Net.DataTypes;

namespace Frapid.Calendar.ViewModels
{
    public class Recurrence
    {
        public List<int> ByMonthDay { get; set; } = new List<int>();
        public List<WeekDay> ByDay { get; set; }
        public FrequencyType Frequency { get; set; }
        public int Interval { get; set; }
        public DateTime Until { get; set; }
    }
}