using System;

namespace Ex03.GarageLogic
{
    public class Owner
    {
        private readonly string r_Name;
        private readonly string r_PhoneNumber;

        public string Name
        {
            get { return r_Name; }
        }

        public string PhoneNumber
        {
            get { return r_PhoneNumber; }
        }

        public Owner(string i_Name, string i_PhoneNumber)
        {
            r_Name = i_Name;
            r_PhoneNumber = i_PhoneNumber;
        }
        public override string ToString()
        {
            return string.Format("Owner name: {0}\nOwner phone: {1}", r_Name, r_PhoneNumber);
        }
    }
}
