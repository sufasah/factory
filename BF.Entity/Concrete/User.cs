namespace BF.Entity.Concrete
{
    public class User:IEntity
    {
        public User(){}

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; set; }
        public string Password { get; set; }
        
    }
}