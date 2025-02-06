using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RTools_NTS.Util;
using System.ComponentModel.DataAnnotations;

public class DoctorService : IDoctorService
{
    private readonly Context _context;
    private readonly Configurations _configuration;
    public DoctorService(Context context, Configurations configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    public async Task<string> AddDoctor(AddDoctorDTO doctor)
    {
        var user = await _context.Doctors.SingleOrDefaultAsync(u => u.Email == doctor.Email);
        if (user != null)
        {
            throw new Exception("Пользователь с такой почтой уже есть");
        }

        var speciality = await _context.Specialties.FindAsync(doctor.Speciality);
        if (speciality == null)
        {
            throw new Exception("Такой специальности нет в базе");
        }

        var newDoctor = new Doctor
        {
            Name = doctor.Name,
            Birthday = doctor.Birthday,
            Email = doctor.Email,
            PhoneNumber = doctor.PhoneNumber,
            Gen = doctor.Gen,
            Password = PasswordHash(doctor.Password),
            Speciality = doctor.Speciality
        };

        await _context.Doctors.AddAsync(newDoctor);
        await _context.SaveChangesAsync();

        return GenerateToken(newDoctor);
    }
    public async Task<string> LoginDoctor(LoginDoctorDTO doctor)
    {
        var user = await _context.Doctors.SingleOrDefaultAsync(u => u.Email == doctor.Email);
        if (user == null || (PasswordHash(doctor.Password) != user.Password))
        {
            throw new Exception("неверный логин или пароль");
        }

        return GenerateToken(user);
    }
    public async Task LogoutDoctor(string token)
    {
        //удалить токен
        var banToken = new Token
        {
            Tokens = token
        };
        _context.Tokens.Add(banToken);
        await _context.SaveChangesAsync();
    }
    public async Task<GetDoctorDTO> GetDoctor(Guid id)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
        {
            throw new Exception("Not Found");
        }

        var getDoctor = new GetDoctorDTO
        {
            Id=doctor.Id,
            CreateTime=doctor.CreateTime,
            Name=doctor.Name,
            Birthday=doctor.Birthday,
            Gen=doctor.Gen, 
            Email=doctor.Email,
            PhoneNumber=doctor.PhoneNumber
        };
        return getDoctor;
    }
    
    public async Task EditDoctor(Guid id, EditDoctorDTO doctor)
    {
        var editDoctor = await _context.Doctors.FindAsync(id);
        if (editDoctor == null)
        {
            throw new Exception("Not Found");
        }

        var user = await _context.Doctors.SingleOrDefaultAsync(u => u.Email == doctor.Email);
        if (user != null)
        {
            throw new Exception("Пользователь с такой почтой уже есть");
        }

        editDoctor.Name = doctor.Name;
        editDoctor.Email = doctor.Email;
        editDoctor.PhoneNumber = doctor.PhoneNumber;
        editDoctor.Gen = doctor.Gen;
        editDoctor.Birthday = doctor.Birthday;

        await _context.SaveChangesAsync();
    }
    private string GenerateToken(Doctor user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Id",user.Id.ToString()),
            new Claim("Name",user.Name),
            new Claim("Email",user.Email)
        }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _configuration.Issuer,
            Audience = _configuration.Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private string PasswordHash(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}


