using System;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
//[Authorize]
[Route("api/patient")]
[ApiController]

public class PatientController : ControllerBase
{
    private IPatientService _patientService;
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPatient([FromBody] CreatePatientDTO patient)
    {
        if (patient == null)
        {
            return BadRequest("Invalid arguments");
        }

        try
        {
            var idPatient = await _patientService.CreateNewPatient(patient);
            return Ok("Patient was registered. Id: " + idPatient);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Дата рождения не должна быть позже сегодняшнего дня.")
            {
                return BadRequest("Дата рождения не должна быть позже сегодняшнего дня.");
            }
            return StatusCode(500, "InternalServerError");
        }
    }
    [HttpGet("patientsList/{userId}")]
    public async Task<ActionResult<PatientsListDTO>> GetPatients(Guid userId,[FromBody] GetPatientsListDTO data)
    {
        try
        {
            //var userId = Guid.Parse(User.FindFirst("Id").Value);
            var result = await _patientService.GetPatients(userId, data);
            return result;
        }
        catch (Exception ex)
        {
            if (ex.Message == "Invalid value for attribute page")
            {
                return BadRequest("Invalid value for attribute page");
            }
            return StatusCode(500, "InternalServerError.");
        }
    }
    [HttpPost("{id}/inspections/{idDoctor}")]
    public async Task<IActionResult> CreateNewInspection(Guid id, CreatInspectionDTO inspection, Guid idDoctor)
    {
        try
        {
            //var idDoctor = Guid.Parse(User.FindFirst("Id").Value);
            var idInspection = await _patientService.CreateNewInspection(idDoctor, id, inspection);
            return Ok("Success. Id: " + idInspection);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Дата осмотра не должна быть позже сегодняшнего дня.")
            {
                return BadRequest("Дата осмотра не должна быть позже сегодняшнего дня.");
            }
            if (ex.Message == "Осмотр обязательно должен иметь один диагноз с типом диагноза “Основной”.")
            {
                return BadRequest("Осмотр обязательно должен иметь один диагноз с типом диагноза “Основной”.");
            }
            if (ex.Message == "При выборе заключения “Болезнь”, необходимо указать дату и время следующего визита.")
            {
                return BadRequest("При выборе заключения “Болезнь”, необходимо указать дату и время следующего визита.");
            }
            if (ex.Message == "При выборе заключения “Смерть”, необходимо указать дату и время смерти.")
            {
                return BadRequest("При выборе заключения “Смерть”, необходимо указать дату и время смерти");
            }
            if (ex.Message == "Такое заключение нельзя.")
            {
                return BadRequest("Такое заключение нельзя.");
            }
            if (ex.Message == "Дата следующего осмотра должна быть позже даты данного осмотра.")
            {
                return BadRequest("Дата следующего осмотра должна быть позже даты данного осмотра.");
            }
            if (ex.Message == "Такого диагноза нет в базе")
            {
                return StatusCode(404, "Такого диагноза нет в базе");
            }
            if (ex.Message == "Такой специальности нет в базе")
            {
                return StatusCode(404, "Такой специальности нет в базе");
            }
            if (ex.Message == "Not Found patient")
            {
                return StatusCode(404, "Not Found patient");
            }
            if (ex.Message == "Not Found doctor")
            {
                return StatusCode(404, "Not Found doctor");
            }
            return StatusCode(500, "InternalServerError.");
        }
    }
    [HttpGet("{id}/inspections")]
    public async Task<ActionResult<GetDictionaryDTO<InspectionPreviewDTO>>> GetListInspections(Guid id, [FromBody] GetInspectionsDTO data)
    {
        try
        {
            return await _patientService.GetListInspections(id, data);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Invalid argument for subsets")
            {
                return StatusCode(400, "Invalid argument for subsets");
            }
            if (ex.Message == "Invalid value for attribute page")
            {
                return StatusCode(400, "Invalid value for attribute page");
            }
            if (ex.Message == "Такого диагноза нет в базе")
            {
                return StatusCode(400, "Такого диагноза нет в базе");
            }
            if (ex.Message == "Это не корневой диагноз")
            {
                return StatusCode(400, "Это не корневой диагноз");
            }
            if (ex.Message == "Not Found")
            {
                return StatusCode(404, "Patient not found");
            }
            return StatusCode(500, "InternalServerError");
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientCardDTO>> GetPatientCard(Guid id)
    {
        try
        {
            return await _patientService.GetPatientCard(id);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Not Found")
            {
                return StatusCode(404, "Not Found");
            }
            return StatusCode(500, "InternalServerError");
        }
    }
    [HttpGet("{id}/inspections/search")]
    public async Task<ActionResult<List<InspectionShortDTO>>> GetBaseInspections(Guid id, RequestDTO request)
    {
        try
        {
            return await _patientService.GetBaseInspections(id, request);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Patient not found")
            {
                return StatusCode(404, "Patient not found");
            }
            return StatusCode(500, "InternalServerError");
        }
    }
}
