using MAL2018_Assessment2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MAL2018_Assessment2.Controllers
{
    public class SignInController : Controller
    {
        private readonly ILogger<SignInController> _logger;

        public SignInController(ILogger<SignInController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            try
            {
                // Validate email and password
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return BadRequest("Email and password are required.");
                }

                // Prepare the payload for the authentication API
                var signinPayload = new
                {
                    email,
                    password
                };
                var json = JsonSerializer.Serialize(signinPayload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                _logger.LogInformation($"Post data: " + email + " " + password);
                _logger.LogInformation($"Request Content: {json}");

                // Call the authentication API
                var authUrl = "https://web.socem.plymouth.ac.uk/COMP2001/auth/api/users";
                using (var httpClient = new HttpClient())
                {
                    var jsonresponse = await httpClient.PostAsync(authUrl, content);
                    _logger.LogInformation($"Request Content: {jsonresponse}");

                    if (jsonresponse.IsSuccessStatusCode)
                    {
                        var verificationStatus = await jsonresponse.Content.ReadFromJsonAsync<List<string>>();
                        _logger.LogInformation($"Verification Status: {string.Join(", ", verificationStatus)}");


                        if (verificationStatus != null && verificationStatus.Count == 2 &&
                            verificationStatus[0] == "Verified" && verificationStatus[1] == "True")
                        {
                            // If authentication succeeds, call the API to get all users
                            var getAllUsersUrl = "https://localhost:7013/api/Users";
                            var getAllUsersResponse = await httpClient.GetAsync(getAllUsersUrl);
                            _logger.LogInformation($"User Response: " + getAllUsersResponse);


                            if (getAllUsersResponse.IsSuccessStatusCode)
                            {
                                var allUsers = await getAllUsersResponse.Content.ReadFromJsonAsync<List<User>>();

                                // Filter the user by email
                                var usersWithEmail = allUsers?.FindAll(u => u.Email == email);

                                if (usersWithEmail != null && usersWithEmail.Count > 0)
                                {
                                    // Perform additional checks if necessary to validate the correct user
                                    var user = usersWithEmail.Find(u => u.Password == password);

                                    if (user != null)
                                    {
                                        // Save the user ID and email in session
                                        HttpContext.Session.SetInt32("UserId", user.UserId);
                                        HttpContext.Session.SetString("UserEmail", email);

                                        // Redirect to the profile page
                                        return RedirectToAction("Index", "Profile");
                                    }
                                    else
                                    {
                                        _logger.LogInformation("Password did not match for any user.");
                                    }
                                }
                                else
                                {
                                    _logger.LogInformation("User email does not exist.");
                                }
                            }
                        }
                    }
                }

                // If authentication fails or user not found, return to login page
                return RedirectToAction("Index", "Home");
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception and return a 500 Internal Server Error
                _logger.LogError(ex, "An error occurred while processing the login request.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
