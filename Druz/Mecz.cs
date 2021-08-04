using projekt.Osoby;


namespace projekt.Druz
{
    public class Mecz
    {
        Druzyna druzyna1;
        Druzyna druzyna2;
        readonly Sedzia sedzia;

        public Mecz(Druzyna d1, Druzyna d2, Sedzia se)
        {
            Druzyna1 = d1;
            Druzyna2 = d2;
            sedzia = se;
        }
        public Druzyna Druzyna1 { get => druzyna1; set => druzyna1 = value; }
        public Druzyna Druzyna2 { get => druzyna2; set => druzyna2 = value; }

        public string ZwrocDaneSedziego()
        {
            return $"{sedzia.imie} {sedzia.nazwisko}";
        }

        public void UstawWynik(int wynik)
        {
            if (wynik == 1)
                Druzyna1.DodajPunkt();
            else if (wynik == 2)
                Druzyna2.DodajPunkt();    
        }

        public override string ToString()
        {
            return "Mecz pomiedzy " + this.Druzyna1.NazwaDruzyny + " (1), a " + this.Druzyna2.NazwaDruzyny + " (2)";
        }


    }
}