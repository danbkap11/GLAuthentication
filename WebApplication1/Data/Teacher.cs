using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public class Teacher
    {
        public int Id { set; get; }
        public string Name { set; get; }
        //
        public List<Discipline> Disciplines { set; get; }
    }
}
