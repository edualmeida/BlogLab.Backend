using Common.Domain.Telemetry;

namespace Common.Infrastructure.Telemetry;
public class ActivityScopeFactory : IActivityScopeFactory
{
    public IActivityScope Start(string name) => new ActivityScope(name);
}
