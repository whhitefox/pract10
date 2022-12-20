namespace Pract10
{
    public class Manager : ICrud
    {
        List<Sotrudnik> sotrudniki;
        private string name;
        public Manager(List<Sotrudnik> sotrudniki, string name)
        {
            this.sotrudniki = sotrudniki;
            this.name = name;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            foreach (var sotrudnik in sotrudniki)
            {
                string fio = $"{sotrudnik.f} {sotrudnik.i} {sotrudnik.o}";
                Console.WriteLine($"  {sotrudnik.id} - {fio}, {sotrudnik.dolzhnost}");
            }
        }

        private void DrawSotrudnik(int index)
        {
            Sotrudnik sotrudnik = sotrudniki[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {sotrudnik.id}");
            Console.WriteLine($"  Фамилия: {sotrudnik.f}");
            Console.WriteLine($"  Имя: {sotrudnik.i}");
            Console.WriteLine($"  Отчество: {sotrudnik.o}");
            string birthday = sotrudnik.birthday.ToString("dd.MM.yyyy");
            Console.WriteLine($"  День рождения: {birthday}");
            Console.WriteLine($"  Паспорт: {sotrudnik.passport}");
            Console.WriteLine($"  Должность: {sotrudnik.dolzhnost}");
            Console.WriteLine($"  Зарплата: {sotrudnik.zarplata}");
            string user_id = sotrudnik.user_id == null ? "" : sotrudnik.user_id.ToString();
            Console.WriteLine($"  ID пользователя: {user_id}");
        }

        public void Start()
        {
            DrawMenu();
            Strelka strelka = new Strelka(4, 4 + sotrudniki.Count - 1);
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)HotKeys.Exit)
            {
                switch (key)
                {
                    case (ConsoleKey)HotKeys.Next:
                        strelka.Next();
                        break;
                    case (ConsoleKey)HotKeys.Prev:
                        strelka.Prev();
                        break;
                    case (ConsoleKey)HotKeys.Submit:
                        Read(strelka.GetIndex());
                        DrawMenu();
                        strelka.SetMax(4 + sotrudniki.Count - 1);
                        strelka.Show(-1);
                        break;
                    case (ConsoleKey)HotKeys.Create:
                        Create();
                        DrawMenu();
                        strelka.SetMax(4 + sotrudniki.Count - 1);
                        strelka.Show(-1);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Create()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id;
            if (sotrudniki.Count > 0)
            {
                id = sotrudniki.Max(s => s.id) + 1;
            } else
            {
                id = 0;
            }
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine("  Фамилия: ");
            Console.WriteLine("  Имя: ");
            Console.WriteLine("  Отчество: ");
            Console.WriteLine("  День рождения: ");
            Console.WriteLine("  Паспорт: ");
            Console.WriteLine("  Должность: ");
            Console.WriteLine("  Зарплата: ");
            Console.WriteLine("  ID пользователя: ");
            Sotrudnik? sotrudnik = null;
            Strelka strelka = new Strelka(5, 12);
            string f = "";
            string i = "";
            string o = "";
            string birthday = "";
            string passport = "";
            string dolzhnost = "";
            string zarplata = "";
            string user_id = "";

            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)HotKeys.Exit)
            {
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

                        if (index == 0)
                        {
                            Console.SetCursorPosition(11, 5);
                            f = Vvod.GetValue(f);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(7, 6);
                            i = Vvod.GetValue(i);
                        }
                        else if (index == 2)
                        {
                            Console.SetCursorPosition(12, 7);
                            o = Vvod.GetValue(o);
                        }
                        else if (index == 3)
                        {
                            Console.SetCursorPosition(17, 8);
                            birthday = Vvod.GetValue(birthday);
                        }
                        else if (index == 4)
                        {
                            Console.SetCursorPosition(11, 9);
                            passport = Vvod.GetValue(passport);
                        }
                        else if (index == 5)
                        {
                            Console.SetCursorPosition(13, 10);
                            dolzhnost = Vvod.GetValue(dolzhnost);
                        }
                        else if (index == 6)
                        {
                            Console.SetCursorPosition(12, 11);
                            zarplata = Vvod.GetValue(zarplata);
                        }
                        else if (index == 7)
                        {
                            Console.SetCursorPosition(19, 12);
                            user_id = Vvod.GetValue(user_id);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (f == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Фамилия не может быть пустой.      ");
                            break;
                        }
                        
                        if (i == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Имя не может быть пустым.          ");
                            break;
                        }
                        
                        if (birthday == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("День рождения не может быть пустым.");
                            break;
                        }
                        DateTime birthday_dt;
                        try
                        {
                            birthday_dt = DateTime.Parse(birthday);
                        } catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат даты.              ");
                            break;
                        }
                        
                        if (passport == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Пасспорт не может быть пустым.     ");
                            break;
                        }
                        
                        if (dolzhnost == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Должность не может быть пустой.    ");
                            break;
                        }
                        
                        if (zarplata == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Зарплата не может быть пустой.     ");
                            break;
                        }
                        float zarplata_f;
                        try
                        {
                            zarplata_f = float.Parse(zarplata);
                        } catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат зарплаты.          ");
                            break;
                        }
                        
                        int? user_id_i;
                        if (user_id != "")
                        {
                            try
                            {
                                user_id_i = int.Parse(user_id);
                            } catch
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.WriteLine("Неверный формат ID пользователя.   ");
                                break;
                            }
                        } else
                        {
                            user_id_i = null;
                        }

                        if (sotrudnik == null)
                        {
                            sotrudnik = new Sotrudnik(id, f, i, birthday_dt, passport, dolzhnost, zarplata_f, user_id_i, o);
                            sotrudniki.Add(sotrudnik);
                        } else
                        {
                            sotrudnik.f = f;
                            sotrudnik.i = i;
                            sotrudnik.o = o;
                            sotrudnik.birthday = birthday_dt;
                            sotrudnik.passport = passport;
                            sotrudnik.dolzhnost = dolzhnost;
                            sotrudnik.zarplata = zarplata_f;
                            sotrudnik.user_id = user_id_i;
                        }
                        Console.SetCursorPosition(0, 13);
                        Console.WriteLine("Сохранено.                         ");

                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Sotrudnik>>(sotrudniki, "sotrudniki.json");
        }

        public void Read(int index)
        {
            DrawSotrudnik(index);

            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)HotKeys.Exit)
            {
                switch (key)
                {
                    case (ConsoleKey)HotKeys.Delete:
                        Delete(index);
                        return;
                    case (ConsoleKey)HotKeys.Update:
                        Update(index);
                        DrawSotrudnik(index);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Update(int index)
        {
            Sotrudnik sotrudnik = sotrudniki[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id = sotrudnik.id;
            string f = sotrudnik.f;
            string i = sotrudnik.i;
            string o = sotrudnik.o;
            string birthday = sotrudnik.birthday.ToString("dd.MM.yyyy");
            string passport = sotrudnik.passport;
            string dolzhnost = sotrudnik.dolzhnost;
            string zarplata = sotrudnik.zarplata.ToString();
            string user_id = sotrudnik.user_id != null ? sotrudnik.user_id.ToString() : "";
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Фамилия: {f}");
            Console.WriteLine($"  Имя: {i}");
            Console.WriteLine($"  Отчество: {o}");
            Console.WriteLine($"  День рождения: {birthday}");
            Console.WriteLine($"  Паспорт: {passport}");
            Console.WriteLine($"  Должность: {dolzhnost}");
            Console.WriteLine($"  Зарплата: {zarplata}");
            Console.WriteLine($"  ID пользователя: {user_id}");
            Strelka strelka = new Strelka(5, 12);

            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)HotKeys.Exit)
            {
                switch (key)
                {
                    case (ConsoleKey)HotKeys.Next:
                        strelka.Next();
                        break;
                    case (ConsoleKey)HotKeys.Prev:
                        strelka.Prev();
                        break;
                    case (ConsoleKey)HotKeys.Submit:
                        int s_index = strelka.GetIndex();

                        if (s_index == 0)
                        {
                            Console.SetCursorPosition(11, 5);
                            f = Vvod.GetValue(f);
                        }
                        else if (s_index == 1)
                        {
                            Console.SetCursorPosition(7, 6);
                            i = Vvod.GetValue(i);
                        }
                        else if (s_index == 2)
                        {
                            Console.SetCursorPosition(12, 7);
                            o = Vvod.GetValue(o);
                        }
                        else if (s_index == 3)
                        {
                            Console.SetCursorPosition(17, 8);
                            birthday = Vvod.GetValue(birthday);
                        }
                        else if (s_index == 4)
                        {
                            Console.SetCursorPosition(11, 9);
                            passport = Vvod.GetValue(passport);
                        }
                        else if (s_index == 5)
                        {
                            Console.SetCursorPosition(13, 10);
                            dolzhnost = Vvod.GetValue(dolzhnost);
                        }
                        else if (s_index == 6)
                        {
                            Console.SetCursorPosition(12, 11);
                            zarplata = Vvod.GetValue(zarplata);
                        }
                        else if (s_index == 7)
                        {
                            Console.SetCursorPosition(19, 12);
                            user_id = Vvod.GetValue(user_id);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (f == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Фамилия не может быть пустой.");
                            break;
                        }

                        if (i == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Имя не может быть пустым.");
                            break;
                        }

                        if (birthday == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Имя не может быть пустым.");
                            break;
                        }
                        DateTime birthday_dt;
                        try
                        {
                            birthday_dt = DateTime.Parse(birthday);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат даты.");
                            break;
                        }

                        if (passport == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Пасспорт не может быть пустым.");
                            break;
                        }

                        if (dolzhnost == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Должность не может быть пустой.");
                            break;
                        }

                        if (zarplata == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Зарплата не может быть пустой.");
                            break;
                        }
                        float zarplata_f;
                        try
                        {
                            zarplata_f = float.Parse(zarplata);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат зарплаты.");
                            break;
                        }

                        int? user_id_i;
                        if (user_id != "")
                        {
                            try
                            {
                                user_id_i = int.Parse(user_id);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.WriteLine("Неверный формат ID пользователя.");
                                break;
                            }
                        }
                        else
                        {
                            user_id_i = null;
                        }

                        sotrudnik.f = f;
                        sotrudnik.i = i;
                        sotrudnik.o = o;
                        sotrudnik.birthday = birthday_dt;
                        sotrudnik.passport = passport;
                        sotrudnik.dolzhnost = dolzhnost;
                        sotrudnik.zarplata = zarplata_f;
                        sotrudnik.user_id = user_id_i;

                        Console.SetCursorPosition(0, 13);
                        Console.WriteLine("Сохранено.                  ");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Sotrudnik>>(sotrudniki, "sotrudniki.json");
        }

        public void Delete(int index)
        {
            sotrudniki.RemoveAt(index);
            Converter.Save(sotrudniki, "sotrudniki.json");
        }
    }
}
