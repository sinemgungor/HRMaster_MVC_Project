using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using HRMaster_MVC_Project.Models.ViewModels.LoginViewModel;
using Microsoft.AspNetCore.Mvc;
using HRMaster_MVC_Project.Models;
using HRMaster_MVC_Project.Models.ViewModels;
using System.Text.Json;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

public class LoginController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LoginController> _logger;

    public LoginController(IHttpClientFactory httpClientFactory, ILogger<LoginController> logger)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        _logger = logger;
    }

    // GET: /Login
    [HttpGet]
    public IActionResult Login()
    {
  
        return View();
    }

    // POST: /Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
    
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/login/login", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Giriş işlemi başarısız oldu. Lütfen e-posta ve şifrenizi kontrol edin.");
                return View(model);
            }

            var responseContent = await response.Content.ReadFromJsonAsync<LoginResult>();

            if (responseContent == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz API yanıtı.");
                return View(model);
            }

            var role = responseContent.Roles.FirstOrDefault();
            var claims = new List<Claim>()
            {
             new Claim      (ClaimTypes.Name,responseContent.User.Email),
             new Claim(ClaimTypes.Role,role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);



            var companyId = responseContent.User.CEORelatedCompanyID ?? responseContent.User.EmployeeRelatedCompanyID;
            if (companyId.HasValue)
            {
                var companyResponse = await _httpClient.GetAsync($"api/company/get-company/{companyId.Value}");
                if (companyResponse.IsSuccessStatusCode)
                {
                    var company = await companyResponse.Content.ReadFromJsonAsync<CompanyDetailsVM>();
                    HttpContext.Session.SetString("CompanyName", company.CompanyName);
                    HttpContext.Session.SetString("CompanyLogo", company.LogoPath);

                }
            }

            if (role == "SuperAdmin")
            {
                SetUserSession(responseContent.User, "SuperAdmin");
                return RedirectToAction("Index", "Panel", new { area = "AdminPanel" });
            }
            else if (role == "Manager")
            {
                SetUserSession(responseContent.User, "Manager");
                return RedirectToAction("Index", "CEOsPanel", new { area = "CEOPanel" });
            }
            else if (role == "Employee")
            {
                SetUserSession(responseContent.User, "Employee");
                return RedirectToAction("Index", "EmployeesPanel", new { area = "EmployeePanel" });
            }

            ModelState.AddModelError(string.Empty, "Rol yetkili değil.");
            return View(model);
        }
        catch (Exception ex)
        {
            // Hata ayıklama ve loglama
            _logger.LogError(ex, "Giriş işlemi sırasında bir hata oluştu.");
            ModelState.AddModelError(string.Empty, "Bir hata oluştu. Lütfen tekrar deneyin.");
            return View(model);
        }
    }
    private void SetUserSession(UserDTO user, string role)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }


        HttpContext.Session.SetInt32("UserID", user.Id);
        if (user.Salary != null)
        {
            HttpContext.Session.SetString("Salary", user.Salary.ToString());
        }
        if (user.PicturePath != null)
        {
            HttpContext.Session.SetString("PicturePath", user.PicturePath);
        }
        if (user.Name != null)
        {
            HttpContext.Session.SetString("Name", user.Name);
        }
        if (user.Surname != null)
        {
            HttpContext.Session.SetString("Surname", user.Surname);
        }
        if (user.Email != null)
        {
            HttpContext.Session.SetString("Email", user.Email);
        }
        if (user.PhoneNumber != null)
        {
            HttpContext.Session.SetString("Phone", user.PhoneNumber);
        }
        if (user.Address != null)
        {
            HttpContext.Session.SetString("Address", user.Address);
        }
        if (user.Title != null)
        {
            HttpContext.Session.SetString("Title", user.Title);
        }
        if (user.CEORelatedCompanyID != null)
        {
            HttpContext.Session.SetInt32("CompanyID", user.CEORelatedCompanyID.GetValueOrDefault());
        }
        if (user.EmployeeRelatedCompanyID!= null)
        {
            HttpContext.Session.SetInt32("CompanyID", user.EmployeeRelatedCompanyID.GetValueOrDefault());
        }

        HttpContext.Session.SetString("UserRole", role);
    }


    // GET: /ForgotPassword
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        // ForgotPassword sayfasını göstermek için View döndürür.
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/login/forgot-password", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Şifre sıfırlama işlemi başarısız oldu. Lütfen e-posta adresinizi kontrol edin.");
                return View(model);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent); // Yanıt içeriğini konsola yazdırın            

            // Başarılı bir şifre sıfırlama işlemi sonrası yapılacak işlemler.
            ViewBag.Message = "Şifre sıfırlama e-postası gönderildi.";
            return View("ForgotPasswordConfirmation");
        }
        catch (Exception ex)
        {
            // Hata ayıklama ve loglama
            _logger.LogError(ex, "Şifre sıfırlama işlemi sırasında bir hata oluştu.");
            ModelState.AddModelError(string.Empty, "Bir hata oluştu. Lütfen tekrar deneyin.");
            return View(model);
        }
    }

    // GET: /ResetPassword
    [HttpGet]
    public IActionResult ResetPassword(string email, string token)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Error", "Login");
        }

        var model = new ResetPasswordVM
        {
            Email = email,
            Token = token
        };

        return View(model);
    }

    // POST: /ResetPassword
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/login/reset-password", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Şifre sıfırlama işlemi başarısız oldu. Lütfen tekrar deneyin.");
                return View(model);
            }

            var resultMessage = await response.Content.ReadAsStringAsync();
            ViewBag.Message = resultMessage;
            return View("ResetPasswordConfirmation");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Şifre sıfırlama işlemi sırasında bir hata oluştu.");
            ModelState.AddModelError(string.Empty, "Bir hata oluştu. Lütfen tekrar deneyin.");
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        var response = await _httpClient.PostAsync("api/login/logout", null);

        if (response.IsSuccessStatusCode)
        {
            // Oturum sonlandırıldıktan sonra kullanıcıyı anasayfaya yönlendir
            HttpContext.Session.Clear();//Tüm session verilerini temizle.
            return RedirectToAction("Index", "Home");
        }

        // Eğer hata olursa hata mesajını göster
        ModelState.AddModelError(string.Empty, "Logout failed. Please try again.");
        return View();
    }

    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Error");
        }

        try
        {
            var response = await _httpClient.GetAsync($"api/login/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}");


            if (response.IsSuccessStatusCode)
            {
                @ViewData["ConfirmedEmail"] = "Emailiniz başarıyla onaylanmıştır. Aşağıdan login sayfasına gidebilirsiniz!";
                return RedirectToAction("ConfirmedEmailPage", "Login");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                // Hata mesajını loglayın
                System.Diagnostics.Debug.WriteLine($"API Hatası: {errorMessage}");
                return RedirectToAction("Error", "Login");
            }
        }
        catch (Exception ex)
        {
            // Genel hata yakalama ve loglama
            System.Diagnostics.Debug.WriteLine($"Genel Hata: {ex.Message}");
            return RedirectToAction("Error", "Login");
        }
    }
    public async Task<IActionResult> ConfirmedEmailPage()
    {
        ViewData["EmailConfirmed"] = "Emailiniz onaylanmıştır. Lütfen kullanıcı girişi yapınız!";
        return View();
    }
    public IActionResult Error()
    {
        return View();
    }

}
