namespace Pract10
{
    public class Kassir
    {
        List<SelectedTovar> tovari;
        List<Zapis> zapisi;
        private string name;

        public Kassir(List<Zapis> zapisi, string name)
        {
            LoadTovari();
            this.zapisi = zapisi;
            this.name = name;
        }

        private void LoadTovari()
        {
            List<SelectedTovar>? tovari = Converter.Load<List<SelectedTovar>>("tovari.json");
            if (tovari == null)
            {
                tovari = new List<SelectedTovar>();
            }
            this.tovari = tovari;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Кассир");
            Console.WriteLine("Enter - Перейти к записи, S - подтвердить покупку");
            Console.WriteLine("------------------------");
            float total = 0;
            foreach (var tovar in tovari)
            {
                total += tovar.price * tovar.selectedCount;
                Console.WriteLine($"  {tovar.id} - {tovar.name}, {tovar.price}руб. | {tovar.selectedCount}");
            }
            Console.WriteLine("------------------------");
            Console.WriteLine($"Итого: {total}руб.");
        }

        private void DrawTovar(int index)
        {
            SelectedTovar tovar = tovari[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Кассир");
            Console.WriteLine("Esc - назад");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {tovar.id} (выдается автоматически)");
            Console.WriteLine($"  Наименование: {tovar.name}");
            Console.WriteLine($"  Цена: {tovar.price}");
            Console.WriteLine($"  Количество на складе: {tovar.count}");
            Console.WriteLine($"  Выбранное количество: {tovar.selectedCount}");
        }

        public void Start()
        {
            DrawMenu();
            Strelka strelka = new Strelka(4, 4 + tovari.Count - 1);
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
                        Select(strelka.GetIndex());
                        DrawMenu();
                        strelka.Show(-1);
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        Submit();
                        LoadTovari();
                        DrawMenu();
                        strelka.Show(-1);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        private void Select(int index)
        {
            DrawTovar(index);
            SelectedTovar tovar = tovari[index];
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)HotKeys.Exit)
            {
                switch (key)
                {
                    case (ConsoleKey)HotKeys.Add:
                        tovar.selectedCount += 1;
                        if (tovar.selectedCount > tovar.count)
                        {
                            tovar.selectedCount = tovar.count;
                        }
                        break;
                    case (ConsoleKey)HotKeys.Sub:
                        tovar.selectedCount -= 1;
                        if (tovar.selectedCount < 0)
                        {
                            tovar.selectedCount = 0;
                        }
                        break;
                }
                Console.SetCursorPosition(24, 8);
                Console.Write($"{tovar.selectedCount}                    ");
                key = Console.ReadKey(true).Key;
            }
        }

        private void Submit()
        {
            List<Tovar> tovari = new List<Tovar>();
            foreach (var tovar in this.tovari)
            {
                tovar.count -= tovar.selectedCount;
                tovari.Add(tovar);

                int zapis_id;
                if (zapisi.Count > 0)
                {
                    zapis_id = zapisi.Max(z => z.id) + 1;
                }
                else
                {
                    zapis_id = 0;
                }
                Zapis zapis = new Zapis(zapis_id, tovar.name, tovar.selectedCount * tovar.price, DateTime.Now, true);
                zapisi.Add(zapis);
                tovar.selectedCount = 0;
            }

            Converter.Save<List<Zapis>>(zapisi, "zapisi.json");
            Converter.Save<List<Tovar>>(tovari, "tovari.json");
        }
    }
}
