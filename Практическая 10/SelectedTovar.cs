namespace Pract10
{
    public class SelectedTovar : Tovar
    {
        public int selectedCount = 0;

        public SelectedTovar(int id, string name, float price, int count, int selectedCount = 0)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.count = count;
            this.selectedCount = selectedCount;
        }
    }
}
