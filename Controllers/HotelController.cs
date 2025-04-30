using HotelListing.API.Core.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private static List<Hotel> hotels=new List<Hotel>
        {
            new Hotel{Id=1,Name = "Grand Plaza",Address = "123 main St",Rating = 3.9},
            new Hotel{ Id = 2,Name = "Ocean View", Address = "456 Beach Rd",Rating = 4.6}
        };
        // GET: api/<HotelController>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get()
        {
            return Ok(hotels);
        }

        // GET api/<HotelController>/5
        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            var hotel= hotels.Find(i => i.Id == id);
            if (hotel==null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // POST api/<HotelController>
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel newHotel)
        {
            if (hotels.Any(h=>h.Id== newHotel.Id))
            {
                return BadRequest(new {message="Hotel with this id already exists"});
            }
            hotels.Add(newHotel);
            return CreatedAtAction(nameof(Get), new { Id = newHotel.Id }, newHotel);
        }

        // PUT api/<HotelController>/5
        [HttpPut("{id}")]
        public ActionResult<Hotel> Put(int id, [FromBody]Hotel updatedHotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel==null)
            {
                return NotFound(new { message = "Hotel with this id doesnt exist" });
            }

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;
            existingHotel.Rating= updatedHotel.Rating;
            return NoContent();
        }

        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public ActionResult<Hotel> Delete(int id)
        {
            var Hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (Hotel == null)
            {
                return NotFound(new { message = "Hotel with this id doesnt exist" });
            }

            hotels.Remove(Hotel);
            return NoContent();

        }
    }

