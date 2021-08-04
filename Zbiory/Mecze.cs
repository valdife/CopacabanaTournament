using System.Collections.Generic;
using projekt.Druz;
using projekt.Osoby;

namespace projekt.Zbiory
{
	public class Mecze
	{
		private readonly List<Mecz> mecze = new List<Mecz>();
		public Mecze() { }

		public List<Mecz> PobierzMecze { get => mecze; }
		public void TworzPustyMecz(Druzyna d1, Druzyna d2, Sedzia sedzia)
        {				
			mecze.Add(new Mecz(d1, d2, sedzia));
        }

		public void TworzPustyMeczSiatkowki(Druzyna d1, Druzyna d2, Sedzia s1, Sedzia s2, Sedzia s3)
        {
			mecze.Add(new MeczSiatkowki(d1, d2, s1, s2, s3));
        }

		public void UsunWszystkieMecze()
        {
			mecze.Clear();
        }

		public override string ToString()
		{
			string combine = "";
			foreach (Mecz mecz in mecze)
			{
				combine += "-" + mecz + "\n";
			}
			return "Lista meczy: \n" + combine;
		}

    }
}
