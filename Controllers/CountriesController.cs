using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.Domain;
using HotelListing.API.Models.Country;
using HotelListing.API.Persistence;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;

        public CountriesController(HotelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryModel>>> GetCountries()
        {
            //refactoring to map our entities to the model
            //then pass it through it using AutoMapper
            var countries = await _context.Countries.ToListAsync();
            //mapping from list to list using .Map<List<Model>>
            var records = _mapper.Map<List<GetCountryModel>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.Include(q=>q.Hotels).FirstOrDefaultAsync(q=>q.Id==id);

            if (country == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<GetCountryDetailsModel>(country);
            return Ok(record);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryModel updateCountry)
        {
            if (id != updateCountry.Id)
            {
                return BadRequest();
            }

            //_context.Entry(country).State = EntityState.Modified;

            var country = await _context.Countries.FindAsync(id);

            if (!CountryExists(id))
            {
                return NotFound();
            }

            _mapper.Map(updateCountry, country);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryModel createdCountry)
        {
            //Refactoring our Input Without AutoMapper
            /*
            var country = new Country
            {
                Name = createdCountry.Name,
                ShortName = createdCountry.ShortName
            };*/

            //Refactoring our Input Using AutoMapper
            //note: we have to inject it into our constrator
            var  country=_mapper.Map<Country>(createdCountry);

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
