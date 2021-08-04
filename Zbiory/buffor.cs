using projekt.Zbiory;


namespace name.Buffor
{
    public static class Buffor
	{
		public static Druzyny druzyny = new Druzyny();
		public static Sedziowie sedziowie = new Sedziowie();
		public static Druzyny finalisci = new Druzyny();

		public static void Serializuj()
		{
			druzyny.Serializuj("druzyny.xml");
			sedziowie.Serializuj();
		}

		public static void Deserializuj()
		{
			druzyny.Deserializuj("druzyny.xml");
			sedziowie.Deserializuj();
		}

		public static void SerializujFinalistow()
		{
			finalisci.Serializuj("finalisci.xml");
		}

		public static void DeserializujFinalistow()
		{
			finalisci.Deserializuj("finalisci.xml");
		}
	}
}