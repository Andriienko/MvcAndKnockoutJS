using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.Models
{
    public interface IRepository
    {
        void Add(Person data);
        void Remove(Person data);
        List<Person> GetAll();
    }
}