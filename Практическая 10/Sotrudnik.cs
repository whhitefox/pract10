namespace Pract10
{
    public class Sotrudnik
    {
        public int id;
        public string f;
        public string i;
        public string o;
        public DateTime birthday;
        public string passport;
        public string dolzhnost;
        public float zarplata;
        public int? user_id;

        public Sotrudnik(int id, string f, string i, DateTime birthday, string passport, string dolzhnost, float zarplata, int? user_id = null, string o = "")
        {
            this.id = id;
            this.f = f;
            this.i = i;
            this.o = o;
            this.birthday = birthday;
            this.passport = passport;
            this.dolzhnost = dolzhnost;
            this.zarplata = zarplata;
            this.user_id = user_id;
        }
    }
}
