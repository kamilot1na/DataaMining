using System;
using ExcelDataReader;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace DataminingTestt2
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = new StreamReader("C:/DataMining/Second/transactions.csv");

            var list = new List<List<string>>();
            StreamWriter stream = new StreamWriter("C:/DataMining/Second/Result2.csv");
            string vvv = "";
            while((vvv = str.ReadLine()) != null)
            {
                var count = 0;
                var temp = new string[2];
                temp = vvv.Split(';');
                foreach (var e in list)
                    if (e[0] == temp[0])
                    {
                        e[1] = (int.Parse(e[1])+1).ToString();
                        count++;
                    }
                if (count == 0)
                    list.Add(new List<string> { temp[0], "0" });
            }
            foreach (var e in list.OrderBy(e => e[0]))
            {
                if (int.Parse(e[1]) <= 100)
                    e[1] = (int.Parse(e[1]) * 2).ToString();
                else
                    e[1] = (int.Parse(e[1]) - int.Parse(e[1])/3).ToString();

                stream.WriteLine(e[0] + ";" + e[1]);


            }

            stream.Close();
            str.Close();
            
        }
    }
}
