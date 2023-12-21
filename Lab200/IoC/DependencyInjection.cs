using Lab200.Interfaces;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;
using Lab200.Repositories;
using Lab200.Services;

namespace Lab200.IoC;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        AddServicesDependencyInjection(services);
        AddRepositoriesDependencyInjection(services);
    }

    private static void AddRepositoriesDependencyInjection(IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IColoursRepository, ColoursRepository>();
        services.AddScoped<ICostCenterRepository, CostCenterRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeItemsRepository, EmployeeItemsRepository>();
        services.AddScoped<IFunctionRepository, FunctionRepository>();
        services.AddScoped<IGridRepository, GridRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IPlantRepository, PlantRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddScoped<IScaleRepository, ScaleRepository>();
        services.AddScoped<ISectorRepository, SectorRepository>();
        services.AddScoped<IThemeRepository, ThemeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVmUserRepository, VmUserRepository>();
    }

    private static void AddServicesDependencyInjection(IServiceCollection services)
    {
        services.AddScoped<ISessionState, SessionState>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IColoursService, ColoursService>();
        services.AddScoped<ICostCenterService, CostCenterService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEmployeeItemsService, EmployeeItemsService>();
        services.AddScoped<IFunctionService, FunctionService>();
        services.AddScoped<IGridService, GridService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IPlantService, PlantService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductTypeService, ProductTypeService>();
        services.AddScoped<IScaleService, ScaleService>();
        services.AddScoped<ISectorService, SectorService>();
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVmUserService, VmUserService>();
    }
}