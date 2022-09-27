# JOS.SystemTextJsonDictionaryStringObjectJsonConverter
https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/

## Information
System.Text.Json custom JsonConverter that allows you to deserialize to a `Dictionary<string, object>`.

**Note**
It supports serialization as well, but I recommend you to only use this custom JsonConverter for **Deserialization**.
If you use it for serialization as well, it will be a bit slower than the default behaviour of System.Text.Json because of how custom JsonConverters work.
Basically, to avoid an infinite loop I need to cast the value to `IDictionary<string, object?>` before serializing. If not, System.Text.Json will call my JsonConverter again, 
and again, and again...until it crashes with a stack overflow, [more info here.](https://github.com/dotnet/docs/issues/19268)

It's really easy to avoid though. When serializing, don't pass in any options that contains this JsonConverter in it's converter list. If you do, it will still work, just a bit 
slower because of the cast.

## Install

Just the JsonConverter
```dotnet add package JOS.SystemTextJson.DictionaryStringObject.JsonConverter```

JsonConverter + Model binder for ASP.Net Core
```dotnet add package JOS.SystemTextJson.DictionaryStringObject.JsonConverter.AspNetCore```
## Tests
This runs all tests except the tests in `Deserialization_DefaultTests`. The tests in that class are supposed to fail. They test if the default behaviour of System.Text.Json 
correctly handles deserialization to `Dictionary<string, object>`. When they don't fail anymore, this package is not needed... :)

`dotnet test --filter 'FullyQualifiedName!~Deserialization_DefaultTests'`
