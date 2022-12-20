namespace Pract10
{
    public class Tovar
    {
        public int id;
        public string name;
        public float price;
        public int count;

        public Tovar() { }
        public Tovar(int id, string name, float price, int count)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.count = count;
        }
    }
}
