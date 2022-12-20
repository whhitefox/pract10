namespace Pract10
{
    public class Auth
    {
        private List<User> users;

        public Auth(List<User> users)
        {
            this.users = users;
        }

        public User Login()
        {
            Console.Clear();
            User? user = null;
            string login = "";
            string password = "";
            Console.WriteLine("Авторизация");
            Console.WriteLine("-----------");

            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Авторизоваться");

            Strelka strelka = new Strelka(2, 4);

            while (user == null)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case (ConsoleKey)HotKeys.Next:
                        strelka.Next();
                        break;
                    case (ConsoleKey)HotKeys.Prev:
                        strelka.Prev();
                        break;
                    case (ConsoleKey)HotKeys.Submit:
                        int index = strelka.GetIndex();
                        if (index == 2)
                        {
                            user = users.Find(u => u.login == login && u.password == password);
                            if (user == null)
                            {
                                Console.SetCursorPosition(0, 5);
                                Console.WriteLine("Неправильный логин или пароль");
                            }
                        } else if (index == 0)
                        {
                            Console.SetCursorPosition(9, 2);
                            login = Vvod.GetValue(login);
                        } else if (index == 1)
                        {
                            Console.SetCursorPosition(10, 3);
                            password = Vvod.GetValue(password, true);
                        }
                        break;
                }
            }

            return user;
        }
    }
}
