using System;

namespace Ex03.GarageLogic
{
    public abstract class EnergyContainer
    {
        protected readonly float r_MaxEnergyAmount;
        protected float m_CurrentEnergyAmount = 0;

        public EnergyContainer(float i_MaxEnergyAmount)
        {
            r_MaxEnergyAmount = i_MaxEnergyAmount;
        }

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }
            set
            {
                if (value < 0 || value > r_MaxEnergyAmount)
                {
                    throw new ValueOutOfRangeException(0, r_MaxEnergyAmount, $"An error occured, the amount of energy should be between {0} to {r_MaxEnergyAmount}");
                }
                else
                {
                    m_CurrentEnergyAmount = value;
                }
            }
        }

        public float MaxEnergyAmount
        {
            get
            {
                return r_MaxEnergyAmount;
            }
        }

        public abstract void AddEnergy(float i_energyAmount, eFuelType? i_FuelTypeToAdd = null);

        public override string ToString()
        {
            return string.Format(
                $@"Max energy amount: {r_MaxEnergyAmount}
                Current energy amount: {m_CurrentEnergyAmount}");
        }
    }
}

