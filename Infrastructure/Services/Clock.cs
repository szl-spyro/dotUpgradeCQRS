using Application.Common.Interfaces;

namespace Infrastructure.Services;

public class Clock : IClock
{
    public virtual DateTime UtcNow => DateTime.Now;
}