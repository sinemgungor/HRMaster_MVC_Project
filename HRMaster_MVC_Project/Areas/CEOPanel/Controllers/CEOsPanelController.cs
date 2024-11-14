using HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminViewModels;
using HRMaster_MVC_Project.Areas.CEOPanel.Models;
using HRMaster_MVC_Project.Areas.CEOPanel.Models.CeoDTOs;
using HRMaster_MVC_Project.Areas.CEOPanel.Models.CeoViewModel;
using HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs;
using HRMaster_MVC_Project.Areas.EmployeePanel.Models.ViewModels;
using HRMaster_MVC_Project.Models;
using HRMaster_MVC_Project.Models.DTOs;
using HRMaster_MVC_Project.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Text.Json;

namespace HRMaster_MVC_Project.Areas.CEOPanel.Controllers
{
    [Area("CEOPanel")]
    [Authorize(Roles = "Manager")]
    public class CEOsPanelController : Controller
    {

        private readonly HttpClient _httpClient;

        public CEOsPanelController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var userID = HttpContext.Session.GetInt32("UserID");


            var response = await _httpClient.GetAsync($"api/Manager/GetManagerIndexVM/{userID}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ManagerIndexVM managerIndexVM = JsonConvert.DeserializeObject<ManagerIndexVM>(content);
                return View(managerIndexVM);
            }



            return RedirectToAction("Login", "Login");
        }


        private int? UserID;
        
