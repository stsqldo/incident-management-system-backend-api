using Microsoft.EntityFrameworkCore;
using IncidentManagement.Domain.Entities;

namespace IncidentManagement.Infrastructure;

public class IncidentRepository : IIncidentRepository
{
    private readonly IncidentDbContext _ctx;

    public IncidentRepository(IncidentDbContext ctx)
    {
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
    }
    public async Task<List<Incident>> GetAllAsync()
    {
        try
        {
            return await _ctx.Incidents
                .Include(i => i.Attachments)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Failed to retrieve incidents from the database.",
                ex);
        }
    }
    public async Task<Incident?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid incident id.", nameof(id));

        try
        {
            return await _ctx.Incidents
                .Include(i => i.Attachments)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Failed to retrieve incident with id {id}.",
                ex);
        }
    }
    public async Task AddAsync(Incident incident)
    {
        if (incident == null)
            throw new ArgumentNullException(nameof(incident));

        try
        {
            await _ctx.Incidents.AddAsync(incident);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(
                "Failed to add incident to the database.",
                ex);
        }
    }
    public async Task SaveAsync()
    {
        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException(
                "Concurrency conflict occurred while saving incident data.",
                ex);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(
                "Database update failed while saving incident data.",
                ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Unexpected error occurred while saving incident data.",
                ex);
        }
    }
    public async Task UpdateAsync(Incident incident)
    {
        if (incident == null)
            throw new ArgumentNullException(nameof(incident));

        try
        {
            // Attach entity if not tracked
            if (_ctx.Entry(incident).State == EntityState.Detached)
            {
                _ctx.Incidents.Attach(incident);
            }

            _ctx.Incidents.Update(incident);

            // Intentionally async for future extensibility
            await Task.CompletedTask;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException(
                "Concurrency conflict occurred while updating the incident.", ex);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(
                "Failed to update incident in the database.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Unexpected error occurred while updating the incident.", ex);
        }
    }

}
