using Microsoft.AspNetCore.SignalR;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Hubs
{
    public class ChatHub : Hub
    {
        private readonly TravelGuideContext _context;

        public ChatHub(TravelGuideContext context)
        {
            _context = context;
        }

        public async Task SendMessage(int chatId, string text, bool isManager = false)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            if (chat == null || chat.Status == ChatStatus.Closed)
            {
                return;
            }

            // Получаем ID пользователя из контекста (из сессии)
            var userIdStr = Context.GetHttpContext()?.Session.GetInt32("UserId");
            if (userIdStr == null) return;
            
            var message = new Message
            {
                ChatId = chatId,
                Text = text,
                Timestamp = DateTime.Now,
                SenderId = userIdStr.Value
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // Отправляем сообщение всем участникам чата
            await Clients.Group($"chat_{chatId}").SendAsync("ReceiveMessage", new
            {
                id = message.Id,
                text = message.Text,
                timestamp = message.Timestamp.ToString("dd.MM HH:mm"),
                senderId = message.SenderId,
                isManager = isManager
            });
        }

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");
        }

        public async Task LeaveChat(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"chat_{chatId}");
        }

        public override async Task OnConnectedAsync()
        {
            // Можно добавить логику при подключении
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Можно добавить логику при отключении
            await base.OnDisconnectedAsync(exception);
        }
    }
}