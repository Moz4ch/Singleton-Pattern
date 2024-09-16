/*using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Servers
{
    private static readonly Lazy<Servers> _instance = new Lazy<Servers>(() => new Servers());
    private Servers()
    {
        _servers = new HashSet<string>();
    }

    public static Servers Instance => _instance.Value;

    private readonly HashSet<string> _servers;

    public bool AddServer(string address)
    {
        if ((address.StartsWith("http://") || address.StartsWith("https://")) && _servers.Add(address))
        {
            return true;
        }
        return false;
    }

    public List<string> GetHttpServers()
    {
        return _servers.Where(server => server.StartsWith("http://")).ToList();
    }

    public List<string> GetHttpsServers()
    {
        return _servers.Where(server => server.StartsWith("https://")).ToList();
    }
}

public class Program
{
    public static void Main()
    {
        var servers = Servers.Instance;

        Console.WriteLine(servers.AddServer("http://example.com"));
        Console.WriteLine(servers.AddServer("https://secure.com"));
        Console.WriteLine(servers.AddServer("http://example.com"));
        Console.WriteLine(servers.AddServer("ftp://files.com"));

        Console.WriteLine("HTTP servers: " + string.Join(", ", servers.GetHttpServers()));
        Console.WriteLine("HTTPS servers: " + string.Join(", ", servers.GetHttpsServers()));
    }
}
*/

using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Servers
{
    private static readonly Servers _instance = new Servers();

    private Servers()
    {
        _servers = new HashSet<string>();
    }

    public static Servers Instance => _instance;

    private readonly HashSet<string> _servers;
    private static readonly object _lock = new object();

    public bool AddServer(string address)
    {
        lock (_lock)
        {
            if ((address.StartsWith("http://") || address.StartsWith("https://")) && _servers.Add(address))
            {
                return true;
            }
            return false;
        }
    }

    public List<string> GetHttpServers()
    {
        lock (_lock)
        {
            return _servers.Where(server => server.StartsWith("http://")).ToList();
        }
    }

    public List<string> GetHttpsServers()
    {
        lock (_lock)
        {
            return _servers.Where(server => server.StartsWith("https://")).ToList();
        }
    }
}

public class Program
{
    public static void Main()
    {
        var servers = Servers.Instance;

        Console.WriteLine(servers.AddServer("http://example.com"));
        Console.WriteLine(servers.AddServer("https://secure.com"));
        Console.WriteLine(servers.AddServer("http://example.com"));
        Console.WriteLine(servers.AddServer("ftp://files.com"));

        Console.WriteLine("HTTP servers: " + string.Join(", ", servers.GetHttpServers()));
        Console.WriteLine("HTTPS servers: " + string.Join(", ", servers.GetHttpsServers()));
    }
}
