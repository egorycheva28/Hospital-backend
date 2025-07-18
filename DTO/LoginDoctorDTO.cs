﻿using System;
using System.ComponentModel.DataAnnotations;

public class LoginDoctorDTO
{
    [Required]
    [MinLength(1)]
    public string Email { get; set; }
    [Required]
    [MinLength(1)]
    public string Password { get; set; }
}
