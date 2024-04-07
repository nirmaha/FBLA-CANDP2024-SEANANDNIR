using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#pragma warning disable CS8618

namespace EduPartners.MVVM.Model
{
    public class Partner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; } // Required
        public string Description { get; set; } // Required
        public string ResourcesAvailable { get; set; } // Required
        public string Industry { get; set; } // Required
        public DateTime StartDate { get; set; } // Required
        public string RepresentativeName { get; set; } // Required
        public string RepresentativeEmail { get; set; } // Required
        public string RepresentativePhoneNumber { get; set; } // Optional
        public string Website { get; set; } // Optional
        public string Address { get; set; } // Optional
        public double Savings { get; set; } // Required
    }
}
