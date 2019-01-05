using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lands.Domain
{
    [NotMapped]

    public class UserView: User
    {
        public byte[] ImageArray { get; set; }

        public string Password { get; set; }
    }
}
