namespace Pract10
{
    class Program
    {
        static List<User> users;
        static List<Sotrudnik> sotrudniki;
        static List<Tovar> tovari;
        static List<Zapis> zapisi;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            LoadFiles();
            ConsoleKey key;
            while (true)
            {
                Auth auth = new Auth(users);
                User auth_user = auth.Login();
                Console.Clear();
                Sotrudnik? auth_sotrudnik = sotrudniki.Find(s => s.user_id == auth_user.id);
                string name = auth_sotrudnik == null ? auth_user.login : auth_sotrudnik.i;
                switch (auth_user.role)
                {
                    case Role.Admin:
                        Admin admin = new Admin(users, name);
                        admin.Start();
                        break;
                    case Role.Manager:
                        Manager manager = new Manager(sotrudniki, name);
                        manager.Start();
                        break;
                    case Role.StoreManager:
                        StoreManager storeManager = new StoreManager(tovari, name);
                        storeManager.Start();
                        break;
                    case Role.Kassir:
                        Kassir kassir = new Kassir(zapisi, name);
                        kassir.Start();
                        break;
                    case Role.Buhgalter:
                        Buhgalter buhgalter = new Buhgalter(zapisi, name);
                        buhgalter.Start();
                        break;
                }
            }
        }

        static void LoadFiles()
        {
            List<User>? loaded_users = Converter.Load<List<User>>("users.json");
            if (loaded_users == null)
            {
                loaded_users = new List<User>();
                User admin = new User(0, "admin", "password", Role.Admin);
                loaded_users.Add(admin);
                Converter.Save<List<User>>(loaded_users, "users.json");
            }
            users = loaded_users;

            List<Sotrudnik>? loaded_sotrudniki = Converter.Load<List<Sotrudnik>>("sotrudniki.json");
            if (loaded_sotrudniki == null)
            {
                loaded_sotrudniki = new List<Sotrudnik>();
                Converter.Save<List<Sotrudnik>>(loaded_sotrudniki, "sotrudniki.json");
            }
            sotrudniki = loaded_sotrudniki;

            List<Tovar>? loaded_tovari = Converter.Load<List<Tovar>>("tovari.json");
            if (loaded_tovari == null)
            {
                loaded_tovari = new List<Tovar>();
                Converter.Save<List<Tovar>>(loaded_tovari, "tovari.json");
            }
            tovari = loaded_tovari;

            List<Zapis>? loaded_zapisi = Converter.Load<List<Zapis>>("zapisi.json");
            if (loaded_zapisi == null)
            {
                loaded_zapisi = new List<Zapis>();
                Converter.Save<List<Zapis>>(loaded_zapisi, "zapisi.json");
            }
            zapisi = loaded_zapisi;
        }
    }
}