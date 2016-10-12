using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace TestTask.Models
{

    public class PersonRepository : IRepository
    {
        private List<Person> _persons;
        private string Path { get; set; }

        public PersonRepository(string path)
        {
            _persons = new List<Person>();
            Path = path;

        }
        public void Add(Person data)
        {
            if (data == null)
                return;
            string json;
            if (!File.Exists(Path))
            {
                _persons.Add(data);
                json = JsonConvert.SerializeObject(_persons);
                System.IO.File.WriteAllText(Path, json);
                return;
            }
            if (IsEmpty())
            {
                _persons.Add(data);
                json = JsonConvert.SerializeObject(_persons);
                System.IO.File.WriteAllText(Path, json);
                return;
            }
            json = System.IO.File.ReadAllText(Path);
            _persons = (List<Person>)JsonConvert.DeserializeObject(json, typeof(List<Person>));
            _persons.Add(data);
            json = JsonConvert.SerializeObject(_persons);
            System.IO.File.WriteAllText(Path, json);
        }
        public List<Person> GetAll()
        {
            string json = System.IO.File.ReadAllText(Path);
            if (IsEmpty())
                return new List<Person>();
            _persons = (List<Person>)JsonConvert.DeserializeObject(json, typeof(List<Person>));
            return _persons;
        }

        public void Remove(Person data)
        {
            if (data == null || !File.Exists(Path) || IsEmpty())
                return;
            string json;
            json = System.IO.File.ReadAllText(Path);
            _persons = (List<Person>)JsonConvert.DeserializeObject(json, typeof(List<Person>));
            _persons.RemoveAll(x => x.FirstName == data.FirstName && x.LastName == data.LastName);
            json = JsonConvert.SerializeObject(_persons);
            System.IO.File.WriteAllText(Path, json);
        }

        public bool IsEmpty()
        {
            string json = System.IO.File.ReadAllText(Path);
            if (json == "[]")
                return true;
            else return false;
        }
    }
}