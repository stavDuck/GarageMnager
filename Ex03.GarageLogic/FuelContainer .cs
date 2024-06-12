using System;

namespace Ex03.GarageLogic
{
    public class FuelContainer : EnergyContainer
    {
        private readonly eFuelType r_FuelType;

        public FuelContainer(float i_MaxEnergyAmount, eFuelType i_FuelType) : base(i_MaxEnergyAmount)
        {
            r_FuelType = i_FuelType;
        }

        public override void AddEnergy(float i_energyAmount, eFuelType? i_FuelTypeToAdd = null)
        {
            if (i_energyAmount < 0 || m_CurrentEnergyAmount + i_energyAmount > r_MaxEnergyAmount || !IsFuelMatching(i_FuelTypeToAdd))
            {
                throw new ValueOutOfRangeException(0, r_MaxEnergyAmount - m_CurrentEnergyAmount);
            }
            else
            {
                m_CurrentEnergyAmount += i_energyAmount;
            }
        }

        public bool IsFuelMatching(eFuelType? i_FuelType = null)
        {
            bool isMatching = i_FuelType == r_FuelType;

            if (!isMatching)
            {
                throw new ArgumentException("The fuel chosen is unmatching");
            }

            return isMatching;
        }

        public override string ToString()
        {
            string resString = base.ToString();
            resString += Environment.NewLine + $"fuel type: {Enum.GetName(typeof(eFuelType), r_FuelType)}";

            return resString;
        }
    }
}
