using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.Domain;
using HotelListing.API.Core.Repository;
using HotelListing.API.Models.Country;
using HotelListing.API.Persistence;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepo _countryRepo;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepo countryRepo, IMapper mapper)
        {
            _countryRepo = countryRepo;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryModel>>> GetCountries()
        {
            //refactoring to map our entities to the model
            //then pass it through it using AutoMapper
            var countries = await _countryRepo.GetAllAsync();
            //mapping from list to list using .Map<List<Model>>
            var records = _mapper.Map<List<GetCountryModel>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _countryRepo.GetDetails(id);

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

            var country = await _countryRepo.GetAsync(id);

            if (!await CountryExists(id))
            {
                return NotFound();
            }

            _mapper.Map(updateCountry, country);


            try
            {
                await _countryRepo.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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

            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();

            await _countryRepo.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryRepo.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countryRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countryRepo.Exists(id);
        }
    }
}
