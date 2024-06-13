using System;

namespace Ex03.GarageLogic
{
    public class GarageTicket
    {
        private eVehicleState m_VechicleState = eVehicleState.InRepair;
        private readonly Owner r_Owner;
        private readonly Vehicle r_Vehicle;

        public eVehicleState VechicleState
        {
            get
            {
                return m_VechicleState;
            }
            set
            {
                m_VechicleState = value;
            }
        }

        public Vehicle Vehicle 
        {
            get { return r_Vehicle; }
        }

        public Owner Owner 
        { 
            get { return r_Owner; } 
        }

        public GarageTicket(Owner i_Owner, Vehicle i_Vehicle) 
        {
            r_Owner = i_Owner;
            r_Vehicle = i_Vehicle;
        }

        public override string ToString()
        {
            string resultString = r_Owner.ToString();
            resultString += Environment.NewLine + r_Vehicle.ToString();
            resultString += Environment.NewLine + string.Format("State in the garage: {0}", Enum.GetName(typeof(eVehicleState), m_VechicleState));

            return resultString;
        }
    }
}
