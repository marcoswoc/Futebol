using Futebol.Shared.Handlers;
using Futebol.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Futebol.Web.Pages.Player;

public partial class List : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public List<PlayerModel> Players { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    [Inject]
    public IPlayerHandler Handler { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.GetAllAsync(new());

            if (result.IsSucess)
                Players = result.Data ?? [];
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

    public Func<PlayerModel, bool> Filter => team =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (team.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (team.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };

    public async void OnDeleteButtonClickedAsync(Guid id, string name)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir o jogador {name} será excluído",
            yesText: "Excluir",
            noText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, name);

        StateHasChanged();
    }

    private async Task OnDeleteAsync(Guid id, string name)
    {
        try
        {
            await Handler.DeleteAsync(new() { Id = id });
            Players.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Jogador {name} excluído", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}
