using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.ViewModels {
    public class GeoLocation {
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        public double Distance { get; set; } = 8.0;
    }
}
