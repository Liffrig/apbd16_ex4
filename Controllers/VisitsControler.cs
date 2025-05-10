using apbd16_ex4.Data;
using apbd16_ex4.Model;
using Microsoft.AspNetCore.Mvc;

namespace apbd16_ex4.Controllers;
    
    [ApiController]
    [Route("api/animals/{animalId}/[controller]")]
    [Produces("application/json")]
    public class VisitsController : ControllerBase {
        // GET api/animals/{animalId}/visits
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Visit>> GetVisitsForAnimal(int animalId) {
            var animal = Database.Animals.FirstOrDefault(a => a.Id == animalId);
            if (animal == null) {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Animal not found" });
            }

            var visits = Database.Visits.Where(v => v.AnimalId == animalId).ToList();
            return StatusCode(StatusCodes.Status200OK, visits);
        }

        // POST api/animals/{animalId}/visits
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public ActionResult<Visit> CreateVisit(int animalId, [FromBody] Visit visit) {
            var animal = Database.Animals.FirstOrDefault(a => a.Id == animalId);
            if (animal == null) {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Animal not found" });
            }

            visit.Id = Database.GetNextVisitId();
            visit.AnimalId = animalId;

            Database.Visits.Add(visit);

            return StatusCode(StatusCodes.Status201Created, visit);
        }

        // GET api/animals/{animalId}/visits/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Visit> GetVisit(int animalId, int id) {
            var animal = Database.Animals.FirstOrDefault(a => a.Id == animalId);
            if (animal == null) {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Animal not found" });
            }

            var visit = Database.Visits.FirstOrDefault(v => v.Id == id && v.AnimalId == animalId);
            if (visit == null) {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Visit not found" });
            }

            return StatusCode(StatusCodes.Status200OK, visit);
        }
    }