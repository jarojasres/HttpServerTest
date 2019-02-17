using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HttpServer
{
    public class Response
    {
        private byte[] Data = null;
        private string Status;
        private string MimeType;

        private Response(string status, string mimeType, byte[] data)
        {
            Data = data;
            Status = status;
            MimeType = mimeType;

        }

        public static Response From(Request request, string message)
        {
            return Ok(message);

            //var defaultFiles = new List<string> {
            // "default.html",
            // "default.htm",
            // "index.htm",
            // "index.html"
            //};

            //if(request == null)
            //{
            //    return NullResponse();
            //}

            //if(request.Type == "GET")
            //{
            //    var file = Environment.CurrentDirectory + Server.WEB_DIR + request.Url;
            //    var fileInfo = new FileInfo(file);
            //    if (fileInfo.Exists &&  fileInfo.Extension.Contains("."))
            //    {
            //        return Ok(fileInfo);
            //    }
            //    else
            //    {
            //        var directoryInfo = new DirectoryInfo(fileInfo + "/");
            //        var files = directoryInfo.GetFiles();

            //        foreach(var ff in files)
            //        {
            //            var name = ff.Name;
            //            if (defaultFiles.Contains(name))
            //            {
            //                fileInfo = ff;
            //                return Ok(ff);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    return MethodNotAllowedResponse();
            //}

            //return NotFoundReponse();
        }

        private static Response Ok(string request)
        {
            var message = "--------Petición------ \r\n\r\n";

            message += request;
            message += "------------------Respuesta------------------\r\n\r\n OK!";


            //var file = Environment.CurrentDirectory + Server.MSG_DIR + "200.html";
            //var fileInfo = new FileInfo(file);
            //var fileStream = fileInfo.OpenRead();
            //var reader = new BinaryReader(fileStream);
            //var bytes = new byte[fileStream.Length];
            //reader.Read(bytes, 0, bytes.Length);
            //fileStream.Close();

            var bytes = Encoding.ASCII.GetBytes(message);

            return new Response("200 OK", "text/plain", bytes);
        }

        private static Response NotFoundReponse()
        {
            var file = Environment.CurrentDirectory + Server.MSG_DIR + "404.html";
            var fileInfo = new FileInfo(file);
            var fileStream = fileInfo.OpenRead();
            var reader = new BinaryReader(fileStream);
            var bytes = new byte[fileStream.Length];
            reader.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            return new Response("404 Not Found", "text/html", bytes);
        }

        private static Response NullResponse()
        {
            var file = Environment.CurrentDirectory + Server.MSG_DIR + "400.html";
            var fileInfo = new FileInfo(file);
            var fileStream = fileInfo.OpenRead();
            var reader = new BinaryReader(fileStream);
            var bytes = new byte[fileStream.Length];
            reader.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            return new Response("400 Bad Request", "text/html", bytes);
        }

        private static Response MethodNotAllowedResponse()
        {
            var file = Environment.CurrentDirectory + Server.MSG_DIR + "405.html";
            var fileInfo = new FileInfo(file);
            var fileStream = fileInfo.OpenRead();
            var reader = new BinaryReader(fileStream);
            var bytes = new byte[fileStream.Length];
            reader.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            return new Response("405 Method Not Allowed", "text/html", bytes);
        }

        public void Post(NetworkStream stream)
        {
            var writer = new StreamWriter(stream);
            
            writer.WriteLine($"{Server.VERSION} {Status}\r\nServer: {Server.NAME}\r\nContent-Type: {MimeType}\r\nAccept-Ranges: bytes\r\nContent-Length: {Data.Length}\r\n");
            writer.Flush();
            stream.Write(Data, 0, Data.Length);
            stream.Close();
        }
    }
}
