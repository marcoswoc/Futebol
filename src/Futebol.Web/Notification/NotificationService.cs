using MudBlazor;

namespace Futebol.Web.Notification;

public class NotificationService
{
    private readonly ISnackbar _snackbar;
    private const int _visibleStateDuration = 4000;
    public NotificationService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }

    private async Task Notify(string message, Severity severity)
    {
        _snackbar.Clear();
        ToSetUp();
        _snackbar.Add(message, severity);
        await Task.Delay(_visibleStateDuration);
        _snackbar.Clear();
    }

    private void ToSetUp()
    {
        var config = _snackbar.Configuration;
        config.PreventDuplicates = true;
        config.NewestOnTop = false;
        config.ShowCloseIcon = true;
        config.VisibleStateDuration = _visibleStateDuration;
        config.HideTransitionDuration = 1000;
        config.ShowTransitionDuration = 1000;
        config.SnackbarVariant = Variant.Filled;
    }

    public async Task Success(string message)
        => await Notify(message, Severity.Success);

    public async Task Error(string message)
        => await Notify(message, Severity.Error);


    public async Task Info(string message)
        => await Notify(message, Severity.Info);

}
