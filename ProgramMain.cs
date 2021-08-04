using name.Buffor;
using static System.Console;

namespace projekt
{
    class ProgramMain
    {
        static void Main(string[] arg)
        {
            WriteLine("Czy chcesz wczytac dane zapisane na dysku?");
            WriteLine("1. Nie");
            WriteLine("2. Tak");
            WriteLine("3. Wyjdź");
            int opcja;
            while (!int.TryParse(ReadLine(), out opcja))
                WriteLine("Wprowadź jedną z opcji: 1, 2 lub 3");
            switch (opcja)
            {
                case 1:
                    Buffor.SerializujFinalistow();
                    Buffor.Serializuj();
                    Tournament.Menu();
                    Buffor.Serializuj();
                    break;
                case 2:
                    Tournament.Menu();
                    Buffor.Serializuj();
                    break;
                case 3:
                    break;
                default:
                    break;
            }

        }
    }
}