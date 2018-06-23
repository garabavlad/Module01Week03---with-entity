using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace Module01Week03
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var p = new PostTable()
            {
                Author = "david",
                Body = "mess",
                TimePublished = DateTime.Now,
            };
            var context = new Entities();
            context.PostTables.Add(p);

            context.PostTables.Add(p);
            context.SaveChanges(); */

            // Creating a new MainMenu instance:
            MainMenu mainMenu = new MainMenu();

            // Getting user input & executing options:
            while(!mainMenu.isExitSelected)
            {
                mainMenu.DisplayMainMenu();
                mainMenu.GetUserOption();
            }

        }
    }
}
