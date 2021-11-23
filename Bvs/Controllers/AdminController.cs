﻿using Bvs.Entities;
using Bvs_API.Data;
using Bvs_API.DTOs;
using Bvs_API.Interfaces;
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
        private readonly ITokenService _tokenService;

        public AdminController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("addAdmin")]
        public async Task<ActionResult<AdminDto>> AddAdmin(AdminRegisterDto adminDto)
        {
            if(await AdminNameExists(adminDto.Username))
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
                Foto = adminDto.Foto,
                Rolle = adminDto.Rolle,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            return new AdminDto
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

        private async Task<bool> AdminNameExists(string username)
        {
            return await _context.Admin.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        private async Task<bool> AdminEmailExists(string email)
        {
            return await _context.Admin.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<AdminDto>> LoginAdmin(AdminLoginDto adminLoginDto)
        {
            var admin = await _context.Admin.SingleOrDefaultAsync(x => x.Username == adminLoginDto.UsernameOrEmail);

            if(admin == null)
            {
                admin = await _context.Admin.SingleOrDefaultAsync(x => x.Email == adminLoginDto.UsernameOrEmail);
            }
                
            if(admin == null)
            {
                return Unauthorized("Username or email is invalid.");
            }

            using var hmac = new HMACSHA512(admin.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminLoginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != admin.PasswordHash[i])
                { 
                    return Unauthorized("Password is invalid.");
                }
            }

            return new AdminDto
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
    }
}