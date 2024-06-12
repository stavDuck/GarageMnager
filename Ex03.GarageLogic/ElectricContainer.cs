using System;

namespace Ex03.GarageLogic
{
    public class ElectricContainer : EnergyContainer
    {
        public ElectricContainer(float i_MaxEnergyAmount) : base(i_MaxEnergyAmount)
        {

        }

        public override void AddEnergy(float i_energyAmount, eFuelType? i_FuelTypeToAdd = null)
        {
            if (i_energyAmount< 0 || m_CurrentEnergyAmount + i_energyAmount> r_MaxEnergyAmount)
            {
                throw new ValueOutOfRangeException(0, r_MaxEnergyAmount - m_CurrentEnergyAmount);
            }
            else
            {
                m_CurrentEnergyAmount += i_energyAmount;
            }
        }
    }
}
