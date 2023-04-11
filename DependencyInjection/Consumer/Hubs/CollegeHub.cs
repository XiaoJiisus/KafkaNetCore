using Microsoft.AspNetCore.SignalR;

namespace Consumer.Hubs;

public class CollegeHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}