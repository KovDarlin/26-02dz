using System;
using System.Collections.Generic;

class Spectacle : IDisposable
{
    public string Title { get; set; }
    public string Theater { get; set; }
    public string Genre { get; set; }
    public int Time { get; set; }
    public List<string> Actors { get; set; }
    private bool disposed = false;

    public Spectacle(string title, string theater, string genre, int time, List<string> actors)
    {
        Title = title;
        Theater = theater;
        Genre = genre;
        Time = time;
        Actors = actors;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Theater: {Theater}");
        Console.WriteLine($"Жанр: {Genre}");
        Console.WriteLine($"Time: {Time} min");
        Console.WriteLine("Actors: " + string.Join(", ", Actors));
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Actors.Clear();
            }

            disposed = true;
        }
    }

    ~Spectacle()
    {
        Dispose(false);
    }
}

class Program
{
    static void Main(string[] args)
    {
        using (Spectacle spectacle = new Spectacle("Romeo anf Guliet", "Soborna", "Drama", 180, new List<string> { "Anna", "Mukola" }))
        {
            spectacle.ShowInfo();
        }

        Console.WriteLine("Deleted");
    }
}
