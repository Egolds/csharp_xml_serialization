using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace yt_SerializationXML
{
    [Serializable]
    public class Users
    {
        public Users() { }
        public List<User> UsersList { get; set; } = new List<User>();
    }

    [Serializable]
    public class User
    {
        [XmlElement("USER")]
        public string Username { get; set; }
        public int Sex { get; set; }
        public int Age { get; set; }

        public User() { }

        public User(string Username, int Sex, int Age)
        {
            this.Username = Username;
            this.Sex = Sex;
            this.Age = Age;
        }
    }
}
