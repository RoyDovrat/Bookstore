using BookStore_RoyDovrat.Models;
using System.Xml.Serialization;

namespace BookStore_RoyDovrat.Services
{
    public class XmlHandler
    {
        private readonly string _xmlFilePath;
        private readonly string _htmlDirectoryPath;

        public XmlHandler(IConfiguration configuration)
        {
            _xmlFilePath = configuration["XmlFilePath"];
            _htmlDirectoryPath = configuration["htmlDirectoryPath"]; ;
        }

        public Bookstore LoadBooks()
        {

            if (!File.Exists(_xmlFilePath))
            {
                return new Bookstore();
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Bookstore));
                using (FileStream fs = new FileStream(_xmlFilePath, FileMode.Open))
                {
                    return (Bookstore)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"unexpected error occurred: {ex.Message}");
                return new Bookstore();
            }
        }

        public Book? GetBook(ulong isbn) //?-return book or null
        {
            var bookstore = LoadBooks();
            foreach (var book in bookstore.Books)
            {
                if (book.ISBN == isbn)
                {
                    return book;
                }
            }
            return null;
        }

        public void SaveBooks(Bookstore bookstore)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Bookstore));
                using (FileStream fs = new FileStream(_xmlFilePath, FileMode.Create))
                {
                    serializer.Serialize(fs, bookstore);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"unexpected error occurred: {ex.Message}");
            }
        }

        public bool AddBook(Book newBook)
        {
            var bookstore = LoadBooks();
            bool checkValid = checkIsbnValidation(newBook.ISBN, bookstore);
            if (checkValid)
            {
                bookstore.Books.Add(newBook);
                SaveBooks(bookstore);
                return true;
            }
            return false;

        }

        private bool checkIsbnValidation(ulong newIsbn, Bookstore bookstore)
        {
            if (newIsbn == null) { return false; }
            foreach (Book book in bookstore.Books)
            {
                if (book.ISBN == newIsbn) { return false; }
            }
            return true;
        }


        public bool RemoveBook(ulong isbnToDelete)
        {
            var bookstore = LoadBooks();
            foreach (var book in bookstore.Books)
            {
                if (book.ISBN == isbnToDelete)
                {
                    bookstore.Books.Remove(book);
                    SaveBooks(bookstore);
                    return true;
                }
            }
            return false;
        }

        public bool UpdateBook(Book newBook)
        {
            var bookstore = LoadBooks();

            foreach (var book in bookstore.Books)
            {
                if (book.ISBN == newBook.ISBN)
                {
                    bookstore.Books.Remove(book);
                    bookstore.Books.Add(newBook);
                    SaveBooks(bookstore);
                    return true;
                }
            }
            return false;

        }

        public bool CreateHtmlReports()
        {
            var bookstore = LoadBooks();
            var html = "<html><body><h1>Bookstore Report</h1><table border='1'>"; //html structure
            html += "<tr><th>ISBN</th><th>Category</th><th>Cover</th><th>Title</th><th>Language</th><th>Authors</th><th>Year</th><th>Price</th></tr>"; //table headline

            foreach (var book in bookstore.Books) //table data
            {
                var authors = string.Join(", ", book.Authors);
                html += $"<tr><td>{book.ISBN}</td><td>{book.Category}</td><td>{book.Cover}</td><td>{book.Title.Value}</td><td>{book.Title.Language}</td><td>{authors}</td><td>{book.Year}</td><td>{book.Price}</td></tr>";
            }

            html += "</table></body></html>"; //html structure

            try
            {
                string filePath = Path.Combine(_htmlDirectoryPath, $"bookstoreReport.html");
                File.WriteAllText(filePath, html);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"unexpected error occurred: {ex.Message}");
                return false;
            }

        }

    }
}
