using System.Xml.Serialization;

namespace AdvanceAPI
{
    /// <summary>
    /// Represents a book with a title, publisher, and a list of authors.
    /// </summary>
    [XmlRoot("Book")] // Define the root element in the XML
    public class Book
    {
        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        [XmlElement("Title")] // Specify the XML element name for the title
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the publisher of the book.
        /// </summary>
        [XmlElement("Publisher")] // Specify the XML element name for the publisher
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the list of authors of the book.
        /// </summary>
        [XmlArray("Authors")] // Specify the XML element for the authors collection
        [XmlArrayItem("Author")] // Specify the XML element for each item (author) in the list
        public List<string> Authors { get; set; }

        /// <summary>
        /// Default parameterless constructor required for XML serialization.
        /// </summary>
        public Book() { }

        /// <summary>
        /// Constructor for initializing a Book object with specific values.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <param name="publisher">The publisher of the book.</param>
        /// <param name="authors">A list of authors of the book.</param>
        public Book(string title, string publisher, List<string> authors)
        {
            Title = title;
            Publisher = publisher;
            Authors = authors;
        }
    }

    /// <summary>
    /// This class demonstrates XML serialization and deserialization of the Book object.
    /// </summary>
    public class XMLSandD
    {
        /// <summary>
        /// Main entry point for the application. It demonstrates XML serialization and deserialization.
        /// </summary>
        static void Main()
        {
            // Create a list of authors for the book
            List<string> authors = new List<string> { "J.K. Rowling", "John Tiffany", "Jack Thorne" };

            // Create a Book object with the authors list, title, and publisher
            Book book = new Book("Harry Potter and the Cursed Child", "Little, Brown and Company", authors);

            // Create an instance of XmlSerializer for the Book type
            XmlSerializer serializer = new XmlSerializer(typeof(Book));

            // Serialize the book object to XML and save it to a file
            using (StreamWriter writer = new StreamWriter("book.xml"))
            {
                serializer.Serialize(writer, book); // Serialize and write the object to a file
            }

            Console.WriteLine("Book object serialized to book.xml.");

            // Create a new instance of XmlSerializer for deserialization
            XmlSerializer serial = new XmlSerializer(typeof(Book));

            // Deserialize the XML file into a Book object
            using (StreamReader reader = new StreamReader("book.xml"))
            {
                Book deserializedBook = (Book)serial.Deserialize(reader); // Deserialize XML into a Book object

                // Output the deserialized data
                Console.WriteLine("Deserialized Book:");
                Console.WriteLine($"Title: {deserializedBook.Title}"); // Output the title
                Console.WriteLine($"Publisher: {deserializedBook.Publisher}"); // Output the publisher
                Console.WriteLine("Authors: ");

                // Output each author from the deserialized book
                foreach (var author in deserializedBook.Authors)
                {
                    Console.WriteLine($"- {author}");
                }
            }
        }
    }
}
