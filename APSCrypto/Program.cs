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
            //variaveis das mensagens não criptografadas
            string msgParaCripto, criptografia = string.Empty, textoOriginal = string.Empty;

            //variavel de identificação do usuário
            string senhaDeCriptografia;

            //variáveis de controle
            int numeroPrimo, i = 0, x = 0, j = 0;
            bool valida = false;

            //cadastro da senha para critografar e descriptografar
            Console.WriteLine("Cadastre a chave para criptografar e descriptografar a mensagem:");
            senhaDeCriptografia = Console.ReadLine();

            Console.Clear();

            //entrada da senha para criptografar a mensagem
            Console.WriteLine("Digite a sua chave");
            //Chamada da função que confirma a senha
            ConfereSenha(senhaDeCriptografia);
            
            Console.Clear();

            //entrada da mensagem para ser criptografada
            Console.WriteLine("Digite a mensagem a ser criptografada:");
            msgParaCripto = Console.ReadLine();

            //==================================================Começo da criptografia===========================================
            numeroPrimo = msgParaCripto.Length + senhaDeCriptografia.Length;
            int[,,] ascii = new int[msgParaCripto.Length, 4, 2];

            //escolher o proximo número primo referente ao tamanho do texto digitado pelo usuario
            do
            {
                if (numeroPrimo > 2)
                {
                    if (numeroPrimo == 3)
                    {
                        valida = true;
                    }
                    else if (numeroPrimo == 4 || numeroPrimo == 5)
                    {
                        numeroPrimo = 7;
                        valida = true;
                    }
                    else
                    {
                        for (i = 2; i < msgParaCripto.Length / 2; i++)
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
                }
                else
                {
                    numeroPrimo = 2;
                    valida = true;
                }
            } while (!valida);

            // tranforma a string em código ascii
            for (i = 0; i < msgParaCripto.Length; i++)
            {
                ascii[i, 0, 0] = (int)msgParaCripto[i];
                //multiplicação do código ascii pelo número primo
                ascii[i, 0, 0] *= numeroPrimo;
            }

            //separação do código ascii multiplicado em 2 caracteres (unid, dezena, centena, milhar, dezena de milhar)
            int dezmilhar, milhar, cent, dez, unid;

            //passar por toda 1° dimensão do array com os decimais do texto original
            for (i = 0; i < msgParaCripto.Length; i++)
            {
                j = 0;
                //passar por toda 2° dimensão do array com os decimais do texto original e separar de 2 em 2 caracteres
                for (x = 1; x < 4; x++)
                {
                    //iniciação do contador utilizado mais a frente
                    ascii[i, x, 1] = 0;
                    //caso o número seja maior que 10.000
                    if (ascii[i, 0, 0] >= 10000)
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
                    else if (ascii[i, 0, 0] >= 1000)
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
                    else if (ascii[i, 0, 0] >= 100)
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

                        ascii[i, x, 0] = dez * 10 + unid;

                        if (j > 0)
                        {
                            ascii[i, x, 0] = 0;
                        }
                        j++;
                    }

                    /*caso o valor separado seja menor que 33 incrementar o número primo até ficar maior que 33, pois na tabela
                     *ascii os caracteres estão do número 34 pra cima
                    */
                    while (ascii[i, x, 0] < 33)
                    {
                        ascii[i, x, 0] += numeroPrimo;
                        ascii[i, x, 1]++;
                    }
                }
            }
            //fim da separação

            //criando a cryptografia
            for (i = 0; i < msgParaCripto.Length; i++)
            {
                for (x = 1; x < 4; x++)
                {
                    criptografia += ((char)ascii[i, x, 0]).ToString();
                }
            }
            Console.WriteLine("A sua mensagem criptografada é: " + criptografia);
            //================================Fim da criptografia================================================================

            Console.Write("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();

            //Entrada da senha para realizar a descriptografia
            int[] asciiDescripto = new int[criptografia.Length];
            int[] descripto = new int[msgParaCripto.Length];
            j = 0;

            //Verificação se a senha esta correta
            Console.WriteLine("Digite a sua chave para efetuar a leitura da mensagem");
            //Chamada da função que confirma a senha
            ConfereSenha(senhaDeCriptografia);

            Console.Clear();
            //QUANDO O USUÁRIO ACERTAR A SENHA

            //======================================Incio da descriptografia==========================================

            //Tranformação da criptografia em código ascii
            for (i = 0; i < criptografia.Length; i++)
            {
                asciiDescripto[i] = (int)criptografia[i];
            }

            //decrementado o número primo no código ascii que ficou abaixo de 33 para ele voltar ao seu valor original
            for (i = 0; i < msgParaCripto.Length; i++)
            {
                for (x = 1; x < 4; x++, j++)
                {
                    if (ascii[i, x, 1] != 0)
                    {
                        asciiDescripto[j] -= (ascii[i, x, 1] * numeroPrimo);
                    }
                }
            }

            /*
                * Com o código sem o incremento, agora irá fazer a junção do valores que foram divididos 
                * de 2 em 2 (unid, dez, cent, milhar, dezena de milhar).
            */
            for (i = 0, j = 0; i < msgParaCripto.Length; i++, j += 3)
            {
                descripto[i] = (asciiDescripto[j + 2] * 10000) + (asciiDescripto[j + 1] * 100) + asciiDescripto[j];

                //dividindo o código pelo número primo para voltar ao valor original
                descripto[i] /= numeroPrimo;

                //voltando o código para texto
                textoOriginal += ((char)descripto[i]).ToString();
            }

            Console.WriteLine("A mensagem é: " + textoOriginal);

            Console.ReadKey();
        }

        //Função para validar a senha digitada pelo usuário
        public static void ConfereSenha(string senha)
        {
            string confirmaSenha = String.Empty;

            do
            {
                confirmaSenha = Console.ReadLine();

                Console.Clear();

                Console.WriteLine("Senha incorreta digite novamente");
            } while (confirmaSenha != senha);
        }
    }       
}