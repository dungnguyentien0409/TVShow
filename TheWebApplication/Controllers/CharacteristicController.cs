using System.Diagnostics;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TheWebApplication.Middleware;
using TheWebApplication.Models;
using ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities = Domain.Entities;
using Services.Interfaces;
using Dto;

namespace TheWebApplication.Controllers;

public class CharacteristicController : Controller
{
    private readonly IMapper _mapper;
    private ICharacteristicService _charService;

    public CharacteristicController(ICharacteristicService charService, IMapper mapper)
    {
        _charService = charService;
        _mapper = mapper;
    }

    [HttpGet]
    [RateLimit(Seconds = 10)]
    public IActionResult Index()
    {
        var viewModel = new List<CharacteristicViewModel>();

        var result = _charService.GetAllCharacteristic();
        foreach(var dto in result)
        {
            viewModel.Add(_mapper.Map<CharacteristicViewModel>(dto));
        }

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult AddNew()
    {
        var newCharItem = _charService.AddNew();
        var viewModel = _mapper.Map<CharacteristicViewModel>(newCharItem);

        return PartialView("_PartialViewAddNew", viewModel);
    }

    [HttpPost]
    public IActionResult CreateCharacteristic(CharacteristicViewModel viewModel)
    {
        var charDto = _mapper.Map<CharacteristicDto>(viewModel);
        var created = _charService.Create(charDto);

        return Json(created);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

