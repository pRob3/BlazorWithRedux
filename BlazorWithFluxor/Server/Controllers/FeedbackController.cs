using BlazorWithFluxor.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWithFluxor.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public void Post(UserFeedbackModel model)
        {
            var email = model.EmailAddress;
            var rating = model.Rating;
            var comment = model.Comment;

            Console.WriteLine($"Received rating {rating} from {email} with comment '{comment}'");
        }
    }
}