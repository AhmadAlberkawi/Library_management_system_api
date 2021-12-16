using Bvs.Entities;
using Bvs_API.Data;
using Bvs_API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.Controllers
{
    public class BookController : BaseApiController
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Book.ToListAsync();
        }

        [HttpPost("addBook")]
        public async Task<ActionResult> AddBook(BookDto bookDto)
        {
            if (await _context.Book.AnyAsync(x => x.Isbn == bookDto.Isbn))
            {
                return BadRequest($"ISBN: {bookDto.Isbn} ist bereits existiert!");
            }

            try
            {
                Book book = new Book
                {
                    Title = bookDto.Title,
                    Isbn = bookDto.Isbn,
                    Verlag = bookDto.Verlag,
                    Verfuegbar = bookDto.Verfuegbar,
                    Anzahl = bookDto.Anzahl,
                    B_foto = "",
                    Autor = bookDto.Autor,
                    Kategorie = bookDto.Kategorie
                };

                _context.Book.Add(book);
                await _context.SaveChangesAsync();

                var booksCount = await _context.Book.CountAsync();
                var overView = await _context.NumberOverview.FirstAsync();
                overView.AnzahlBook = booksCount;
                await _context.SaveChangesAsync();

                return Accepted();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("editBook")]
        public async Task<ActionResult> EditBook(Book bk)
        {
            var book = await _context.Book.FindAsync(bk.Id);

            if (await checkIsbn(bk.Isbn))
            {
                return BadRequest($"ISBN ");
            }

            if (book != null)
            {
                book.Title = bk.Title;
                book.Isbn = bk.Isbn;
                book.Verlag = bk.Verlag;
                book.Isbn = bk.Isbn;
                book.Verfuegbar = bk.Verfuegbar;
                book.Anzahl = bk.Anzahl;
                book.B_foto = "";
                book.Autor = bk.Autor;
                book.Kategorie = bk.Kategorie;

                await _context.SaveChangesAsync();
                return Accepted();
            }
            else
            {
                return NotFound($"Book mit Id= {bk.Id} wurde nicht gefunden.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);

            if(book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();

                var booksCount = await _context.Book.CountAsync();
                var overView = await _context.NumberOverview.FirstAsync();
                overView.AnzahlBook = booksCount;
                await _context.SaveChangesAsync();

                return Accepted();
            }
            else
            {
                return NotFound($"Book mit {id} wurde nicht gefunden.");
            }
        }

        private async Task<bool> checkIsbn(string isbn)
        {
            return await _context.Book.AnyAsync(x => x.Isbn.ToLower() == isbn.ToLower());
        }

        private async void BookCount()
        {
            var booksCount = await _context.Book.CountAsync();
            var overView = await _context.NumberOverview.FirstAsync();
            overView.AnzahlBook = booksCount;
        }
    }
}
