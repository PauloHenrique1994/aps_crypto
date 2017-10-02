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
            string msgParaCrypto, msg2, cript1, cript2;

            //variavel de identificação do usuário
            string confUsuario, senhaDeCryptografia, confirmaSenha;

            //array com usuários cadastrados
            string[] usuarios = new string[2];

            //variáveis de controle
            char continua = 's';
            int i = 0;
            bool valida = false;


            //cadastro da senha para crytografar e descryptografar
            Console.WriteLine("Cadastre a chave para cryptografar e descryptografar:");
            senhaDeCryptografia = Console.ReadLine();

            //cadastro do id do usuario que podera ler a mensagem
            Console.WriteLine("Cadastre os usuários que poderão ler a mensagem");
            do
            {
                usuarios[i] = Console.ReadLine();

                i++;

                Console.WriteLine("Deseja cadastrar mais um usuário? s/n");
                continua = char.Parse(Console.ReadLine());
            } while (continua == 's');

            Console.Clear();

            //entrada da senha para cryptografar a mensagem
            Console.WriteLine("Digite a sua senha de cryptografia");
            do
            {
                confirmaSenha = Console.ReadLine();

                Console.Clear();

                Console.WriteLine("Senha incorreta digite novamente");
            } while (confirmaSenha != senhaDeCryptografia);

            //reset da confirmação de senha
            confirmaSenha = "";

            Console.Clear();

            //entrada da mensagem para ser cryptografada
            Console.WriteLine("Escreva o caralho:");
            msgParaCrypto = Console.ReadLine();

            //chamada da função de conversão para binário
            cript1 = (StringToBinary(msgParaCrypto));
            Console.WriteLine(cript1);
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.Clear();

            //entrada do usuário cadastrado de leitura
            do
            {
                Console.WriteLine("Digite um usuário cadastrado para efetuar a leitura da mensagem");
                confUsuario = Console.ReadLine();

                //verificação no array de usuarios se o usuário informado existe
                foreach (string c in usuarios)
                {
                    //caso exista
                    if (confUsuario == c)
                    {
                        //confirmação da senha para a leitura da mensagem
                        do
                        {
                            Console.WriteLine("Digite a senha para ler a mensagem");
                            confirmaSenha = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Senha incorreta digite novamente");
                        } while (confirmaSenha != senhaDeCryptografia);

                        Console.Clear();
                        //linha de teste para ver se as validações estão corretas ainda temos que descriptogar a mensagem
                        Console.WriteLine(msgParaCrypto);
                        valida = true;
                    }
                }

                if (valida == false)
                {
                    Console.WriteLine("Usuário não encontrado digite um usuário valido");
                }
            } while (valida == false);

            Console.ReadKey();
        }

        //função de conversão para binário
        public static string StringToBinary(string data)
        {
           
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return  sb.ToString();
        }
    }
}

