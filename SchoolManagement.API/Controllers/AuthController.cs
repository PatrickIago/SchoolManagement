using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DTOs.Login;
using SchoolManagement.Models;
using SchoolManagement.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolManagement.Controllers;

/// <summary>
/// Controlador para autenticação e gerenciamento de tokens.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    /// <summary>
    /// Registra um novo usuário.
    /// </summary>
    /// <param name="registerDto">Dados do novo usuário.</param>
    /// <returns>Confirmação de registro.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new ApplicationUser
        {
            UserName = registerDto.UserName
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new Response { Status = "Success", Message = "Usuário registrado com sucesso" });
    }

    /// <summary>
    /// Faz login e gera tokens de acesso e refresh.
    /// </summary>
    /// <param name="loginDto">Dados de login.</param>
    /// <returns>Tokens de acesso e refresh.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
            return Unauthorized(new Response { Status = "Error", Message = "Tentativa de login inválida" });

        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var accessToken = _tokenService.GenerateAcessToken(claims, _configuration);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        return Ok(new TokenModelDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken = refreshToken
        });
    }

    /// <summary>
    /// Atualiza o token de acesso usando o token de refresh.
    /// </summary>
    /// <param name="tokenModelDto">Modelo de token contendo o token de refresh.</param>
    /// <returns>Novos tokens de acesso e refresh.</returns>
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken([FromBody] TokenModelDto tokenModelDto)
    {
        if (tokenModelDto == null || string.IsNullOrEmpty(tokenModelDto.RefreshToken))
            return BadRequest(new Response { Status = "Error", Message = "Solicitação de cliente inválida" });

        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenModelDto.AccessToken, _configuration);

        var userName = principal.Identity.Name;
        var user = _userManager.FindByNameAsync(userName).Result;

        if (user == null || user.RefreshToken != tokenModelDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest(new Response { Status = "Error", Message = "Token de refresh inválido" });

        var newAccessToken = _tokenService.GenerateAcessToken(principal.Claims, _configuration);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        _userManager.UpdateAsync(user).Wait();

        return Ok(new TokenModelDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken
        });
    }
}