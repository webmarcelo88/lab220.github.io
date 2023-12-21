using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Components.Company.VmUser;

public partial class VmUserForm
{
    #region Injections
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    #region Parameters
    [Parameter]
    public EventCallback OnValidSubmitAsync { get; set; }

    [Parameter]
    public Entities.VmUser? VmUser { get; set; }
    [Parameter]
    public bool IsParentLoading { get; set; } = false;
    #endregion

    public bool _isLoading = false;
    public bool _isProcessing = false;
    private string _confirmPass = string.Empty;
    public bool success;
    public string[] errors = { };

    MudTextField<string> pwField1;
    MudForm form;

    protected async override Task OnInitializedAsync()
    {
        if (VmUser != null)
        {
            _confirmPass = VmUser.Pass;
        }

        if (VmUser == null)
        {
            VmUser = new();
            VmUser.IsActive = true;
        }
        _isLoading = false;
    }

    private async Task SaveAsync()
    {
        _isProcessing = true;
        await form.Validate();
        if (form.IsValid && OnValidSubmitAsync.HasDelegate)
        {
            await OnValidSubmitAsync.InvokeAsync();
        }
        _isProcessing = false;
    }

    private IEnumerable<string> PasswordStrength(string pw)
    {
        if (string.IsNullOrWhiteSpace(pw))
        {
            yield return "Senha é obrigatória!!";
            yield break;
        }
        //if (pw.Length < 8)
        //    yield return "Senha deve conter no mínimo 8 caracteres";
        //if (!Regex.IsMatch(pw, @"[A-Z]"))
        //    yield return "Senha deve contar ao menos uma letra maiúscula";
        //if (!Regex.IsMatch(pw, @"[a-z]"))
        //    yield return "Senha deve contar ao menos uma letra minúscula";
        if (!Regex.IsMatch(pw, @"[0-9]"))
            yield return "Senha deve conter ao menos um número";
    }

    private string PasswordMatch(string arg)
    {
        if (pwField1.Value != arg)
            return "As senhas não batem!";
        return null;
    }
}