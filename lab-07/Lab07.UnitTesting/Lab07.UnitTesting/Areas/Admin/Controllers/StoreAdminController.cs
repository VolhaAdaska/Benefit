using System;
using System.Web.Mvc;
using AutoMapper;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using System.Linq;
using Lab07.UnitTesting.Models;
using Lab07.UnitTesting.DAL.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lab07.UnitTesting.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class StoreAdminController : Controller
    {
        private readonly IStoreService storeService;
        private readonly IStoreTypeService storeTypeService;
        private readonly IMapper mapper;

        public StoreAdminController(IStoreService storeService, IStoreTypeService storeTypeService, IMapper mapper)
        {
            this.storeService = storeService ?? throw new ArgumentNullException(nameof(storeService));
            this.storeTypeService = storeTypeService ?? throw new ArgumentNullException(nameof(storeTypeService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult StoreList(int id)
        {
            var result = storeService.GetAllStore(id);
            if (result.Count == 0)
            {
                return View("Error");
            }
            var newModel = mapper.Map<IList<StoreDto>, IList<StoreViewModel>>(result);

            return View(newModel);
        }

        [HttpGet]
        public ActionResult AddStore()
        {
            StoreViewModel storeViewModel = new StoreViewModel
            {
                StoreTypeList = CreateStoreTypeList()
            };

            return View(storeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddStore(StoreViewModel model)
        {
            model.StoreTypeList = model.StoreTypeList ?? CreateStoreTypeList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StoreDto storeDto = mapper.Map<StoreViewModel, StoreDto>(model);
            OperationDetails operationDetails = await storeService.AddStoreAsync(storeDto);
            if (operationDetails.Succedeed)
            {
                return View("SuccessAdd");
            }
            ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            return View(model);
        }

        private IList<SelectListItem> CreateStoreTypeList()
        {
            var resultStoreType = storeTypeService.GetAllStoreType();
            return resultStoreType.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        }

        [HttpGet]
        public async Task<ActionResult> DeleteStore(StoreViewModel model)
        {
            StoreDto storeDto = mapper.Map<StoreViewModel, StoreDto>(model);

            OperationDetails operationDetails = await storeService.DeleteStoreAsync(storeDto);
            if (operationDetails.Succedeed)
            {
                return RedirectToAction("StoreTypeList", "StoreTypeAdmin");
            }
            ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            return RedirectToAction("StoreTypeList", "StoreTypeAdmin");
        }

        [HttpGet]
        public async Task<ActionResult> UpdateStore(int id)
        {
            StoreDto storeDto = await storeService.GetStoreByIdAsync(id);
            return View(mapper.Map<StoreDto, StoreViewModel>(storeDto));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStore(StoreViewModel model)
        {
            StoreDto storeDto = mapper.Map<StoreViewModel, StoreDto>(model);

            OperationDetails operationDetails = await storeService.UpdateStoreAsync(storeDto);
            if (operationDetails.Succedeed)
            {
                return RedirectToAction("StoreTypeList", "StoreTypeAdmin");
            }
            ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            return RedirectToAction("StoreTypeList", "StoreTypeAdmin");
        }
    }
}

