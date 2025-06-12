using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Service
{
    public class NotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotificationEntity>> GetUserNotificationsAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(20)
                .ToListAsync();
        }

        public async Task AddNotificationAsync(string userId, string message)
        {
            var notification = new NotificationEntity
            {
                UserId = userId,
                Message = message
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
