using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public class Discipline
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Annotation { set; get; }
        //
        public Teacher Teacher { set; get; }
        public int TeacherId { get; set; }
        public List<StudDisc> StudDiscs { set; get; }
    }
}
