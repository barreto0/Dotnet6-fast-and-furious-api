namespace FastAndFuriousApi.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserModel(string name, string nickname, string email, string password)
        {
            Name = name;
            Nickname = nickname;
            Email = email;
            Password = password;
        }
    }
}