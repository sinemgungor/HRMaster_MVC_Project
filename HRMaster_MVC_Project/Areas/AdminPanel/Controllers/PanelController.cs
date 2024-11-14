using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using AutoMapper;
using HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminDTOs;
using HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminViewModels;
using HRMaster_MVC_Project.Models.ViewModels;
using HRMaster_MVC_Project.Models.DTOs;

namespace HRMaster_MVC_Project.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "SuperAdmin")]
    public class PanelController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public PanelController(IHttpClientFactory httpClientFactory,IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _mapper=mapper;

        }
        private int? UserID;

        public async Task<IActionResult> Index()
        {
            UserID = HttpContext.Session.GetInt32("UserID");
            var response = await _httpClient.GetFromJsonAsync<AdminSumVM>($"api/Admin/get-admin-summary/{UserID}");
            if (response != null)
            {

                return View(response);
            }



            return RedirectToAction("Login", "Login");

        }
        public async Task<IActionResult> AdminDetails(int id)
        {
            UserID = HttpContext.Session.GetInt32("UserID");
            var response = await _httpClient.GetAsync($"api/Admin/get-admin-detail/{UserID}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                AdminSummaryVM adminDetailVM = JsonConvert.DeserializeObject<AdminSummaryVM>(content);
                return View(adminDetailVM);
            }



            return RedirectToAction("AdminSummary", "Panel");
        }

        public async Task<IActionResult> Update()
        {
            UserID = HttpContext.Session.GetInt32("UserID");
            UpdateAdminVM updAdminVM = new UpdateAdminVM();
            var response = await _httpClient.GetAsync($"api/Admin/get-admin-detail/{UserID}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                updAdminVM = JsonConvert.DeserializeObject<UpdateAdminVM>(content);
                return View(updAdminVM);
            }
            else
            {
                TempData["Message"] = "Bir hata oluştu!";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAdminVM updAdminVM)
        {
            if (!ModelState.IsValid)
            {
                UserID = HttpContext.Session.GetInt32("UserID");
                UpdateAdminVM updateAdminVM = new UpdateAdminVM();
                var rspn = await _httpClient.GetAsync($"api/Admin/get-admin-detail/{UserID}");
                if (rspn.IsSuccessStatusCode)
                {
                    var content = await rspn.Content.ReadAsStringAsync();
                    updateAdminVM = JsonConvert.DeserializeObject<UpdateAdminVM>(content);
                    return View(updateAdminVM);
                }
                else
                {
                    TempData["Message"] = "Bir hata oluştu!";
                    return RedirectToAction("Index");
                }
            }

            UpdateAdminDTO updAdminDTO = new UpdateAdminDTO();

            
            if (updAdminVM.UpdatedPicture != null && updAdminVM.UpdatedPicture.Length > 0)
            {
                string strFileName = "";
                Guid guid = Guid.NewGuid();
                strFileName = guid + "_" + updAdminVM.UpdatedPicture.FileName;
                string strPath = Path.Combine("wwwroot/Photos", strFileName);
                using (var fs = new FileStream(strPath, FileMode.Create))
                {
                    await updAdminVM.UpdatedPicture.CopyToAsync(fs);
                }
                updAdminDTO.PicturePath = strFileName;
            }
            int? id = HttpContext.Session.GetInt32("UserID");
      
            updAdminDTO.Address = updAdminVM.UpdatedAddress;
            updAdminDTO.Phone = updAdminVM.UpdatedPhone;
            var response = await _httpClient.PutAsJsonAsync($"api/Admin/update-user/{id}", updAdminDTO);

            if (response.IsSuccessStatusCode)
            {
                TempData["UpdMessage"] = "Kullanıcı Güncellenmiştir";
                return RedirectToAction("Index");
            }
            return View();

        }


        public async Task<IActionResult> Companies()
        {

            var companies = await _httpClient.GetFromJsonAsync<List<ShowCompanyVM>>("api/Company/get-all-companies");

            return View(companies);

        }
        public async Task<IActionResult> CompanyDetails(int id)
        {

            var company = await _httpClient.GetFromJsonAsync<CompanyDetailsVM>($"api/Company/get-company/{id}");

            if (company == null)
            {
                return NotFound(); 
            }

            return View(company);

        }
        public IActionResult AddCompany()
        {

            return View(new CompanyVM());

        }
        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyVM companyVM)
        {
            if (!ModelState.IsValid)
            {

                //return RedirectToAction("Index");
                return View();
            }
            var newcompanyDTO=_mapper.Map<AddCompanyDTO>(companyVM);
            string strFileName = "bosResim.jpg";
            if (companyVM.LogoPath != null && companyVM.LogoPath.Length > 0)
            {
                Guid guid = Guid.NewGuid();
                strFileName = guid + "_" + companyVM.LogoPath.FileName;
                string strPath = Path.Combine("wwwroot/Images/Logos", strFileName);
                using (var fs = new FileStream(strPath, FileMode.Create))
                {
                    await companyVM.LogoPath.CopyToAsync(fs);
                }
            }

            newcompanyDTO.LogoPath = strFileName;

            var response = await _httpClient.PostAsJsonAsync("api/Company/add-company", newcompanyDTO);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Şirket başarıyla eklendi.";
            }
            else
            {
                TempData["Error"] = "Şirket eklenirken bir hata oluştu.";
                return View(companyVM);
            }

            return RedirectToAction("Companies");
        }
        public async Task<IActionResult> AddCEO()
        {
            var companies = await _httpClient.GetFromJsonAsync<List<ShowCompanyVM>>("api/Company/get-all-companies");
            var companyDtos = companies.Select(c => new CompanyDTO
            {
                ID = c.ID,
                CompanyName = c.CompanyName
            }).ToList();

            AddCEOVM addCEOVM = new();
            addCEOVM.Companies = new SelectList(companyDtos, "ID", "CompanyName");

            return View(addCEOVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddCEO(AddCEOVM newCEO)
        {
            var companies = await _httpClient.GetFromJsonAsync<List<ShowCompanyVM>>("api/Company/get-all-companies");
            var companyDtos = companies.Select(c => new CompanyDTO
            {
                ID = c.ID,
                CompanyName = c.CompanyName
            }).ToList();
            newCEO.Companies = new SelectList(companyDtos, "ID", "CompanyName");

            if (!ModelState.IsValid)
            {
                return View(newCEO);

            }
            
            var company = await _httpClient.GetFromJsonAsync<CompanyDetailsVM>($"api/Company/get-company/{newCEO.SelectedCompanyID}");
            if(company.CEOId==null || company.CEOId==0)
            {
                string strFileName = "default-avatar.jpg";
                if (newCEO.PicturePath != null && newCEO.PicturePath.Length > 0)
                {
                    Guid guid = Guid.NewGuid();
                    strFileName = guid + "_" + newCEO.PicturePath.FileName;
                    string strPath = Path.Combine("wwwroot/Photos", strFileName);
                    using (var fs = new FileStream(strPath, FileMode.Create))
                    {
                        await newCEO.PicturePath.CopyToAsync(fs);
                    }
                }

                var ceoDTO = new PersonelRegisterDTO
                {
                    Name = newCEO.Name,
                    SecondName = newCEO.SecondName,
                    Surname = newCEO.Surname,
                    SecondSurname = newCEO.SecondSurname,
                    Picture = strFileName,
                    BirthDate = newCEO.BirthDate,
                    BirthState = newCEO.BirthState,
                    IdentityNumber = newCEO.IdentityNumber,
                    HireDate = newCEO.HireDate,
                    Job = newCEO.Job,
                    Address = newCEO.Address,
                    Salary = newCEO.Salary,
                    Title = newCEO.Title,
                    BloodGroup = newCEO.BloodGroup,
                    Gender = newCEO.Gender,
                    MarialStatus = newCEO.MarialStatus,
                    CompanyID = newCEO.SelectedCompanyID,
                    Email = newCEO.Email,
                    PhoneNumber = newCEO.PhoneNumber,
                };

                var response = await _httpClient.PostAsJsonAsync("api/Admin/add-new-manager", ceoDTO);

                if (response.IsSuccessStatusCode)
                {
                    TempData["AddedCEO"] = $"{newCEO.Name} {newCEO.Surname} adlı Şirket Yöneticisi Eklenmiştir";
                    return RedirectToAction("Index");
                }

                ViewData["ErrorAddManager"] = "Yönetici oluşturulurken bir hata meydana geldi!";
                return View(newCEO);

            }
            else
            {
                ViewData["ErrorAddManager"] = "Bu şirket için zaten bir yönetici bulunmakta!";
                return View(newCEO); 
            }

        }        
        
        public async Task<IActionResult> Ceos()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("api/Manager/get-all-ceos");
                var ceos = JsonConvert.DeserializeObject<List<ManagerListVM>>(response);
                return View(ceos);
            }
            catch
            {
                // Hata yönetimi
                return View("Error");
            }
        }
        public IActionResult Error()
        {
            return View(); // Error.cshtml görümünü döner
        }
    }
}
