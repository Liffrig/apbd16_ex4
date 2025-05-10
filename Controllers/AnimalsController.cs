using apbd16_ex4.Data;
using apbd16_ex4.Model;
using Microsoft.AspNetCore.Mvc;

namespace apbd16_ex4.Controllers {

    //api/animals -> [controller] = Animals
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase {

        // api/animals
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Animal>> GetAnimals() {
            return StatusCode(StatusCodes.Status200OK, Database.Animals);
        }


        // api/animals/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Animal> GetAnimal(int id) {
            var animal = Database.Animals.FirstOrDefault(a => a.Id == id);
            return animal == null
                ? StatusCode(StatusCodes.Status404NotFound, new { message = "Animal not found" })
                : StatusCode(StatusCodes.Status200OK, animal);
        }

        // POST api/animals
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        public ActionResult<Animal> CreateAnimal([FromBody] Animal animal) {
            animal.Id = Database.GetNextAnimalId();
            Database.Animals.Add(animal);

            return StatusCode(StatusCodes.Status201Created, animal);
        }

        // PUT api/animals/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public IActionResult UpdateAnimal(int id, [FromBody] Animal updatedAnimal) {
            var animal = Database.Animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Animal not found" });
            }

            updatedAnimal.Id = id;

            Database.Animals.Remove(animal);
            Database.Animals.Add(updatedAnimal);

            return StatusCode(StatusCodes.Status204NoContent);
        }


        // DELETE api/animals/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAnimal(int id) {
            var animal = Database.Animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Animal not found" });
            }

            Database.Animals.Remove(animal);

            // Remove related visits
            Database.Visits.RemoveAll(v => v.AnimalId == id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
        
        // GET api/animals/search/{name}
        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Animal>> SearchAnimalsByName(string name) {
            var animals = Database.Animals.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return StatusCode(StatusCodes.Status200OK, animals);
        }

    }

}