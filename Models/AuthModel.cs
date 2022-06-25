namespace FastAndFuriousApi.Models
{
    public class GetTokenModel
    {
        public string Token { get; set; }
        public object User { get; set; }
        public DateTime ValidTo { get; set; }
        public GetTokenModel(string token, object user, DateTime validate)
        {
            Token = token;
            User = user;
            ValidTo = validate;
        }
    }
}