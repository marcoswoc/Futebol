namespace Futebol.Persistence.Events;
public class ContextEventHandler
{
    public event EventHandler<EventArgs> SavingChanges;
    public event EventHandler<EventArgs> SavedChanges;

    public ContextEventHandler()
    {
        SavingChanges += AuditEvent.SavingChanges;
        SavedChanges += AuditEvent.SavedChanges;
    }

    public void InvokeSavingChanges(object sender, EventArgs? eventArgs = null)
    {
        SavingChanges?.Invoke(sender, eventArgs);
    }

    public void InvokeSavedChanges(object sender, EventArgs? eventArgs = null)
    {
        SavedChanges?.Invoke(sender, eventArgs);
    }
}
