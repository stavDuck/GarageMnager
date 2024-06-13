using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string r_ModelName = null;
        protected readonly string r_LicenseNumber = null;
        private float? m_EnergyPercent = null;
        private readonly List<Wheel> r_Wheels = null;
        protected EnergyContainer m_EnergyContainer = null;
        protected readonly List<string> r_ChildExtraProperties = new List<string>(); // include list of extra properties for children
        private readonly eEnergyContainerType r_EnergyContainerType;

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }

        public eEnergyContainerType EnergyContainerType
        {
            get
            {
                return r_EnergyContainerType;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public List<string> ChildExtraProperties
        {
            get
            {
                return r_ChildExtraProperties;
            }
        }


        internal Vehicle(string i_ModelName, string i_WheelsManufacturerName, string i_LicenseNumber, int i_NumOfWheels,
            float i_MaxAirPressure, eEnergyContainerType i_EnergyType)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new List<Wheel>(i_NumOfWheels);

            for (int i = 1; i <= i_NumOfWheels; i++)
            {
                r_Wheels.Add(new Wheel(i_MaxAirPressure, i_WheelsManufacturerName));
            }

            r_EnergyContainerType = i_EnergyType;
        }

        public abstract void SetChildExtraProperties(List<string> i_ChildExtraProperties);

        public void CompleteVehicleConfiguration(List<string> i_ChildExtraProperties, float i_CurrentWheelPresure, float i_CurrentEnergy)
        {
            m_EnergyContainer.CurrentEnergyAmount = i_CurrentEnergy;
            m_EnergyPercent = m_EnergyContainer.CurrentEnergyAmount / m_EnergyContainer.MaxEnergyAmount * 100;

            foreach (Wheel wheel in r_Wheels)
            {
                wheel.CurrentAirPressure = i_CurrentWheelPresure;
            }

            SetChildExtraProperties(i_ChildExtraProperties);
        }

        public void AddEnergy(float i_energyAmount, eFuelType? i_FuelTypeToAdd = null)
        {
            m_EnergyContainer.AddEnergy(i_energyAmount, i_FuelTypeToAdd);
        }

        public override int GetHashCode()
        {
            return r_LicenseNumber.GetHashCode();
        }

        public override string ToString()
        {
            string resultString = string.Format("Model name: {0}\nLicense number: {1}\nEnergy source: {2}", r_ModelName, r_LicenseNumber, r_EnergyContainerType);
            resultString += Environment.NewLine + m_EnergyContainer.ToString();
            resultString += Environment.NewLine + string.Format("Energy percent: {0}", m_EnergyPercent);
            for (int i = 1; i <= r_Wheels.Count(); i++)
            {
                resultString += string.Format("\nWheel #{0}:\n{1}", i, r_Wheels[i - 1].ToString());
            }

            return resultString;
        }
    }
}
