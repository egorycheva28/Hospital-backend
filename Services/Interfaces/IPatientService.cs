using Microsoft.AspNetCore.Mvc;
using System;

public interface IPatientService
{
    public Task<Guid> CreateNewPatient(CreatePatientDTO patient);
    public Task<PatientsListDTO> GetPatients(Guid userId,GetPatientsListDTO patients);
    public Task<Guid> CreateNewInspection(Guid idDoctor, Guid id, CreatInspectionDTO inspection);
    public Task<GetDictionaryDTO<InspectionPreviewDTO>> GetListInspections(Guid id, GetInspectionsDTO data);
    public Task<PatientCardDTO> GetPatientCard(Guid id);
    public Task<List<InspectionShortDTO>> GetBaseInspections(Guid id, RequestDTO request);
}
