using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Nebuchenazar
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load dictionary data into an array
            string[] dictionary = File.ReadAllLines("C:\\Users\\Evan\\Desktop\\dictionary.txt");
            
            // Run loop, parsing the website plus a word from the array
            for(var i=0; i < dictionary.Length; i++)
            {
                if(i%10 == 0)
                {
                    Thread.Sleep(15000);
                }

                Thread.Sleep(5000);

                var url = "http://www.heyyourcity.com/c/" + dictionary[i];

                try
                {
                    WebRequest request = WebRequest.Create(url);
                    request.Method = "GET";
                    WebResponse response = request.GetResponse();

                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);

                    string content = reader.ReadToEnd();
                    reader.Close();
                    response.Close();

                    if(content.Equals("coming soon!"))
                    {
                        continue;
                    }

                    var endedMessage = content.ToLower().IndexOf("this sweepstakes has ended.");

                    if(endedMessage == -1)
                    {
                        Console.WriteLine(dictionary[i] + " - Found.\n");
                    }
                }
                catch(Exception ex)
                {
                    Console.Write("Exception occured\n");
                    continue;
                }
            }

            Console.ReadKey();
        }
    }
}
