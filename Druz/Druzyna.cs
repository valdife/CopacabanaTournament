using System.Collections.Generic;
using System.Xml.Serialization;
using projekt.Osoby;

namespace projekt.Druz
{

    public enum Dyscypliny
	{
		Siatkowka = 1,
		DwaOgnie = 2,
		Przeciaganie = 3
	}

	public class Druzyna
	{
		
		private string nazwaDruzyny;
		private int punkty;
		public Dyscypliny dyscyplina;
		[XmlIgnore]
		private readonly List<Zawodnik> zawodnicy;



        public int Punkty { get => punkty; set => punkty = value; }
		public string NazwaDruzyny { get => nazwaDruzyny; set => nazwaDruzyny = value; }

		public Druzyna () 
		{
			zawodnicy = new List<Zawodnik>();
		}

		public Druzyna(string nazwa_druzyny)
		{
			nazwaDruzyny = nazwa_druzyny;
			zawodnicy = new List<Zawodnik>();
			punkty = 0;
		}

		public Druzyna(string nazwa_druzyny, int dyscy)
        {
			nazwaDruzyny = nazwa_druzyny;
			zawodnicy = new List<Zawodnik>();
			punkty = 0;
			dyscyplina = (Dyscypliny)dyscy;
		}
		
		public bool IstniejeZawodnik(string imie, string nazwisko)
        {
			return zawodnicy.Exists(x => x.imie == imie && x.nazwisko == nazwisko);
		}
		public void DodajZawodnika(string imie, string nazwisko)
		{
			Zawodnik zawodnik = new Zawodnik(imie, nazwisko);
			zawodnicy.Add(zawodnik);
		}

		public void UsunZawodnika(string imie, string nazwisko)
		{
			zawodnicy.Remove(zawodnicy.Find(x => x.imie == imie && x.nazwisko == nazwisko));
		}

        public void DodajPunkt() => punkty++;

		public void ZresetujPunkty() => punkty = 0;

        public override string ToString()
		{
			string combine = "";
			foreach (Zawodnik zawodnik in zawodnicy)
            {
				combine += "-" + zawodnik + "\n";
            }
			return nazwaDruzyny + "\n" + combine;
		}

	}
}
