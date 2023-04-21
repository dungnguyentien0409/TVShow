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
    private const int PAGE_SIZE = 5;

    public CharacteristicController(ICharacteristicService charService, IMapper mapper)
    {
        _charService = charService;
        _mapper = mapper;
    }

    [HttpGet]
    [RateLimit(Seconds = 10)]
    public IActionResult Index(int? pageIndex)
    {
        return View();
    }

    [HttpGet]
    [RateLimit(Seconds = 10)]
    public IActionResult GetCharacteristicGrid(int? pageIndex)
    {
        var index = pageIndex.HasValue ? Math.Max(pageIndex.Value, 0) : 0;
        var viewModel = new PagedResponse<List<CharacteristicViewModel>>();

        var result = _charService.GetAllCharacteristic(index, PAGE_SIZE);
        foreach (var dto in result.Results)
        {
            viewModel.Results.Add(_mapper.Map<CharacteristicViewModel>(dto));
        }

        viewModel.TotalPage = result.TotalPage;
        viewModel.PageIndex = result.PageIndex;
        return PartialView("_PartialViewCharacteristic", viewModel);
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

