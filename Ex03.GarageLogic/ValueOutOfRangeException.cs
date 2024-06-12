using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message = null) 
            : base(i_Message ?? string.Format($"An error occured, the number should be between {i_MinValue} to {i_MaxValue}"))
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }
    }
}
