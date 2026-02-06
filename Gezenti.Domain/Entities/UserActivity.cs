using Gezenti.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gezenti.Domain.Entities
{
    [BsonIgnoreExtraElements] 
    public  class UserActivity : BaseEntity
    {
        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string PlaceId { get; set; } = null!;

        public string PlaceCategories { get; set; } = null!;

        public string City { get; set; } = null!;
        public ActivityDetails Details { get; set; } = new();


    }

    public class ActivityDetails
    {
        public double RatingGiven { get; set; }
    }
}

