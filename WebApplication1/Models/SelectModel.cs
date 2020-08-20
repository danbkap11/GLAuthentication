using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class SelectModel
    {
        public Student Student { get; set; }
        public IEnumerable<Discipline> SelectedDiscs { get; set; }
        public IEnumerable<Discipline> NotSelectedDiscs { get; set; }
    }
}
