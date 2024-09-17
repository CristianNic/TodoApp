using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using TodoApp.Models;

public class ToDoItemSerializer : SerializerBase<ToDoItem>, IBsonDocumentSerializer
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ToDoItem value)
    {
        var bsonWriter = context.Writer;
        bsonWriter.WriteStartDocument();
        bsonWriter.WriteName("title");
        BsonSerializer.Serialize(bsonWriter, value.Title);
        bsonWriter.WriteName("text");
        BsonSerializer.Serialize(bsonWriter, value.Text);
        bsonWriter.WriteName("completed");
        BsonSerializer.Serialize(bsonWriter, value.Completed);
        bsonWriter.WriteName("status");
        BsonSerializer.Serialize(bsonWriter, value.Status);
        bsonWriter.WriteName("deadline");
        BsonSerializer.Serialize(bsonWriter, value.Deadline);
        bsonWriter.WriteEndDocument();
    }

    public override ToDoItem Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonReader = context.Reader;
        bsonReader.ReadStartDocument();

        // Read the _id field as an ObjectId and convert to a string
        ObjectId objectId = bsonReader.ReadObjectId();

        var title = bsonReader.ReadString();
        var text = bsonReader.ReadString();
        var completed = bsonReader.ReadBoolean();
        var status = bsonReader.ReadString();
        var deadlineLong = bsonReader.ReadDateTime();
        bsonReader.ReadEndDocument();

        var deadline = DateTime.FromFileTimeUtc(deadlineLong);

        return new ToDoItem
        {
            // Id = id,
            Id = objectId.ToString(),
            Title = title,
            Text = text,
            Completed = completed,
            Status = status,
            Deadline = deadline
        };
    }

    public bool TryGetMemberSerializationInfo(string memberName, out BsonSerializationInfo serializationInfo)
    {
        switch (memberName)
        {
            case "Title":
                serializationInfo = new BsonSerializationInfo("title", BsonSerializer.LookupSerializer<string>(), typeof(string));
                return true;
            case "Text":
                serializationInfo = new BsonSerializationInfo("text", BsonSerializer.LookupSerializer<string>(), typeof(string));
                return true;
            case "Completed":
                serializationInfo = new BsonSerializationInfo("completed", BsonSerializer.LookupSerializer<bool>(), typeof(bool));
                return true;
            case "Status":
                serializationInfo = new BsonSerializationInfo("status", BsonSerializer.LookupSerializer<string>(), typeof(string));
                return true;
            case "Deadline":
                serializationInfo = new BsonSerializationInfo("deadline", BsonSerializer.LookupSerializer<DateTime>(), typeof(DateTime));
                return true;
            default:
                serializationInfo = null;
                return false;
        }
    }
}