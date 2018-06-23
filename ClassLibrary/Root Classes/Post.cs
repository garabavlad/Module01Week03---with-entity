using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /*
    *  This class is used to create a new post (message) which will be shown
    * into the Board class.
    */
    public class Post : IEquatable<Post>, IComparable<Post>
    {
        public System.DateTime TimePublished { get; set; }
        public User Author { get; set; }
        public string Body { get; set; }
        // Temporarily disabled
        //private bool _changedBody = false;

        /*
         *  Post class constructor.
         *  Creates a new Post with standard input information.
         */
        public Post(DateTime TimeWhenPosted, string Message, User Author)
        {
            this.TimePublished = TimeWhenPosted;
            this.Body = Message;
            this.Author = Author;
        }

        public Post()
        { }

        // Temporarily disabled
        ///*
        // *  Method used for editing post:
        // */
        //public void EditPost(string newBody)
        //{
        //    // !! Temporarily disabled
        //    //changedBody = true;
        //    this.Body = newBody;
        //}




        /*
         *  Overriding default Methods:
         *  Also adding methods for IComparable & IEquatable.
         *  It is used to sort the list of these objects.
         */
        public override string ToString()
        {
            return "Message: " + Body;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Post objAsPost = obj as Post;
            if (objAsPost == null) return false;
            else return Equals(objAsPost);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // Default comparer for Part type.
        public int CompareTo(Post comparePost)
        {
            // A null value means that this object is greater.
            if (comparePost == null)
                return 1;

            else
                return this.TimePublished.CompareTo(comparePost.TimePublished);
        }

        public bool Equals(Post other)
        {
            if (other == null) return false;
            return (this.TimePublished.Equals(other.TimePublished));
        }
    }
}
