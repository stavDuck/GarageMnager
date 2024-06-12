using System;
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
                    //isUserExited = executeUserChiose(userChoose);
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



    }
}
