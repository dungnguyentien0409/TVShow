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
using Microsoft.AspNetCore.RateLimiting;
using Middleware;

namespace TheWebApplication.Controllers;

[ApiController]
[Route("characteristic-management")]
[Route("")]
public class CharacteristicController : Controller
{
    private readonly IMapper _mapper;
    private ICharacteristicService _charService;
    private IConfiguration _config;
    private const int PAGE_SIZE = 5;

    public CharacteristicController(ICharacteristicService charService, IMapper mapper, IConfiguration config)
    {
        _charService = charService;
        _mapper = mapper;
        _config = config;
    }

    [HttpGet]
    public IActionResult Index(int? pageIndex)
    {
        var searchCriteriaDto = _charService.GetSearchCriterias();
        var viewModel = _mapper.Map<SearchCriteriaViewModel>(searchCriteriaDto);

        return View(viewModel);
    }

    [HttpPost("characteristics")]
    [AddHeader("from-database", "true")]
    [RateLimitingAttributeFactory(Name = "GetCharacteristic")]
    public IActionResult GetCharacteristicGrid([FromForm] CharacteristicGridViewModel request)
    {
        var index = request.PageIndex.HasValue ? Math.Max(request.PageIndex.Value, 0) : 0;
        var viewModel = new PagedResponse<List<CharacteristicViewModel>>();
        var pageSize = _config.GetValue<int>("PageSize");

        var result = _charService.GetAllCharacteristic(request.LocationId, index, pageSize);
        foreach (var dto in result.Results)
        {
            viewModel.Results.Add(_mapper.Map<CharacteristicViewModel>(dto));
        }

        viewModel.TotalPage = result.TotalPage;
        viewModel.PageIndex = result.PageIndex;
        return PartialView("_PartialViewCharacteristic", viewModel);
    }

    [HttpGet("characteristic")]
    public IActionResult AddNew()
    {
        var newCharItem = _charService.AddNew();
        var viewModel = _mapper.Map<CharacteristicViewModel>(newCharItem);

        return PartialView("_PartialViewAddNew", viewModel);
    }

    [HttpPost("characteristic")]
    [RateLimitingAttributeFactory(Name = "GetCharacteristic", IsCreating = true)]
    public IActionResult CreateCharacteristic([FromForm] CharacteristicViewModel viewModel)
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

