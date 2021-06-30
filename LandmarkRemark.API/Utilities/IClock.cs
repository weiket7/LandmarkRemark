using System;

namespace LandmarkRemark.API.Utilities
{
    public interface IClock
    {
        DateTime Now();
    }
}