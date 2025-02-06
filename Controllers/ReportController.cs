using Microsoft.AspNetCore.Mvc;
using System;

[Route("api/report")]
[ApiController]
public class ReportController: ControllerBase
{
    private IReportService _reportService;
    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("icdrootsreport")]
    public async Task<ActionResult<IcdRootReportModel>> GetReport (IcdRootsReportFiltersModel data)
    {
        if (data == null)
        {
            return BadRequest("Invalid arguments");
        }

        try
        {
            return await _reportService.GetReport(data);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Some fields in request are invalid")
            {
                return BadRequest("Some fields in request are invalid");
            }
            return StatusCode(500, "InternalServerError");
        }
    }
}
