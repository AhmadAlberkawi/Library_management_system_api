using Bvs.Entities;
using Bvs_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.Controllers
{
    public class OverviewController : BaseApiController
    {
        private readonly DataContext _context;

        public OverviewController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NumberOverview>>> Getoverview()
        {
            return await _context.NumberOverview.ToListAsync();
        }
    }
}
