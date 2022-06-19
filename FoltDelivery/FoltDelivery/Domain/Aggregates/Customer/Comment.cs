using FoltDelivery.Model.Enums;
using System;

namespace FoltDelivery.Model
{
    public class Comment : Entity
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public String Text { get; set; }
        public int Rating { get; set; }
        public int Commented { get; set; }
        public CommentStatus Status { get; set; }
        public int LogicalDeleted { get; set; }

        public Comment() { }

        public Comment(Guid id, int userId, int restaurantId, String text, int rating, CommentStatus status,
            int logicalDeleted)
        {
            Id = id;
            UserId = userId;
            RestaurantId = restaurantId;
            Text = text;
            Rating = rating;
            Status = status;
            LogicalDeleted = logicalDeleted;
        }
    }
}
