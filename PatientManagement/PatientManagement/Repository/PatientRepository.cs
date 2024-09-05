using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Data;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _context;
        private readonly IMapper _mapper;

        public PatientRepository(PatientContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PatientModel>> GetAllPatientsAsync()
        {
            var patientEntities = await _context.Patients.ToListAsync();
            return _mapper.Map<List<PatientModel>>(patientEntities);
        }

        public async Task<PatientModel> GetPatientByIdAsync(int patientId)
        {
            var patientEntity = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
            return _mapper.Map<PatientModel>(patientEntity);
        }

        public async Task<int> AddPatientAsync(PatientModel patientModel)
        {
            var newPatient = _mapper.Map<Patient>(patientModel);
            newPatient.CreatedDate = DateTime.Now;

            _context.Patients.Add(newPatient);
            await _context.SaveChangesAsync();

            return newPatient.Id;
        }

        public async Task UpdatePatientAsync(int patientId, PatientModel patientModel)
        {
            var existingPatient = await _context.Patients.FindAsync(patientId);
            if (existingPatient != null)
            {
                _mapper.Map(patientModel, existingPatient);
                existingPatient.UpdatedDate = DateTime.Now;

                _context.Patients.Update(existingPatient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePatientPatchAsync(int PatientId, JsonPatchDocument<PatientModel> patientModelPatch)
        {
            var existingPatient = await _context.Patients.FindAsync(PatientId);
            if (existingPatient != null)
            {
                var patientModel = _mapper.Map<PatientModel>(existingPatient);

                patientModelPatch.ApplyTo(patientModel);

                _mapper.Map(patientModel, existingPatient);

                existingPatient.UpdatedDate = DateTime.Now;

                _context.Patients.Update(existingPatient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePatientAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
