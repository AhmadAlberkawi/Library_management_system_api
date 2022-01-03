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
    public class BorrowController : BaseApiController
    {
        private readonly DataContext _context;

        public BorrowController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("addBorrow")]
        public async Task<ActionResult> AddBorrow(BorrowDto borrowDto)
        {
            Student student = await _context.Student.FindAsync(borrowDto.StudentId);

            Book book = await _context.Book.FindAsync(borrowDto.BookId);

            await _context.Borrow.AddAsync(new Borrow
            {
                students = student,
                Books = book
            });

            await _context.SaveChangesAsync();

            await BorrowCount();
            await _context.SaveChangesAsync();

            return Accepted();
        }        [HttpGet()]
        public async Task<ActionResult<object>> BorrowList()
        {
            var BookBorrowList = from st in _context.Student
                                 from bk in _context.Book
                                 from br in _context.Borrow
                                 where br.Books == bk && br.students == st
                                 orderby bk.Title
                                 select new
                                 {
                                     br.Id,
                                     bk.B_foto,
                                     bk.Title,
                                     bk.Isbn,
                                     bk.Verlag,
                                     bk.Autor,
                                     st.Name,
                                     BorrowedUntil = br.BorrowedUntil.ToString("d"),
                                     Days = br.GetremainingDays()
                                 };

            return await BookBorrowList.ToListAsync();
        }        [HttpGet("{id}")]
        public async Task<ActionResult<object>> BorrowForStudent(int id)
        {
            Student student = await _context.Student.FindAsync(id);

            var BookBorrowList = from st in _context.Student
                                 from bk in _context.Book
                                 from br in _context.Borrow
                                 where br.Books == bk && br.students == st && st == student
                                 orderby bk.Title
                                 select new
                                 {
                                     br.Id,
                                     bk.B_foto,
                                     bk.Title,
                                     bk.Isbn,
                                     bk.Verlag,
                                     bk.Autor,
                                     BorrowedUntil = br.BorrowedUntil.ToString("d"),
                                     Days = br.GetremainingDays()
                                 };

            return await BookBorrowList.ToListAsync();
        }        [HttpDelete("{id}")]        public async Task<ActionResult> DeleteBorrow(int id)
        {
            Borrow borrow = await _context.Borrow.FindAsync(id);

            if (borrow != null)
            {
                _context.Borrow.Remove(borrow);
                await _context.SaveChangesAsync();

                await BorrowCount();
                await _context.SaveChangesAsync();

                return Accepted();
            }
            else
            {
                return NotFound($"Ausleihe id {id} wurde nicht gefunden.");
            }
        }        private async Task BorrowCount()
        {
            var borrowCount = await _context.Borrow.CountAsync();
            var numberOverview = await _context.NumberOverview.FirstAsync();
            numberOverview.AnzahlBorrow = borrowCount;
        }    }
}
