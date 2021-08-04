using projekt.Osoby;

namespace projekt.Druz
{
    class MeczSiatkowki : Mecz
    {
        protected Sedzia pomocniczy1;
        protected Sedzia pomocniczy2;
        public MeczSiatkowki(Druzyna druzyna, Druzyna druzynaPrzeciwna, Sedzia s1, Sedzia s2, Sedzia s3) : base(druzyna, druzynaPrzeciwna, s1)
        {
            pomocniczy1 = s2;
            pomocniczy2 = s3;
        }
        public string ZwrocDaneSedziowPom()
        {
            return $"-{pomocniczy1.imie} {pomocniczy1.nazwisko} \n-{pomocniczy2.imie} {pomocniczy2.nazwisko} ";
        }
    }
}