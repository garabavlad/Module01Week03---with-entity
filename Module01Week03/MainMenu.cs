using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace Module01Week03
{
    class MainMenu
    {
        public bool isExitSelected { get; private set; } = false;
        // mainBoard istance is used as the main app system.
        private Board mainBoard;
        private User _loggedUser = null;

        // Constructor:
        public MainMenu()
        {
            try
            {
                // Deserializing the board from the last session.
                //mainBoard = BoardSerializator.DeserializeBoard();
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("" + e);
            }

            if(mainBoard == null)
                mainBoard = new Board();

        }

        // Method for displaying menu:
        public void DisplayMainMenu()
        {
            Console.WriteLine("\nMain menu:" +
                "\nEnter 1 to log in." +
                "\nEnter 2 to sign up." +
                "\nEnter 3 to enter as guest (can't post messages)." +
                "\nEnter 4 to exit program." +
                "");
        }

        // Method for getting user desired option and executing it:
        public void GetUserOption()
        {
            Console.WriteLine("Choice:");
            try
            {
                int userInput = Int32.Parse(Console.ReadLine());
                DoOption(userInput);
            }
            catch (FormatException excep)
            {
                Console.WriteLine(excep);
            }
        }

        private void DoOption(int option)
        {
            switch (option)
            {
                // Login
                case 1:
                    LoginOption();
                    break;

                // Sign up
                case 2:
                    SignUpOption();
                    break;

                // As guest
                case 3:
                    AsGuestOption();
                    break;

                // Exit
                case 4:
                    ExitOption();
                    break;

                default:
                    Console.WriteLine("No such option.");
                    break;

            }
        }

        /*
         * Switch case options:
         */
        private void LoginOption()
        {
            Console.WriteLine("Enter your email:");
            var email = Console.ReadLine();

            // Looking for user in DB:
            var foundUser = mainBoard.FindUser(email);
            if (foundUser != null)
            {
                _loggedUser = foundUser;

                //Initializing a new user menu instance:
                var userMenu = new UserMenu(mainBoard, _loggedUser);

                //Running UserMenu:
                while (!userMenu.isExitSelected)
                {
                    userMenu.GetUserOption();
                }
            }

        }
        private void SignUpOption()
        {
            // Getting input:
            DateTime bdate;

            Console.WriteLine("\nEnter your information:\nEmail:");
            var email = Console.ReadLine();

            Console.WriteLine("First name:");
            var fname = Console.ReadLine();

            Console.WriteLine("Last name");
            var lname = Console.ReadLine();

            try
            {
                Console.WriteLine("Birth year:");
                var year = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Birth month:");
                var month = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Birth day:");
                var day = Int32.Parse(Console.ReadLine());

                bdate = new DateTime(year, month, day);
            }
            catch (FormatException excep)
            {
                Console.WriteLine(excep);
                return;
            }

            // Adding it to DB:
            var newUser = new User(email, fname, lname, bdate);
            if (mainBoard.AddUser(newUser))
            {
                _loggedUser = newUser;

                // Implementing a new User Menu & running it:
                var userMenu = new UserMenu(mainBoard, _loggedUser);
                while (!userMenu.isExitSelected)
                {
                    userMenu.GetUserOption();
                }
            }
            else
            {
                Console.WriteLine("User already exists!");
                return;
            }
        }
        private void AsGuestOption()
        {
            UserMenu userMenu = new UserMenu(mainBoard, null);
            while (!userMenu.isExitSelected)
            {
                userMenu.GetUserOption();
            }
        }
        private void ExitOption()
        {
            isExitSelected = true;

            // Serialize instance:
            /*BoardSerializator boardSerializator = new BoardSerializator();
            boardSerializator.SerializeBoard(mainBoard);*/
        }
    }
}
