using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Db.API.Controllers
{
  /// <summary>
  /// REST API Controller for authentication
  /// </summary>
  [ApiController]
  [EnableCors("AllowAll")]
  [Route("[controller]")]
  public sealed class AuthController
    : ControllerBase
  {
    #region Fields

    private readonly IConfiguration m_configuration;
    private readonly SignInManager<IdentityUser> m_signInManager;
    private readonly UserManager<IdentityUser> m_userManager;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="signInManager">Injected sign in manager</param>
    /// <param name="userManager">Injected user manager instance</param>
    /// <param name="configuration">Injected web service configuration</param>
    public AuthController(IConfiguration configuration, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
      m_configuration = configuration;
      m_signInManager = signInManager;
      m_userManager = userManager;
    }

    #region API Methods

    /// <summary>
    /// Logs in a given user
    /// </summary>
    /// <param name="userData">Data of the user to sign in</param>
    [HttpPost(nameof(SignInUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> SignInUser([FromBody] Models.REST.Auth.LoginRequest userData)
    {
      var result = await m_signInManager.PasswordSignInAsync(userData.Email, userData.Password, userData.RememberMe, false).ConfigureAwait(false);

      if (!result.Succeeded)
        return BadRequest();

      var claims = new[]
      {
        new Claim(ClaimTypes.Name, userData.Email)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_configuration["JwtSecurityKey"]));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expiry = DateTime.Now.AddDays(Convert.ToInt32(m_configuration["JwtExpiryInDays"]));

      var token = new JwtSecurityToken(
        m_configuration["JwtIssuer"],
        m_configuration["JwtAudience"],
        claims,
        expires: expiry,
        signingCredentials: credentials
      );

      return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }

    /// <summary>
    /// Signs out the currently signed in user
    /// </summary>
    [HttpPost(nameof(SignOutUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignOutUser()
    {
      if (HttpContext.User.Identity is null || !HttpContext.User.Identity.IsAuthenticated)
        return BadRequest();

      await HttpContext.SignOutAsync().ConfigureAwait(false);
      return Ok();
    }

    /// <summary>
    /// Attempts to register a new user
    /// </summary>
    /// <param name="userData">New user data</param>
    [HttpPost(nameof(RegisterUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterUser([FromBody] Models.REST.Auth.RegistrationRequest userData)
    {
      if (await m_userManager.FindByNameAsync(userData.Email).ConfigureAwait(false) is null)
        return BadRequest();

      var user = new IdentityUser
      {
        UserName = userData.Email
      };
      await m_userManager.CreateAsync(user, userData.Password).ConfigureAwait(false);

      return Ok();
    }

    /// <summary>
    /// Checks if a user of a given <paramref name="email"/> exists
    /// </summary>
    /// <param name="email">Email of the user to check</param>
    /// <returns>Check result</returns>
    [HttpPost(nameof(UserExists))]
    public async Task<ActionResult<bool>> UserExists(string email)
    {
      var user = await m_userManager.FindByNameAsync(email).ConfigureAwait(false);

      return Ok(user is not null);
    }

    /// <summary>
    /// Gets the currently signed in users email
    /// </summary>
    /// <returns>The signed in user email. Empty string in no user is signed in</returns>
    [HttpGet(nameof(SignedInEmail))]
    public async Task<string> SignedInEmail()
    {
      if (HttpContext.User.Identity is null || !HttpContext.User.Identity.IsAuthenticated)
        return string.Empty;

      var user = await m_userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
      return user.Email;
    }

    #endregion
  }
}