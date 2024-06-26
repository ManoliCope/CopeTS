﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Beneficiary;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Beneficiary;
using ProjectX.Entities.Resources;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using Irony.Parsing;
using ProjectX.Business.Users;

namespace ProjectX.Controllers
{
	public class BeneficiaryController : Controller
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private IBeneficiaryBusiness _beneficiaryBusiness;
		private IGeneralBusiness _generalBusiness;
		private readonly TrAppSettings _appSettings;
		private TR_Users _user;
		private IUsersBusiness _usersBusiness;

		private IWebHostEnvironment _env;


		public BeneficiaryController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IBeneficiaryBusiness beneficiaryBusiness, IWebHostEnvironment env, IUsersBusiness usersBusiness)
		{
			_httpContextAccessor = httpContextAccessor;
			_beneficiaryBusiness = beneficiaryBusiness;
			_generalBusiness = generalBusiness;
			_appSettings = appIdentitySettingsAccessor.Value;
			_user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
			_env = env;
			_usersBusiness = usersBusiness;
		}

		// GET: CobController
		public ActionResult Index()
		{
			string tes = string.Empty;
			LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
			{
				//loadCountries = true,
				//loadProfileTypes = true,
				//loadDocumentTypes = true
			});

			ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);
			return View(response);
		}

		[HttpPost]
		public BeneficiarySearchResp Search(BeneficiarySearchReq req)
		{
			BeneficiarySearchResp response = new BeneficiarySearchResp();
			response.beneficiary = _beneficiaryBusiness.GetBeneficiaryList(req, _user.U_Id);
			response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

			return response;
		}


		public ActionResult Create()
		{
			LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
			{
				loadDestinations = true,

			});

			ViewData["filldata"] = response;

			BeneficiaryGetResp ttt = new BeneficiaryGetResp();
			ttt.beneficiary = new TR_Beneficiary();
			return View(ttt);
		}


		[HttpPost]
		public BeneficiaryResp CreateBeneficiary(BeneficiaryReq req)
		{
			BeneficiaryResp response = new BeneficiaryResp();
			if (string.IsNullOrEmpty(req.FirstName) || string.IsNullOrWhiteSpace(req.FirstName))
			{
				response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
				return response;
			}

			return _beneficiaryBusiness.ModifyBeneficiary(req, "Create", _user.U_Id);
		}


		public ActionResult Edit(int id)
		{
			LoadDataResp filldata = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
			{
				loadDestinations = true,
			});

			ViewData["filldata"] = filldata;
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);

            BeneficiaryResp response = new BeneficiaryResp();
			response = _beneficiaryBusiness.GetBeneficiary(id, _user.U_Id);
			return View("details", response);
		}

		[HttpPost]
		public BeneficiaryResp EditBeneficiary(BeneficiaryReq req)
		{
			BeneficiaryResp response = new BeneficiaryResp();
			if (req.Id == 0)
			{
				response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
				return response;
			}

			if (string.IsNullOrEmpty(req.FirstName) || string.IsNullOrWhiteSpace(req.FirstName))
			{
				response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
				return response;
			}


			return _beneficiaryBusiness.ModifyBeneficiary(req, "Update", _user.U_Id);
		}

		[HttpPost]
		public BeneficiaryResp DeleteBeneficiary(int id)
		{
			BeneficiaryReq req = new BeneficiaryReq();
			req.Id = id;

			return _beneficiaryBusiness.ModifyBeneficiary(req, "Delete", _user.U_Id);
		}

		public BeneficiarySearchResp SearchBeneficiaryPref(string prefix)
		{
			BeneficiarySearchResp resp = new BeneficiarySearchResp();
			resp = _beneficiaryBusiness.SearchBeneficiaryPref(prefix, _user.U_Id);
			return resp;
		}
		private List<TR_Beneficiary> GenerateRandomBeneficiaries(string prefix)
		{
			List<TR_Beneficiary> beneficiaries = new List<TR_Beneficiary>();

			// Generate random beneficiary data
			for (int i = 0; i < 5; i++)
			{
				TR_Beneficiary beneficiary = new TR_Beneficiary
				{
					BE_Id = i + 1,
					BE_Sex = i % 2 == 0 ? 1 : 2, // Example: Alternates between 1 and 2 for sex
					BE_SexName = i % 2 == 0 ? "Male" : "Female", // Example: Alternates between "Male" and "Female" for sex name
					BE_FirstName = "FirstName" + i,
					BE_MiddleName = "MiddleName" + i,
					BE_LastName = "LastName" + i,
					BE_PassportNumber = "PASS" + i,
					BE_DOB = DateTime.Now.AddYears(-i).AddDays(i) // Example: Varies DOB based on index
				};

				beneficiaries.Add(beneficiary);
			}

			return beneficiaries;
		}

		public BeneficiaryResp getbeneficiary(int id)
		{
			//id = 74;
			BeneficiaryResp response = new BeneficiaryResp();
			response = _beneficiaryBusiness.GetBeneficiary(id, _user.U_Id);
			return response;
		}

		public IActionResult OpenPopUpImportBeneficiaries()
		{
			return PartialView("~/Views/Shared/PopUpImportBeneficiaries.cshtml");
		}

		[HttpPost]
		[Consumes("multipart/form-data")]
		public List<ImportBeneficiariesReq> exceltotable([FromForm(Name = "files")] IFormFileCollection files, int isProduction)
		{
			List<ImportBeneficiariesReq> beneficiaries = new List<ImportBeneficiariesReq>();

			foreach (IFormFile formFile in files)
			{
				System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
				using (var stream = new System.IO.MemoryStream())
				{

					formFile.CopyTo(stream);
					stream.Position = 0;
					using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
					{
						while (reader.Read())
						{
							if (reader.Depth != 0)
							{
								beneficiaries.Add(new ImportBeneficiariesReq
								{
									FirstName = reader.IsDBNull(0) ? "" : (reader.GetValue(0)?.ToString() ?? ""),
									MiddleName = reader.IsDBNull(1) ? "" : (reader.GetValue(1)?.ToString() ?? ""),
									LastName = reader.IsDBNull(2) ? "" : (reader.GetValue(2)?.ToString() ?? ""),
									PassportNumber = reader.IsDBNull(3) ? "" : (reader.GetValue(3)?.ToString() ?? ""),
									DateOfBirth = reader.IsDBNull(4) ? DateTime.Today : Convert.ToDateTime(reader.GetValue(4)),
									Nationality = reader.IsDBNull(5) ? "" : (reader.GetValue(5)?.ToString() ?? ""),
									CountryResidence = reader.IsDBNull(6) ? "" : (reader.GetValue(6)?.ToString() ?? ""),
									Gender = reader.IsDBNull(7) ? "" : (reader.GetValue(7)?.ToString() ?? ""),
									RemoveDeductible = isProduction == 1 ? (reader.IsDBNull(8) ? "" : (reader.GetValue(8)?.ToString() ?? "")) : "",
									AddSportsActivities = isProduction == 1 ? (reader.IsDBNull(9) ? "" : (reader.GetValue(9)?.ToString() ?? "")) : ""
								});
							}
						}
					}
				}
			}
			return beneficiaries;
		}

		public BeneficiariesBatchSaveResp importBeneficiaries(string importedbatch, int isProduction)
		{
			BeneficiariesBatchSaveReq reqq = new BeneficiariesBatchSaveReq();
			List<ImportBeneficiariesReq> beneficiariesBatchDetailsList = DeserializeJsonString(importedbatch);
			reqq.beneficiaries = beneficiariesBatchDetailsList;
			reqq.userid = _user.U_Id;

			return _beneficiaryBusiness.SaveBeneficiariesBatch(reqq, isProduction);
		}

		public List<ImportBeneficiariesReq> DeserializeJsonString(string jsonString)
		{
			return JsonConvert.DeserializeObject<List<ImportBeneficiariesReq>>(jsonString);
		}

	}
}
