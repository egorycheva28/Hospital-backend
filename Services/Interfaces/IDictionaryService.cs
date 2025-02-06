using System;

public interface IDictionaryService
{
    public Task<GetDictionaryDTO<Speciality>> GetSpeciality(GetDictionaryListDTO data);
    public Task<GetDictionaryDTO<Icd10DTO>> GetDiagnosis(GetDictionaryListDTO data);
    public Task<List<Icd10DTO>> GetRootDiagnosis();
}
