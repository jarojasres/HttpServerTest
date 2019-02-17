using System;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando en el puerto 8081");
            var server = new Server(8081);
            server.Start();
            Console.ReadKey();
        }
    }
}
