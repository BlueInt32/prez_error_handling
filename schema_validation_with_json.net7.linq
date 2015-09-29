<Query Kind="Program">
  <Reference Relative="Json70r1\Bin\Net45\Newtonsoft.Json.dll">C:\Users\Simon.budin\Simon\Git\prez_error_handling\Json70r1\Bin\Net45\Newtonsoft.Json.dll</Reference>
  <Reference Relative="JsonSchema10r11\Bin\Net45\Newtonsoft.Json.Schema.dll">C:\Users\Simon.budin\Simon\Git\prez_error_handling\JsonSchema10r11\Bin\Net45\Newtonsoft.Json.Schema.dll</Reference>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
string schemaJson = @"{
  'description': 'A person',
  'type': 'object',
  'properties': {
    'name': {'type':'string'},
    'hobbies': {
      'type': 'array',
      'items': {'type':'string'}
    }
  }
}";

JSchema schema = JSchema.Parse(schemaJson);

JObject person = JObject.Parse(@"{
  'name': null,
  'hobbies': ['valid', 0.123456789]
}");

IList<string> messages = new List<string>();
SchemaValidationEventHandler validationEventHandler = (sender, args) => args.ValidationError.SchemaId.OriginalString.Dump();

person.Validate(schema, validationEventHandler);

foreach (string message in messages)
{
    Console.WriteLine(message);
}
// Invalid type. Expected String but got Null. Line 2, position 21.
// Invalid type. Expected String but got Number. Line 3, position 51.
}

// Define other methods and classes here
