namespace SweepstakesScript.Logic
{
    public class EntryData
    {
        public string firstName;
        public string lastName;
        public string email;
        public string dob;
        public string postalCode;

        public EntryData(string _firstName, string _lastName, string _email, string _dob, string _postalCode = null)
        {
            firstName = _firstName;
            lastName = _lastName;
            email = _email;
            dob = _dob;
            postalCode = _postalCode;
        }
    }
}
