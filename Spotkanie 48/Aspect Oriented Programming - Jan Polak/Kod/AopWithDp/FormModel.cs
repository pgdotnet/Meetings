using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopWithDp
{
    public class FormModel: IFormModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
    }
}
