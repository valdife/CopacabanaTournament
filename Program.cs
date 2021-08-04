using System.Collections.Generic;
using System.Linq;
using name.Buffor;
using projekt.Druz;
using projekt.Osoby;
using projekt.Zbiory;
using static System.Console;

namespace projekt
{
    public class Tournament
    {
        
        public static Druzyny druzynySiatkowki = new Druzyny(1);
        public static Druzyny druzynyDwaOgnie = new Druzyny(2);
        public static Druzyny druzynyPrzeciaganieLiny = new Druzyny(3);
        public static Druzyny[] dyscypliny = new Druzyny[] { druzynySiatkowki, druzynyDwaOgnie, druzynyPrzeciaganieLiny };
        public static Sedziowie sedziowie = new Sedziowie();
        public static Mecze eliminacje = new Mecze();

        public static Sedzia WybierzSedziego()
        {
            WriteLine(sedziowie + "\nWybierz sędziego:");
            string nazwaDoPodzialu;
            string[] name;
            while (true)
            {
                nazwaDoPodzialu = ReadLine();
                while (!nazwaDoPodzialu.Contains(' ') || nazwaDoPodzialu.EndsWith(' ') || nazwaDoPodzialu.StartsWith(' '))
                {
                    WriteLine("Podaj poprawne imie i nazwisko");
                    nazwaDoPodzialu = ReadLine();
                }
                name = nazwaDoPodzialu.Split(' ');
                if (sedziowie.IstniejeSedzia(name[0], name[1]))
                {
                    return sedziowie.ZnajdzSedziego(name[0], name[1]);
                }
                else
                {
                    WriteLine("Nie ma takiego sędziego, spróbuj ponownie.");
                }
            }
        }

        public static void StworzMecze(Druzyny zbiorDruzyn)
        {
            List<Druzyna> druzynyPo = new List<Druzyna>();
            foreach (Druzyna druzyna in zbiorDruzyn.druzyny)
            {
                foreach (Druzyna druzynaPrzeciwna in zbiorDruzyn.druzyny)
                {
                    if (druzyna == druzynaPrzeciwna || druzynyPo.Contains(druzynaPrzeciwna))
                    continue;
                    else
                    {
                        if (zbiorDruzyn.ZwrocDyscypline() == 1)
                        {
                            WriteLine("Kreator meczu pomiedzy siatkowki " + druzyna.NazwaDruzyny + " i " + druzynaPrzeciwna.NazwaDruzyny);
                            eliminacje.TworzPustyMeczSiatkowki(druzyna, druzynaPrzeciwna, WybierzSedziego(), WybierzSedziego(), WybierzSedziego());
                        }
                        else
                        {
                            WriteLine("Kreator meczu pomiedzy " + druzyna.NazwaDruzyny + " i " + druzynaPrzeciwna.NazwaDruzyny);
                            eliminacje.TworzPustyMecz(druzyna, druzynaPrzeciwna, WybierzSedziego());
                        }
                    }
                    Clear();
                }
                druzynyPo.Add(druzyna);

            }
        }
        public static void WypelnijMecze(Mecze mecze)
        {
            int wynik;
            foreach(Mecz mecz in mecze.PobierzMecze)
            {
                Write(mecz);
                WriteLine("\nSedziuje: " + mecz.ZwrocDaneSedziego());
                WriteLine("Kto wygral? (1 lub 2)");
                while (!(int.TryParse(ReadLine(), out wynik) && (wynik == 1 || wynik == 2)))
                    WriteLine("Wprowadz jedną z opcji: 1 lub 2");
                mecz.UstawWynik(wynik);
                Clear();
            }
        }
        public static void WypelnijMeczeSiatkowki(Mecze mecze)
        {
            int wynik;
            foreach (MeczSiatkowki mecz in mecze.PobierzMecze)
            {
                Write(mecz);
                WriteLine("\nSedziuje: \n-" + mecz.ZwrocDaneSedziego() +"\n"+ mecz.ZwrocDaneSedziowPom());
                WriteLine("Kto wygral? (1 lub 2)");
                while (!(int.TryParse(ReadLine(), out wynik) && (wynik == 1 || wynik == 2)))
                    WriteLine("Wprowadz jedną z opcji: 1 lub 2");
                mecz.UstawWynik(wynik);
                Clear();
            }
        }
        public static void PokazWyniki(Druzyny druzyny)
        {
            int i=1;
            WriteLine("Tabela drużyn: ");
            foreach(Druzyna druzyna in druzyny.druzyny.OrderByDescending(d => d.Punkty))
            {
                WriteLine(i + ". " + druzyna.NazwaDruzyny + " - " + druzyna.Punkty);
                i++;
            }
            ReadLine();
        }

