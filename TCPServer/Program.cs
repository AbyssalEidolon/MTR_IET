using TCPServer;
if (args.Length == 0)
{
    Server server = new();
}
else if (args.Contains<string>("--client"))
{
    TestClient client = new();
}

