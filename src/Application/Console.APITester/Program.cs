using System.Threading.Tasks;

namespace Console.APITester
{
  public static class Program
  {
    private static async Task Main()
    {
      var client = new MRI.Email.EmailClient();
      await client.SendRegistrationConfirmationAsync("test@test.com", "MyCode").ConfigureAwait(false);
    }
  }
}
