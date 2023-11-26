using System;

namespace RecordingSystem_CarMaintenance
{
    public class CustomerСontacts
    {
        public string ID { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string Email { get; private set; }
        public string Telephone { get; private set; }

        public CustomerСontacts(string NewLastName, string NewFirstName, string NewMiddleName, string NewEmail, string NewTelephone)
        { LastName = NewLastName; FirstName = NewFirstName; MiddleName = NewMiddleName; Email = NewEmail; Telephone = NewTelephone; }

        public void IDA (string NewID) => ID= NewID;
    }

    public class MaintenanceRequests
    {
        public string ID { get; private set; }
        public string TransportModel { get; private set; }
        public string DescriptionProblem { get; private set; }
        public int Cost { get; private set; }
        public string StatusWork { get; private set; }
        public string StatusPayment { get; private set; }
        public CustomerСontacts Contact_Client { get; private set; }
        public CustomerСontacts Contact_Master { get; private set; }
        public DateTime Date { get; private set; }

        public MaintenanceRequests(string NewID, string NewTransportModel, string NewDescriptionProblem, int NewCost, string NewStatusWork, string NewStatusPayment, CustomerСontacts NewClient, CustomerСontacts NewMaster, DateTime NewDate)
        { ID = NewID; TransportModel = NewTransportModel; DescriptionProblem = NewDescriptionProblem; Cost = NewCost; StatusWork = NewStatusWork; StatusPayment = NewStatusPayment; Contact_Client = NewClient; Contact_Master = NewMaster; Date = NewDate; }
    }
}