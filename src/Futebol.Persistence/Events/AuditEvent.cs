using Futebol.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Persistence.Events;
public static class AuditEvent
{
    public static void SavingChanges(object sender, EventArgs eventArgs = null)
    {
        if (sender is not DbContext context) return;

        foreach (var entrie in context.ChangeTracker.Entries())
        {
            if (entrie.Entity is not IAuditable auditable)
                continue;

            if (entrie.State == EntityState.Added || entrie.State == EntityState.Detached)
            {
                auditable.CreatedAt = DateTime.Now;
            }
            else if (entrie.State == EntityState.Modified)
            {
                auditable.UpdatedAt = DateTime.Now;
            }
            else if (entrie.State == EntityState.Deleted)
            {
                auditable.DeletedAt = DateTime.Now;
                entrie.State = EntityState.Modified;
            }
        }
    }

    public static void SavedChanges(object sender, EventArgs eventArgs = null) { }
}
