using BulkyBookWeb.DTO;

namespace BulkyBookWeb.IService
{
    public interface IAuthenticationSer
    {
        Task<StatusDTO>RegistrationAsync(RegistrationDTO registrationDTO);
        Task<StatusDTO>LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
    }
}
