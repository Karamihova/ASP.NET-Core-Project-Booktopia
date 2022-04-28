﻿namespace Booktopia.Services.Books
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using Booktopia.Models.Books;
    using System.Collections.Generic;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly BooktopiaDbContext data;

        public BookService(BooktopiaDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<BookServiceModel> All()
            => GetBooks(this.data
                .Books
                .OrderByDescending(x => x.CreatedOn));
                

        public IEnumerable<BookServiceModel> ByAuthor(string userId)
            => GetBooks(this.data
                .Books
                .Where(b => b.Author.UserId == userId));

        public IEnumerable<BookServiceModel> ByCategoryType(string category)
            => GetBooks(this.data
                .Books
                .Where(b => b.Category.Type == category));

        public IEnumerable<BookCategoryServiceModel> AllCategories()
         => this.data
                .Categories
                .Select(c => new BookCategoryServiceModel
                {
                    Id = c.Id,
                    Type = c.Type
                }).ToList();

        public BookViewModel ById(int id)
            => this.data
                .Books
                .Where(b => b.Id == id)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Annotation = b.Annotation,
                    ImageUrl = b.ImageUrl,
                    CategoryType = b.Category.Type,
                    AuthorName = b.Author.Name,
                    ChaptersCount = b.Chapters.Count(),
                    ReviewsCount = b.Reviews.Count(),
                    Rating = b.Reviews.Average(r => r.Rating) == null ? 0 : b.Reviews.Average(r => r.Rating),
                    QuotesCount = b.Quotes.Count()
                })
                .FirstOrDefault();

        public BookDetailsServiceModel Details(int id)
            => this.data
            .Books
            .Where(b => b.Id == id)
            .Select(b => new BookDetailsServiceModel
            {
                Id = b.Id,
                Title = b.Title,
                Annotation = b.Annotation,
                ImageUrl = b.ImageUrl,
                CategoryName = b.Category.Type,
                AuthorName = b.Author.Name,
                AuthorId = b.Author.Id,
                UserId = b.Author.UserId,
                CategoryId = b.Category.Id
            })
            .FirstOrDefault();

        public bool CategoryExists(int categoryId)
         => this.data
            .Categories
            .Any(c => c.Id == categoryId);

        public int Create(string title, string annotation, string imageUrl, int categoryId, int authorId)
        {
            var bookData = new Book
            {
                Title = title,
                Annotation = annotation,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                AuthorId = authorId
            };

            this.data.Books.Add(bookData);
            this.data.SaveChanges();

            return bookData.Id;
        }

        public bool Edit(int id, string title, string annotation, string imageUrl, int categoryId)
        {
            var bookData = this.data.Books.Find(id);

            if (bookData == null)
            {
                return false;
            }

            bookData.Title = title;
            bookData.Annotation = annotation;
            bookData.ImageUrl = imageUrl;
            bookData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByAuthor(int id, int authorId)
            => this.data
            .Books
            .Any(b => b.Id == id && b.AuthorId == authorId);

        private static IEnumerable<BookServiceModel> GetBooks(IQueryable<Book> bookQuery)
            => bookQuery
                .Select(b => new BookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Annotation = b.Annotation.Substring(0, 200),
                    ImageUrl = b.ImageUrl,
                    CategoryName = b.Category.Type,
                    AuthorName = b.Author.Name
                })
                .ToList();

        
    }
}
