using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.API.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.Domain;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;
using HotelListing.API.Persistence;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepo _hotelRepo;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepo hotelRepo,IMapper mapper)
        {
            _hotelRepo = hotelRepo;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelModel>>> GetHotels()
        {
            var hotels= await _hotelRepo.GetAllAsync();
            var records = _mapper.Map<List<Hotel>>(hotels);
            return Ok(records);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDetailsModel>> GetHotel(int id)
        {
            var hotel = await _hotelRepo.GetHotelAsync(id);
                //_hotelRepo.Hotels.Include(h=>h.Country).FirstOrDefaultAsync(h=>h.Id==id);

            if (hotel == null)
            {
                return NotFound();
            }
            var record = _mapper.Map<GetHotelDetailsModel>(hotel);
            
            return Ok(record);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelModel updateHotel)
        {
            if (id != updateHotel.Id)
            {
                return BadRequest();
            }

            var hotel =  await _hotelRepo.GetAsync(id);
            
            if (!await HotelExists(id))
                return NotFound(); 
            
            
            _mapper.Map(updateHotel,hotel);

            try
            {
                await _hotelRepo.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelModel CreatedHotel)
        {
            var hotel = _mapper.Map<Hotel>(CreatedHotel);
            //_hotelRepo.Hotels.Add(hotel);
            //await _hotelRepo.SaveChangesAsync();
            await _hotelRepo.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepo.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            //_hotelRepo.Hotels.Remove(hotel);
            //await _hotelRepo.SaveChangesAsync();
            await _hotelRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepo.Exists(id);
        }
    }
}
