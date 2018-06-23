using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataBase
{
    /*
     * This class is designed to do all DataBase logic.
     * Add new methods to add / remove something from any database or table.
     * Check the methods beneath!
     */

    public class DbManager
    {
        private readonly DataBaseContext _context;

        // Constructor
        public DbManager()
        {
            _context = new DataBaseContext();
        }

        // Method that adds new post into DB.
        public void AddPost(ClassLibrary.Post pendingPost)
        {
            // !! Improvements pending.
            var author = _context.UserTable.Single(a => a.email == pendingPost.Author.email);
            //var author = _context.UserTable.Single(a => a.Id == 1);
            var post = new PostTable
            {
                Author = author,
                Body = pendingPost.Body,
                TimePublished = pendingPost.TimePublished
            };
            _context.PostTable.Add(post);
            _context.SaveChanges();
        }

        // Method that adds a new user into DB.
        // (Returns false if user with same email exists in DB)
        public bool AddUser(ClassLibrary.User newUser)
        {
            // Checking if such user exists by checking email:
            try
            {
                var usrFromDb = _context.UserTable.Single(a => a.email == newUser.email);
                return false;
            }
            catch (InvalidOperationException)
            {
                // Adding it:
                var usrToDb = new UserTable
                {
                    birthDate = newUser.birthDate,
                    email = newUser.email,
                    firstName = newUser.firstName,
                    lastName = newUser.lastName,
                    NewMessages = 0
                };
                _context.UserTable.Add(usrToDb);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        // Method that finds a user in DB by its email:
        // (Returns a new ClassLibrary.User instance of found user.
        // Used for checking the user in login instruction)
        public ClassLibrary.User FindByEmail(string email)
        {
            UserTable usrFromDb = null;
            try
            {
                usrFromDb = _context.UserTable.Single(a => a.email == email);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("User not found!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (usrFromDb != null)
            {
                return new ClassLibrary.User
                {
                    email = usrFromDb.email,
                    birthDate = usrFromDb.birthDate,
                    firstName = usrFromDb.firstName,
                    lastName = usrFromDb.lastName,
                    NewMessages =  usrFromDb.NewMessages
                };
            }
            else
            {
            return null;
            }
        }

        // Method that checks if a Meta user "System" exists in DB & adds it if needed:
        public void CheckForSystem()
        {
            var sys = new ClassLibrary.User
            {
                email = "System",
                birthDate = DateTime.Now,
                firstName = "",
                lastName = "",
                NewMessages = 0
            };
            AddUser(sys);
        }

        // Method that returns entire list of posts from DB:
        // (Returns a IEnumerable of Posts instances)
        public IEnumerable<Post> GetAllPosts()
        {
            var allPosts = new List<Post>();
            /*foreach (var postTable in _context.PostTable.ToList())
            {
                allPosts.Add(
                    new Post
                    {
                        Author = postTable.Author,
                        Body = postTable.Body,
                        TimePublished = postTable.TimePublished
                    });
            }*/
                allPosts = _context.PostTable.Cast<Post>().ToList();


            return allPosts;
        }
    }
}
