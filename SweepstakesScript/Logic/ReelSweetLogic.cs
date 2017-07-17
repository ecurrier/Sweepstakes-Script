using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace SweepstakesScript.Logic
{
    public class ReelSweetLogic
    {
        public ReelSweetLogic()
        {

        }

        public void Execute()
        {
            var AllEntries = new List<EntryData>();
            AllEntries.Add(new EntryData("Evan", "Currier", "emc2207@yahoo.com", "1993-07-22"));
            AllEntries.Add(new EntryData("Evan", "Currier", "ecurri3@gmail.com", "1993-07-22"));
            AllEntries.Add(new EntryData("Sarah", "Ellison", "sarahbella224@aol.com", "1992-12-23"));
            AllEntries.Add(new EntryData("Erica", "Loken", "erica.loken@yahoo.com", "1992-01-26"));
            AllEntries.Add(new EntryData("Chris", "Badolato", "chrisdbady@yahoo.com", "1993-01-14"));
            AllEntries.Add(new EntryData("Reid", "Olsen", "reidodorito@gmail.com", "1992-09-30"));

            foreach (var entry in AllEntries)
            {
                var url = "http://www.reelsweetsweepsamoe.com/api.php";
                var data = Encoding.ASCII.GetBytes($"act=Register&formid=5cbf4a2d6e3b17bc1f301df9071671e1fa3b5e4315d978094a9081fed17053db&pge=143226&app=197&ssn=04OSA2067IIITL9WF408Z8NBWJZK4TT6&usr=04OSA206SF16EGLBR008T41SM3E04F86&regsrc=RMENTS&fname={entry.firstName}&lname={entry.lastName}&email={entry.email}&phone=&mphone=&address=&city=&state=&postal=&SetCountry=&SetLang=en&company=&account=&secq=&seca=&dob={entry.dob}&gender=&opt1=0&opt2=0&Flex0=&Flex1=&Flex2=&Flex3=&Flex4=&Flex5=&Flex6=&Flex7=&Flex8=&Flex9=");

                var request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if ((int)response.StatusCode == 200)
                {
                    Console.WriteLine($"{entry.email} Successfully entered into ReelSweet Sweepstakes.");
                }

                response.Close();
                Thread.Sleep(5000);
            }
        }
    }
}
