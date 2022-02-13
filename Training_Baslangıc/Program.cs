using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Training_Baslangıc
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\Data.txt");
            List<Data> liste = DeserializeToList<Data>(jsonString);
            List<Data> temizListe = new List<Data>();
            List<int> id = new List<int>();

            Yazdir(liste);

            //temizle
            foreach (Data item in liste)
            { 
                if (!id.Contains(item.id))
                {
                    id.Add(item.id);
                    temizListe.Add(item);
                }  
            }

            Yazdir(temizListe);

            List<Data> women = GetWomen(temizListe);
            Yazdir(women);
            List<Data> men = GetMen(temizListe);
            Yazdir(men);
            List<List<Data>> samegendersamecountry = GetSameOnes(temizListe);
            Yazdir2(samegendersamecountry);
        }
        public static void Yazdir(List<Data> list)
        {
            foreach (Data item in list)
            {
                Console.WriteLine(item.id + " " + item.FirstName + " " + item.LastName + " " + item.Gender + " " + item.Email + " " + item.Country);
            }
        }
        public static void Yazdir2(List<List<Data>> list)
        {
            foreach (var sublist in list)
            {
                foreach (var obj in sublist)
                {
                    Console.WriteLine(obj.id + " " + obj.FirstName + " " + obj.LastName + " " + obj.Gender + " " + obj.Email + " " + obj.Country);
                }
            }
        }
        public static List<Data> GetWomen(List<Data> cleanlist)
        {
            List<Data> women = new List<Data>();
            foreach (Data item in cleanlist)
            {
                if (item.Gender == "Female")
                {
                    women.Add(item);
                }
            }
            return women;
        }
        public static List<Data> GetMen(List<Data> cleanlist)
        {
            List<Data> men = new List<Data>();
            foreach (Data item in cleanlist)
            {
                if (item.Gender == "Male")
                {
                    men.Add(item);
                }
            }
            return men;
        }
        public static List<List<Data>> GetSameOnes(List<Data> cleanlist)
        {
            List<List<Data>> list = new List<List<Data>>();
            var groups = cleanlist.GroupBy(p => new { p.Gender, p.Country });
            foreach (var group in groups.OrderBy(g => g.Key.Gender).ThenBy(g => g.Key.Country))
            {
                List<Data> list1 = new List<Data>();
                foreach (var person in group)
                {
                    list1.Add(person);
                }
                if(list1.Count > 1)
                    list.Add(list1);

                //Yazdir(list1);
                //Console.WriteLine();
                //Console.WriteLine();
                //Console.WriteLine();
            }
            return list;
        }
        public static List<T> DeserializeToList<T>(string jsonString)
        {
            var array = JArray.Parse(jsonString);
            List<T> objectsList = new List<T>();

            foreach (var item in array)
            {
                objectsList.Add(item.ToObject<T>());
            }
            return objectsList;
        }
    }

    class Data
        {
            public int id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string Country { get; set; }
        }
}
