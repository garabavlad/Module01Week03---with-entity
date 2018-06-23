using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataBase
{
    /*
     * PostTable table form:
     *
     */
    public class PostTable : ClassLibrary.Post
    {
        [Key]
        public int Id { get; set; }

        // These lines were commented due to inheritance. Uncomment if cancel inheritance
        //public System.DateTime TimePublished { get; set; }
        //public string Body { get; set; }
        public new UserTable Author { get; set; }

    }
}
