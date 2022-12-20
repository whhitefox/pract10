namespace Pract10
{
    public class Buhgalter
    {
        List<Zapis> zapisi;
        private string name;
        public Buhgalter(List<Zapis> zapisi, string name)
        {
            this.zapisi = zapisi;
            this.name = name;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            float total = 0;
            foreach (var zapis in zapisi)
            {
                if (zapis.prihod)
                {
                    total += zapis.sum;
                } else
                {
                    total -= zapis.sum;
                }

                string date = zapis.date.ToString("dd.MM.yyyy");
                string type = zapis.prihod ? "Прибавка" : "Вычет";
                Console.WriteLine($"  {zapis.id} - {zapis.name}, {date}, {zapis.sum}руб., {type}");
            }
            Console.WriteLine("------------------------");
            Console.WriteLine($"Итого: {total}руб.");
        }

        private void DrawZapis(int index)
        {
            Zapis zapis = zapisi[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {zapis.id}");
            Console.WriteLine($"  Название: {zapis.name}");
            Console.WriteLine($"  Сумма: {zapis.sum}");
            string date = zapis.date.ToString("dd.MM.yyyy");
            Console.WriteLine($"  Дата: {date}");
            Console.WriteLine($"  Прибавка?: {zapis.prihod}");
        }

        public void Start()
        {
            DrawMenu();
            Strelka strelka = new Strelka(4, 4 + zapisi.Count - 1);
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
                        strelka.SetMax(4 + zapisi.Count - 1);
                        strelka.Show(-1);
                        break;
                    case (ConsoleKey)HotKeys.Create:
                        Create();
                        DrawMenu();
                        strelka.SetMax(4 + zapisi.Count - 1);
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
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id;
            if (zapisi.Count > 0)
            {
                id = zapisi.Max(s => s.id) + 1;
            }
            else
            {
                id = 0;
            }
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine("  Название: ");
            Console.WriteLine("  Сумма: ");
            Console.WriteLine("  Дата: ");
            Console.WriteLine("  Прибавка?: ");
            Zapis? zapis = null;
            Strelka strelka = new Strelka(5, 8);
            string z_name = "";
            string sum = "";
            string date = "";
            string prihod = "";

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
                            Console.SetCursorPosition(12, 5);
                            z_name = Vvod.GetValue(z_name);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(9, 6);
                            sum = Vvod.GetValue(sum);
                        }
                        else if (index == 2)
                        {
                            Console.SetCursorPosition(8, 7);
                            date = Vvod.GetValue(date);
                        }
                        else if (index == 3)
                        {
                            Console.SetCursorPosition(13, 8);
                            prihod = Vvod.GetValue(prihod);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (z_name == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Название не может быть пустым.");
                            break;
                        }

                        if (sum == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Сумма не может быть пустой.   ");
                            break;
                        }
                        float sum_f;
                        try
                        {
                            sum_f = float.Parse(sum);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат суммы.        ");
                            break;
                        }

                        DateTime date_dt;
                        if (date == "")
                        {
                            date_dt = DateTime.Now;
                        }
                        else
                        {
                            try
                            {
                                date_dt = DateTime.Parse(date);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 9);
                                Console.WriteLine("Неверный формат даты.         ");
                                break;
                            }
                        }

                        bool prihod_b;
                        if (prihod.ToLower() == "false")
                        {
                            prihod_b = false;
                        }
                        else if (prihod.ToLower() == "true")
                        {
                            prihod_b = true;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат прибавки.     ");
                            break;
                        }


                        if (zapis == null)
                        {
                            zapis = new Zapis(id, z_name, sum_f, date_dt, prihod_b);
                            zapisi.Add(zapis);
                        }
                        else
                        {
                            zapis.name = z_name;
                            zapis.sum = sum_f;
                            zapis.date = date_dt;
                            zapis.prihod = prihod_b;
                        }
                        Console.SetCursorPosition(0, 9);
                        Console.WriteLine("Сохранено.                    ");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Zapis>>(zapisi, "zapisi.json");
        }

        public void Read(int index)
        {
            DrawZapis(index);

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
                        DrawZapis(index);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Update(int index)
        {
            Zapis zapis = zapisi[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id = zapis.id;
            string z_name = zapis.name;
            string sum = zapis.sum.ToString();
            string date = zapis.date.ToString("dd.MM.yyyy");
            string prihod = zapis.prihod.ToString();
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Название: {z_name}");
            Console.WriteLine($"  Сумма: {sum}");
            Console.WriteLine($"  Дата: {date}");
            Console.WriteLine($"  Прибавка?: {prihod}");
            Strelka strelka = new Strelka(5, 8);

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
                            Console.SetCursorPosition(12, 5);
                            z_name = Vvod.GetValue(z_name);
                        }
                        else if (s_index == 1)
                        {
                            Console.SetCursorPosition(9, 6);
                            sum = Vvod.GetValue(sum);
                        }
                        else if (s_index == 2)
                        {
                            Console.SetCursorPosition(8, 7);
                            date = Vvod.GetValue(date);
                        }
                        else if (s_index == 3)
                        {
                            Console.SetCursorPosition(13, 8);
                            prihod = Vvod.GetValue(prihod);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (z_name == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Название не может быть пустым.");
                            break;
                        }

                        if (sum == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Сумма не может быть пустой.   ");
                            break;
                        }
                        float sum_f;
                        try
                        {
                            sum_f = float.Parse(sum);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат суммы.        ");
                            break;
                        }

                        DateTime date_dt;
                        if (date == "")
                        {
                            date_dt = DateTime.Now;
                        }
                        else
                        {
                            try
                            {
                                date_dt = DateTime.Parse(date);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 9);
                                Console.WriteLine("Неверный формат даты.         ");
                                break;
                            }
                        }

                        bool prihod_b;
                        if (prihod.ToLower() == "false")
                        {
                            prihod_b = false;
                        }
                        else if (prihod.ToLower() == "true")
                        {
                            prihod_b = true;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат прибавки.     ");
                            break;
                        }


                        zapis.name = z_name;
                        zapis.sum = sum_f;
                        zapis.date = date_dt;
                        zapis.prihod = prihod_b;
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.                    ");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Zapis>>(zapisi, "zapisi.json");
        }

        public void Delete(int index)
        {
            zapisi.RemoveAt(index);
            Converter.Save(zapisi, "zapisi.json");
        }
    }
}