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

            Console.WriteLine("Digite o teste");
            teste = Console.ReadLine();

            StringToAscii(teste);

            Console.ReadKey();
        }



        //converte para ascii
        public static void StringToAscii(string texto)
        {
            //números primos 2, 3, 5, 7, 11, 13
            //declaração das variáveis
            int numeroPrimo, i, x, j = 0;
            numeroPrimo = texto.Length;

            int[,,] ascii = new int[texto.Length, 4, 2];

            bool valida = false;


            //escolher o proximo número primo referente ao tamanho do texto digitado pelo usuario
            numeroPrimo = texto.Length;
            do
            {
                if (numeroPrimo > 2)
                {
                    for (i = 2; i < texto.Length / 2; i++)
                    {
                        if (numeroPrimo % i == 0)
                        {
                            numeroPrimo++;
                        }
                        else
                        {
                            valida = true;
                        }
                    }
                }
                else
                {
                    numeroPrimo = 2;
                    valida = true;
                }
            } while (!valida);


            // tranforma a string em código ascii
            for (i = 0; i < texto.Length; i++)
            {
                ascii[i, 0, 0] = (int)texto[i];

                //multiplicação do código ascii pelo número primo
                ascii[i, 0, 0] *= numeroPrimo;
            }

            //separação do código ascii multiplicado em 2 caracteres (unid, dezena, centena, milhar, dezena de milhar)
            int dezmilhar, milhar, cent, dez, unid;
            //passar por toda 1° dimensão do array com os decimais do texto original
            for (i = 0; i < texto.Length; i++)
            {
                //passar por toda 2° dimensão do array com os decimais do texto original e separar de 2 em 2 caracteres
                for (x = 1; x < 4; x++)
                {
                    //iniciação do contador utilizado mais a frente
                    ascii[i, x, 1] = 0;
                    //caso o número seja maior que 10.000
                    if (ascii[i, 0, 0] > 10000)
                    {
                        dezmilhar = ascii[i, 0, 0] / 10000;
                        milhar = ascii[i, 0, 0] % 10000;

                        milhar /= 1000;
                        cent = ascii[i, 0, 0] % 1000;

                        cent /= 100;
                        dez = ascii[i, 0, 0] % 100;

                        dez /= 10;
                        unid = ascii[i, 0, 0] % 10;

                        if (x == 1)
                        {
                            ascii[i, x, 0] = dez * 10 + unid;
                        }
                        else if (x == 2)
                        {
                            ascii[i, x, 0] = milhar * 10 + cent;
                        }
                        else if (x == 3)
                        {
                            ascii[i, x, 0] = dezmilhar;
                        }
                        else
                        {
                            ascii[i, x, 0] = 0;
                        }
                    }
                    //caso o número seja maior que 1.000
                    else if (ascii[i, 0, 0] > 1000)
                    {
                        milhar = ascii[i, 0, 0] / 1000;
                        cent = ascii[i, 0, 0] % 1000;

                        cent /= 100;
                        dez = ascii[i, 0, 0] % 100;

                        dez /= 10;
                        unid = ascii[i, 0, 0] % 10;

                        if (x == 1)
                        {
                            ascii[i, x, 0] = dez * 10 + unid;
                        }
                        else if (x == 2)
                        {
                            ascii[i, x, 0] = milhar * 10 + cent;
                        }
                        else
                        {
                            ascii[i, x, 0] = 0;
                        }
                    }
                    //caso o número seja maior que 100
                    else if (ascii[i, 0, 0] > 100)
                    {
                        cent = ascii[i, 0, 0] / 100;
                        dez = ascii[i, 0, 0] % 100;

                        dez /= 10;
                        unid = ascii[i, 0, 0] % 10;

                        if (x == 1)
                        {
                            ascii[i, x, 0] = dez * 10 + unid;
                        }
                        else if (x == 2)
                        {
                            ascii[i, x, 0] = cent;
                        }
                        else
                        {
                            ascii[i, x, 0] = 0;
                        }
                    }
                    //caso o número seja maior que 10
                    else
                    {
                        dez = ascii[i, 0, 0] / 10;
                        unid = ascii[i, 0, 0] % 10;

                        if (ascii[i, x, 0] != 1)
                        {
                            ascii[i, x, 0] = 0;
                        }
                        ascii[i, x, 0] = dez * 10 + unid;
                    }

                    //caso o valor separado seja menor que 33 incrementar o número primo até ficar maior que 33, pois na tabela
                    //ascii os caracteres estão do número 34 pra cima
                    while (ascii[i, x, 0] < 33)
                    {
                        ascii[i, x, 0] += numeroPrimo;
                        ascii[i, x, 1]++;

                    }
                }
            }
            //fim da separação


            //criando a cryptografia
            string cryptografia = string.Empty;

            for (i = 0; i < texto.Length; i++)
            {
                for (x = 1; x < 4; x++)
                {
                    cryptografia += ((char)ascii[i, x, 0]).ToString();
                }
            }
            Console.WriteLine("Cryptografia: " + cryptografia);
            //================================Fim da criptografia================================================================

            Console.WriteLine("Digite enter para Descrytografar");
            Console.ReadLine();

            //======================================Incio da descriptografia=====================================================
            int[] asciiDescripto = new int[cryptografia.Length];
            int[] descripto = new int[texto.Length];

            string textoOriginal = string.Empty;

            //Tranformação da criptografia em código ascii
            for (i = 0; i < cryptografia.Length; i++)
            {
                asciiDescripto[i] = (int)cryptografia[i];
            }

            //decrementado o número primo no código ascii que ficou abaixo de 33 para ele voltar ao seu valor original
            for (i = 0; i < texto.Length; i++)
            {
                for (x = 1; x < 4; x++)
                {
                    if (ascii[i, x, 1] != 0)
                    {
                        asciiDescripto[j] -= (ascii[i, x, 1] * numeroPrimo);
                    }
                    j++;
                }
            }

            j = 0;

            /*
             * Com o código sem o incremento, agora irá fazer a junção do valores que foram divididos de 2 em 2 (unid, dez, cent,
             * milhar, dezena de milhar).
            */
            for (i = 0; i < texto.Length; i++)
            {
                descripto[i] = (asciiDescripto[j + 2] * 10000) + (asciiDescripto[j + 1] * 100) + asciiDescripto[j];

                //dividindo o código pelo número primo para voltar ao valor original
                descripto[i] /= numeroPrimo;

                //voltando o código para texto
                textoOriginal += ((char)descripto[i]).ToString();
                j += 3;
            }

            Console.WriteLine("Texto original " + textoOriginal);
        }
    }
}

//https://social.msdn.microsoft.com/Forums/vstudio/pt-BR/dcaf24db-0dda-47cb-ba71-49e286c8021c/transformar-uma-string-em-nmeros-e-depois-novamente-em-string?forum=vscsharppt