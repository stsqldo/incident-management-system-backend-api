using IncidentManagement.Domain.Entities;

namespace IncidentManagement.Infrastructure;

public interface IIncidentRepository
{
    Task<List<Incident>> GetAllAsync();
    Task<Incident?> GetByIdAsync(int id);
    Task AddAsync(Incident incident);
    Task SaveAsync();
    Task UpdateAsync(Incident incident);
}