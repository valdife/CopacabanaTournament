using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using static System.Console;
using projekt.Osoby;

namespace projekt.Zbiory
{
	public class Sedziowie
	{
		public List<Sedzia> sedziowie = new List<Sedzia>();

		public Sedziowie() { }

		public void DodajSedziego(string imie, string nazwisko)
		{
			sedziowie.Add(new Sedzia(imie, nazwisko));
		}

		public void WczytajSedziego(Sedzia sedzia)
        {
			sedziowie.Add(sedzia);
        }

		public int ZliczSedziowie()
		{
			return sedziowie.Count();
		}
		public Sedzia ZnajdzSedziego(string imie, string nazwisko)
        {
			return sedziowie.Find(x => x.imie == imie && x.nazwisko == nazwisko);
		}

		public void UsunSedziego(string imie, string nazwisko)
		{
			sedziowie.Remove(ZnajdzSedziego(imie, nazwisko));
		}
		public bool IstniejeSedzia(string imie, string nazwisko)
		{
			return sedziowie.Exists(x => x.imie == imie && x.nazwisko == nazwisko);
		}
		public override string ToString()
		{
			string combine = "";
			foreach (Sedzia sedzia in sedziowie)
			{
				combine += "-" + sedzia + "\n";
			}
			return "Lista sedziow: \n" + combine;
		}

		public void Serializuj()
		{
			var serializer = new XmlSerializer(sedziowie.GetType());
			using var writer = XmlWriter.Create("sedziowie.xml", new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "\t",
				NewLineOnAttributes = false,
				OmitXmlDeclaration = true
			});
			serializer.Serialize(writer, sedziowie);
		}
		public void Deserializuj()
		{
			var serializer = new XmlSerializer(sedziowie.GetType());
			try
			{
				using (var reader = XmlReader.Create("sedziowie.xml"))
				{
					sedziowie = (List<Sedzia>)serializer.Deserialize(reader);
				}
				WriteLine("Pomyslnie wczytano plik sedziowie.xml");
			}
			catch (FileNotFoundException)
			{
				WriteLine("Plik sedziowie.xml nie istnieje");
			}
		}

	}
}
