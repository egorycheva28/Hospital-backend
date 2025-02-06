using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;

[Route("api/inspection")]
[ApiController]
//[Authorize]
public class InspectionController: ControllerBase
{
    private IInspectionService _inspectionService;
    public InspectionController(IInspectionService inspectionService)
    {
        _inspectionService = inspectionService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CopyInspection>> GetFullInspection(Guid id)
    {
        try
        {
            var inspection = await _inspectionService.GetFullInspection(id);
            return inspection;
        }
        catch (Exception ex)
        {
            if (ex.Message == "Patient Not Found")
            {
                return StatusCode(404, "Patient Not Found");
            }
            if (ex.Message == "Doctor Not Found")
            {
                return StatusCode(404, "Doctor Not Found");
            }
            if (ex.Message == "Inspection Not Found")
            {
                return StatusCode(404, "Inspection Not Found");
            }
            return StatusCode(500, "InternalServerError.");
        }
    }
    [HttpPut("{id}/{userId}")]
    public async Task<IActionResult> EditInspection(Guid id, EditInspectionDTO editInspection, Guid userId)
    {
        if (editInspection == null)
        {
            return BadRequest("Invalid arguments");
        }

        try
        {
            //var userId = Guid.Parse(User.FindFirst("Id").Value);
            await _inspectionService.EditInspection(userId, id, editInspection);
            return Ok("Success");
        }
        catch (Exception ex)
        {
            if (ex.Message == "User doesn't have editing rights (not the inspection author)")
            {
                return StatusCode(403, "User doesn't have editing rights (not the inspection author)");
            }
            if (ex.Message == "Not Found")
            {
                return StatusCode(404, "Inspection not found");
            }
            return StatusCode(500, "InternalServerError.");
        }

    }
    [HttpGet("{id}/chain")]
    public async Task<ActionResult<List<InspectionPreviewDTO>>> GetInspectionChain(Guid id)
    {
        try
        {
            var answer = await _inspectionService.GetInspectionChain(id);
            return answer;
        }
        catch (Exception ex)
        {
            if (ex.Message == "Это не рутовый осмотр")
            {
                return BadRequest("Это не рутовый осмотр");
            }
            if (ex.Message == "Not Found")
            {
                return StatusCode(404, "Not Found");
            }
            return StatusCode(500, "InternalServerError.");
        }
    }
}
