using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RazorPagesMovie.Hubs
{
    public class ChatHub : Hub
    {
	    public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }
    }
}

