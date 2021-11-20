using Bvs.Entities;
using Bvs_API.Data;
using Bvs_API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bvs_API.Controllers
{
    public class AdminController: BaseApiController
    {
        private readonly DataContext _context;
        public AdminController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("addAdmin")]
        public async Task<ActionResult<Admin>> AddAdmin(AdminDto adminDto)
        {
            if(await AdminExists(adminDto.Name))
            {
                return BadRequest("Admin Name is taken.");
            }

            using var hmac = new HMACSHA512();

            var admin = new Admin
            {
                Name = adminDto.Name,
                Vorname = adminDto.Vorname,
                Email = adminDto.Email,
                Foto = adminDto.Foto,
                Rolle = adminDto.Rolle,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            return admin;
        }

        private async Task<bool> AdminExists(string name)
        {
            return await _context.Admin.AnyAsync(x => x.Name == name.ToLower());
        }
    }
}
