using System;
using System.Collections.Generic;

/// <summary>
/// Registry, that is being serialized and deserialized every game ending to save data of run times
/// </summary>
[Serializable]
public class RunTimeHistoryRegistry
{
    /// <summary>
    /// Queue of recent run times
    /// </summary>
    public Queue<TimeSpan> runTimes;

    /// <summary>
    /// Queue of recent run dates
    /// </summary>
    public Queue<DateTime> dates;

    public TimeSpan bestTime { get; private set; }

    private const int maxRunTimes = 8;

    public RunTimeHistoryRegistry()
    {
        runTimes = new Queue<TimeSpan>();
        dates = new Queue<DateTime>();
    }

    /// <summary>
    /// Adds runTime to runTimes queue and assigns to bestTime if required
    /// As well as dequeues the items if queue size limit is reached
    /// </summary>
    /// <param name="timeSpan"></param>
    public void AddRunTime(TimeSpan timeSpan)
    {
        if (maxRunTimes == runTimes.Count)
        {
            runTimes.Dequeue();
            dates.Dequeue();
        }
        runTimes.Enqueue(timeSpan);
        dates.Enqueue(DateTime.Today);

        if (bestTime < timeSpan)
            bestTime = timeSpan;

    }
    /// <summary>
    /// Get run times queue as array
    /// </summary>
    /// <returns>TimeSpan array</returns>
    public TimeSpan[] GetRunTimes()
    {
        return runTimes.ToArray();
    }

    /// <summary>
    /// Get dates of runtimes
    /// </summary>
    /// <returns>DateTime array</returns>
    public DateTime[] GetDateTimes()
    {
        return dates.ToArray();
    }
}