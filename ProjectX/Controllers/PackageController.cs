using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Profile;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectX.Controllers
{
    public class PackageController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IProfileBusiness _profileBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly CcAppSettings _appSettings;
        private User _user;

        private IWebHostEnvironment _env;


        public PackageController(IHttpContextAccessor httpContextAccessor, IOptions<CcAppSettings> appIdentitySettingsAccessor, IProfileBusiness profileBusiness, IGeneralBusiness generalBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _profileBusiness = profileBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (User)httpContextAccessor.HttpContext.Items["User"];
            _env = env;

        }


        // GET: CobController
        public ActionResult Index()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadCountries = true,
                loadProfileTypes = true,
                loadDocumentTypes = true
            });
            return View(response);
        }

        // GET: CobController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CobController/Create
        public ActionResult Create()
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = new LoadDataModel();
            ViewData["filldata"] = response;

            GetProfileResp ttt = new GetProfileResp();
            ttt.profile = new Profile();
            return View(ttt);
        }

        // POST: CobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CobController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CobController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CobController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
