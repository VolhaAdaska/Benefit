using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Lab07.UnitTesting.Models;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Lab07.UnitTesting.DTO;
using System.Collections.Generic;

namespace Lab07.UnitTesting.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly IStoreService storeService;
        private readonly IStoreTypeService storeTypeService;
        private readonly IMapper mapper;

        public StoreController(IStoreService storeService, IStoreTypeService storeTypeService, IMapper mapper)
        {
            this.storeService = storeService ?? throw new ArgumentNullException(nameof(storeService));
            this.storeTypeService = storeTypeService ?? throw new ArgumentNullException(nameof(storeTypeService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult StoreTypeList()
        {
            var result = storeTypeService.GetAllStoreType();
            var newModel = mapper.Map<IList<StoreTypeDto>, IList<StoreTypeViewModel>>(result);
            return View(newModel);
        }

        [HttpGet]
        public ActionResult StoreList(int id)
        {
            var result = storeService.GetAllStore(id);
            if (result.Count == 0)
            {
                return RedirectToAction("Error", "Error");
            }
            var newModel = mapper.Map<IList<StoreDto>, IList<StoreViewModel>>(result);
            return View(newModel);
        }
    }
}