using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eMotorcycleLicenseType
        {
            A,
            A1,
            AA,
            B1
        }

        private const int k_NumOfWheels = 2;
        private const int k_MaximumAirPressure = 33;
        private const float k_MaximumFuelEnergy = 5.5f;
        private const float k_MaximumElectricEnergy = 2.5f;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private eMotorcycleLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle(string i_ModelName, string i_WheelsManufacturerName, string i_LicenseNumber, eEnergyContainerType i_EnergyType) :
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

            setListOfMotorcycleExtraProperties();
        }

        private void setListOfMotorcycleExtraProperties()
        {
            r_ChildExtraProperties.Add("motorcycle License Type (" + string.Join(", ", Enum.GetNames(typeof(eMotorcycleLicenseType))) + ")");
            r_ChildExtraProperties.Add("engine Capacity");
        }

        public override void SetChildExtraProperties(List<string> i_ChildExtraProperties)
        {
            const int k_LicenseTypeIndex = 0;
            const int k_EngineCapacityIndex = 1;
            bool parseLicenseType = !int.TryParse(i_ChildExtraProperties[k_LicenseTypeIndex], out _) &&
                Enum.TryParse<eMotorcycleLicenseType>(i_ChildExtraProperties[k_LicenseTypeIndex], out m_LicenseType);
            bool parseEngineCapacity = int.TryParse(i_ChildExtraProperties[k_EngineCapacityIndex], out m_EngineCapacity);

            if (!(parseLicenseType && parseEngineCapacity))
            {
                throw new FormatException(string.Format("Invalid Input Format"));
            }
        }

        public override string ToString()
        {
            string resultString = base.ToString();
            resultString += Environment.NewLine + string.Format("License Type: {0}\nEngine Capacity: {1}", m_LicenseType, m_EngineCapacity);

            return resultString;
        }
    }
}