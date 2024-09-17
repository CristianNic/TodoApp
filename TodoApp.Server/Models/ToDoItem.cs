using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TodoApp.Models
{
    public class ToDoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }  // Changed type to string? to match MongoDB ObjectId

        [BsonElement("title")]
        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [BsonElement("text")]
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [BsonElement("deadline")]
        [JsonPropertyName("deadline")]
        public DateTime? Deadline { get; set; }

        [BsonElement("status")]
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [BsonElement("completed")]
        [JsonPropertyName("completed")]
        public bool Completed { get; set; }
    }
}
