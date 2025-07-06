namespace Common.Domain.Telemetry;
public interface IActivityScope : IDisposable
{
    void AddTag(string key, string value);
    void AddEvent(string name);
}
