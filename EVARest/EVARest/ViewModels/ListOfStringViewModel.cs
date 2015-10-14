using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.ViewModels {

    public class ListOfStringViewModel {
        
        [Required]
        public IEnumerable<string> Values { get; set; }
    }
}
