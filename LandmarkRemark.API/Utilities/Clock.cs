using System;

namespace LandmarkRemark.API.Utilities
{
    public class Clock : IClock
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}