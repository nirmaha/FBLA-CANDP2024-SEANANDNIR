using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#pragma warning disable CS8618

namespace EduPartners.MVVM.Model
{
    /* This User class has properties such as Id, Name, Email, About, PhoneNumber,
    Password, ProfileImage, and HomeSchool. */
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        public string About { get; set; }

        public string PhoneNumber { get; set; }

        [BsonRequired]
        public string Password { get; set; }

        public BsonBinaryData ProfileImage { get; set; }

        [BsonRequired]
        public School HomeSchool { get; set; }
    }
}
