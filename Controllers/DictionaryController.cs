using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.Design;

[Route("api/dictionary")]
[ApiController]
public class DictionaryController: ControllerBase
{
    private IDictionaryService _dictionaryService;
    public DictionaryController(IDictionaryService dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }

    [HttpGet("speciality")]
    public async Task<ActionResult<GetDictionaryDTO<Speciality>>> GetSpeciality([FromBody] GetDictionaryListDTO data)
    {
        try
        {
            return await _dictionaryService.GetSpeciality(data);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Invalid value for attribute page")
            {
                return StatusCode(400, "Invalid value for attribute page");
            }
            return StatusCode(500, "InternalServerError");
        }
    }
    [HttpGet("icd10")]
    public async Task<ActionResult<GetDictionaryDTO<Icd10DTO>>> GetDiagnosis([FromBody] GetDictionaryListDTO data)
    {
        try
        {
            return await _dictionaryService.GetDiagnosis(data);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Invalid value for attribute page")
            {
                return StatusCode(400, "Invalid value for attribute page");
            }
            return StatusCode(500, "InternalServerError");
        }
    }
    [HttpGet("icd10/roots")]
    public async Task<ActionResult<List<Icd10DTO>>> GetRootDiagnosis()
    {
        try
        {
            return await _dictionaryService.GetRootDiagnosis();
        }
        catch
        {
            return StatusCode(500, "InternalServerError");
        }
    }
}
