

using eBookShop.Models;
using eBookShop.Repositories;

namespace eBookShop.Data
{
    ///<summary>
    /// Class TestData is for creating test data
    /// </summary>>
    public class TestData
    {
        public TestData()
        {
            _booksRepository = new BooksRepository();
            _categoriesRepository = new CategoriesRepository();
        }
        private IBooksRepository _booksRepository;
        private ICategoriesRepository _categoriesRepository;
        ///<summary>
        /// Initializing books and their categories
        /// </summary>>
        public void Initialize()
        {
            InitializeCategories();
            InitializeBooks();
        }

        private void InitializeBooks()
        {
            var category = _categoriesRepository.FindCategory("Fantasy");
            var testBook1 = new Book()
            {
                Author = "Jim Butcher", 
                Price = 15, 
                Title = "Storm Front ",
                Description = "Jim Butcher layers fantasy elements on top of hardboiled mysteries, following magician-for-hire and Chicago P.D. consultant Harry Dresden—more Philip Marlowe than Albus Dumbledore.",
                CreatedDateTime = DateTime.Now,
            };
            testBook1.Categories.Add(category);
            _booksRepository.Create(testBook1);
            
            var testBook2 = new Book()
            {
                Author = "Unknown", 
                Price = 10.5, 
                Title = "The Arabian Nights",
                Description = "Nearly everyone is familiar with this collection of folktales, also known as One Thousand and One Nights, and its infamous framing device: Scheherazade, the vizier’s daughter, is set to be married and then killed by the king; she forestalls this destiny by convincing the king to hear a story, which she then draws out for 1,001 nights by ending each evening on a cliffhanger.",
                CreatedDateTime = DateTime.Now
            };
            testBook2.Categories.Add(category);
            _booksRepository.Create(testBook2);
            
            var testBook3 = new Book()
            {
                Author = "Le Morte d’Arthur",
                Price = 19, 
                Title = "Thomas Malory",
                Description = "One of the earliest printed works of the fantasy genre can be found in the 15th century’s Le Morte d’Arthur, French for “the death of Arthur.",
                CreatedDateTime = DateTime.Now
            };
            testBook3.Categories.Add(category);
            _booksRepository.Create(testBook3);

            _booksRepository.SaveChangesAsync();
        }
        
        private void InitializeCategories()
        {
            _categoriesRepository.Create(new Category() { Name = "Fantasy"});
            _categoriesRepository.Create(new Category() { Name = "Sci-Fi"});
            _categoriesRepository.Create(new Category() { Name = "Mystery"});
            _categoriesRepository.Create(new Category() { Name = "Romance"});
            _booksRepository.SaveChangesAsync();
        }
    }   
}