using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Attributes;

namespace WebApplication1.Data
{
    public class Student
    {
        public int Id { set; get; }

        [Forbidden("aaa", "bbb", "ccc")]
        public string Name { set; get; }

        [Forbidden("aaa", "bbb", "ccc")]
        public string Group { set; get; }

        //
        public List<StudDisc> StudDiscs { set; get; }
    }
}
