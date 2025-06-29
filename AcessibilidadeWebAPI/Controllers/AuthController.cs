using AcessibilidadeWebAPI.Models.Auth;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Repositorios.Dispositivos;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AcessibilidadeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IVoluntarioRepositorio _voluntarioRepositorio;
        private readonly IDeficienteRepositorio _deficienteRepositorio;
        private readonly IDispositivoRepositorio _dispositivoRepositorio;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, IUsuarioRepositorio usuarioRepositorio,
            IVoluntarioRepositorio voluntarioRepositorio, IDeficienteRepositorio deficienteRepositorio, IMapper mapper, IDispositivoRepositorio dispositivoRepositorio)
        {
            _configuration = configuration;
            _usuarioRepositorio = usuarioRepositorio;
            _voluntarioRepositorio = voluntarioRepositorio;
            _deficienteRepositorio = deficienteRepositorio;
            _mapper = mapper;
            _dispositivoRepositorio = dispositivoRepositorio;
        }

        /// <summary>
        /// Login do usuário
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                // Buscar usuário por email
                Entidades.Usuario usuario = await _usuarioRepositorio.ObterUsuarioPorEmail(loginRequest.Email);

                if (usuario == null || usuario.Senha != loginRequest.Senha)
                {
                    return Unauthorized(new { message = "Credenciais inválidas" });
                }

                // Gerar token JWT
                string token = GerarTokenJWT(usuario.IdUsuario, usuario.Email, usuario.Nome);

                // Criar UsuarioInfo completo com informações específicas
                UsuarioInfo usuarioInfo = await CriarUsuarioInfoCompleto(usuario);

                return Ok(new LoginResponse
                {
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddHours(24),
                    Usuario = usuarioInfo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Registro de novo usuário
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                // Verificar se usuário já existe
                Entidades.Usuario usuarioExistente = await _usuarioRepositorio.ObterUsuarioPorEmail(registerRequest.Email);
                if (usuarioExistente != null)
                {
                    return BadRequest(new { message = "Usuário já existe com este email" });
                }

                // Validar campos específicos baseado no tipo de usuário
                if (registerRequest.TipoUsuario == TipoUsuario.Deficiente && !registerRequest.TipoDeficiencia.HasValue)
                {
                    return BadRequest(new { message = "Tipo de deficiência é obrigatório para usuários deficientes" });
                }

                // Criar novo usuário
                Entidades.Usuario novoUsuario = new Entidades.Usuario
                {
                    Nome = registerRequest.Nome,
                    Email = registerRequest.Email,
                    Telefone = registerRequest.Telefone,
                    Senha = registerRequest.Senha // Em produção, usar hash da senha
                };

                await _usuarioRepositorio.InserirUsuario(novoUsuario);

                // Criar registro específico baseado no tipo de usuário
                await CriarRegistroEspecifico(novoUsuario.IdUsuario, registerRequest);

                Entidades.Dispositivo dispositivo = new Entidades.Dispositivo
                {
                    DataRegistro = DateTime.UtcNow,
                    NumeroSerie = registerRequest.NumeroSerie,
                    UsuarioProprietarioId = novoUsuario.IdUsuario
                };

                _dispositivoRepositorio.Inserir(dispositivo);

                // Gerar token JWT
                string token = GerarTokenJWT(novoUsuario.IdUsuario, novoUsuario.Email, novoUsuario.Nome);

                // Criar UsuarioInfo completo com informações específicas
                UsuarioInfo usuarioInfo = await CriarUsuarioInfoCompleto(novoUsuario);
                usuarioInfo.TipoUsuario = registerRequest.TipoUsuario.ToString();

                return Ok(new LoginResponse
                {
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddHours(24),
                    Usuario = usuarioInfo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Obter informações do usuário autenticado
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UsuarioInfo>> GetCurrentUser()
        {
            try
            {
                Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);
                Entidades.Usuario usuario = await _usuarioRepositorio.ObterUsuario(userId);

                if (usuario == null)
                {
                    return NotFound();
                }

                // Criar UsuarioInfo completo com informações específicas
                UsuarioInfo usuarioInfo = await CriarUsuarioInfoCompleto(usuario);

                return Ok(usuarioInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Obter informações de um usuário específico por ID
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns></returns>
        [HttpGet("usuario/{userId}")]
        public async Task<ActionResult<UsuarioInfo>> GetUserById(int userId)
        {
            try
            {
                Entidades.Usuario usuario = await _usuarioRepositorio.ObterUsuario(userId);

                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                // Criar UsuarioInfo completo com informações específicas
                UsuarioInfo usuarioInfo = await CriarUsuarioInfoCompleto(usuario);

                return Ok(usuarioInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar perfil do usuário autenticado
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileRequest updateRequest)
        {
            try
            {
                Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);
                Entidades.Usuario usuario = await _usuarioRepositorio.ObterUsuario(userId);

                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                // Atualizar dados básicos
                usuario.Nome = updateRequest.Nome ?? usuario.Nome;
                usuario.Email = updateRequest.Email ?? usuario.Email;
                usuario.Telefone = updateRequest.Telefone ?? usuario.Telefone;

                if (!string.IsNullOrEmpty(updateRequest.NovaSenha))
                {
                    // TODO: Implementar hash da senha em produção
                    usuario.Senha = updateRequest.NovaSenha;
                }

                _usuarioRepositorio.Editar(usuario);

                return Ok(new { message = "Perfil atualizado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Excluir conta do usuário autenticado
        /// </summary>
        /// <returns></returns>
        [HttpDelete("account")]
        [Authorize]
        public async Task<ActionResult> DeleteAccount()
        {
            try
            {
                Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);

                // TODO: Implementar deleção em cascata dos perfis específicos
                _usuarioRepositorio.Deletar(userId);

                return Ok(new { message = "Conta excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        private string GerarTokenJWT(int userId, string email, string nome)
        {
            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");
            string? secretKey = jwtSettings["SecretKey"];
            string? issuer = jwtSettings["Issuer"];
            string? audience = jwtSettings["Audience"];

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task CriarRegistroEspecifico(int idUsuario, RegisterRequest registerRequest)
        {
            if (registerRequest.TipoUsuario == TipoUsuario.Voluntario)
            {
                Entidades.Voluntario voluntario = new Entidades.Voluntario
                {
                    IdUsuario = idUsuario,
                    Disponivel = registerRequest.Disponivel ?? true,
                    Avaliacao = 0, // Avaliação inicial
                };

                _voluntarioRepositorio.Inserir(voluntario);
            }
            else if (registerRequest.TipoUsuario == TipoUsuario.Deficiente)
            {
                Entidades.Deficiente deficiente = new Entidades.Deficiente
                {
                    IdUsuario = idUsuario,
                    TipoDeficiencia = (Models.Auth.TipoDeficiencia)registerRequest.TipoDeficiencia!.Value
                };

                _deficienteRepositorio.Inserir(deficiente);
            }
        }

        private async Task<UsuarioInfo> CriarUsuarioInfoCompleto(Entidades.Usuario usuario)
        {
            // Mapear usuário básico
            UsuarioInfo usuarioInfo = _mapper.Map<UsuarioInfo>(usuario);

            // Verificar se é voluntário
            Entidades.Voluntario? voluntario = _voluntarioRepositorio.Listar(v => v.IdUsuario == usuario.IdUsuario).FirstOrDefault();
            if (voluntario != null)
            {
                usuarioInfo.TipoUsuario = "Voluntario";
                usuarioInfo.Voluntario = _mapper.Map<VoluntarioInfo>(voluntario);
            }

            // Verificar se é deficiente
            Entidades.Deficiente? deficiente = _deficienteRepositorio.Listar(d => d.IdUsuario == usuario.IdUsuario).FirstOrDefault();
            if (deficiente != null)
            {
                usuarioInfo.TipoUsuario = "Deficiente";
                usuarioInfo.Deficiente = _mapper.Map<DeficienteInfo>(deficiente);
            }

            return usuarioInfo;
        }
    }
}