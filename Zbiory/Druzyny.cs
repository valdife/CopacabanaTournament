using projekt.Druz;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using static System.Console;

namespace projekt.Zbiory
{
    public enum Dyscypliny
    {
        Siatkowka = 1,
        DwaOgnie = 2,
        Przeciaganie = 3
    }

    public class Druzyny
    {
        [XmlIgnore]
        public Dyscypliny dyscyplina;
        public List<Druzyna> druzyny = new List<Druzyna>();

        public Druzyny() { }

        public Druzyny(int dyscyplina)
        {
            this.dyscyplina = (Dyscypliny)dyscyplina;
        }

        public Druzyny(int dyscyplina, List<Druzyna> druz)
        {
            this.dyscyplina = (Dyscypliny)dyscyplina;
            druzyny = druz;
        }

        public void DodajDruzyne(string NazwaDruzyny)
        {
            druzyny.Add(new Druzyna(NazwaDruzyny));
        }
        
        public void DodajDruzyne(string Nazwa, int Dyscy)
        {
            druzyny.Add(new Druzyna(Nazwa, Dyscy));
        }

        public void DodajDruzyne(Druzyna druzyna)
        {
            druzyny.Add(druzyna);
        }

        public bool IstniejeDruzyna(string NazwaDruzyny)
        {
            return druzyny.Exists(x => x.NazwaDruzyny.Equals(NazwaDruzyny));
        }
        public Druzyna ZnajdzDruzyne(string NazwaDruzyny)
        {
            return druzyny.Find(x => x.NazwaDruzyny.Equals(NazwaDruzyny));
        }

        public void UsunDruzyne(string NazwaDruzyny)
		{
            druzyny.Remove(ZnajdzDruzyne(NazwaDruzyny));
		}
		public void Pokaz()
		{
            Write("Lista drużyn: \n");
            druzyny.ForEach(x => Write("- " + x.NazwaDruzyny + "\n"));
		}
        public bool CzyPusty()
        {
            return druzyny.Count() == 0;
        }
        public int ZwrocDyscypline()
        {
            return (int)dyscyplina;
        }

        public int Zlicz()
        {
            return druzyny.Count;
        }

        public Druzyny ZwrocFinalistow()
        {
            return new Druzyny((int)dyscyplina, druzyny.OrderByDescending(d => d.Punkty).Take(4).ToList());
        }

        public void Serializuj(string rodzaj)
        {
            var serializer = new XmlSerializer(druzyny.GetType());
            using var writer = XmlWriter.Create(rodzaj, new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = false,
                OmitXmlDeclaration = true
            });
            serializer.Serialize(writer, druzyny);
        }
        public void Deserializuj(string rodzaj)
        {
            var serializer = new XmlSerializer(druzyny.GetType());
            try
            {
                using (var reader = XmlReader.Create(rodzaj))
                {
                    druzyny = (List<Druzyna>)serializer.Deserialize(reader);
                }
                WriteLine("Pomyslnie wczytano plik " + rodzaj);
            }
            catch (FileNotFoundException)
            {
                WriteLine("Plik " + rodzaj + " nie istnieje");
            }
        }
    }
}

