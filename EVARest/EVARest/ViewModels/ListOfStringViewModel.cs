using EVARest.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.ViewModels {

    public class ListOfStringViewModel {
        
        [Required(ErrorMessageResourceName = "ListOfStringValuesRequired", ErrorMessageResourceType =typeof(Resources))]
        public IEnumerable<string> Values { get; set; }
    }
}
