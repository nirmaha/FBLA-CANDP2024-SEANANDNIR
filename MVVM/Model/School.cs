using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

#pragma warning disable CS8618

namespace EduPartners.MVVM.Model
{
    public class School
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid Code { get; set; }
    }
}
