using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class DogWalks
    {
        public int Id { get; set; }
        public Walker Walker { get; set; }  
        public int Duration { get; set; }
        public List<Dog> Dogs { get; set; }
        public DateTime DateTime { get; set; }
    }
}
