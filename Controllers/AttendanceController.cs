﻿using FC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC.Controllers
{
    [Route("api/attendance")]
    public class AttendanceController: Controller
    {
        private readonly AttendanceContext _context;

        public AttendanceController (AttendanceContext context)
        {
            _context = context;

            if (_context.Attendances.Count() == 0)
            {
                NoContent();
                _context.SaveChanges();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var item = _context.Attendances.FirstOrDefault(t => t.id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult NewRecord([FromBody]Attendance attendance)
        {
            if (attendance == null)
            {
                return BadRequest();
            }
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return new ObjectResult(_context.Attendances);
            //return CreatedAtRoute("GetUser", new { id = user.id }, user);
        }

    }
}
