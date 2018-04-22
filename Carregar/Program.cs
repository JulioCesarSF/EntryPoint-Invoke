using System;
using System.Threading.Tasks;

namespace Carregar
{
    //programa exemplo para ser carregador pelo ExecuteExe
    class Program
    {
        public static void Main(string[] args)
        {
            Task.Factory.StartNew(() => Tarefa());

            Console.ReadLine();
        }
        private static void Tarefa()
        {
            int i = 0;
            while (true)
            {
                i++;
                Console.Write($"\r{i}%");
            }
        }
    }

}
