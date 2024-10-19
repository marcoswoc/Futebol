using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Player;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Futebol.Web.Pages.Player;

public partial class Create : ComponentBase
{
    public bool IsBusy { get; set; }
    public CreatePlayerRequest InputModel { get; set; } = new();

    [Inject]
    public IPlayerHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSucess)
            {
                Snackbar.Add(result.Message ?? "", Severity.Success);
                NavigationManager.NavigateTo("/");
            }
            else
                Snackbar.Add(result.Message ?? "", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
}