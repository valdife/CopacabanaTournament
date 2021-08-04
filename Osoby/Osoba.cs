namespace projekt.Osoby
{
    public class Osoba
	{
		public string imie;
		public string nazwisko;
	
		public Osoba() { }

		public Osoba(string imie, string nazwisko)
		{
			this.imie = imie;
			this.nazwisko = nazwisko;

		}
		public override string ToString()
		{
			return imie + " " + nazwisko;
		}
	}

}