        public static void RozegrajFinal(Druzyny finalisci)
        {
            static string tekst(Druzyna druz1, Druzyna druz2)
            {
                return $"Kreator meczu pomiedzy {druz1.NazwaDruzyny} i {druz2.NazwaDruzyny}";
            }
            List<Druzyna> czempion = new List<Druzyna>();
            Mecze finaly = new Mecze();
            finalisci.druzyny.ForEach(x => x.ZresetujPunkty());
            czempion = finalisci.druzyny;
            if (finalisci.ZwrocDyscypline() == 1)
            {
                WriteLine(tekst(czempion[0], czempion[1]));
                finaly.TworzPustyMeczSiatkowki(czempion[0], czempion[1], WybierzSedziego(), WybierzSedziego(), WybierzSedziego());
                Clear();
                WriteLine(tekst(czempion[2], czempion[3]));
                finaly.TworzPustyMeczSiatkowki(czempion[2], czempion[3], WybierzSedziego(), WybierzSedziego(), WybierzSedziego());
                Clear();
                WypelnijMeczeSiatkowki(finaly);
            }
            else
            {
                WriteLine(tekst(czempion[0], czempion[1]));
                finaly.TworzPustyMecz(czempion[0], czempion[1], WybierzSedziego());
                Clear();
                WriteLine(tekst(czempion[2], czempion[3]));
                finaly.TworzPustyMecz(czempion[2], czempion[3], WybierzSedziego());
                Clear();
                WypelnijMecze(finaly);
            }
            czempion.RemoveAll(x => x.Punkty < 1);
            finaly.UsunWszystkieMecze();
            WriteLine(tekst(czempion[0], czempion[1]));
            if (finalisci.ZwrocDyscypline() == 1)
            {
                finaly.TworzPustyMeczSiatkowki(czempion[0], czempion[1], WybierzSedziego(), WybierzSedziego(), WybierzSedziego());
                Clear();
                WypelnijMeczeSiatkowki(finaly);
            }
            else
            {
                finaly.TworzPustyMecz(czempion[0], czempion[1], WybierzSedziego());
                Clear();
                WypelnijMecze(finaly);
            }
            czempion.RemoveAll(x => x.Punkty < 2);
            WriteLine($"Zespół, który zajął pierwsze miejsce:\n{czempion.First()}");

        }

