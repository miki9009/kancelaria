using System.Threading.Tasks;
using Kancelaria.API.Data;
using Kancelaria.API.Dtos;
using Kancelaria.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kancelaria.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        readonly DataContext _context;
        public CasesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCases()
        {
            var cases = await _context.Cases.ToListAsync();
            return Ok(cases);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCase(int id)
        {
            var c = await _context.Cases.SingleOrDefaultAsync(x=> x.Id == id);
            return Ok(c);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(CreateCaseDto createCaseDto)
        {
            var c = new Case()
            {
                CaseName = createCaseDto.CaseName, 
                Signature = createCaseDto.Signature, 
                DateAdded = createCaseDto.DateAdded
                };
            await _context.Cases.AddAsync(c);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _case = await _context.Cases.SingleOrDefaultAsync(x=>x.Id == id);
            if(_case != null)
            {
                _context.Cases.Remove(_case);
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }
            return StatusCode(404);
        }
    }
}