        public async Task<IActionResult> Detail(int id)
        {

            var response = await _httpClient.GetFromJsonAsync<ManagerDetailVM>($"api/Manager/get-manager-detail/{id}");

            return View(response);
        }
        public async Task<IActionResult> Update()
        {

            UserID = HttpContext.Session.GetInt32("UserID");
            UpdateManagerVM updManagerVM = new UpdateManagerVM();
            var response = await _httpClient.GetAsync($"api/Manager/get-manager-detail/{UserID}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                updManagerVM = JsonConvert.DeserializeObject<UpdateManagerVM>(content);
                return View(updManagerVM);
            }
            else
            {
                TempData["Message"] = "Bir hata oluştu!";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateManagerVM updMngVM)
        {

            if (!ModelState.IsValid)
            {
                UserID = HttpContext.Session.GetInt32("UserID");
                UpdateManagerVM updManagerVM = new UpdateManagerVM();
                var rspn = await _httpClient.GetAsync($"api/Manager/get-manager-detail/{UserID}");
                if (rspn.IsSuccessStatusCode)
                {
                    var content = await rspn.Content.ReadAsStringAsync();
                    updManagerVM = JsonConvert.DeserializeObject<UpdateManagerVM>(content);
                    return View(updManagerVM);
                }
                else
                {
                    TempData["Message"] = "Bir hata oluştu!";
                    return RedirectToAction("Index");
                }
            }
            UpdateManagerDTO updManagerDTO = new UpdateManagerDTO();

            if (updMngVM.UpdatedPicture != null && updMngVM.UpdatedPicture.Length > 0)
            {

                string strFileName = "";
                Guid guid = Guid.NewGuid();
                strFileName = guid + "_" + updMngVM.UpdatedPicture.FileName;
                string strPath = Path.Combine("wwwroot/Photos", strFileName);
                using (var fs = new FileStream(strPath, FileMode.Create))
                {
                    await updMngVM.UpdatedPicture.CopyToAsync(fs);
                }
                updManagerDTO.PicturePath = strFileName;
            }

            int? id = HttpContext.Session.GetInt32("UserID");
            updManagerDTO.Address = updMngVM.UpdatedAddress;
            updManagerDTO.Phone = updMngVM.UpdatedPhone;
            var response = await _httpClient.PutAsJsonAsync($"api/Manager/update-manager/{id}", updManagerDTO);

            if (response.IsSuccessStatusCode)
            {
                TempData["UpdMessage"] = "Kullanıcı Güncellenmiştir";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult AddPersonel()
        {
            return View(new PersonelRegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonel(PersonelRegisterVM newPersonel)
        {
            if (!ModelState.IsValid)
            {
                return View(newPersonel);
            }
            var employeeExist = await _httpClient.GetAsync($"api/Employee/employee-exist/{newPersonel.Email}");
            if(employeeExist.IsSuccessStatusCode)
            {
                ViewData["ErrorAddPersonel"] = "Bu personel zaten kayıtlı!";
                return View(newPersonel);
            }
            string strFileName = "unnamed.jpg";
            if (newPersonel.PicturePath != null && newPersonel.PicturePath.Length > 0)
            {
                Guid guid = Guid.NewGuid();
                strFileName = guid + "_" + newPersonel.PicturePath.FileName;
                string strPath = Path.Combine("wwwroot/Photos", strFileName);
                using (var fs = new FileStream(strPath, FileMode.Create))
                {
                    await newPersonel.PicturePath.CopyToAsync(fs);
                }
            }
            int? companyId = HttpContext.Session.GetInt32("CompanyID");
            var personelDTO = new PersonelRegisterDTO
            {
                Name = newPersonel.Name,
                SecondName = newPersonel.SecondName,
                Surname = newPersonel.Surname,
                SecondSurname = newPersonel.SecondSurname,
                Picture = strFileName,
                BirthDate = newPersonel.BirthDate,
                BirthState = newPersonel.BirthState,
                IdentityNumber = newPersonel.IdentityNumber,
                HireDate = newPersonel.HireDate,
                Job = newPersonel.Job,
                Address = newPersonel.Address,
                Salary = newPersonel.Salary,
                Title = newPersonel.Title,
                BloodGroup = newPersonel.BloodGroup,
                Gender = newPersonel.Gender,
                MarialStatus = newPersonel.MarialStatus,
                CompanyID = companyId,
                Email = newPersonel.Email,
                PhoneNumber = newPersonel.PhoneNumber,
            };

            var response = await _httpClient.PostAsJsonAsync("api/Manager/add-employee", personelDTO);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessAddPersonel"] = "Personel başarıyla eklendi";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Personel eklenirken bir hata oluştu.");
                return View(newPersonel);
            }
        }

       
       


        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(AddDepartmentDTO model)
        {
            int? companyID = HttpContext.Session.GetInt32("CompanyID");

            if (!companyID.HasValue)
            {
                TempData["ErrorMessage"] = "Şirket bilgisi bulunamadı. Lütfen tekrar deneyin.";
                return View(model);
            }

            var response = await _httpClient.PostAsJsonAsync($"api/Department/Add-Department/{companyID.Value}", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Departman başarıyla eklendi!";
                return View();
            }

            TempData["ErrorMessage"] = $"Departman ekleme işlemi başarısız oldu: {await response.Content.ReadAsStringAsync()}";
            return View(model);
        }



        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }



        public async Task<IActionResult> AssignDepartment(int id)
        {
            var userID = HttpContext.Session.GetInt32("UserID");
            var companyID = HttpContext.Session.GetInt32("CompanyID");

            if (!userID.HasValue || !companyID.HasValue)
            {
                return RedirectToAction("Error");
            }


            var departmentsResponse = await _httpClient.GetAsync($"api/Department/Get-All-Departments/{companyID}");

            if (departmentsResponse.IsSuccessStatusCode)
            {
                var departments = await departmentsResponse.Content.ReadFromJsonAsync<List<ShowDepartmentDTO>>();



                var model = new AssignDepartmentVM
                {
                    EmployeeID = id,
                    Departments = new SelectList(departments, "ID", "DepartmentName")
                };

                return View(model);
            }
            return RedirectToAction("Error");
        }



        [HttpPost]
        public async Task<IActionResult> AssignDepartment(AssignDepartmentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AssignDepartmentDTO assignDepartmentDTO = new AssignDepartmentDTO()
            {
                EmployeeID = model.EmployeeID,
                SelectedDepartmentID = model.SelectedDepartmentID.GetValueOrDefault(),
            };      

            var response = await _httpClient.PostAsJsonAsync($"api/Manager/assign-department",assignDepartmentDTO);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Departman başarıyla atandı!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Departman atanırken bir hata oluştu.";
            return View(model);
        }

        public async Task<IActionResult> ExpenseRequest(ResultMessage? resultMessage)
        {

            if (resultMessage is not null && resultMessage.IsSuccess)
            {
                TempData["Success"] = resultMessage.Message;
            }
            else if(resultMessage is not null && !resultMessage.IsSuccess)
            {
                TempData["Error"]= resultMessage.Message;
            }

            ExpenseRequestPageVM requestPageVM = new ExpenseRequestPageVM();
            var companyID = HttpContext.Session.GetInt32("CompanyID");
            var response = await _httpClient.GetAsync($"api/ExpenseRequest/get-expenseRequestsForManager/{companyID}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowExpenseRequestDTO>>>(responseBody);
                requestPageVM.ExpenseRequests = apiResponse.Data;
            }


            return View(requestPageVM);
        }

        public async Task<IActionResult> ApproveExpenseRequest(int id)
        {
            
            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/ExpenseRequest/approve-expenseRequest/{id}", null);
               
                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {                 
                        return RedirectToAction("ExpenseRequest",new ResultMessage { Message= "Çalışan talebi onaylandı!" ,IsSuccess=true}); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {
                      
                        return RedirectToAction("ExpenseRequest", new ResultMessage { Message = "Onaylama işlemi başarısız!", IsSuccess = false });
                    }
                }
                else
                {
                   
                    return RedirectToAction("ExpenseRequest",new ResultMessage { Message = "Onaylama işlemi başarısız!", IsSuccess = false });
                }
            }
            else
            {
             
                return RedirectToAction("ExpenseRequest", new ResultMessage { Message = "Onaylama işlemi başarısız!", IsSuccess = false });
            }
        }        
        public async Task<IActionResult> RejectExpenseRequest(int id)
        {
            
            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/ExpenseRequest/reject-expenseRequest/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {
                        TempData["Success"] = "Çalışan talebi reddedildi!";
                       
                        return RedirectToAction("ExpenseRequest", new ResultMessage { Message = "Çalışan talebi reddedildi!", IsSuccess = true }); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {
                        TempData["Error"] = "Reddetme işlemi başarısız!";
                       
                        return RedirectToAction("ExpenseRequest", new ResultMessage { Message = "Reddetme işlemi başarısız!", IsSuccess = false });
                    }
                }
                else
                {

                    TempData["Error"] = "Reddetme işlemi başarısız!";
                   
                    return RedirectToAction("ExpenseRequest", new ResultMessage { Message = "Reddetme işlemi başarısız!", IsSuccess = false });
                }
            }
            else
            {
                TempData["Error"] = "Geçersiz ID!";
             
                return RedirectToAction("ExpenseRequest", new ResultMessage { Message = "Reddetme işlemi başarısız!", IsSuccess = false });
            }

        }

