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
            string msg1, msg2;
            int id;
            long cript1, cript2;
            int[] senha = new int[2];

            Console.WriteLine("Cadastre a sua chave:");
            senha[0] = int.Parse(Console.ReadLine());

            Console.WriteLine("Cadastre a chave a segunda chave:");
            senha[1] = int.Parse(Console.ReadLine());

            Console.Clear();

            Console.WriteLine("Digite a sua chave:");
            id = int.Parse(Console.ReadLine());

            Console.Clear();

            foreach (char i in senha)
            {
               if(id == i)
                {
                    Console.WriteLine("Escreva o caralho:");
                    msg1 = Console.ReadLine();

                    Console.WriteLine(StringToBinary(msg1));
                }
               else
                {
                    Console.WriteLine("Digite a sua chave:");
                    id = int.Parse(Console.ReadLine());
                }
            }
            Console.ReadKey();
        }
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

