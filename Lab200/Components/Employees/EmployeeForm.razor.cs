using System;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Lab200.Pages.Employee;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using MudBlazor;

namespace Lab200.Components.Employees;

public partial class EmployeeForm
{
    #region Injections
    [Inject] ISnackbar _snackBar { get; set; }
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] ICostCenterService _costCenterService { get; set; } = null!;
    [Inject] IPlantService _plantService { get; set; } = null!;
    [Inject] ISectorService _sectorService { get; set; } = null!;
    [Inject] IFunctionService _functionService { get; set; } = null!;
    [Inject] IEmployeeService _employeeService { get; set; } = null!;
    [Inject] IItemService _itemService { get; set; } = null!;
    [Inject] IWebHostEnvironment _webHostEnvironment { get; set; } = null!;
    #endregion

    #region Parameters
    [Parameter]
    public Entities.Employee Employee { get; set; }

    [Parameter]
    public List<CostCenter>? CostCenters { get; set; }

    [Parameter]
    public List<Plant>? Plants { get; set; }

    [Parameter]
    public EventCallback OnSectorIdChange { get; set; }

    [Parameter]
    public EventCallback OnValidSubmitAsync { get; set; }
    #endregion

    #region DropDowns
    List<CostCenter>? costCenters;
    List<Plant>? plants;
    List<Sector>? sectors;
    List<Function>? functions;
    #endregion

    #region FormHelpers
    private string costCenter { get; set; }
    private string plantIdentifier { get; set; }
    private string sectorIdentifier { get; set; }
    private string functionIdentifier { get; set; }

    IBrowserFile picture;
    string imageData;
    bool _processing;
    #endregion

    protected async override Task OnInitializedAsync()
    {
        Employee.ClientId = _sessionState.User.ClientId ?? 32;
        costCenter = Employee.CostCenterId.ToString() ?? string.Empty;
        costCenters = await _costCenterService.GetCostCentersByClientAsync(Employee.ClientId);
        plants = await _plantService.GetAllPlantsByClientAsync(Employee.ClientId);

        if (Employee.PlantId != null)
        {
            plantIdentifier = Employee.PlantId.Value.ToString();
        }
        if (Employee.SectorId != null)
        {
            sectorIdentifier = Employee.SectorId.Value.ToString();
            if (Employee.CostCenterId != null)
                sectors = await _sectorService.GetSectorByClientAndCostCenterAsync(Employee.ClientId, Employee.CostCenterId.Value);
        }
        if (Employee.FunctionId != null)
        {
            functionIdentifier = Employee.FunctionId.Value.ToString();
            if (Employee.CostCenterId != null)
                functions = await _functionService.GetFunctionsByClientAndCostCenterAsync(Employee.ClientId, Employee.CostCenterId.Value);
        }
        if (Employee.Id != (int)default)
        {
            imageData = $"images\\funcionarios\\{Employee.Picture}";
        }
    }

    private async Task GetFunctionsAndSectorsAsync(IEnumerable<string> costCenterId)
    {
        Employee.CostCenterId = int.Parse(costCenter);


        if (string.IsNullOrWhiteSpace(costCenter))
        {
            sectors = null;
            sectorIdentifier = string.Empty;
            Employee.SectorId = null;
            functions = null;
        }
        else
        {
            sectors = await _sectorService.GetSectorByClientAndCostCenterAsync(Employee.ClientId, int.Parse(costCenter));
            functions = await _functionService.GetFunctionsByClientAndCostCenterAsync(Employee.ClientId, int.Parse(costCenter));
        }
    }

    private async Task GetSectorItemsAsync(string sectorId)
    {
        sectorIdentifier = sectorId;
        Employee.SectorId = int.Parse(sectorId);

        await OnSectorIdChange.InvokeAsync();
    }

    private async Task HandleSaveButtonClick()
    {
        if (!string.IsNullOrWhiteSpace(sectorIdentifier))
            Employee.SectorId = int.Parse(sectorIdentifier);

        if (!string.IsNullOrWhiteSpace(costCenter))
            Employee.CostCenterId = int.Parse(costCenter);

        if (!string.IsNullOrWhiteSpace(functionIdentifier))
            Employee.FunctionId = int.Parse(functionIdentifier);


        if (OnValidSubmitAsync.HasDelegate)
        {
            await OnValidSubmitAsync.InvokeAsync();
        }
    }

    private async Task OnPictureSelectionAsync(InputFileChangeEventArgs e)
    {

        Random random = new Random();

        int maxDigits = 1000000; // 10^6

        foreach (var image in e.GetMultipleFiles(int.MaxValue))
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string imagesPath = Path.Combine(wwwrootPath, $"images\\funcionarios\\{Employee.ClientId}_{Employee.Registration}.jpeg");
            await using FileStream fs = new(imagesPath, FileMode.Create);
            await image.OpenReadStream().CopyToAsync(fs);
            imageData = $"images\\funcionarios\\{Employee.ClientId}_{Employee.Registration}.jpeg";
            Employee.Picture = $"{Employee.ClientId}_{Employee.Registration}.jpeg";
            StateHasChanged();
        }

    }
}

