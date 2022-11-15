using CowboyAPI.Models;

namespace CowboyAPI.Helpers
{
    public static class CalculateDistanceUtil
    {
        /// <summary>
        /// Distance calculated between two objects based on geocoordinates.
        /// </summary>
        /// <param name="cb1 - Cowboy at Source point"></param>
        /// <param name="cb2 - Cowboy at destination point"></param>
        /// <returns>Return an approximate value in metres based on Haversine formula.</returns>
        public static double CalculateDistance(Cowboy cb1, Cowboy cb2)
        {
            var d1 = (double)cb1.latitude * (Math.PI / 180.0);
            var num1 = (double)cb1.longitude * (Math.PI / 180.0);
            var d2 = (double)cb2.latitude * (Math.PI / 180.0);
            var num2 = (double)cb2.longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

    }
}
