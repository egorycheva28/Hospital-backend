using System;

public interface IDoctorService
{
	public Task<string> AddDoctor(AddDoctorDTO doctor);
	public Task<string> LoginDoctor(LoginDoctorDTO doctor);
	public Task LogoutDoctor(string token);
    public Task<GetDoctorDTO> GetDoctor(Guid id);
    public Task EditDoctor(Guid id, EditDoctorDTO doctor);
}
