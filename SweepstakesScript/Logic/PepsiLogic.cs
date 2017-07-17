using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SweepstakesScript.Logic
{
    public class PepsiLogic
    {
        public PepsiLogic()
        {

        }

        public void Execute()
        {
            var AllEntries = new List<EntryData>();
            AllEntries.Add(new EntryData("Evan", "Currier", "emc2207@yahoo.com", "1993-07-22", "60642"));
            AllEntries.Add(new EntryData("Sarah", "Ellison", "sarahbella224@aol.com", "1992-12-23", "60613"));
            AllEntries.Add(new EntryData("Erica", "Loken", "erica.loken@yahoo.com", "1992-01-26", "60657"));
            AllEntries.Add(new EntryData("Chris", "Badolato", "chrisdbady@yahoo.com", "1993-01-14", "32903"));
            AllEntries.Add(new EntryData("Reid", "Olsen", "reidodorito@gmail.com", "1992-09-30", "60642"));

            var url1 = "https://www.pepsiwalmartsweepstakes.com/api/Sweepstakes/Validate/";
            var url2 = "https://www.pepsifiresweepstakes.com/api/Sweepstakes/Validate/";

            // Enter each email in the subscription list
            foreach (var entry in AllEntries)
            {
                var data = Encoding.ASCII.GetBytes("{\"birthDate\":\"" + entry.dob + "\",\"postalCode\":\"" + entry.postalCode + "\",\"email\":\"" + entry.email + "\"}");
                // Pepsi Walmart
                try
                {
                    var request = WebRequest.Create(url1);
                    request.Method = "POST";
                    request.ContentType = "application/json;charset=UTF-8";
                    request.ContentLength = data.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();
                    response.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Pepsi Fire
                try
                {
                    var request = WebRequest.Create(url2);
                    request.Method = "POST";
                    request.ContentType = "application/json;charset=UTF-8";
                    request.ContentLength = data.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();
                    response.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
