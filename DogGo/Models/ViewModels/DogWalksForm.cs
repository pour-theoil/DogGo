using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class DogWalksForm
    {
        public Walks Walk { get; set; }
        public List<Dog> Dogs { get; set; }
        public List<Walker> Walkers { get; set; }

        public int[] SelectedValues { get; set; }
    }
}
