using Microsoft.AspNetCore.Mvc;

namespace BetHive.Wallet.Api.Controllers
{
    public class DefaultController : Controller
    {
        [Route("")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi()
        {
            return Redirect("/swagger/");
        }
    }
}
