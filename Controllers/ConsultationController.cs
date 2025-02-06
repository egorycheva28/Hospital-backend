using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

[Route("api/consultation")]
[ApiController]
//[Authorize]
public class ConsultationController: ControllerBase
{
    private IConsultationService _consultationService;
    public ConsultationController(IConsultationService consultationService)
    {
        _consultationService = consultationService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<GetDictionaryDTO<InspectionPreviewDTO>>> GetFullInspection(Guid userId, GetInspectionsDTO data)
    {
        try
        {
            var list = await _consultationService.GetListInspectionConsultation(userId, data);
            return list;
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
            if (ex.Message == "Это не корневой диагноз")
            {
                return StatusCode(400, "Это не корневой диагноз");
            }
            if (ex.Message == "Такого диагноза нет в базе")
            {
                return StatusCode(404, "Not Found");
            }
            return StatusCode(500, "InternalServerError.");
        }
    }

    [HttpGet("consultation/{id}")]
    public async Task<ActionResult<Consultation>> GetConsultation(Guid id)
    {
        try
        {
            var consultation = await _consultationService.GetConsultation(id);
            return consultation;
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

    [HttpPost("{id}/comment/{userId}")]
    public async Task<ActionResult<Guid>> AddComment(Guid id, AddCommentDTO comment, Guid userId)
    {
        if(comment == null)
        {
            return BadRequest("Invalid arguments");
        }

        try
        {
            //var userId = Guid.Parse(User.FindFirst("Id").Value);
            var idComment=await _consultationService.AddComment(userId, id, comment);
            return Ok("Success. Id: "+ idComment);
        }
        catch (Exception ex)
        {
            if (ex.Message == "User is not the author of the comment")
            {
                return StatusCode(403, "User doesn't have add comment to consultation (unsuitable specialty and not the inspection author)");
            }
            if (ex.Message == "Comment not found")
            {
                return StatusCode(403, "Comment not found");
            }
            if (ex.Message == "Not Found")
            {
                return StatusCode(404, "Consultation or parent comment not found");
            }
            return StatusCode(500, "InternalServerError.");
        }
    }
    [HttpPut("comment/{id}/{userId}")]
    public async Task<IActionResult> EditComment(Guid id, EditCommentDTO editComment, Guid userId)
    {
        if (editComment == null)
        {
            return BadRequest("Invalid arguments");
        }

        try
        {
            //var userId = Guid.Parse(User.FindFirst("Id").Value);
            await _consultationService.EditComment(userId, id, editComment);
            return Ok("Success");
        }
        catch (Exception ex)
        {
            if (ex.Message == "User is not the author of the comment")
            {
                return StatusCode(403, "User is not the author of the comment");
            }
            if (ex.Message == "Not Found")
            {
                return StatusCode(404, "Comment not found");
            }
            return StatusCode(500, "InternalServerError.");
        }
    }
}
