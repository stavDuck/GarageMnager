using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private const string k_LicenseNumberMessage = "Please insert a license number:";
        private const string k_ModelNameMessage = "Please insert the model name:";
        private const string k_WheelManufacturerNameMessage = "Please insert the wheels manufacturer name:";
        private const string k_GetOwnerNameMsg = "Please insert the owner name:";
        private const string k_OptionOneMessageInMenu = "Insert a new vehicle in the garage.";
        private const string k_OptionTwoMessageInMenu = "Show all license numbers of vehcles in the garage (with filter option).";
        private const string k_OptionThreeMessageInMenu = "Chage the state of a vehicle in the garage.";
        private const string k_OptionFourMessageInMenu = "Fill the vehcle's wheels with air to maximum.";
        private const string k_OptionFiveMessageInMenu = "Fule a vehicle.";
        private const string k_OptionSixMessageInMenu = "Charge an electric vehicle.";
        private const string k_OptionSevenMessageInMenu = "Show full detailes of a vehicle by license number.";
        private const string k_OptionEightMessageInMenu = "Exit.";
        private const string k_OptionOne = "1";
        private const string k_OptionTwo = "2";
        private const string k_OptionThree = "3";
        private const string k_OptionFour = "4";
        private const string k_OptionFive = "5";
        private const string k_OptionSix = "6";
        private const string k_OptionSeven = "7";
        private const string k_OptionEight = "8";
        private readonly GarageManager r_Garage = new GarageManager();

        public void StartGarageApp()
        {
            bool isUserExited = false;

            while (!isUserExited)
            {
                Console.Clear();
                printOptionMenu();
                string userChoose = Console.ReadLine();
                try
                {
                    isUserExited = userChioseAction(userChoose);
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch (ArgumentException argException)
                {
                    Console.WriteLine(argException.Message);
                }
                catch (ValueOutOfRangeException rangeException)
                {
                    Console.WriteLine(rangeException.Message);
                }

                if (!isUserExited)
                {
                    System.Console.WriteLine("Press any key to go back to the manu...");
                    System.Console.ReadKey();
                }
            }
        }

        public void printOptionMenu()
        {
            Console.WriteLine(
                String.Format("Welcome to the Garage Application, please select your action: \n" +
                "1. {0}\n2. {1}\n3. {2}\n4. {3}\n5. {4}\n6. {5}\n7. {6}\n8. {7}",
                k_OptionOneMessageInMenu, k_OptionTwoMessageInMenu, k_OptionThreeMessageInMenu,
                k_OptionFourMessageInMenu, k_OptionFiveMessageInMenu, k_OptionSixMessageInMenu,
                k_OptionSevenMessageInMenu, k_OptionEightMessageInMenu));
        }

        private bool userChioseAction(string i_UserChoose)
        {
            bool wantToExit = false;

            switch (i_UserChoose)
            {
                case k_OptionOne:
                    insertVehicleToGarage();
                    break;

                case k_OptionTwo:
                    listLicenseNumbersWithFilterOption();
                    break;

                case k_OptionThree:
                    changeVehicleState();
                    break;

                case k_OptionFour:
                    //fillWheelsToMax();
                    break;

                case k_OptionFive:
                    //fuelVehicle();
                    break;

                case k_OptionSix:
                    //chargeVehicle();
                    break;

                case k_OptionSeven:
                    printVehicleTicketDetails();
                    break;

                case k_OptionEight:
                    wantToExit = true;
                    break;

                default:
                    if (!int.TryParse(i_UserChoose, out int intChoise))
                    {
                        throw new FormatException("Only digits are allowed!");
                    }
                    else if (intChoise < 1 || intChoise > 8)
                    {
                        throw new ValueOutOfRangeException(1, 8);
                    }

                    break;
            }

            return wantToExit;
        }

        private string getStringFromUser(string i_Message)
        {
            Console.WriteLine(i_Message);
            string resultString = Console.ReadLine();

            if (resultString.Equals(""))
            {
                throw new ArgumentException("Error ! input can't be an empty string!");
            }

            return resultString;
        }
       
        private void printEnumTypesOptionMenu<T>()
        {
            int i = 1;
            foreach (string tType in Enum.GetNames(typeof(T)))
            {
                Console.WriteLine(string.Format("{0}. {1}", i, tType));
                i++;
            }
        }
        
        private void changeVehicleState()
        {
            string userLicenseNumber = getValidLicenseNumberFromUser(k_LicenseNumberMessage);
            eVehicleState requiredState = getValidVehicleStateFromUser();
            r_Garage.SetVehicleState(userLicenseNumber, requiredState);
            Console.WriteLine(string.Format("Vehicle with license number {0} new state: {1}", userLicenseNumber, requiredState));
        }

        public string getValidLicenseNumberFromUser(string i_Message)
        {
            bool isInputValid = false;
            bool isLicenseNumberExist = false;
            string input = null;

            while (!isInputValid && !isLicenseNumberExist)
            {
                try
                {
                    input = getStringFromUser(i_Message);
                    isInputValid = true;
                    isLicenseNumberExist = r_Garage.CheckIfVehicleExist(input);
                    if (!isLicenseNumberExist)
                    {
                        Console.WriteLine("The Vehicle is not in the garage, please try another number.");
                    }
                }
                catch (ArgumentException argException)
                {
                    Console.WriteLine(argException.Message);
                }
            }

            return input;
        }

        public eVehicleState getValidVehicleStateFromUser()
        {
            eVehicleState? vehicleStateInput = null;
            bool isValidState = false;

            while (!isValidState)
            {
                try
                {
                    vehicleStateInput = getVehicleStateFromUser();
                    isValidState = true;
                }
                catch (FormatException formatExeption)
                {
                    Console.WriteLine(formatExeption.Message);
                }
                catch (ValueOutOfRangeException rangeExeption)
                {
                    Console.WriteLine(rangeExeption.Message);
                }
            }

            return (eVehicleState)vehicleStateInput;
        }

        private eVehicleState getVehicleStateFromUser()
        {
            Console.WriteLine("Please choose a new vehicle state from the list:");
            printEnumTypesOptionMenu<eVehicleState>();
            if (int.TryParse(Console.ReadLine(), out int userChoise))
            {
                if (userChoise < 1 || userChoise > Enum.GetNames(typeof(eVehicleState)).Length)
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eVehicleState)).Length);
                }
            }
            else
            {
                throw new FormatException("Invalid input format");
            }

            return (eVehicleState)(userChoise - 1);
        }

        private void fillWheelsToMax()
        {
            string userLicenseNumber = getValidLicenseNumberFromUser(k_LicenseNumberMessage);
            try
            {
                r_Garage.FillVehicleWheelsToMax(userLicenseNumber);
                Console.WriteLine(string.Format("Pumped wheels to max for Vehicle with license number: {0}", userLicenseNumber));
            }
            catch (ArgumentException argException)
            {
                Console.WriteLine(argException.Message);
            }
        }

        private void fuelVehicle()
        {
            string userLicenseNumber = getValidLicenseNumberFromUser(k_LicenseNumberMessage);
            eFuelType fuelType = getValidFuelTypeFromUser();
            Console.WriteLine("Please insert fuel amount:");
            if (!float.TryParse(Console.ReadLine(), out float amount))
            {
                throw new FormatException("Invalid Format Input!");
            }

            r_Garage.GetTicket(userLicenseNumber).Vehicle.AddEnergy(amount, fuelType);
            Console.WriteLine(string.Format("Added {0} liters of {1} to vheicle: {2}", amount, Enum.GetName(typeof(eFuelType), fuelType), userLicenseNumber));
        }
      
        private void insertVehicleToGarage()
        {
            string userLicenseNumber = getStringFromUser(k_LicenseNumberMessage);

            if (r_Garage.CheckIfVehicleExist(userLicenseNumber))
            {
                Console.WriteLine("It seems that the vehicle already exists in the garage. We're setting its state to: in repair.");
            }
            else
            {
                Console.WriteLine("It seems that the vehicle isn't exists in the garage. " +
                    "Lets get all the details and enter your vehicle to the garage");
                insertNewVehicleToGarage(userLicenseNumber);
                Console.WriteLine(string.Format("Vehicle {0} has been added to the garage!", userLicenseNumber));
            }

            r_Garage.SetVehicleState(userLicenseNumber, eVehicleState.InRepair);
        }

        private void insertNewVehicleToGarage(string i_VehicleLicenseNumber)
        {
            eVehicleType vehicleType = getVehicleTypeFromUserAndCheckItsValida();
            string modelName = getStringFromUser(k_ModelNameMessage);
            string WheelsManufacturerName = getStringFromUser(k_WheelManufacturerNameMessage);
            Vehicle newVehicle = VehicleFactory.CreateNewVehicle(vehicleType, i_VehicleLicenseNumber, modelName, WheelsManufacturerName);
            bool isValidProperty = false;
            float currentEnergy, currentWheelsPresure;

            while (!isValidProperty)
            {
                List<string> extraMembers = getExtraPropertiesFromUser(newVehicle.ChildExtraProperties);
                try
                {
                    currentEnergy = getCurrentEnergyInTheVehicleFromUser(newVehicle.EnergyContainerType);
                    currentWheelsPresure = getCurrentWheelsPresureInTheVehicleFromUser();
                    newVehicle.CompleteVehicleConfiguration(extraMembers, currentWheelsPresure, currentEnergy);
                    isValidProperty = true;
                }
                catch (ValueOutOfRangeException rangeExeption)
                {
                    Console.WriteLine(rangeExeption.Message);
                }
                catch (FormatException formatExeption)
                {
                    Console.WriteLine(formatExeption.Message);
                }
            }
        }

        private List<string> getExtraPropertiesFromUser(List<string> i_ChildExtraProperties)
        {
            List<string> userInputList = new List<string>();

            foreach (string property in i_ChildExtraProperties)
            {
                Console.WriteLine(string.Format("Please enter {0}:", property));
                Console.WriteLine(string.Format("Note that its case sensitive"));
                userInputList.Add(Console.ReadLine());
            }

            return userInputList;
        }

        private float getCurrentEnergyInTheVehicleFromUser(eEnergyContainerType i_EnergyContainerType)
        {
            string currentEnergyAmountFromUserString;
            float currentEnergyAmountFromUser;
            
            Console.WriteLine($"Please insert the current {Enum.GetName(typeof(eEnergyContainerType), i_EnergyContainerType)} amount:");
            currentEnergyAmountFromUserString = Console.ReadLine();
            if (!float.TryParse(currentEnergyAmountFromUserString, out currentEnergyAmountFromUser))
            {
                throw new FormatException("Invalid input format");
            }

            return currentEnergyAmountFromUser;
        }

        private float getCurrentWheelsPresureInTheVehicleFromUser()
        {
            string currentAirPressureInputString;
            float currentWheelsPresureInput;

            Console.WriteLine("Please enter the current air pressure of the wheels:");
            currentAirPressureInputString = Console.ReadLine();
            if (!float.TryParse(currentAirPressureInputString, out currentWheelsPresureInput))
            {
                throw new FormatException("Invalid input format");
            }

            return currentWheelsPresureInput;
        }

        private eVehicleType getVehicleTypeFromUserAndCheckItsValida()
        {
            eVehicleType vehicleTypeChoice = eVehicleType.Car;
            bool isValidType = false;

            while (!isValidType)
            {
                try
                {
                    vehicleTypeChoice = getVehicleTypeFromUser();
                    isValidType = true;
                }
                catch (FormatException formatExeption)
                {
                    Console.WriteLine(formatExeption.Message);
                }
                catch (ValueOutOfRangeException rangeExeption)
                {
                    Console.WriteLine(rangeExeption.Message);
                }
            }

            return vehicleTypeChoice;
        }

        private eVehicleType getVehicleTypeFromUser()
        {
            string vehicleTypeInputString;
            int vehicleTypeInput;

            Console.WriteLine("Please choose vehicle type number from folowed list:");
            printEnumTypesOptionMenu<eVehicleType>();
            vehicleTypeInputString = Console.ReadLine();
            if (int.TryParse(vehicleTypeInputString, out vehicleTypeInput))
            {
                if (vehicleTypeInput < 1 || vehicleTypeInput > Enum.GetNames(typeof(eVehicleType)).Length)
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eVehicleType)).Length);
                }
            }
            else
            {
                throw new FormatException("Invalid input format");
            }

            return (eVehicleType)vehicleTypeInput;
        }

        private eFuelType getValidFuelTypeFromUser()
        {
            eFuelType? fuelTypeInput = null;
            bool isValidType = false;

            while (!isValidType)
            {
                try
                {
                    fuelTypeInput = getFuelTypeFromUser();
                    isValidType = true;
                }
                catch (FormatException formatExeption)
                {
                    Console.WriteLine(formatExeption.Message);
                }
                catch (ValueOutOfRangeException rangeExeption)
                {
                    Console.WriteLine(rangeExeption.Message);
                }
            }

            return (eFuelType)(fuelTypeInput - 1);
        }

        private eFuelType getFuelTypeFromUser()
        {
            Console.WriteLine("Please choose fuel type number from the list:");
            printEnumTypesOptionMenu<eFuelType>();
            if (int.TryParse(Console.ReadLine(), out int userChoise))
            {
                if (userChoise < 1 || userChoise > Enum.GetNames(typeof(eFuelType)).Length)
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eFuelType)).Length);
                }
            }
            else
            {
                throw new FormatException("Invalid input format");
            }

            return (eFuelType)userChoise;
        }

        private void listLicenseNumbersWithFilterOption()
        {
            List<string> licenseNumbersInGarge = new List<string>();
            string userInput;
            eVehicleState vehicleStateToFilterLicenseNumberBy;

            Console.WriteLine("Do you want to filter the list by vechicle state? (Y/N)");
            userInput = Console.ReadLine();
            while (userInput != "Y" && userInput != "N")
            {
                Console.WriteLine("Invalid input, make sure the input is 'Y' or 'N', any other option isn't valid");
                userInput = Console.ReadLine();
            }

            if (userInput == "Y")
            {
                vehicleStateToFilterLicenseNumberBy = getVehicleStateFromUser();
                licenseNumbersInGarge = r_Garage.GetLicenseNumbersWithFilteredByState(vehicleStateToFilterLicenseNumberBy);
                Console.WriteLine($"List of the license numbers of vehicles in the garage with state {vehicleStateToFilterLicenseNumberBy}:");
            }
            else if (userInput == "N")
            {
                licenseNumbersInGarge = r_Garage.GetAllLicenseNumbers();
                Console.WriteLine("List of the License numbers of all the vehicles in the garage:");
            }

            if (licenseNumbersInGarge.Count == 0)
            {
                Console.WriteLine("The list is empty");
            }

            foreach (string licenseNumber in licenseNumbersInGarge)
            {
                Console.WriteLine($"{licenseNumber}");
            }
        }

        private void chargeVehicle()
        {
            string userLicenseNumber = getValidLicenseNumberFromUser(k_LicenseNumberMessage);

            Console.WriteLine("Please insert duration in minutes to charge:");
            if (!float.TryParse(Console.ReadLine(), out float amount))
            {
                throw new FormatException("Invalid Format Input!");
            }

            amount = amount / 60;
            r_Garage.GetTicket(userLicenseNumber).Vehicle.AddEnergy(amount);
            Console.WriteLine(string.Format("Vehicle {0} was charged with {1} hours", userLicenseNumber, amount));
        }

        private void printVehicleTicketDetails()
        {
            string userLicenseNumber = getValidLicenseNumberFromUser(k_LicenseNumberMessage);
            GarageTicket ticket = r_Garage.GetTicket(userLicenseNumber);
            Console.WriteLine(ticket.ToString());
        }
    }
}
