using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataBase
{

    /*
     * Context class used for migrations:
     * Has 2 tables, with a relationship.
     *
     */

    class DataBaseContext : DbContext
    {
        public DbSet<PostTable> PostTable { get; set; }
        public DbSet<UserTable> UserTable { get; set; }
    }
}
