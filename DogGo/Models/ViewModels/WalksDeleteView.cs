using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalksDeleteView
    {
        public List<Walks> Walks { get; set; }
        public int[] SelectedValues { get; set; }
    }
}
