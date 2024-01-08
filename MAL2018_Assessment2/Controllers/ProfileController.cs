using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MAL2018_Assessment2.Models;
using System.Text.Json;

namespace MAL2018_Assessment2.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IHttpClientFactory _clientfactory;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IHttpClientFactory clientfactory, ILogger<ProfileController> logger)
        { 
            _clientfactory = clientfactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve user information from session
            var sessionUserId = HttpContext.Session.GetInt32("UserId");

            if(sessionUserId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Get user personal information
            var profileinfo = await GetProfile(sessionUserId.Value);

            return View("~/Views/Profile/Index.cshtml", profileinfo);
            
        }

        private async Task<Profile>GetProfile(int sessionUserId)
        {
            var apiUrl = $"https://localhost:7013/api/Profiles/{sessionUserId}";
            _logger.LogInformation($"api url:  +{ apiUrl}");
            var client = _clientfactory.CreateClient();

            var response = await client.GetAsync(apiUrl);
            _logger.LogInformation($"Response : {response}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response Content: {content}");

                var profile = JsonSerializer.Deserialize<Profile>(content);
                _logger.LogInformation($"Deserialized Profile: {JsonSerializer.Serialize(profile)}");
                var profiles = new Profile
                {
                    //Show Related Data
                    ProfileId = profile.ProfileId,
                    UserId = profile.UserId,
                    ProfileName = profile.ProfileName,
                    UserContact = profile.UserContact,
                    Bio = profile.Bio
                };

                _logger.LogInformation($"User id:  { profile.ProfileId}, Profile Name:  { profile.ProfileName},  User Contact:  { profile.UserContact}, User Bio:  { profile.Bio}");

                return profiles;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult>Update(Profile profile)
        {
            var sessionUserId = HttpContext.Session.GetInt32("UserId");
            if(sessionUserId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var isSuccess = await UpdateProfileInfo(sessionUserId.Value, profile);

            if (isSuccess)
            {
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                return View("Error");
            }
        }

        private async Task<bool> UpdateProfileInfo(int userId, Profile profile)
        {
            var apiUrl = $"https://localhost:7013/api/Profiles/{userId}";
            var client = _clientfactory.CreateClient();

            var profiles = new Profile
            {
                ProfileId = userId,
                UserId = userId,
                ProfileName = profile.ProfileName,
                UserContact = profile.UserContact,
                Bio = profile.Bio
            };
            var jsonContent = JsonSerializer.Serialize(profiles);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            //Read and log the actual content
            string actualContent = await content.ReadAsStringAsync();
            _logger.LogInformation($"API Json Content: {actualContent}");

            var response = await client.PutAsync(apiUrl, content);
            _logger.LogInformation($"Response : {response}");
            return response.IsSuccessStatusCode;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
