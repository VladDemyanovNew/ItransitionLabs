using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4.Models
{
    public class IndexViewModel
    {
        public List<User> Users { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
