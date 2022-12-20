namespace Pract10
{
    public class Zapis
    {
        public int id;
        public string name;
        public float sum;
        public DateTime date;
        public bool prihod;

        public Zapis(int id, string name, float sum, DateTime date, bool prihod)
        {
            this.id = id;
            this.name = name;
            this.sum = sum;
            this.date = date;
            this.prihod = prihod;
        }
    }
}
