using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace Nebuchenazar
{
    class Program
    {
        enum Codes
        {
            NonExistant = 12,
            Ended = 821,
            Ok = 871
        }

        static void Main(string[] args)
        {
            var validKeywords = new List<string>();
            using (var reader = new StreamReader("..\\..\\..\\dictionary.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Thread.Sleep(100);
                    var url = "http://www.heyyourcity.com/c/" + line;

                    var request = WebRequest.Create(url);
                    request.Method = "GET";

                    try
                    {
                        var response = request.GetResponse();
                        var responseStream = response.GetResponseStream();
                        var responseReader = new StreamReader(responseStream);

                        var content = responseReader.ReadToEnd();
                        responseReader.Close();
                        response.Close();

                        if (content.Contains("<title>Age Gate</title>"))
                        {
                            Console.WriteLine($"Valid Keyword found: {line}");
                            validKeywords.Add(line);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
