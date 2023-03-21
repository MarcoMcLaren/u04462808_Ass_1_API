using Courses.Data;
using Courses.Models;
using Courses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller     
    {

        private readonly MyDbContext _db;
        public CoursesController(MyDbContext mydbcontext)
        {
            _db = mydbcontext;
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var employees = await _db.Modules.ToListAsync();
            return Ok(employees);
        }

        //Create a new module
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCourse([FromBody] Module module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Modules.Add(module);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(courseDetails), new { id = module.ModuleId }, module);
        }

        //Populate selected Update row 
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> courseDetails([FromRoute] int id)
        {
            var modules = await _db.Modules.SingleOrDefaultAsync(x => x.ModuleId == id);

            if (modules == null)
            {
                return NotFound();
            }
            return Ok(modules);
        }

        //officually update in database
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> updateCourse([FromRoute] int id, Module officialUpdate)
        {
            var modules = await _db.Modules.FindAsync(id);

            if (modules == null)
            {
                return NotFound();
            }
            modules.Name = officialUpdate.Name;
            modules.Description = officialUpdate.Description;
            modules.Duration = officialUpdate.Duration;

            await _db.SaveChangesAsync();

            return Ok(modules);
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> deleteCourse([FromRoute] int id)
        {
            var employee = await _db.Modules.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            _db.Modules.Remove(employee);
            await _db.SaveChangesAsync();
            return Ok(employee);
        }

    }
}
