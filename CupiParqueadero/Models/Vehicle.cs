using ParkingChargeSimulator;
using System.Text.Json.Serialization;

namespace CupiParqueadero.Models
{
    public class Vehicle
    {
        
        public int Id { get; set; }
        public string? Plate { get; set; }
        public string? Make { get; set; }
        public string? Color { get; set; }
        public DateTime? EntryDate { get; set; } = DateTime.Now;
        public DateTime? PayDate {  get; set; }
        public bool? IsPayable { get; set; } = false;
        public double? Pay { get; set; } = 0;

        public Vehicle(string Plate, string Make, string Color)
        {
            this.Plate = Plate;
            this.Make = Make;
            this.Color = Color;
        }

        /* This method calculates the number of hours during the day and night that 
        * the vehicle remains in the parking lot
        * Return = hoursDay, hoursNight, numDays
        */
        private static TimeSpanVehicle NumHours(DateTime startTime, DateTime endTime)
        {
            TimeSpan timeSpan = endTime - startTime; //The time between entering and leaving the parking lot
            TimeSpanVehicle timeSpanVehicle = new TimeSpanVehicle();
            DateTime limitDay = startTime.Date.AddHours(18); //6PM, the night time starts
            DateTime limitNight = startTime.Date.AddHours(6);

            //The vehicle is in the parking lot more than eight hours
            if (timeSpan > new TimeSpan(7, 0, 0))
            {
                if (timeSpan.TotalHours <= 24)
                {
                    timeSpanVehicle.SetNumDays(1);
                    return timeSpanVehicle;
                }

                // This is to know about the hours of the last day in the timeSpanVehicle
                TimeSpanVehicle recursive = NumHours(startTime.AddDays(timeSpan.Days), endTime);

                timeSpanVehicle.SetNumDays(timeSpan.Days);
                timeSpanVehicle.SetHoursDay(recursive.GetHoursDay());
                timeSpanVehicle.SetHoursNight(recursive.GetHoursNight());
                if (recursive.GetNumDays() == 1)
                    timeSpanVehicle.SetNumDays(timeSpanVehicle.GetNumDays() + 1);

                return timeSpanVehicle;
            }

            //The vehicle enters the parking lot before 6PM, and leaves AFTER 6PM 
            if (startTime < limitDay && endTime > limitDay)
            {
                timeSpanVehicle.SetHoursDay((limitDay - startTime).Hours);
                timeSpanVehicle.SetHoursNight(timeSpan.Hours - timeSpanVehicle.GetHoursDay());
                if (timeSpan.Minutes != 0)
                    timeSpanVehicle.SetHoursNight(timeSpanVehicle.GetHoursNight() + 1);

                return timeSpanVehicle;
            }

            //The vehicle enters the parking lot AFTER 6PM, and leaves BEFORE 6PM (03:00 - 08:00)
            if (endTime >= limitNight && startTime < limitNight)
            {
                timeSpanVehicle.SetHoursNight((limitNight - startTime).Hours);
                timeSpanVehicle.SetHoursDay(timeSpan.Hours - timeSpanVehicle.GetHoursNight());
                if (timeSpan.Minutes != 0)
                    timeSpanVehicle.SetHoursDay(timeSpanVehicle.GetHoursDay() + 1);

                return timeSpanVehicle;
            }

            //The vehicle enters the parking lot BEFORE 6AM, and leaves BEFORE 6AM (02:00 - 04:00)
            if (startTime < limitNight && endTime < limitNight)
            {
                timeSpanVehicle.SetHoursNight(timeSpan.Hours);
                if (timeSpan.Minutes != 0)
                    timeSpanVehicle.SetHoursNight(timeSpanVehicle.GetHoursNight() + 1);

                return timeSpanVehicle;
            }

            //The vehicle enters the parking lot BEFORE 6PM, and leaves BEFORE 6PM (12:00 - 16:00)
            if (startTime < limitDay && endTime <= limitDay)
            {
                timeSpanVehicle.SetHoursDay(timeSpan.Hours);
                timeSpanVehicle.SetHoursNight(timeSpan.Hours - timeSpanVehicle.GetHoursDay());
                if (timeSpan.Minutes != 0)
                    timeSpanVehicle.SetHoursDay(timeSpanVehicle.GetHoursDay() + 1);

                return timeSpanVehicle;
            }

            //The vehicle enters the parking lot AFTER 6PM (20:00 - 02:00)
            if (startTime >= limitDay)
            {
                timeSpanVehicle.SetHoursNight(timeSpan.Hours);
                if (timeSpan.Minutes != 0)
                    timeSpanVehicle.SetHoursNight(timeSpanVehicle.GetHoursNight() + 1);

                return timeSpanVehicle;
            }

            return timeSpanVehicle;
        }

        // Calculate the final payment of the vehicle with the entering date and the leaving date
        public double PaymentParking(DateTime startTime, DateTime endTime)
        {
            TimeSpanVehicle timeSpanVehicle = NumHours(startTime, endTime);
            int hoursDay = timeSpanVehicle.GetHoursDay();
            int hoursNight = timeSpanVehicle.GetHoursNight();
            int numDays = timeSpanVehicle.GetNumDays();
            double payment = 0.0;
            payment += hoursNight * 35;

            // More than one day in the parking 
            if (numDays > 0)
            {
                payment = numDays * 200;
                // Calculate the value of the last day in the parking
                double lastDayPayment = PaymentParking(startTime.AddDays(numDays), endTime, hoursDay, hoursNight);
                payment += lastDayPayment;
                return payment;
            }

            // The value of each hour of Monday or Wednesday is 10
            if (startTime.DayOfWeek == DayOfWeek.Monday || startTime.DayOfWeek == DayOfWeek.Wednesday)
            {
                payment += hoursDay * 10;
                return payment;
            }

            // The value of each hour of the weekend is 25
            if (startTime.DayOfWeek == DayOfWeek.Saturday || startTime.DayOfWeek == DayOfWeek.Sunday)
            {
                payment += hoursDay * 25;
                return payment;
            }

            // Normal value
            payment += hoursDay * 20;

            return payment;
        }

        public static double PaymentParking(DateTime startTime, DateTime endTime, int hoursDay, int hoursNight)
        {
            double payment = 0.0;
            payment += hoursNight * 35;

            if (hoursDay + hoursNight >= 8)
            {
                return 200;
            }
            // The value of each hour of Monday or Wednesday is 10
            if (startTime.DayOfWeek == DayOfWeek.Monday || startTime.DayOfWeek == DayOfWeek.Wednesday)
            {
                payment += hoursDay * 10;
                return payment;
            }

            // The value of each hour of the weekend is 25
            if (startTime.DayOfWeek == DayOfWeek.Saturday || startTime.DayOfWeek == DayOfWeek.Sunday)
            {
                payment += hoursDay * 25;
                return payment;
            }

            // Normal value
            payment += hoursDay * 20;

            return payment;
        }

    }


}
