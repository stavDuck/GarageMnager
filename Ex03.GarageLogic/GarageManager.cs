using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<int, GarageTicket> r_TicketManager;  

        public GarageManager()
        {
            r_TicketManager = new Dictionary<int, GarageTicket>(); // key = vehicle hashcode, value = ticket information
        }

        public void InsertTicketDetails(string i_LicenseNumber, Owner i_Owner, Vehicle i_vehicle)
        {
            r_TicketManager[i_LicenseNumber.GetHashCode()] = new GarageTicket(i_Owner, i_vehicle);
        }

        public GarageTicket GetTicket(string i_LicenseNumber)
        {
            return r_TicketManager[i_LicenseNumber.GetHashCode()];
        }

        public void SetVehicleState(string i_LicenseNumber, eVehicleState i_NewState)
        {
            r_TicketManager[i_LicenseNumber.GetHashCode()].VechicleState = i_NewState;
        }
        public bool CheckIfVehicleExist(string i_LicenseNumber)
        {
            int vehicleHashCode = i_LicenseNumber.GetHashCode();

            return r_TicketManager != null && r_TicketManager.ContainsKey(vehicleHashCode);
        }

        public void FillVehicleWheelsToMax(string i_VehicleLicenseNumber)
        {
            if (r_TicketManager.TryGetValue(i_VehicleLicenseNumber.GetHashCode(), out GarageTicket ticket))
            {
                foreach (Wheel wheel in ticket.Vehicle.Wheels)
                {
                    wheel.FillWheelToMax();
                }
            }
            else
            {
                throw new ArgumentException(string.Format("Vehicle with License Number: {0} not exist", i_VehicleLicenseNumber));
            }
        }

        public List<string> GetLicenseNumbersWithFilteredByState(eVehicleState i_StateFilter)
        {
            List<string> filteredLicenseNumbers = new List<string>();
            foreach (GarageTicket ticket in r_TicketManager.Values)
            {
                if (ticket.VechicleState == i_StateFilter)
                {
                    filteredLicenseNumbers.Add(ticket.Vehicle.LicenseNumber);
                }
            }

            return filteredLicenseNumbers;
        }

        public List<string> GetAllLicenseNumbers()
        {
            List<string> LicenseNumbers = new List<string>();
            foreach (GarageTicket ticket in r_TicketManager.Values)
            {
                LicenseNumbers.Add(ticket.Vehicle.LicenseNumber);
            }

            return LicenseNumbers;
        }
    }
}
