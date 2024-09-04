using System;
using Unity.PerformanceTesting;

public static class Utility
{
    public delegate void MeasureAction(string name, Action run, Action setup = null, Action cleanup = null);

    public static MeasureAction CreateMeasure(int warmup = 10, int iterations = 10000, int measureCount = 20, bool reportGC = false)
    {
        return (name, run, setup, cleanup) =>
        {
            var method = Measure.Method(run)
                .SampleGroup(name)
                .WarmupCount(warmup)
                .IterationsPerMeasurement(iterations)
                .MeasurementCount(measureCount)
            ;
            if (setup != null) method = method.SetUp(setup);
            if (cleanup != null) method = method.CleanUp(cleanup);
            if (reportGC) method = method.GC();
            method.Run();
        };
    }
}
