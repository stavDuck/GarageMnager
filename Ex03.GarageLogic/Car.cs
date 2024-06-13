using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eCarColor
        {
            Yellow,
            White,
            Red,
            Black
        }

        public enum eNumOfCarDoors
        { 
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        private const int k_NumOfWheels = 5;
        private const int k_MaximumAirPressure = 31;
        private const float k_MaximumFuelEnergy = 45f;
        private const float k_MaximumElectricEnergy = 3.5f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private eNumOfCarDoors m_NumOfDoors;
        private eCarColor m_CarColor;

        public Car(string i_ModelName, string i_WheelsManufacturerName, string i_LicenseNumber, eEnergyContainerType i_EnergyType) :
            base(i_ModelName, i_WheelsManufacturerName, i_LicenseNumber, k_NumOfWheels, k_MaximumAirPressure, i_EnergyType)
        {
            if (i_EnergyType == eEnergyContainerType.Fuel)
            {
                m_EnergyContainer = new FuelContainer(k_MaximumFuelEnergy, k_FuelType);
            }
            else
            {
                m_EnergyContainer = new ElectricContainer(k_MaximumElectricEnergy);
            }

            setListOfCarExtraProperties();
        }

        private void setListOfCarExtraProperties()
        {
            r_ChildExtraProperties.Add("car color (" + string.Join(", ", Enum.GetNames(typeof(eCarColor))) + ")");
            r_ChildExtraProperties.Add("number of doors (" + string.Join(", ", Enum.GetNames(typeof(eNumOfCarDoors))) + ")");
        }

        public override void SetChildExtraProperties(List<string> i_ChildExtraProperties)
        {
            const int k_CarColorIndex = 0;
            const int k_NumOfCarDoorsIndex = 1;
            const int k_MinNumOfDoors = 2;
            const int k_MaxNumOfDoors = 5;

            bool parseCarColor = !int.TryParse(i_ChildExtraProperties[k_CarColorIndex], out _) &&
                Enum.TryParse<eCarColor>(i_ChildExtraProperties[k_CarColorIndex], out m_CarColor);
            bool parseCarDoors = Enum.TryParse<eNumOfCarDoors>(i_ChildExtraProperties[k_NumOfCarDoorsIndex], out m_NumOfDoors);
            
            if (!(parseCarColor && parseCarDoors))
            {
                throw new FormatException(string.Format("Invalid Input Format"));
            }
            else if (!parseCarDoors)
            {
                throw new ValueOutOfRangeException(k_MinNumOfDoors, k_MaxNumOfDoors, $"Error ! the number of doors should be between {k_MinNumOfDoors} to {k_MaxNumOfDoors}");
            }
        }

        public override string ToString()
        {
            string resultString = base.ToString();
            resultString += Environment.NewLine + string.Format("Car Color: {0}\nNumber Of Doors: {1}", m_CarColor, m_NumOfDoors);
            
            return resultString;
        }
    }
}