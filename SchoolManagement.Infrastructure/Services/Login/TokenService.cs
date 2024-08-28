using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.Services;
public class TokenService : ITokenService
{
    // Método para gerar um token de acesso
    public JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration _config)
    {
        // Obtém a chave secreta da configuração ou lança uma exceção se não for encontrada
        var key = _config.GetSection("JWT").GetValue<string>("SecretKey")
                  ?? throw new ArgumentNullException("Invalid secret Key");

        // Converte a chave secreta em bytes
        var privateKey = Encoding.UTF8.GetBytes(key);

        // Cria credenciais de assinatura usando a chave secreta
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey),
                                    SecurityAlgorithms.HmacSha256Signature);

        // Cria um manipulador de tokens JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        // Define as propriedades do token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), // Define as claims do token
            Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")), // Define a expiração do token
            Audience = _config.GetSection("JWT").GetValue<string>("ValidAudience"), // Define a audiência válida
            Issuer = _config.GetSection("JWT").GetValue<string>("ValidIssuer"), // Define o emissor válido
            SigningCredentials = signingCredentials // Define as credenciais de assinatura
        };

        // Cria o token com base nas propriedades definidas
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Retorna o token gerado
        return (JwtSecurityToken)token;
    }

    // Método para gerar um token de atualização (refresh token)
    public string GenerateRefreshToken()
    {
        // Cria um array de bytes criptograficamente seguro para o token de atualização
        var secureteRandomBytes = new byte[128];

        // Gera bytes aleatórios usando um gerador de números aleatórios seguro
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(secureteRandomBytes);

        // Converte os bytes aleatórios em uma string Base64
        var refreshToken = Convert.ToBase64String(secureteRandomBytes);

        // Retorna o token de atualização gerado
        return refreshToken;
    }

    // Método para obter as claims de um token expirado
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config)
    {
        // Obtém a chave secreta da configuração ou lança uma exceção se não for encontrada
        var secretKey = _config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid key");

        // Define os parâmetros de validação do token
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, // Não valida a audiência
            ValidateIssuer = false, // Não valida o emissor
            ValidateIssuerSigningKey = true, // Valida a chave de assinatura do emissor
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), // Define a chave de assinatura
            ValidateLifetime = false // Não valida a expiração do token
        };

        // Cria um manipulador de tokens JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        // Valida o token e obtém as claims principais (principal)
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        // Verifica se o token é um JWT e se o algoritmo de assinatura é HmacSha256
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token"); // Lança uma exceção se o token for inválido
        }

        // Retorna as claims principais (principal)
        return principal;
    }
}