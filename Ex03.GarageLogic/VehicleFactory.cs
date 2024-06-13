using System;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public static Vehicle CreateNewVehicle(eVehicleType i_VechicleType, string i_VehicleLicenseNumber, string i_ModelName, 
            string i_WheelsManufacturerName)
        {
            Vehicle newVehicle = null;

            switch (i_VechicleType)
            {
                case eVehicleType.Car:
                    newVehicle = new Car(i_ModelName, i_WheelsManufacturerName, i_VehicleLicenseNumber, eEnergyContainerType.Fuel);
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck(i_ModelName, i_WheelsManufacturerName, i_VehicleLicenseNumber);
                    break;
                case eVehicleType.Motorcycle:
                    newVehicle = new Motorcycle(i_ModelName, i_WheelsManufacturerName, i_VehicleLicenseNumber, eEnergyContainerType.Fuel);
                    break;
                case eVehicleType.ElectricCar:
                    newVehicle = new Car(i_ModelName, i_WheelsManufacturerName, i_VehicleLicenseNumber, eEnergyContainerType.Electric);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new Motorcycle(i_ModelName, i_WheelsManufacturerName, i_VehicleLicenseNumber, eEnergyContainerType.Electric);
                    break;
            }

            return newVehicle;
        }
    }
}