        public async Task<IActionResult> ViewDocument(int id)
        {
            var response = await _httpClient.GetAsync($"api/ExpenseRequest/get-filePath/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseBody);
            var filePath = apiResponse.Data;
            if (string.IsNullOrEmpty(filePath))
            {
                return NotFound();
            }

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "expenseFiles", filePath);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            var fileContent = await System.IO.File.ReadAllBytesAsync(fullPath);
            var contentType = MimeMapping.GetMimeMapping(fullPath); // MIME türünü belirle

            return File(fileContent, contentType, filePath);
        }


        public async Task<IActionResult> AdvanceRequest()
        {
            AdvanceRequestPageVM requestPageVM = new AdvanceRequestPageVM();
            var companyID = HttpContext.Session.GetInt32("CompanyID");
            var response = await _httpClient.GetAsync($"api/AdvanceRequest/get-advanceRequestsForManager/{companyID}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowAdvanceRequestDTO>>>(responseBody);
                requestPageVM.AdvanceRequests = apiResponse.Data;
            }


            return View(requestPageVM);
        }
        public async Task<IActionResult> ApproveAdvanceRequest(int id)
        {

            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/AdvanceRequest/approve-advanceRequest/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {
                        TempData["SuccessAdvance"] = "Çalışan talebi onaylandı.";
                        return RedirectToAction("AdvanceRequest"); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {
                        TempData["ErrorAdvance"] = "Çalışan talebi onaylanırken bir hata oluştu.";
                        return RedirectToAction("AdvanceRequest");
                    }
                }
                else
                {
                    TempData["ErrorAdvance"] = "Çalışan talebi onaylanırken bir hata oluştu.";
                    return RedirectToAction("AdvanceRequest");
                }
            }
            else
            {
                TempData["ErrorAdvance"] = "Çalışan talebi onaylanırken bir hata oluştu.";
                return RedirectToAction("AdvanceRequest");
            }
        }
        public async Task<IActionResult> RejectAdvanceRequest(int id)
        {

            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/AdvanceRequest/reject-advanceRequest/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {
                        TempData["SuccessAdvanceReject"] = "Çalışan talebi reddedildi!";

                        return RedirectToAction("AdvanceRequest"); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {
                        TempData["ErrorAdvanceReject"] = "Reddetme işlemi başarısız!";

                        return RedirectToAction("AdvanceRequest");
                    }
                }
                else
                {

                    TempData["ErrorAdvanceReject"] = "Reddetme işlemi başarısız!";

                    return RedirectToAction("AdvanceRequest");
                }
            }
            else
            {
                return RedirectToAction("AdvanceRequest");
            }

        }

        public async Task<IActionResult> LeaveRequest(ResultMessage? resultMessage)
        {

            if (resultMessage is not null && resultMessage.IsSuccess)
            {
                TempData["Success"] = resultMessage.Message;
            }
            else if (resultMessage is not null && !resultMessage.IsSuccess)
            {
                TempData["Error"] = resultMessage.Message;
            }

            LeaveRequestPageVM requestPageVM = new LeaveRequestPageVM();
            var companyID = HttpContext.Session.GetInt32("CompanyID");
            var response = await _httpClient.GetAsync($"api/LeaveRequest/get-leaveRequestsForManager/{companyID}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowLeaveRequestDTO>>>(responseBody);
                requestPageVM.LeaveRequests = apiResponse.Data;
            }


            return View(requestPageVM);
        }

        public async Task<IActionResult> ApproveLeaveRequest(int id)
        {

            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/LeaveRequest/approve-leaveRequest/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {
                        return RedirectToAction("LeaveRequest", new ResultMessage { Message = "Çalışan talebi onaylandı!", IsSuccess = true }); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {

                        return RedirectToAction("LeaveRequest", new ResultMessage { Message = "Onaylama işlemi başarısız!", IsSuccess = false });
                    }
                }
                else
                {

                    return RedirectToAction("LeaveRequest", new ResultMessage { Message = "Onaylama işlemi başarısız!", IsSuccess = false });
                }
            }
            else
            {

                return RedirectToAction("LeaveRequest", new ResultMessage { Message = "Onaylama işlemi başarısız!", IsSuccess = false });
            }
        }
        public async Task<IActionResult> RejectLeaveRequest(int id)
        {

            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/LeaveRequest/reject-leaveRequest/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {
                        TempData["Success"] = "Çalışan talebi reddedildi!";

                        return RedirectToAction("LeaveRequest", new ResultMessage { Message = "Çalışan talebi reddedildi!", IsSuccess = true }); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {
                        TempData["Error"] = "Reddetme işlemi başarısız!";

                        return RedirectToAction("LeaveRequest", new ResultMessage { Message = "Reddetme işlemi başarısız!", IsSuccess = false });
                    }
                }
                else
                {

                    TempData["Error"] = "Reddetme işlemi başarısız!";

                    return RedirectToAction("LeaveRequest", new ResultMessage { Message = "Reddetme işlemi başarısız!", IsSuccess = false });
                }
            }
            else
            {
                TempData["Error"] = "Geçersiz ID!";

                return RedirectToAction("LeaveRequest", new ResultMessage { Message = "Reddetme işlemi başarısız!", IsSuccess = false });
            }

        }
       

    }
}
