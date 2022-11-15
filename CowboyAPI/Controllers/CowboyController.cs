using AutoMapper;
using CowboyAPI.DataRepos;
using CowboyAPI.DtoMapperProfiles;
using CowboyAPI.Dtos;
using CowboyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using CowboyAPI.Helpers;

namespace CowboyAPI.Controllers
{
    //api/Cowboys
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CowboysController : ControllerBase
    {
        private readonly ICowboyAPIRepo _cowboyAPIRepository;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        //Inject dependencies via constructor
        public CowboysController(ICowboyAPIRepo cowboyAPIRepository, IHttpClientFactory httpClientFactory, 
            IMapper mapper)
        {
            _cowboyAPIRepository = cowboyAPIRepository;
            _httpClient = httpClientFactory.CreateClient("FakeNameAPI");
            _mapper = mapper;
        }

  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CowboyFetchRequestDto>> CreateCowboys(GunDetailsRequestDto req)
        {
            // Make a call to fetch personal details for cowboy
            var response = await FakeNameAPICommand.FakeNameAPIExecute(_httpClient);

            //Create a Cowboy entity to add in database
            if (response == null) return null;

            CowboyCreateRequestDto request = new CowboyCreateRequestDto();

            request.Name = response.name;
            request.DateOfBirth = response.birth_data;
            request.longitude = response.longitude;
            request.latitude = response.latitude;
            request.Hair = response.hair;
            request.Height = response.height;

            DateTime dateOfBirth = DateTime.Now;
            if(response.birth_data != null && DateTime.TryParse(response.birth_data,out dateOfBirth))
            {
                request.Age = FakeNameAPICommand.CalculateAge(dateOfBirth);
            }

            // Load Gun Serial Id
            request.GunSerialId = Guid.NewGuid();
            request.GunName = req.GunName;
            request.MaxBullets = req.MaxBullets;
            request.BulletsLeftOver = req.BulletsLeftOver;

            if (request == null || string.IsNullOrEmpty(request.GunName) 
                || string.IsNullOrEmpty(response.name)) return BadRequest();

            var cowBoyToAdd = _mapper.Map<Cowboy>(request);
            await _cowboyAPIRepository.AddCowboy(cowBoyToAdd);
            await _cowboyAPIRepository.SaveChanges();

            var cowBoyAdded = _mapper.Map<CowboyFetchRequestDto>(cowBoyToAdd);

            return CreatedAtRoute(nameof(GetCowboysById), new {id = cowBoyAdded.Id }, cowBoyAdded);   
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCowboys(int id, JsonPatchDocument<CowboyUpdateRequestDto> patchToDoc)
        {
            if(!_cowboyAPIRepository.GetCowboy(id , out Cowboy? cowboyFromDb)) return NotFound();

            var cowBoyToPatch = _mapper.Map<CowboyUpdateRequestDto>(cowboyFromDb);
            patchToDoc.ApplyTo(cowBoyToPatch,ModelState);

            if(!TryValidateModel(cowBoyToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(cowBoyToPatch, cowboyFromDb);
            if(cowboyFromDb != null)
            await _cowboyAPIRepository.UpdateCowboy(cowboyFromDb);
            await _cowboyAPIRepository.SaveChanges();

            return NoContent();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Cowboy>> GetAllCowboys()
        {
            var cowboysListe = _cowboyAPIRepository.GetAllCowboys();
            return Ok(cowboysListe);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetCowboysById(int id)
        {
            if (!_cowboyAPIRepository.GetCowboy(id, out Cowboy? resultObject)) return NotFound();
            return Ok(resultObject);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult DeleteCowboys(int id)
        {
            if(!_cowboyAPIRepository.GetCowboy(id, out Cowboy? cowboyToDelete)) return NotFound();

            _cowboyAPIRepository.DeleteCowboy(cowboyToDelete);
            _cowboyAPIRepository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ShootWithGun([FromRoute] int id , [FromBody] JsonPatchDocument<CowboyUpdateRequestDto> patchToDoc)
        {
            if (!_cowboyAPIRepository.GetCowboy(id, out Cowboy? cowboyFromDb)) return NotFound();

            var cowBoyToPatch = _mapper.Map<CowboyUpdateRequestDto>(cowboyFromDb);
            patchToDoc.ApplyTo(cowBoyToPatch, ModelState);

            if (!TryValidateModel(cowBoyToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(cowBoyToPatch, cowboyFromDb);
            if(cowboyFromDb != null)
                await _cowboyAPIRepository.UpdateCowboy(cowboyFromDb);
            await _cowboyAPIRepository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult ReloadGun([FromRoute] int id,[FromBody] JsonPatchDocument<CowboyUpdateRequestDto> patchDoc)
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult CalculateDistanceBtwCowboys([FromQuery] int cbId01, [FromQuery] int cbId02)
        {
            if (cbId01 < 0 || cbId02 < 0) return BadRequest(string.Format("Error Occurred. Please provide correct id " +
                "values for {0} & {1}", nameof(cbId01), nameof(cbId02)));

            _cowboyAPIRepository.GetCowboy(cbId01, out Cowboy? cb1);
            _cowboyAPIRepository.GetCowboy(cbId02, out Cowboy? cb2);

            if (!(cb1 != null & cb2 != null)) return BadRequest("Object not found.Please provide correct id's.");
            return Ok(string.Format("Distance calculated between two cowboys (in metres) is {0}", _cowboyAPIRepository.CalculateDistance(cb1, cb2)));
        }

        [HttpGet]
        public ActionResult CombatBtwCowboys([FromQuery] int cbId01, [FromQuery] int cbId02)
        {
            if (cbId01 < 0 || cbId02 < 0) return BadRequest(string.Format("Error Occurred. Please provide correct id " +
             "values for {0} & {1}", nameof(cbId01), nameof(cbId02)));

            _cowboyAPIRepository.GetCowboy(cbId01, out Cowboy? cb1);
            _cowboyAPIRepository.GetCowboy(cbId02, out Cowboy? cb2);

            if (!(cb1 != null & cb2 != null)) return BadRequest("Object not found.Please provide correct id's.");
            return Ok(string.Format("Winner of the combat between two cowboys is {0}", _cowboyAPIRepository.CombatBtwCowboys(cb1, cb2)));
        }


    }
}
