using Bvs.Entities;
using Bvs_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bvs_API.DTOs;

namespace Bvs_API.Controllers
{   
    public class StudentController : BaseApiController
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Student.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            return await _context.Student.FindAsync(id);
        }

        [HttpPost("addStudent")]
        public async Task<ActionResult> AddStudent(StudentDto studentDto)
        {
            if (await _context.Student.AnyAsync(x => x.MatrikelNum == studentDto.MatrikelNum))
            {
                return BadRequest($"Matrikelnummer {studentDto.MatrikelNum} ist bereits existiert!");
            }

            if (await _context.Student.AnyAsync(x => x.BibNum == studentDto.BibNum))
            {
                return BadRequest($"Bibliotheknummber {studentDto.BibNum} ist bereits existiert!");
            }

            var student = new Student
            {
                Name = studentDto.Name,
                Vorname = studentDto.Vorname,
                Email = studentDto.Email,
                MatrikelNum = studentDto.MatrikelNum,
                BibNum = studentDto.BibNum,
                Foto = ""
            };

            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            await StudentCount();
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPut("editStudent")]
        public async Task<ActionResult> EditStudent(Student st)
        {
            var student = await _context.Student.FindAsync(st.Id);

            if (student != null)
            {
                student.Name = st.Name;
                student.Vorname = st.Vorname;
                student.Email = st.Email;
                student.MatrikelNum = st.MatrikelNum;
                student.BibNum = st.BibNum;
                student.Foto = "";

                await _context.SaveChangesAsync();
                return Accepted();
            }
            else
            {
                return NotFound($"Student mit Id= {st.Id} wurde nicht gefunden.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student != null)
            {
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();

                await StudentCount();
                await _context.SaveChangesAsync();

                return Accepted();
            }
            else
            {
                return NotFound($"Student mit Id= {id} wurde nicht gefunden.");
            }
        }

       private async Task StudentCount()
       {
            var studentCount = await _context.Student.CountAsync();
            var numberOverview = await _context.NumberOverview.FirstAsync();
            numberOverview.AnzahlStudent = studentCount;
       }

     }
}
