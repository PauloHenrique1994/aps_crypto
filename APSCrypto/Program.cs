using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSCrypto
{
    class Program
    {
        static void Main(string[] args)
        {
            string teste;

            // exemplo de matriz multidimensional -----------> int[,] numeros = new int[5, 2];

            Console.WriteLine("Digite o teste");
            teste = Console.ReadLine();

            //Console.WriteLine(StringToBinary(teste));
            StringToAscii(teste);

            Console.ReadKey();
        }


        //converte para binario
        public static string StringToBinary(string data)
        {

            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }




        //converte para ascii
        public static void StringToAscii(string texto)
        {
            //números primos 2, 3, 5, 7, 11, 13
            //declaração das variáveis
            int resto, numeroPrimo, quociente;
            numeroPrimo = texto.Length;

            int[,] ascii = new int[texto.Length, 5];
            int[] primos = new int[6] {2, 3, 5, 7, 11, 13};

            bool valida = false;
            
            
            //escolher um número primo
            do
            {
                for (int i = 0; i < 6; i++)
                {
                    quociente = numeroPrimo / primos[i];
                    resto = numeroPrimo % primos[i];

                    if (resto != 0)
                    {
                        if (quociente <= primos[i])
                        {
                            valida = true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (valida == false)
                {
                    numeroPrimo += 1;
                }

            } while (valida == false);


            // tranforma a string em código ascii
            for (int i = 0; i < texto.Length; i++)
            {
                ascii[i, 0] = (int)texto[i];

                //multiplicação do código ascii
                ascii[i, 0] *= numeroPrimo;
                //Console.WriteLine("ASCII múltiplicado" + ascii[i, 0]);
            }


            //separação do código ascii multiplicado em 2 caracteres 
            int dezmilhar, milhar, cent, dez, unid;
            //passar por toda 1° dimensão do array com os decimais do texto original
            for (int i = 0; i < texto.Length; i++)
            {
                //passar por toda 2° dimensão do array com os decimais do texto original e separar de 2 em 2 caracteres
                for (int x = 1; x < 5; x++)
                {
                    //caso o número seja maior que 10.000
                    if (ascii[i, 0] > 10000)
                    {
                        dezmilhar = ascii[i, 0] / 10000;
                        milhar = ascii[i, 0] % 10000;

                        milhar /= 1000;
                        cent = ascii[i, 0] % 1000;

                        cent /= 100;
                        dez = ascii[i, 0] % 100;

                        dez /= 10;
                        unid = ascii[i, 0] % 10;
                        
                        if (x == 1)
                        {
                            ascii[i, x] = dez * 10 + unid;
                        }
                        else if (x == 2)
                        {
                            ascii[i, x] = milhar * 10 + cent;
                        }
                        else if(x == 1)
                        {
                            ascii[i, x] = dezmilhar * 10;
                        }
                        else
                        {
                            ascii[i, x] = 0;
                        }
                    }
                    //caso o número seja maior que 1.000
                    else if (ascii[i, 0] > 1000)
                    {
                        milhar = ascii[i, 0] / 1000;
                        cent = ascii[i, 0] % 1000;

                        cent /= 100;
                        dez = ascii[i, 0] % 100;

                        dez /= 10;
                        unid = ascii[i, 0] % 10;

                        if (x == 1)
                        {
                            ascii[i, x] = dez * 10 + unid;
                        }
                        else if (x == 2)
                        {
                            ascii[i, x] = milhar * 10 + cent;
                        }
                        else
                        {
                            ascii[i, x] = 0;
                        }
                    }
                    //caso o número seja maior que 100
                    else if (ascii[i, 0] > 100)
                    {
                        cent = ascii[i, 0] / 100;
                        dez = ascii[i, 0] % 100;

                        dez /= 10;
                        unid = ascii[i, 0] % 10;

                        if(x == 1)
                        {
                            ascii[i, x] = dez * 10 + unid;
                        }
                        else if (x == 2)
                        {
                            ascii[i, x] = cent * 10;
                        }
                        else
                        {
                            ascii[i, x] = 0;
                        }
                    }
                    //caso o número seja maior que 10
                    else
                    {
                        dez = ascii[i, 0] / 10;
                        unid = ascii[i, 0] % 10;
                        
                        if(ascii[i, x] != 1)
                        {
                            ascii[i, x] = 0;
                        }

                        ascii[i, x] = dez * 10 + unid;
                    }

                    while (ascii[i, x] < 33)
                    {
                        ascii[i, x] += numeroPrimo;
                    }

                    //Console.WriteLine("Separação " + ascii[i, x]);
                }
            }
            //fim da separação


            //criando a cryptografia
            string cryptografia = string.Empty;

            for (int i = 0; i < texto.Length; i++)
            {
                for (int x = 1; x < 5; x++)
                {
                    cryptografia += ((char)ascii[i, x]).ToString();
                }
            }
            Console.WriteLine("Cryptografia: " + cryptografia);


            /*
            // transforma de ascii em string
            string textoOriginal = string.Empty;
            char[] asciiStrCodes = new char[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                asciiStrCodes[i] = (char)x[i];
                textoOriginal += ((char)x[i]).ToString();
                //Imprime na tela uma letra de cada vez
                Console.WriteLine(asciiStrCodes[i]);
            }
            //imprime na tela a frase completa
            Console.WriteLine(textoOriginal);
            */
        }
    }
}

//https://social.msdn.microsoft.com/Forums/vstudio/pt-BR/dcaf24db-0dda-47cb-ba71-49e286c8021c/transformar-uma-string-em-nmeros-e-depois-novamente-em-string?forum=vscsharppt