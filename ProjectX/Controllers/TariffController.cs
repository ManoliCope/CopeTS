using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Tariff;
using ProjectX.Business.Profile;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Tariff;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using OfficeOpenXml;
using ExcelDataReader;

namespace ProjectX.Controllers
{
    public class TariffController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ITariffBusiness _tariffBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;



        public TariffController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, ITariffBusiness tariffBusiness, IGeneralBusiness generalBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _tariffBusiness = tariffBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
        }


        // GET: CobController
        public ActionResult Index()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadPlans = true,
            });

            //  throw new Exception("This is an example error.");

            return View(response);
        }

        [HttpPost]
        public TariffSearchResp Search(TariffSearchReq req)
        {
            TariffSearchResp response = new TariffSearchResp();
            response.tariff = _tariffBusiness.GetTariffList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadPlans = true,
            });
            ViewData["filldata"] = load;

            TariffGetResp response = new TariffGetResp();
            response.tariff = new TR_Tariff();
            return View(response);
        }


        public ActionResult importTariff()
        {
            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadPlans = true,
            });
            ViewData["filldata"] = load;

            TariffGetResp response = new TariffGetResp();
            response.tariff = new TR_Tariff();
            return View(response);
        }

        [HttpPost]
        public TariffResp CreateTariff(TariffReq req)
        {
            TariffResp response = new TariffResp();
            //if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
            //    return response;
            //}

            return _tariffBusiness.ModifyTariff(req, "Create", _user.U_Id);
        }


        public ActionResult Edit(int id)
        {
            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadPlans = true,
            });
            ViewData["filldata"] = load;

            TariffResp response = new TariffResp();
            response = _tariffBusiness.GetTariff(id);

            return View("details", response);
        }

        [HttpPost]
        public TariffResp EditTariff(TariffReq req)
        {
            TariffResp response = new TariffResp();
            if (req.id == 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            //if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
            //    return response;
            //}


            return _tariffBusiness.ModifyTariff(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public TariffResp DeleteTariff(int id)
        {
            TariffReq req = new TariffReq();
            req.id = id;
            req.tariff_starting_date = DateTime.Today;
            TariffResp response = new TariffResp();
            return _tariffBusiness.ModifyTariff(req, "Delete", _user.U_Id);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]

        public IActionResult exceltotable([FromForm(Name = "files")] IFormFileCollection files, int tarPackageid, int tarPlanid)
        {
            List<TR_Tariff> tariffs = new List<TR_Tariff>();
            List<int> rowsWithError = new List<int>();
            TariffResp response = new TariffResp();
            foreach (IFormFile formFile in files)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = new System.IO.MemoryStream())
                {
                    formFile.CopyTo(stream);
                    stream.Position = 0;
                    int rowNumber = 1;
                    using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth != 0)
                            {
                                rowNumber++;
                                //if (rowNumber > reader.Depth)
                                //    break; 
                                try
                                {
                                    var verifyrow = reader.GetValue(2);
                                    if (verifyrow != null)
                                        tariffs.Add(new TR_Tariff
                                        {
                                            P_Id = tarPackageid,
                                            T_Start_Age = Convert.ToInt16(reader.GetValue(1)),
                                            T_End_Age = Convert.ToInt16(reader.GetValue(2)),
                                            T_Number_Of_Days = Convert.ToInt16(reader.GetValue(3)),
                                            T_Price_Amount = Convert.ToDouble(reader.GetValue(4)),
                                            T_Net_Premium_Amount = Convert.ToDouble(reader.GetValue(5)),
                                            T_PA_Amount = Convert.ToDouble(reader.GetValue(6)),
                                            T_Tariff_Starting_Date = Convert.ToDateTime(reader.GetValue(7).ToString()),
                                            T_Override_Amount = Convert.ToDouble(reader.GetValue(8)),
                                            PL_Id = tarPlanid
                                        });
                                }
                                catch (Exception ex)
                                {
                                    rowsWithError.Add(rowNumber);
                                }

                            }
                        }
                    }
                    response = _tariffBusiness.ImportDataTariff(tariffs, _user.U_Id);
                }
            }

            if (rowsWithError.Count > 0)
            {
                string numbersString = string.Join(",", rowsWithError);
                return BadRequest(numbersString);
            }

            ///  after no error detected
            ///  call business and insert to db and change below return ok to text success

            return Ok(response);
        }

    }


}



