using Common.Domain.Telemetry;
using System.Diagnostics;

namespace Common.Infrastructure.Telemetry;
public class ActivityScope : IActivityScope
{
    private readonly Activity? _activity;
    private bool _disposed;

    public ActivityScope(string name)
    {
        _activity = new Activity(name);
        _activity.Start();
    }

    public void AddTag(string key, string value)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(ActivityScope));
        _activity?.AddTag(key, value);
    }

    public void AddEvent(string name)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(ActivityScope));
        _activity?.AddEvent(new ActivityEvent(name));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _activity?.Stop();
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ActivityScope()
    {
        Dispose(false);
    }
}