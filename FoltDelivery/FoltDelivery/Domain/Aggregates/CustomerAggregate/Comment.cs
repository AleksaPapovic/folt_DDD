using System;
using FoltDelivery.Model.Enums;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.CustomerAggregate
{
    public class Comment : Entity
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public String Text { get; set; }
        public int Rating { get; set; }
        public CommentStatus Status { get; set; }
        public int LogicalDeleted { get; set; }

        public Comment(Guid id):base(id) { }

        public Comment(Guid id, int userId, int restaurantId, String text, int rating, CommentStatus status,
            int logicalDeleted) : base(id)
        {
            UserId = userId;
            RestaurantId = restaurantId;
            Text = text;
            Rating = rating;
            Status = status;
            LogicalDeleted = logicalDeleted;
        }
    }
}