        public static int Menu()
        {
            Buffor.Deserializuj();
            foreach (Sedzia sedzia in Buffor.sedziowie.sedziowie)
            {
                sedziowie.WczytajSedziego(sedzia);
            }
            foreach (Druzyna druzyna in Buffor.druzyny.druzyny)
            {
                switch (druzyna.dyscyplina)
                {
                    case Druz.Dyscypliny.Siatkowka:
                        druzynySiatkowki.DodajDruzyne(druzyna);
                        break;
                    case Druz.Dyscypliny.DwaOgnie:
                        druzynyDwaOgnie.DodajDruzyne(druzyna);
                        break;
                    case Druz.Dyscypliny.Przeciaganie:
                        druzynyPrzeciaganieLiny.DodajDruzyne(druzyna);
                        break;
                    default:
                        break;
                }
            }
            bool end = false;
            int opcja;
            while (!end)
            {
                Clear();
                WriteLine("Menu");
                WriteLine("1. Rozpocznij turniej");
                WriteLine("2. Zespoły");
                WriteLine("3. Sędziowie");
                WriteLine("9. Wyjdz");
                while (!int.TryParse(ReadLine(), out opcja))
                    WriteLine("Wprowadz jedną z opcji: 1, 2, 3 lub 9");
                switch (opcja)
                {
                    case 1:
                        eliminacje.UsunWszystkieMecze();
                        druzynySiatkowki.druzyny.ForEach(x => x.ZresetujPunkty());
                        Clear();
                        WriteLine("0. Kontunuuj wczesniejszy turniej");
                        WriteLine("1. Turniej siatkówki");
                        WriteLine("2. Turniej w dwa ognie");
                        WriteLine("3. Turniej przeciągania liny");
                        WriteLine("4. Powrót");
                        while (!int.TryParse(ReadLine(), out opcja))
                            WriteLine("Wprowadz jedną z opcji: 1, 2, 3 lub 9");
                        Clear();
                        switch (opcja)
                        {
                            case 0:
                                if (sedziowie.ZliczSedziowie() > 2)
                                {
                                    Buffor.DeserializujFinalistow();
                                    if (Buffor.finalisci.CzyPusty())
                                    {
                                        Write("Brak zapisanych postepow na dysku!\nNacisnij dowolny przycisk aby kontynuowac...");
                                        ReadLine();
                                        break;
                                    }
                                    WriteLine("Druzyny w finale:");
                                    Buffor.finalisci.Pokaz();
                                    Druzyna test = Buffor.finalisci.druzyny.First();
                                    Buffor.finalisci.dyscyplina = (Zbiory.Dyscypliny)test.dyscyplina;
                                    Write("Nacisnij dowolny przycisk aby kontynuowac...");
                                    ReadLine();
                                    Clear();
                                    RozegrajFinal(Buffor.finalisci);
                                }
                                else
                                {
                                    WriteLine("Za mało drużyn i/lub sędziów!");
                                    ReadKey();
                                }
                                break;
                            case 1:                   
                                if (druzynySiatkowki.Zlicz() >= 4 && sedziowie.ZliczSedziowie() > 2)
                                {
                                    StworzMecze(druzynySiatkowki);
                                    WypelnijMeczeSiatkowki(eliminacje);
                                    PokazWyniki(druzynySiatkowki);
                                    Buffor.finalisci = druzynySiatkowki.ZwrocFinalistow();
                                    Buffor.SerializujFinalistow();
                                    RozegrajFinal(druzynySiatkowki.ZwrocFinalistow());
                                }
                                else
                                {
                                    WriteLine("Za mało drużyn i/lub sędziów!");
                                    ReadKey();
                                }
                                break;
                            case 2:
                                if (druzynyDwaOgnie.Zlicz() >= 4 && sedziowie.ZliczSedziowie() > 0)
                                {
                                    StworzMecze(druzynyDwaOgnie);
                                    WypelnijMecze(eliminacje);
                                    PokazWyniki(druzynyDwaOgnie);
                                    Buffor.finalisci = druzynyDwaOgnie.ZwrocFinalistow();
                                    Buffor.SerializujFinalistow();
                                    RozegrajFinal(druzynyDwaOgnie.ZwrocFinalistow());
                                }
                                else
                                {
                                    WriteLine("Za mało drużyn i/lub sędziów!");
                                    ReadKey();
                                }
                                break;
                            case 3:
                                if (druzynyPrzeciaganieLiny.Zlicz() > 4 && sedziowie.ZliczSedziowie() > 0)
                                {
                                    StworzMecze(druzynyPrzeciaganieLiny);
                                    WypelnijMecze(eliminacje);
                                    PokazWyniki(druzynyPrzeciaganieLiny);
                                    Buffor.finalisci = druzynyPrzeciaganieLiny.ZwrocFinalistow();
                                    Buffor.SerializujFinalistow();
                                    RozegrajFinal(druzynyPrzeciaganieLiny.ZwrocFinalistow());
                                }
                                else
                                {
                                    WriteLine("Za mało drużyn i/lub sędziów!");
                                    ReadKey();
                                }
                                break;
                            default:
                                break; 
                        }
                        ReadKey();
                        break;
                    case 2:
                        Druzyny wybranaDyscyplina;
                        while (true) 
                        { 
                            Clear();
                            WriteLine("1.Siatkówka");
                            WriteLine("2.Dwa ognie");
                            WriteLine("3.Przeciąganie liny");
                            WriteLine("4.Powrót");
                            int dyscy;
                            while (!int.TryParse(ReadLine(), out dyscy))
                                WriteLine("Wprowadź jedną z opcji: 1, 2, 3 lub 4");
                            if (dyscy >= 4)
                                break;

                            wybranaDyscyplina = dyscypliny[dyscy - 1];
                            string nazwa;
                            Clear();
                            WriteLine("1. Dodaj zespół");
                            WriteLine("2. Usuń zespół");
                            WriteLine("3. Pokaż zespoły");
                            WriteLine("4. Edytuj skład drużyny");
                            WriteLine("5. Powrot");
                            while (!int.TryParse(ReadLine(), out opcja))
                                WriteLine("Wprowadź jedną z opcji: 1, 2, 3 lub 4");
                            switch (opcja)
                            {
                                case 1:
                                    Clear();
                                    WriteLine("Wpisz nazwę drużyny: ");
                                    nazwa = ReadLine();
                                    if (wybranaDyscyplina.IstniejeDruzyna(nazwa))
                                    {
                                        Clear();
                                        WriteLine("Druzyna o tej nazwie juz istnieje!\nWciśnij dowolny klawisz, aby kontynuować...");
                                        ReadLine();
                                    }
                                    else
                                        wybranaDyscyplina.DodajDruzyne(nazwa);
                                        Buffor.druzyny.DodajDruzyne(nazwa, dyscy);
                                    break;
                                case 2:
                                    Clear();
                                    wybranaDyscyplina.Pokaz();
                                    WriteLine("Wpisz nazwę drużyny do usunięcia: ");
                                    nazwa = ReadLine();
                                    if (!wybranaDyscyplina.IstniejeDruzyna(nazwa))
                                    {
                                        Clear();
                                        WriteLine("Druzyna o tej nazwie nie istnieje!\nWciśnij dowolny klawisz, aby kontynuować...");
                                        ReadLine();
                                    }
                                    else
                                        wybranaDyscyplina.UsunDruzyne(nazwa);
                                        Buffor.druzyny.UsunDruzyne(nazwa);
                                    break;
                                case 3:
                                    Clear();
                                    wybranaDyscyplina.Pokaz();
                                    ReadLine();
                                    break;
                                case 4:
                                    Clear();
                                    wybranaDyscyplina.Pokaz();
                                    WriteLine("Wpisz nazwe drużyny: ");
                                    nazwa = ReadLine();
                                    if (!wybranaDyscyplina.IstniejeDruzyna(nazwa))
                                    {
                                        Clear();
                                        WriteLine("Druzyna o tej nazwie nie istnieje!\nWciśnij dowolny klawisz, aby kontynuować...");
                                        ReadLine();
                                        break;
                                    }
                                    Druzyna wybranaDruzyna = wybranaDyscyplina.ZnajdzDruzyne(nazwa);
                                    Clear();
                                    WriteLine("1. Dodaj zawodnika");
                                    WriteLine("2. Usuń zawodnika");
                                    WriteLine("3. Pokaż zawodników");
                                    switch (int.Parse(ReadLine()))
                                    {
                                        case 1:
                                            Clear();
                                            WriteLine("Podaj pierwsze imię i nazwisko zawodnika: ");
                                            string nameToTrim = ReadLine();
                                            if (!nameToTrim.Contains(' ') || nameToTrim.EndsWith(' ') || nameToTrim.StartsWith(' '))
                                            {
                                                Clear();
                                                WriteLine("Podaj poprawne imie i nazwisko");
                                                ReadLine();
                                                break;
                                            }
                                            string[] name = nameToTrim.Split(' ');
                                            if (wybranaDruzyna.IstniejeZawodnik(name[0], name[1]))
                                            {
                                                Clear();
                                                WriteLine("Taki zawodnik juz jest w tej druzynie!");
                                                ReadLine();
                                                break;
                                            }
                                            wybranaDruzyna.DodajZawodnika(name[0], name[1]);
                                            break;
                                        case 2:
                                            Clear();
                                            WriteLine("Podaj pierwsze imię i nazwisko zawodnika do usunięcia: ");
                                            nameToTrim = ReadLine();
                                            if (!nameToTrim.Contains(' ') || nameToTrim.EndsWith(' ') || nameToTrim.StartsWith(' '))
                                            {
                                                Clear();
                                                WriteLine("Niepoprawniy format wprowadzonych danych.");
                                                ReadLine();
                                                break;
                                            }
                                            name = nameToTrim.Split(' ');
                                            if (!wybranaDruzyna.IstniejeZawodnik(name[0], name[1]))
                                            {
                                                Clear();
                                                WriteLine($"Zawodnik {name} nie jest w tej druzynie!");
                                                ReadLine();
                                                break;
                                            }
                                            wybranaDruzyna.UsunZawodnika(name[0], name[1]);
                                            break;
                                        case 3:
                                            Clear();
                                            Write(wybranaDruzyna);
                                            ReadLine();
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case 5:
                                default:
                                    break;
                            }
                        }
                        break;
                    case 3:
                        Clear();
                        WriteLine("1. Dodaj sędziego");
                        WriteLine("2. Usuń sędziego");
                        WriteLine("3. Pokaż sędziów");
                        WriteLine("4. Powrót");
                        switch (int.Parse(ReadLine()))
                        {
                            case 1:
                                Clear();
                                WriteLine("Podaj pierwsze imię i nazwisko sedziego: ");
                                string nameToTrim = ReadLine();
                                if (!nameToTrim.Contains(' ') || nameToTrim.EndsWith(' ') || nameToTrim.StartsWith(' '))
                                {
                                    Clear();
                                    WriteLine("Podaj poprawne imie i nazwisko");
                                    ReadLine();
                                    break;
                                }
                                string[] name = nameToTrim.Split(' ');
                                if (sedziowie.IstniejeSedzia(name[0], name[1]))
                                {
                                    Clear();
                                    WriteLine("Taki sedzia juz jest wpisany do bazy!");
                                    ReadLine();
                                    break;
                                }
                                sedziowie.DodajSedziego(name[0], name[1]);
                                Buffor.sedziowie.DodajSedziego(name[0], name[1]);
                                break;
                            case 2:
                                Clear();
                                Write(sedziowie);
                                WriteLine("Podaj pierwsze imię i nazwisko sedziego do usuniecia: ");
                                nameToTrim = ReadLine();
                                if (!nameToTrim.Contains(' ') || nameToTrim.EndsWith(' ') || nameToTrim.StartsWith(' '))
                                {
                                    Clear();
                                    WriteLine("Podaj poprawne imie i nazwisko");
                                    ReadLine();
                                    break;
                                }
                                name = nameToTrim.Split(' ');
                                if (!sedziowie.IstniejeSedzia(name[0], name[1]))
                                {
                                    Clear();
                                    WriteLine("Taki sedzia nie istnieje w bazie!");
                                    ReadLine();
                                    break;
                                }
                                sedziowie.UsunSedziego(name[0], name[1]);
                                Buffor.sedziowie.UsunSedziego(name[0], name[1]);
                                break;
                            case 3:
                                Clear();
                                Write(sedziowie);
                                ReadLine();
                                break;
                            case 4:
                            default:
                                break;
                        }
                        break;              
                    case 9:
                        end = true;
                        break;
                    default:
                        break;
                }
            }
            return 0;
        } 
    }
}