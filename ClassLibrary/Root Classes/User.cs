using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /*
     *  This class is used to create new users.
     */
    public class User
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        // For event handler:
        public int NewMessages { get; internal set; } = 0;

        /*
         *  User class constructor.
         *  Creates a new user with standard input information.
         */
        public User(string email, string firstName, string lastName, DateTime birthDate)
        {
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
        }

        public User()
        { }

        //// Method for editting user information:
        //public void EditUserInformation(string email, string firstName, string lastName, DateTime birthDate)
        //{
        //    this.email = email;
        //    this.firstName = firstName;
        //    this.lastName = lastName;
        //    this.birthDate = birthDate;

        //}

        // @override display method
        public override string ToString()
        {
            return firstName + " " + lastName;
        }

    }
}
