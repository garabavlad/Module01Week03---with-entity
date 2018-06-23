using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataBase
{
    /*
     * User table form:
     *
     */
    public class UserTable : ClassLibrary.User
    {
        [Key] public int Id { get; set; }

        // These lines were commented due to inheritance. Uncomment if cancel inheritance
        //public string email { get; set; }
        //public string firstName { get; set; }
        //public string lastName { get; set; }
        //public DateTime birthDate { get; set; }
    }
}
