using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Models;
using PatientManagement.Repository;

namespace PatientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();
            if (patients == null || patients.Count == 0)
            {
                return NotFound();
            }
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById([FromRoute] int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientModel patientModel)
        {
            bool isDuplicate = await _patientRepository.PatientExistsByEmailAsync(patientModel.Email);

            if (isDuplicate)
            {
                return BadRequest("Patient with the same email already exists.");
            }
            else
            {
                var id = await _patientRepository.AddPatientAsync(patientModel);
                return CreatedAtAction(nameof(GetPatientById), new { id = id, controller = "patient" }, id);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientModel patientModel, [FromRoute] int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            await _patientRepository.UpdatePatientAsync(id, patientModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatientPatch([FromBody] JsonPatchDocument<PatientModel> patientModelPatch, [FromRoute] int id)
        {
            var existingPatient = await _patientRepository.GetPatientByIdAsync(id);

            if (existingPatient == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            await _patientRepository.DeletePatientAsync(id);
            return Ok();
        }
    }
}
