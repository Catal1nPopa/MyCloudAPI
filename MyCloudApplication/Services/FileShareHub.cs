using Microsoft.AspNetCore.SignalR;

namespace MyCloudApplication.Services
{
    public class FileShareHub : Hub
    {
        public async Task NotifyFileShared(int groupId, string fileName)
        {
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveFileNotification", fileName); //notificare catre utilizatorii din grupul specificat
        }

        public async Task JoinGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId); //alaturarea unui utilizator la un grup
        }

        public async Task LeaveGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId); //eliminarea unui utilizator dintr-un group
        }

        public async Task NotifyRemainingSpace(int userId, double remainingSpace)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveRemainingSpace", remainingSpace);
        }
    }
}
