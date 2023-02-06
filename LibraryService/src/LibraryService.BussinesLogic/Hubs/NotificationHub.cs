using Microsoft.AspNetCore.SignalR;

namespace LibrarySevice.BussinesLogic.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
