namespace BooksShop
{
    public class Book
    {
        public object BookId { get; internal set; }
        public string Title { get; internal set; }
        public string Autor { get; internal set; }
        public int Price { get; internal set; }
    }
}