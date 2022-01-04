using Bvs.Entities;
using Bvs_API.Data;
using Bvs_API.DTOs;
using Bvs_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class AdminController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AdminController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<AdminDto>> GetAdmins()
        {
            return await _context.Admin
                .Select(x => new AdminDto(x.Id, x.Name, x.Vorname, x.Email, x.Foto, x.Rolle)).ToListAsync();
        }

        [HttpPost("addAdmin")]
        public async Task<ActionResult<AdminTokenDto>> AddAdmin(AdminRegisterDto adminDto)
        {
            if (await AdminNameExists(adminDto.Username))
            {
                return BadRequest("Admin username is taken.");
            }

            if (await AdminEmailExists(adminDto.Email))
            {
                return BadRequest("Admin Email is taken.");
            }

            using var hmac = new HMACSHA512();

            var admin = new Admin
            {
                Username = adminDto.Username,
                Name = adminDto.Name,
                Vorname = adminDto.Vorname,
                Email = adminDto.Email,
                Foto = "",
                Rolle = adminDto.Rolle,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            await AdminsCount();
            await _context.SaveChangesAsync();

            return new AdminTokenDto
            {
                Username = admin.Username,
                Name = admin.Name,
                Vorname = admin.Vorname,
                Email = admin.Email,
                Foto = admin.Foto,
                Rolle = admin.Rolle,
                Token = _tokenService.CreateToken(admin)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AdminTokenDto>> LoginAdmin(AdminLoginDto adminLoginDto)
        {
            var admin = await _context.Admin.SingleOrDefaultAsync(x => x.Username == adminLoginDto.UsernameOrEmail);

            if (admin == null)
            {
                admin = await _context.Admin.SingleOrDefaultAsync(x => x.Email == adminLoginDto.UsernameOrEmail);
            }

            if (admin == null)
            {
                return Unauthorized("Username or email is invalid.");
            }

            bool password = CheckPassword(admin.PasswordSalt, admin.PasswordHash, adminLoginDto.Password);

            if (!password)
            {
                return Unauthorized("Password is invalid.");
            }

            //using var hmac = new HMACSHA512(admin.PasswordSalt);

            //var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminLoginDto.Password));

            //for (int i = 0; i < computedHash.Length; i++)
            //{
            //    if (computedHash[i] != admin.PasswordHash[i])
            //    { 
            //        return Unauthorized("Password is invalid.");
            //    }
            //}

            return new AdminTokenDto
            {
                Username = admin.Username,
                Name = admin.Name,
                Vorname = admin.Vorname,
                Email = admin.Email,
                Foto = admin.Foto,
                Rolle = admin.Rolle,
                Token = _tokenService.CreateToken(admin)
            };
        }

        [HttpPut("editAdmin")]
        public async Task<ActionResult> EditProfil(AdminRegisterDto adminEdit)
        {
            Admin admin = await _context.Admin.SingleOrDefaultAsync(x => x.Email == adminEdit.Email);

            if (admin != null)
            {
                bool password = CheckPassword(admin.PasswordSalt, admin.PasswordHash, adminEdit.Password);

                if (!password)
                {
                    return Unauthorized("Password is invalid.");
                }

                if (await AdminNameExists(adminEdit.Username))
                {
                    return BadRequest("Admin username is taken.");
                }

                admin.Username = adminEdit.Username;
                admin.Vorname = adminEdit.Vorname;
                admin.Name = adminEdit.Name;
                //  admin.Foto = ""; //adminEdit.Foto;

                await _context.SaveChangesAsync();

                return Accepted("Aenderungen gespeichert");
            }
            else
            {
                return NotFound($"Admin mit Emailadresse: {adminEdit.Email} wurde nicht gefunden.");
            }
        }

        [HttpPut("changePassword")]
        public async Task<ActionResult> ChangePassword(AdminPasswordDto adminPassword)
        {
            Admin admin = await _context.Admin.SingleOrDefaultAsync(x => x.Email == adminPassword.Email);

            if (admin != null)
            {
                // check the old password

                bool password = CheckPassword(admin.PasswordSalt, admin.PasswordHash, adminPassword.Password);

                if (!password)
                {
                    return Unauthorized("Password is invalid.");
                }

                // change password to new 

                using var hmac = new HMACSHA512();

                admin.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminPassword.NewPassword));
                admin.PasswordSalt = hmac.Key;

                await _context.SaveChangesAsync();

                return Accepted("Password Changed");
            }
            else
            {
                return NotFound($"Admin mit Emailadresse: {adminPassword.Email} wurde nicht gefunden.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admin.FindAsync(id);

            if (admin != null)
            {
                _context.Admin.Remove(admin);
                await _context.SaveChangesAsync();

                await AdminsCount();
                await _context.SaveChangesAsync();
                return Accepted();
            }
            else
            {
                return NotFound($"Admin with Id= {id} not found");
            }
        }

        // Check methods for element exsit 

        private async Task<bool> AdminNameExists(string username)
        {
            return await _context.Admin.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        private async Task<bool> AdminEmailExists(string email)
        {
            return await _context.Admin.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }

        // Check Password

        private bool CheckPassword(byte[] PasswordSaltFromDB, byte[] PasswordHashFromDB,
            string PasswordFromClient)
        {
            using var hmac = new HMACSHA512(PasswordSaltFromDB);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(PasswordFromClient));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != PasswordHashFromDB[i])
                {
                    return false;
                }
            }

            return true;
        }

        // Admin counter for overview Page

        private async Task AdminsCount()
        {
            var adminCount = await _context.Admin.CountAsync();
            var numberOverview = await _context.NumberOverview.FirstAsync();
            numberOverview.AnzahlAdmin = adminCount;
        }
    }
}
