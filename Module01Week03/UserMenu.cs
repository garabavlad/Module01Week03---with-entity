using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary;

namespace Module01Week03
{
    class UserMenu
    {
        public bool isExitSelected { get; private set; } = false;
        // mainBoard reference from MainMenu Class.
        private Board mainBoard;
        private User _loggedUser;

        public UserMenu(Board boardRef, User user)
        {
            mainBoard = boardRef;
            _loggedUser = user;
            if (_loggedUser == null)
            {
                Console.WriteLine("\nYou logged in as a guest!");
                Console.WriteLine("Note: Guests don't have an account and can't post messages.");

            }
            else
            {
                Console.WriteLine("\nWelcome " + _loggedUser.email + "!" +
                    "\nYou have {0} new messages!",_loggedUser.NewMessages);

                // Creating events:
                //mainBoard.SeenNewMessages += OnSeenMessages;
                mainBoard.NewPostedMessage += OnNewPost;
            }
        }

        void DisplayUserMenu()
        {
            Console.WriteLine("\nUser menu:" +  
                "\nEnter 1 to view the board." +
                "\nEnter 2 to post a message." +
                "\nEnter 3 to edit profile." +
                "\nEnter 4 to log out." +
                "");
        }
        
        public void GetUserOption()
        {
            DisplayUserMenu();
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
                // Show Board
                case 1:
                    ShowBoardOption();
                    break;

                // New Post
                case 2:
                    PostOption();
                    break;

                // Edit Profile
                case 3:
                    EditProfileOption();
                    break;

                // Exit
                case 4:
                    SignOutOption();
                    break;

                default:
                    Console.WriteLine("No such option.");
                    break;

            }
        }
        
        private void ShowBoardOption()
        {
            // Reseting new messages if logged user has new messages:
            if (_loggedUser != null && _loggedUser.NewMessages > 0)
            {
                mainBoard.ResetNewMessages(_loggedUser);
            }

            // Displaying:
            Console.WriteLine("Board:");
            mainBoard.ShowBoard();
        }
        private void PostOption()
        {
            if(_loggedUser == null)
            {
                Console.WriteLine(">System (" + DateTime.Now + "): You are not siggned in!\nPlease sign in to post a message.");
                return;
            }
            // Creating new post:
            Console.WriteLine("Enter your message: ");
            string message = Console.ReadLine();
            mainBoard.AddPost(message, _loggedUser);

        }
        private void EditProfileOption()
        {
            if (_loggedUser == null)
            {
                Console.WriteLine(" > System(" + DateTime.Now + "): You are not siggned in!\nPlease sign in to manage your account.");
                return;
            }

            Console.WriteLine("In development... (too lame to implement this now so let's suppose it works correct).");
        }
        private void SignOutOption()
        {
            isExitSelected = true;
            // Deataching events so they won't stack up:
            mainBoard.NewPostedMessage -= OnNewPost;
            //mainBoard.SeenNewMessages -= OnSeenMessages;
        }

        // Method used to announce new users about new message:
        // (used for an event)
        private void OnNewPost(object sender,EventArgs args)
        {
            mainBoard.AnnounceNewPost((User)sender);
            Console.WriteLine("New message added!");
        }

        /*// Method used to reset new messages:
        // (used for an event)
        private void OnSeenMessages(object looker, EventArgs args)
        {
            mainBoard.ResetNewMessages((User)looker);
        }*/
    }

}
