using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordingSystem_CarMaintenance
{
    public class CustomerСontacts
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string Email { get; private set; }
        public string Telephone { get; private set; }

        public CustomerСontacts(string NewLastName, string NewFirstName, string NewMiddleName, string NewEmail, string NewTelephone)
        { LastName = NewLastName; FirstName = NewFirstName; MiddleName = NewMiddleName; Email = NewEmail; Telephone = NewTelephone; }
    }
}
