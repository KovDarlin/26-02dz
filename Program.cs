using System;
using System.Diagnostics;

public class MemoryAllocator : IDisposable
{
    private object[] _objects;
    private bool _disposed = false;

    public MemoryAllocator(int size = 1000000)
    {
        _objects = new object[size];
        for (int i = 0; i < size; i++)
        {
            _objects[i] = new byte[1024]; 
        }
    }

    public void SimulateMemoryUsage()
    {
        for (int i = 0; i < _objects.Length; i += 1000)
        {
            _objects[i] = null;
        }
        GC.Collect(); 
    }

    public int GetGeneration()
    {
        if (_objects.Length > 0 && _objects[0] != null)
        {
            return GC.GetGeneration(_objects[0]);
        }
        return -1; 
    }

    public TimeSpan MeasureGcPerformance()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _objects = null;
            GC.Collect();
            _disposed = true;
        }
    }

    ~MemoryAllocator()
    {
        Dispose();
    }
}

public class MemoryAllocatorTest
{
    public static void RunTests()
    {
        Console.WriteLine("Running tests MemoryAllocator...");

        var allocator = new MemoryAllocator();
        Console.WriteLine($"Generation before purification: {allocator.GetGeneration()}");

        allocator.SimulateMemoryUsage();
        Console.WriteLine($"Generation after cleaning: {allocator.GetGeneration()}");

        var gcTime = allocator.MeasureGcPerformance();
        Console.WriteLine($"Execution time GC: {gcTime.TotalMilliseconds} MS");

        allocator.Dispose();
        Console.WriteLine("Tests completed.");
    }
}

class Program
{
    static void Main()
    {
        MemoryAllocatorTest.RunTests();
    }
}
