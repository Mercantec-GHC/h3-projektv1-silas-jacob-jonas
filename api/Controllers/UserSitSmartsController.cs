﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSitSmartsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UserSitSmartsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/UserSitSmarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSitSmart>>> GetUserSitSmart()
        {
            return await _context.UserSitSmart.ToListAsync();
        }

        // GET: api/UserSitSmarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSitSmart>> GetUserSitSmart(int id)
        {
            var userSitSmart = await _context.UserSitSmart.FindAsync(id);

            if (userSitSmart == null)
            {
                return NotFound();
            }

            return userSitSmart;
        }

        // PUT: api/UserSitSmarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSitSmart(int id, UserSitSmart userSitSmart)
        {
            if (id != userSitSmart.Id)
            {
                return BadRequest();
            }

            _context.Entry(userSitSmart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSitSmartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserSitSmarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSitSmart>> PostUserSitSmart(UserSitSmart userSitSmart)
        {
            _context.UserSitSmart.Add(userSitSmart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSitSmart", new { id = userSitSmart.Id }, userSitSmart);
        }

        // DELETE: api/UserSitSmarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSitSmart(int id)
        {
            var userSitSmart = await _context.UserSitSmart.FindAsync(id);
            if (userSitSmart == null)
            {
                return NotFound();
            }

            _context.UserSitSmart.Remove(userSitSmart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSitSmartExists(int id)
        {
            return _context.UserSitSmart.Any(e => e.Id == id);
        }
    }
}
