using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopWithDp
{
    public interface IFormModel
    {
        string Name { get; set; }
        string Phone { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        string City { get; set; }
        string Postcode { get; set; }
        string Email { get; set; }
    }
}
