using PatientManagement.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientManagement.Repository
{
    public interface IPatientRepository
    {
        Task<List<PatientModel>> GetAllPatientsAsync();
        Task<PatientModel> GetPatientByIdAsync(int patientId);
        Task<int> AddPatientAsync(PatientModel patientModel);
        Task UpdatePatientAsync(int patientId, PatientModel patientModel);
        Task UpdatePatientPatchAsync(int PatientId, JsonPatchDocument<PatientModel> patientModelPatch);

        Task DeletePatientAsync(int patientId);
        Task<bool> PatientExistsByEmailAsync(string email);
    }
}
