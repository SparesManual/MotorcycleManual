using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Db.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public sealed class AuthController
    : ControllerBase
  {
    #region Fields

    private readonly UserManager<IdentityUser> m_userManager;

    #endregion

    public AuthController (UserManager<IdentityUser> userManager)
    {
      m_userManager = userManager;
    }

    #region API Methods

    /// <summary>
    /// Logs in a given user
    /// </summary>
    /// <param name="userData">Data of the user to sign in</param>
    [HttpPost(nameof(SignInUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignInUser(Tuple<string, string> userData)
    {
      var user = await m_userManager.FindByEmailAsync(userData.Item1).ConfigureAwait(false);
      if (user is null)
        return BadRequest();

      if (!await m_userManager.CheckPasswordAsync(user, userData.Item2).ConfigureAwait(false))
        return BadRequest();

      var claims = await m_userManager.GetClaimsAsync(user).ConfigureAwait(false);
      var claimsIdentity = new ClaimsIdentity(claims, "serverAuth");
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      await HttpContext.SignInAsync(claimsPrincipal).ConfigureAwait(false);

      return Ok();
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