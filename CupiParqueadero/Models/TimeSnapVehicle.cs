using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingChargeSimulator
{
    internal class TimeSpanVehicle
    {
        private int hoursNight { get; set; } = 0;
        private int hoursDay { get; set; } = 0;
        private int numDays { get; set; } = 0;

        public void SetHoursNight(int value)
        {
            hoursNight = value;
        }

        public int GetHoursNight()
        {
            return hoursNight;
        }

        public void SetHoursDay(int value)
        {
            hoursDay = value;
        }

        public int GetHoursDay()
        {
            return hoursDay;
        }

        public void SetNumDays(int value)
        {
            numDays = value;
        }

        public int GetNumDays()
        {
            return numDays;
        }
    }
}
