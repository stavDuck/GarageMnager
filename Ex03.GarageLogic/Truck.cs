using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumOfWheels = 12;
        private const int k_MaximumAirPressure = 28;
        private const float k_MaximumEnergy = 120f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private bool m_IsDangerousCargo;
        private float m_CargoVolume;

        public Truck(string i_ModelName, string i_WheelsManufacturerName, string i_LicenseNumber) :
         base(i_ModelName, i_WheelsManufacturerName, i_LicenseNumber, k_NumOfWheels, k_MaximumAirPressure, eEnergyContainerType.Fuel)
        {
            m_EnergyContainer = new FuelContainer(k_MaximumEnergy, k_FuelType);
            setListOfTruckExtraProperties();
        }

        private void setListOfTruckExtraProperties()
        {
            r_ChildExtraProperties.Add("is dangerous cargo (True / False)");
            r_ChildExtraProperties.Add("cargo volume");
        }

        public override void SetChildExtraProperties(List<string> i_ChildExtraProperties)
        {
            const int k_DangerousCargoIndex = 0;
            const int k_CargoVolumeIndex = 1;
            bool parseCargoVolume = float.TryParse(i_ChildExtraProperties[k_CargoVolumeIndex], out m_CargoVolume);
            bool parseDangerousCargo = bool.TryParse(i_ChildExtraProperties[k_DangerousCargoIndex], out m_IsDangerousCargo);
            
            if(!parseCargoVolume)
            {
                throw new FormatException(string.Format("Invalid Input Format for cargo volume"));
            }
            if (!parseDangerousCargo)
            {
                throw new FormatException(string.Format("Invalid Input Format for if cargo is dangerous"));
            }
        }

        public override string ToString()
        {
            string resultString = base.ToString();
            resultString += Environment.NewLine + string.Format("Cargo Volume: {0}\nAre there dangerous materials: {1}",
                m_CargoVolume, m_IsDangerousCargo);

            return resultString;
        }
    }
}
