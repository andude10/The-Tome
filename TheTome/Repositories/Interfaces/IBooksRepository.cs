using TheTome.Models;

namespace TheTome.Repositories.Interfaces;

public interface IBooksRepository
{
    /// <summary>
    ///     GetBook returns a book WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>Book WITHOUT associated data</returns>
    Book GetBook(int id);
    
    /// <summary>
    ///     Returns a list of books with no associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns></returns>
    IEnumerable<Book> GetBooks();

    public IEnumerable<Book> GetBooks(int skipSize, int takeSize, SortBookState sortBookState);

    /// <summary>
    ///     Loads all orders of the book
    /// </summary>
    void LoadBookOrders(Book book);

    /// <summary>
    ///     Loads all users who liked the book
    /// </summary>
    void LoadUsersWhoLike(Book book);

    /// <summary>
    ///     Loads all categories the book belongs to
    /// </summary>
    void LoadCategories(Book book);

    /// <summary>
    ///     The GiveStarToBook give a star to book
    /// </summary>
    /// <param name="email">Email of the user who likes</param>
    /// <param name="bookId">The book id</param>
    void GiveStarToBook(int bookId, string email);

    void Create(Book item);

    void Update(Book item);

    void Delete(int id);
}