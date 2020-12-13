namespace .Users.Api.Models
{
    public class User
    {
        private readonly int _id;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _email;

        public User(int id, string firstName, string lastname, string email)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastname;
            _email = email;
        }

        public int Id
        {
            get { return _id; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public string Email
        {
            get { return _email; }
        }
    }
}
