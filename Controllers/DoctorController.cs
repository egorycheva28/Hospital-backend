using System;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[Route("api/doctor")]
[ApiController]

public class DoctorController : ControllerBase
{
    private IDoctorService _doctorService;
    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> RegisterDoctor([FromBody] AddDoctorDTO newDoctor)
    {
        if (newDoctor == null)
        {
            return BadRequest("Invalid arguments");
        }

        try
        {
            string token = await _doctorService.AddDoctor(newDoctor);
            return token;
        }
        catch (Exception ex)
        {
            if (ex.Message == "Пользователь с такой почтой уже есть")
            {
                return BadRequest(ex.Message);
            }
            if (ex.Message == "Такой специальности нет в базе")
            {
                return BadRequest(ex.Message);
            }
            return StatusCode(500, "InternalServerError");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginDoctor([FromBody] LoginDoctorDTO doctor)
    {
        if (doctor == null)
        {
            return BadRequest("Invalid arguments");
        }

        try
        {
            string token = await _doctorService.LoginDoctor(doctor);
            return token;
        }
        catch (Exception ex)
        {
            if (ex.Message == "неверный логин или пароль")
            {
                return StatusCode(400, "Неверный логин или пароль");
            }
            return StatusCode(500, "InternalServerError");
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutDoctor()
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _doctorService.LogoutDoctor(token);
            return Ok("Success.");
        }
        catch
        {
            return StatusCode(500, "InternalServerError");
        }
    }

    [HttpGet("profile/{id}")]
    //[Authorize]
    public async Task<ActionResult<GetDoctorDTO>> GetDoctor(Guid id)
    {
        try
        {
            //var id = Guid.Parse(User.FindFirst("Id").Value);
            return await _doctorService.GetDoctor(id);
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

    [HttpPut("profile/{id}")]
    //[Authorize]
    public async Task<IActionResult> EditDoctor(Guid id, [FromBody] EditDoctorDTO newDoctor)
    {
        if (newDoctor == null)
        {
            return BadRequest();
        }

        try
        {
            //var id = Guid.Parse(User.FindFirst("Id").Value);
            await _doctorService.EditDoctor(id, newDoctor);
            return Ok("Success");
        }
        catch (Exception ex)
        {
            if (ex.Message == "Пользователь с такой почтой уже есть")
            {
                return BadRequest(ex.Message);
            }
            if (ex.Message == "Not Found")
            {
                return StatusCode(404, "Not Found");
            }
            return StatusCode(500, "InternalServerError");
        }
    }
}
