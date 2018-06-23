using System;
using System.Collections.Generic;
using ClassLibrary.DataBase;

namespace ClassLibrary
{
    /*
     *  This is the central class where are kept all the users and all 
     *  the messages.
     */
    public class Board
    {

        // Database connection instance :
        // Read DBManager documentation for more information.
        private DbManager database;

        // Events:
        // Event handler for new messages:
        public delegate void NewMessageEventHandler(object source, EventArgs args);
        public event NewMessageEventHandler NewPostedMessage;
        // Event handler for new users:
        private event EventHandler NewAddedUser;


        // Class constructor:
        public Board()
        {
            database = new DbManager();
            database.CheckForSystem();
        }

        // Method for adding a new user in User List.
        public bool AddUser(string email, string firstName, string lastName, DateTime birthDate)
        {
            var newUser = new User(email,firstName,lastName,birthDate);
            return AddUser(newUser);
        }
        // @Overload
        public bool AddUser(User newUser)
        {
            NewAddedUser += PostNewUserAdded;
            if (database.AddUser(newUser))
            {
                OnNewUser(newUser);
                return true;
            }
            else
            {
                return false;
            }
        }


        // Method for adding a new post in Post List.
        public void AddPost(string message, User author)
        {
            var postTime = DateTime.Now;
            var newPost = new Post(postTime,message,author);
            database.AddPost(newPost);
            OnNewPost(author);
        }

        //  Method for displaying all posts.
        public void ShowBoard()
        {
            var posts = database.GetAllPosts();

            //////////////////////////////////////////////////////////////////posts.Sort();
            foreach (var el in posts)
            {
                if (el.Author.email != "System")
                {
                    Console.WriteLine("\n>" + el.Author + " wrote on " + el.TimePublished +
                        ":\n" + el.Body);
                }
                else
                {
                    Console.WriteLine("\n>SystemUser (" + el.TimePublished + "): " + el.Body);
                }
            }
        }

        // Method for checking if user exists (used for login):
        public User FindUser(User checkedUser)
        {
            return FindUser(checkedUser.email);
        }

        // @Overload
        public User FindUser(string email)
        {
            return database.FindByEmail(email);
        }

        
        // Method to fire when new message was posted:
        protected virtual void OnNewPost(User sender)
        {
            NewPostedMessage?.Invoke(sender, EventArgs.Empty);
        }
        
        // Method to fire when new user joined the board:
        protected virtual void OnNewUser(User newUser)
        {
            if(NewAddedUser != null)
            {
                NewAddedUser(newUser, EventArgs.Empty);
            }
        }


        // Method to announce all users about new message:
        // (used for NewPostedMessage event)
        public void AnnounceNewPost(User sender)
        {
            //foreach(var user in userList)
            //{
            //    if(user != sender)
            //    {
            //        user.NewMessages++;
            //    }
            //}
        }

        // Method to post a new message when a new user joins the board:
        // (used for NewAddedUser event)
        private void PostNewUserAdded(object sender,EventArgs args)
        {
            User newUser = (User)sender;
            this.AddPost(newUser.ToString() + " has joined the board!", new User("System","","",DateTime.Now));
            NewAddedUser -= PostNewUserAdded;
        }

        // Method to reset new messages a user sees after it check new messages:
        public void ResetNewMessages(User looker)
        {
            looker.NewMessages = 0;
        }
    }
}
