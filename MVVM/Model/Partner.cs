using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#pragma warning disable CS8618

namespace EduPartners.MVVM.Model
{
   /* The Partner class defines properties for a partner entity with required fields such as Name,
   Description, ResourcesAvailable, Industry, StartDate, RepresentativeName, RepresentativeEmail,
   and Savings. Optional fields include RepresentativePhoneNumber, Website, and Address. */
    public class Partner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Description { get; set; }

        [BsonRequired]
        public string ResourcesAvailable { get; set; }

        [BsonRequired]
        public string Industry { get; set; }

        [BsonRequired]
        public DateTime StartDate { get; set; }

        [BsonRequired]
        public string RepresentativeName { get; set; }

        [BsonRequired]
        public string RepresentativeEmail { get; set; }

        public string RepresentativePhoneNumber { get; set; } // Optional
        public string Website { get; set; } // Optional
        public string Address { get; set; } // Optional

        [BsonRequired]
        public double Savings { get; set; }
    }
}
