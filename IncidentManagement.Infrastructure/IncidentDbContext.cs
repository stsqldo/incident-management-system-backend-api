using Microsoft.EntityFrameworkCore;
using IncidentManagement.Domain.Entities;

namespace IncidentManagement.Infrastructure;

public class IncidentDbContext : DbContext
{
    public IncidentDbContext(DbContextOptions<IncidentDbContext> options) : base(options) {}

    public DbSet<Incident> Incidents => Set<Incident>();
}