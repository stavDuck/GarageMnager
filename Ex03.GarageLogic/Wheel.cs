using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName = null;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value < 0 || value > r_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure, string.Format("Error ! , the wheel's air pressure should be between 0 to {0}", r_MaxAirPressure));
                }
                else
                {
                    m_CurrentAirPressure = value;
                }
            }
        }

        public Wheel(float i_MaxAirPressure, string i_ManufacturerName, float i_CurrentAirPressure = 0)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public void FillWheelToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        public override string ToString()
        {
            return string.Format("Manufacturer: {0}\nMax air pressure: {1}\nCurrent air pressure: {2}",
                r_ManufacturerName, r_MaxAirPressure, m_CurrentAirPressure);
        }
    }
}
