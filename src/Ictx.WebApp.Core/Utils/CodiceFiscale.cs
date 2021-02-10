using System;

namespace Ictx.WebApp.Core.Utils
{
    public static class CodiceFiscale
    {

        public static string Calcola( string Nome, string Cognome, DateTime DataDiNascita, string Sesso, string CodiceISTAT)
        {

            string Mesi = "ABCDEHLMPRST",
                 
                   CodiceFiscale = "";


            Cognome = Cognome.ToUpper();
            Nome = Nome.ToUpper();

            CodiceFiscale = CalcCognome( Cognome );
            CodiceFiscale += CalcNome( Nome );

            //DateTimeFormatInfo dtfi = new CultureInfo( "IT-it" ).DateTimeFormat;
            CodiceFiscale += DataDiNascita.Year.ToString().Substring( 2 ) + Mesi[DataDiNascita.Month - 1];

            if ( Sesso == "F" ) CodiceFiscale += ( DataDiNascita.Day + 40 ).ToString();
            else CodiceFiscale += DataDiNascita.Day.ToString( "00" );

            CodiceFiscale += CodiceISTAT;

            CodiceFiscale += CalcolaCIN(CodiceFiscale);

            return CodiceFiscale;

        }


        public static string CalcolaCIN(string CF)
        {
            int AppoNum = 0;
            int TempNum = 0;
            int I = 0;

            //Controllo caratteri pari 

            TempNum = 0;
            I = 1;
            do
            {
                // I DISPARI 
                AppoNum = "B1A0KKPPLLC2QQD3RRE4VVOOSSF5TTG6UUH7MMI8NNJ9WWZZYYXX".IndexOf(Mid(CF, I, 1)) + 1;
                TempNum = TempNum + (int)((AppoNum - 1) & 0x7ffe) / 2;
                I = I + 1;
                if (I > 15)
                    break; // TODO: might not be correct. Was : Exit Do


                // I PARI 
                AppoNum = "A0B1C2D3E4F5G6H7I8J9KKLLMMNNOOPPQQRRSSTTUUVVWWXXYYZZ".IndexOf(Mid(CF, I, 1)) + 1;
                TempNum = TempNum + (int)((AppoNum - 1) & 0x7ffe) / 2;
                I = I + 1;
            } while (true);
            TempNum = TempNum % 26;
            return Mid("ABCDEFGHIJKLMNOPQRSTUVWXYZ", TempNum + 1, 1);

        }
        private static string Mid(string str, int start, int lenght)
        {
            if (string.IsNullOrEmpty(str) || start < 1 || lenght < 1)
                return string.Empty;
            else
                return str.Substring(start - 1, lenght);
        }

        private static string CalcCognome( string Cognome )
        {
            string treLettere = String.Empty,
                   Vocali = "AEIOU",
                   Consonanti = "BCDFGHJKLMNPQRSTVWXYZ";
            Int32 i = 0;

            Cognome = StripAccentate( Cognome );

            while ( treLettere.Length < 3 && i < Cognome.Length )
            {

                for ( int j = 0; j < Consonanti.Length; j++ )
                    if ( Cognome[i] == Consonanti[j] ) treLettere += Cognome[i];

                i++;

            }
            i = 0;

            while ( treLettere.Length < 3 && i < Cognome.Length )
            {
                for ( int j = 0; j < Vocali.Length; j++ )
                    if ( Cognome[i] == Vocali[j] ) treLettere += Cognome[i];

                i++;
            }

            if ( treLettere.Length < 3 )
                for ( int j = treLettere.Length; j < 2; j-- )
                    treLettere += "X";

            return treLettere;

        }

        private static string CalcNome( string Nome )
        {

            string Vocali = "AEIOU",
                   Consonanti = "BCDFGHJKLMNPQRSTVWXYZ",
                   treLettere = String.Empty,
                   cons = String.Empty;

            int i = 0;

            Nome = StripAccentate( Nome );

            while ( cons.Length < 4 && i < Nome.Length )
            {
                for ( int j = 0; j < Consonanti.Length; j++ )
                    if ( Nome[i] == Consonanti[j] ) cons += Nome[i];

                i++;
            }

            if ( cons.Length > 3 ) treLettere = cons[0].ToString() + cons[2].ToString() + cons[3].ToString();
            else treLettere = cons;
            i = 0;

            while ( treLettere.Length < 3 && i < Nome.Length )
            {
                for ( int j = 0; j < Vocali.Length; j++ )
                    if ( Nome[i] == Vocali[j] ) treLettere += Nome[i];

                i++;
            }

            if ( treLettere.Length < 3 )
                for ( int j = treLettere.Length; j < 2; j-- )
                    treLettere += "X";

            return treLettere;

        }

        private static string StripAccentate( string s )
        {
            string Accentate = "ÀÈÉÌÒÙàèéìòù",
                   NonAccentate = "AEEIOUAEEIOU",
                   tmp = "";

            for ( int i = 0; i < s.Length; i++ )
            {
                bool accentata = false;
                int tmpI = 0;

                for ( int j = 0; j < Accentate.Length;j++)
                    if ( s[i] == Accentate[j] )
                    {
                        accentata = true;
                        tmpI = j;
                    }
                if ( accentata ) tmp += NonAccentate[tmpI];
                else tmp += s[i];
            }

            return tmp;

        }

    }
}