using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.ViewModels;

public class BookViewerViewModel
{
    public BookViewerViewModel(bool isBought, Book book)
    {
        IsBought = isBought;
        Book = book;
    }
    
    public Book Book { get; set; }
    public bool IsBought { get; set; }
}