using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Team;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Futebol.Web.Pages.Team;

public partial class Edit : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public UpdateTeamRequest InputModel { get; set; } = new();

    [Parameter]
    public string Id { get; set; } = string.Empty;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public ITeamHandler Handler { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        GetByIdTeamRequest? request = null;

        try
        {
            request = new() { Id = Guid.Parse(Id) };
        }
        catch
        {
            Snackbar.Add("Parâmetro inválido", Severity.Error);
        }

        if (request is null)
            return;

        IsBusy = true;

        try
        {
            await Task.Delay(3000);
            var response = await Handler.GetByIdAsync(request);

            if (response is { IsSucess: true, Data: not null })
                InputModel = new()
                {
                    Id = response.Data.Id,
                    Name = response.Data.Name
                };
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

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.UpdateAsync(InputModel);

            if (result.IsSucess)
            {
                Snackbar.Add("Time atualizada", Severity.Success);
                NavigationManager.NavigateTo("/times");
            }
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
