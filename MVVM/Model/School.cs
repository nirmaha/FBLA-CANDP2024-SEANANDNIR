using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

#pragma warning disable CS8618

namespace EduPartners.MVVM.Model
{
    public class School
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Address { get; set; }

        [BsonRequired]
        public string Code { get; set; }

        [BsonRequired]
        public string City { get; set; }

        [BsonRequired]
        public string State { get; set; }

        [BsonRequired]
        public string Zip { get; set; }

        public Optional<List<Partner>> Partners { get; set; } = new Optional<List<Partner>>();
       
    }
}
