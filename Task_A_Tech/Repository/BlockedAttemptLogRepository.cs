using System.Collections.Concurrent;
using Task_A_Tech.Models;

public class BlockedAttemptLogRepository
{
    private readonly ConcurrentBag<BlockedAttemptLog> _logs = new();

    public void Add(BlockedAttemptLog log)
    {
        _logs.Add(log);
    }

    public List<BlockedAttemptLog> GetAll(int page, int pageSize)
    {
        return _logs
            .OrderByDescending(log => log.Timestamp)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}