using AutoMapper;
using HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminViewModels;
using HRMaster_MVC_Project.Areas.CEOPanel.Models.CeoDTOs;
using HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs;
using HRMaster_MVC_Project.Areas.EmployeePanel.Models.ViewModels;
using HRMaster_MVC_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Controllers
{
    [Area("EmployeePanel")]
    [Authorize(Roles = "Employee")]
    public class EmployeesPanelController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public EmployeesPanelController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _mapper = mapper;
        }
        private int? EmployeeID;
        private decimal? EmployeeSalary;
        public async Task<IActionResult> Index()
        {
            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response = await _httpClient.GetFromJsonAsync<EmplyoeeSumVM>($"api/Employee/get-employee-summary/{EmployeeID}");

            return View(response);
        }
        public async Task<IActionResult> UpdateEmployee()
        {

            UpdateEmployeeVM updEmployeeVM = new UpdateEmployeeVM();

            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response = await _httpClient.GetAsync($"api/Employee/get-employee-summary/{EmployeeID}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                updEmployeeVM = JsonConvert.DeserializeObject<UpdateEmployeeVM>(content);
                return View(updEmployeeVM);
            }
            else
            {
                TempData["Message"] = "Bir hata oluştu!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeVM updEmployeeVM)
        {
            if (!ModelState.IsValid)
            {
                UpdateEmployeeVM updateEmployeeVM = new UpdateEmployeeVM();

                EmployeeID = HttpContext.Session.GetInt32("UserID");
                var rspn = await _httpClient.GetAsync($"api/Employee/get-employee-summary/{EmployeeID}");
                if (rspn.IsSuccessStatusCode)
                {
                    var content = await rspn.Content.ReadAsStringAsync();
                    updateEmployeeVM = JsonConvert.DeserializeObject<UpdateEmployeeVM>(content);
                    return View(updateEmployeeVM);
                }
                else
                {
                    TempData["Message"] = "Bir hata oluştu!";
                    return RedirectToAction("Index");
                }
            }

            UpdateEmployeeDTO updEmployeeDTO = new UpdateEmployeeDTO();

            if (updEmployeeVM.UpdatedPicture != null && updEmployeeVM.UpdatedPicture.Length > 0)
            {
                string strFileName = "";
                Guid guid = Guid.NewGuid();
                strFileName = guid + "_" + updEmployeeVM.UpdatedPicture.FileName;
                string strPath = Path.Combine("wwwroot/Photos", strFileName);
                using (var fs = new FileStream(strPath, FileMode.Create))
                {
                    await updEmployeeVM.UpdatedPicture.CopyToAsync(fs);
                }
                updEmployeeDTO.PicturePath = strFileName;
            }
            int? id = HttpContext.Session.GetInt32("UserID");

            updEmployeeDTO.Address = updEmployeeVM.UpdatedAddress;
            updEmployeeDTO.Phone = updEmployeeVM.UpdatedPhone;
            var response = await _httpClient.PutAsJsonAsync($"api/Employee/update-employee/{id}", updEmployeeDTO);

            if (response.IsSuccessStatusCode)
            {
                TempData["UpdMessage"] = "Kullanıcı Güncellenmiştir";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult _RequestForPermission()
        {// Gerekli verileri modelle doldurabilirsiniz
            return PartialView("_RequestForPermissionPartial");
        }

        public async Task<IActionResult> EmployeeDetails(int id)
        {

            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response = await _httpClient.GetAsync($"api/Employee/get-employee-detail/{EmployeeID}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employeeDetailVM = JsonConvert.DeserializeObject<EmployeeDetailsVM>(content);
                return View(employeeDetailVM);
            }
            return RedirectToAction("Index", "EmployeesPanel");
        }

        public async Task<IActionResult> AdvanceRequest()
        {
            AdvanceRequestPageVM requestPageVM = new AdvanceRequestPageVM();
            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response = await _httpClient.GetAsync($"api/AdvanceRequest/get-advanceRequestsForEmployee/{EmployeeID}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowAdvanceRequestDTO>>>(responseBody);
                requestPageVM.AdvanceRequests = apiResponse.Data;
            }
            TempData["Salary"] = HttpContext.Session.GetString("Salary");
            return View(requestPageVM);
        }

        [HttpPost]
        public async Task<IActionResult> AdvanceRequest(AdvanceRequestPageVM requestPageVM)
        {
            if (requestPageVM.AddAdvanceRequestDTO != null)
            {
               var salaryStr = HttpContext.Session.GetString("Salary");

                EmployeeSalary =decimal.Parse(salaryStr);
                if(EmployeeSalary.HasValue&& requestPageVM.AddAdvanceRequestDTO.Amount<=EmployeeSalary.Value*3)
                {
                    AddAdvanceRequestDTO newAdvance = new AddAdvanceRequestDTO
                    {
                        EmployeeID = HttpContext.Session.GetInt32("UserID").GetValueOrDefault(),

                        Amount = requestPageVM.AddAdvanceRequestDTO.Amount,
                        Currency = requestPageVM.AddAdvanceRequestDTO.Currency,
                        Description = requestPageVM.AddAdvanceRequestDTO.Description,
                        AdvanceType = requestPageVM.AddAdvanceRequestDTO.AdvanceType

                    };

                    var advanceResponse = await _httpClient.PostAsJsonAsync("api/AdvanceRequest/add-advanceRequest", newAdvance);
                    if (advanceResponse.IsSuccessStatusCode)
                    {
                        ViewData["AddedAdvanceRequest"] = "Avans Talebi Oluşturuldu!";
                    }
                    else
                    {
                        ViewData["AddedAdvanceRequestError"] = "Avans talebi oluşturulurken bir hata oluştu!";
                    }
                }
                else
                {
                    ViewData["AddedAdvanceRequestError"] = "Avans miktarı, maaşınızın üç katından fazla olamaz!";
                }
            }
                
            AdvanceRequestPageVM refreshedPageVM2 = new AdvanceRequestPageVM();
            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response3 = await _httpClient.GetAsync($"api/AdvanceRequest/get-advanceRequestsForEmployee/{EmployeeID}");
            if (response3.IsSuccessStatusCode)
            {
                var responseBody = await response3.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowAdvanceRequestDTO>>>(responseBody);
                refreshedPageVM2.AdvanceRequests = apiResponse.Data;
            }
            return View(refreshedPageVM2);
        }
        public async Task<IActionResult> CancelAdvanceRequest(int id)
        {

            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/AdvanceRequest/cancel-advanceRequest/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {
                        return RedirectToAction("AdvanceRequest"/*, new ResultMessage { Message = "Çalışan talebi iptal edildi!", IsSuccess = true }*/); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {

                        return RedirectToAction("AdvanceRequest" /*new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
                    }
                }
                else
                {

                    return RedirectToAction("AdvanceRequest"/* new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
                }
            }
            else
            {

                return RedirectToAction("AdvanceRequest" /*new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
            }
        }

        public async Task<IActionResult> ExpenseRequest()
        {
            ExpenseRequestPageVM requestPageVM = new ExpenseRequestPageVM();
            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response = await _httpClient.GetAsync($"api/ExpenseRequest/get-expenseRequestsForEmployee/{EmployeeID}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowExpenseRequestDTO>>>(responseBody);
                requestPageVM.ExpenseRequests = apiResponse.Data;
            }

            return View(requestPageVM);
        }

        [HttpPost]
        public async Task<IActionResult> ExpenseRequest(ExpenseRequestPageVM requestPageVM)
        {
            if (requestPageVM.AddExpenseRequestVM != null)
            {
                AddExpenseRequestDTO newExpense = new AddExpenseRequestDTO();
                newExpense.EmployeeID = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
                newExpense.Currency = requestPageVM.AddExpenseRequestVM.Currency;
                newExpense.Amount = requestPageVM.AddExpenseRequestVM.Amount;
                newExpense.ExpenseType = requestPageVM.AddExpenseRequestVM.ExpenseType;

                string strFileName = "bosBelge.jpg";
                if (requestPageVM.AddExpenseRequestVM.FilePath != null && requestPageVM.AddExpenseRequestVM.FilePath.Length > 0)
                {
                    Guid guid = Guid.NewGuid();
                    strFileName = guid + "_" + requestPageVM.AddExpenseRequestVM.FilePath.FileName;
                    string strPath = Path.Combine("wwwroot/expenseFiles", strFileName);
                    using (var fs = new FileStream(strPath, FileMode.Create))
                    {
                        await requestPageVM.AddExpenseRequestVM.FilePath.CopyToAsync(fs);
                    }
                }
                newExpense.FilePath = strFileName;

                var response = await _httpClient.PostAsJsonAsync("api/ExpenseRequest/add-expenseRequest", newExpense);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["AddedExpense"] = "Harcama Talebi Oluşturuldu!";
                }
                else
                {
                    ViewData["AddExpenseError"] = "Harcama talebi oluşturulurken bir hata oluştu!";
                }
            }
            ExpenseRequestPageVM refreshedPageVM = new ExpenseRequestPageVM();
            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response2 = await _httpClient.GetAsync($"api/ExpenseRequest/get-expenseRequestsForEmployee/{EmployeeID}");
            if (response2.IsSuccessStatusCode)
            {
                var responseBody = await response2.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowExpenseRequestDTO>>>(responseBody);
                refreshedPageVM.ExpenseRequests = apiResponse.Data;
            }

            return View(refreshedPageVM);
        }
        public async Task<IActionResult> CancelExpenseRequest(int id)
        {

            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/ExpenseRequest/cancel-expenseRequest/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {
                        return RedirectToAction("ExpenseRequest"/*, new ResultMessage { Message = "Çalışan talebi iptal edildi!", IsSuccess = true }*/); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {

                        return RedirectToAction("ExpenseRequest" /*new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
                    }
                }
                else
                {

                    return RedirectToAction("ExpenseRequest"/* new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
                }
            }
            else
            {

                return RedirectToAction("ExpenseRequest" /*new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
            }
        }




        public async Task<IActionResult> LeaveRequest()
        {
            LeaveRequestPageVM requestPageVM = new LeaveRequestPageVM();
            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response = await _httpClient.GetAsync($"api/LeaveRequest/get-leaveRequestsForEmployee/{EmployeeID}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowLeaveRequestDTO>>>(responseBody);
                requestPageVM.LeaveRequests = apiResponse.Data;
            }
            return View(requestPageVM);
        }
        [HttpPost]
        public async Task<IActionResult> LeaveRequest(LeaveRequestPageVM requestPageVM)
        {
            if (requestPageVM.AddLeaveRequestDTO != null)
            {
                AddLeaveRequestDTO newLeave = new AddLeaveRequestDTO
                {
                    EmployeeID = HttpContext.Session.GetInt32("UserID").GetValueOrDefault(),
                    LeaveType = requestPageVM.AddLeaveRequestDTO.LeaveType,
                    LeaveStartingDate = requestPageVM.AddLeaveRequestDTO.LeaveStartingDate,
                    LeaveEndDate = requestPageVM.AddLeaveRequestDTO.LeaveEndDate,
                    LeaveDays = requestPageVM.AddLeaveRequestDTO.LeaveDays
                };


                var leaveResponse = await _httpClient.PostAsJsonAsync("api/LeaveRequest/add-leaveRequest", newLeave);

                if (leaveResponse.IsSuccessStatusCode)
                {
                    ViewData["AddedLeaveRequest"] = "İzin Talebi Oluşturuldu!";
                }
                else
                {
                    ViewData["AddedLeaveRequestError"] = "İzin talebi oluşturulurken bir hata oluştu!";
                }
            }
            LeaveRequestPageVM refreshedPageVM2 = new LeaveRequestPageVM();
            EmployeeID = HttpContext.Session.GetInt32("UserID");
            var response3 = await _httpClient.GetAsync($"api/LeaveRequest/get-leaveRequestsForEmployee/{EmployeeID}");
            if (response3.IsSuccessStatusCode)
            {
                var responseBody = await response3.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShowLeaveRequestDTO>>>(responseBody);
                refreshedPageVM2.LeaveRequests = apiResponse.Data;
            }
            return View(refreshedPageVM2);
        }
        public async Task<IActionResult> CancelLeaveRequest(int id)
        {

            if (id != 0)
            {
                var response = await _httpClient.PutAsync($"api/LeaveRequest/cancel-leaveRequest/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // API'den gelen yanıtı işleme
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(responseContent);

                    if (result.Success)
                    {
                        return RedirectToAction("LeaveRequest"/*, new ResultMessage { Message = "Çalışan talebi iptal edildi!", IsSuccess = true }*/); // Yönlendirmek istediğiniz aksiyona yönlendirin.
                    }
                    else
                    {

                        return RedirectToAction("LeaveRequest" /*new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
                    }
                }
                else
                {

                    return RedirectToAction("LeaveRequest"/* new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
                }
            }
            else
            {

                return RedirectToAction("LeaveRequest" /*new ResultMessage { Message = "İptal işlemi başarısız!", IsSuccess = false }*/);
            }
        }


       
    }
}
