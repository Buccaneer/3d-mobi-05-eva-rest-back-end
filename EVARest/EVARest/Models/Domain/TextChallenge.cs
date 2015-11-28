using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.Models.Domain {
   public  class TextChallenge  : Challenge{
        protected override void OnTypeChanged() {
            if (Type.Equals("Suikervrij"))
                Earnings = 2;
        }
    }
}
