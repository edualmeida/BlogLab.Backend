namespace Common.Domain.Telemetry;
public interface IActivityScopeFactory
{
    IActivityScope Start(string name);
}
