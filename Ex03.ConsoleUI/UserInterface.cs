using System;
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
        private const string k_OptionSevenMessageInMenu = "Show full detailes of a vehicle.";
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
                    //insertNewVehicleToGarage();
                    break;

                case k_OptionTwo:
                    //listLicenseNumbersWithFilterOption();
                    break;

                case k_OptionThree:
                    changeVehicleState();
                    break;

                case k_OptionFour:
                    fillWheelsToMax();
                    break;

                case k_OptionFive:
                    fuelVehicle();
                    break;

                case k_OptionSix:
                    chargeVehicle();
                    break;

                case k_OptionSeven:
                    showVehicleDetails();
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

            while(!isInputValid && !isLicenseNumberExist)
            {
                try
                {
                    input = getStringFromUser(i_Message);
                    isInputValid = true;
                    isLicenseNumberExist = r_Garage.CheckIfVehicleExist(input);
                    if(!isLicenseNumberExist)
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
            Console.WriteLine("Please choose a new vehicle state from folowed list:");
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

        }



    }
}
