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
            string msg1, msg2, cript1, cript2;
            //variavel de identificação do usuário
            int id;
            //array com as senhas cadastradas
            int[] senha = new int[2];


            //cadastro da chave do usuário
            Console.WriteLine("Cadastre a sua chave:");
            senha[0] = int.Parse(Console.ReadLine());
            //cadastro da chave do segundo usuário
            Console.WriteLine("Cadastre a chave a segunda chave:");
            senha[1] = int.Parse(Console.ReadLine());

            Console.Clear();

            //entrada da chave do usuário
            Console.WriteLine("Digite a sua chave:");
            id = int.Parse(Console.ReadLine());

            Console.Clear();

            //leitura do array
            foreach (char i in senha)
            {
               //comparação se a chave do usuário está cadastrada no array
               if(id == i)
                {
                    Console.WriteLine("Escreva o caralho:");
                    msg1 = Console.ReadLine();

                    //chamada da função de conversão para binário
                    cript1 = (StringToBinary(msg1));
                    Console.WriteLine(cript1);
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    //loop caso a chave digitada esteja incorreta
                    Console.WriteLine("Digite a sua chave:");
                    id = int.Parse(Console.ReadLine());
                }
            }
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

