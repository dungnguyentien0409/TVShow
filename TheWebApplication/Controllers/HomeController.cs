using System.Diagnostics;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TheWebApplication.Middleware;
using TheWebApplication.Models;
using ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities = Domain.Entities;

namespace TheWebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [RateLimit(Seconds = 10)]
    public IActionResult Index()
    {
        var viewModel = new List<CharacteristicViewModel>();

        var response = _unitOfWork.Characteristic.GetAll().ToList();
        foreach(var item in response)
        {
            item.StatusItem = _unitOfWork.Status.GetByIdOrDefault(item.StatusId);
            item.TypeItem = _unitOfWork.Type.GetByIdOrDefault(item.TypeId);
            item.SpeciesItem = _unitOfWork.Species.GetByIdOrDefault(item.SpeciesId);
            item.GenderItem = _unitOfWork.Gender.GetByIdOrDefault(item.GenderId);
            item.Episodes = _unitOfWork.Episode.GetByCharacteristicId(item.Id).ToList();
            item.Location = _unitOfWork.Location.GetByIdOrDefault(item.LocationId);
            item.Origin = _unitOfWork.Origin.GetByIdOrDefault(item.OriginId);

            var vm = _mapper.Map<CharacteristicViewModel>(item);
            viewModel.Add(vm);
        }

        return View(viewModel);
    }

    public IActionResult AddNew()
    {
        var viewModel = new CharacteristicViewModel();

        viewModel.Locations = _unitOfWork.Location.GetAll()
            .Select(s => new SelectListItem
            {
                Text = s.Name, Value = s.Id.ToString()
            });
        viewModel.Types = _unitOfWork.Type.GetAll()
            .Select(s => new SelectListItem
            {
                Text = s.Type,
                Value = s.Id.ToString()
            });
        viewModel.Genders = _unitOfWork.Gender.GetAll()
            .Select(s => new SelectListItem
            {
                Text = s.Gender,
                Value = s.Id.ToString()
            });
        viewModel.Specieses = _unitOfWork.Species.GetAll()
            .Select(s => new SelectListItem
            {
                Text = s.Species,
                Value = s.Id.ToString()
            });
        viewModel.Statuses = _unitOfWork.Status.GetAll()
            .Select(s => new SelectListItem
            {
                Text = s.Status,
                Value = s.Id.ToString()
            });
        viewModel.Origins = _unitOfWork.Origin.GetAll()
            .Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
        viewModel.Url = "";
        viewModel.Image = "";

        return PartialView("_PartialViewAddNew", viewModel);
    }

    [HttpPost]
    public IActionResult CreateCharacteristic(CharacteristicViewModel viewModel)
    {
        try
        {
            var item = _mapper.Map<Entities.Characteristic>(viewModel);
            item.GenderItem =  null;
            item.SpeciesItem = null;
            item.Location =  null;
            item.Origin = null;
            item.TypeItem =  null;
            item.StatusItem = null;

            _unitOfWork.Characteristic.Add(item);
            _unitOfWork.Save();

            return Json(true);
        }
        catch(Exception ex)
        {
            _logger.LogError("Error when inserting new characteristic", ex);
            return Json(false);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

