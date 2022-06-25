namespace FastAndFuriousApi.Domain.Quote
{
    public class Phrase : Entity
    {
        public Phrase()
        {
            Active = true;
        }
        public string Text { get; set; }
        public bool Active { get; set; }
        public Guid AuthorId { get; set; } //torna a primary key obrigatoria One To Many
        public Author Author { get; set; }
    }
}