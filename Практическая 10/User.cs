namespace Pract10
{
    public class User
    {
        public int id;
        public string login;
        public string password;
        public Role role;

        public User(int id, string login, string password, Role role)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.role = role;
        }
    }
}
