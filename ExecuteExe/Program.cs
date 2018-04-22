using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExecuteExe
{
    class Program
    {
        private static string _file = @"f:\meuExecutavel.arrayBytes";
        [STAThread]
        static void Main(string[] args)
        {
            //OpenFileExe();

            //testar os 2 abaixo juntos
            //ExecutableToArray();
            //EntryPointCallFromArray();

            Console.ReadKey();
        }
        #region Abrir executável de um array de bytes
        //salvar um executável em array de bytes
        public static void ExecutableToArray()
        {
            var openFile = new OpenFileDialog()
            {
                Filter = "Executable (*.exe)|*.exe",
                InitialDirectory = @"c:\"
            };
            var result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                var file = openFile.FileName;
                var arrayBytes = File.ReadAllBytes(file);
                //salvando o array de bytes para um arquivo
                File.WriteAllBytes(_file, arrayBytes);
                Console.WriteLine($"Salvou: {_file}");
            }
        }
        //executar o "array de bytes"
        public static void EntryPointCallFromArray()
        {
            try
            {
                Console.WriteLine($"Executar {_file}");
                //caregar o arquivo com o array de bytes
                var bytes = File.ReadAllBytes(_file);
                var assembly = Assembly.Load(bytes);
                //pegar o entryPoint
                var entryPoint = assembly.EntryPoint;
                //args
                var args = new string[] { _file };
                //executar, fazer a chama do entryPoint
                entryPoint.Invoke(assembly.CreateInstance(entryPoint.Name), new object[] { args });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion

        #region Abrir executável sem array de bytes
        //abrir um arquivo exe, precisa do STAThread pois é um componente de UI
        public static void OpenFileExe()
        {
            var openFile = new OpenFileDialog()
            {
                Filter = "Executable (*.exe)|*.exe",
                InitialDirectory = @"c:\"
            };
            var result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                EntryPointCall(openFile.FileName);
            }
        }
        //carregar um exe chamando seu entrypoint
        public static void EntryPointCall(string file)
        {
            if (string.IsNullOrEmpty(file))
                throw new ArgumentException("no file");

            try
            {
                var assembly = Assembly.LoadFile(file);
                var entryPoint = assembly.EntryPoint;
                var args = new string[] { file };
                entryPoint.Invoke(null, new object[] { args });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion
    }
}
