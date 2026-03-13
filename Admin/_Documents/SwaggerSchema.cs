namespace Admin.App.Models
{
public class SwaggerDocument
    {
        public OpenApiInfo Info { get; set; }
        public string Openapi { get; set; } // e.g., "3.0.1"
        public List<OpenApiServer> Servers { get; set; }
        public Dictionary<string, OpenApiPathItem> Paths { get; set; }
        public OpenApiComponents Components { get; set; }
        public List<OpenApiTag> Tags { get; set; }
        public List<Dictionary<string, List<string>>> Security { get; set; }
    }

    // Info section
    public class OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public OpenApiContact Contact { get; set; }
        public OpenApiLicense License { get; set; }
    }

    public class OpenApiContact
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
    }

    public class OpenApiLicense
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    // Server section
    public class OpenApiServer
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }

    // Paths and operations
    public class OpenApiPathItem
    {
        public OpenApiOperation Get { get; set; }
        public OpenApiOperation Post { get; set; }
        public OpenApiOperation Put { get; set; }
        public OpenApiOperation Delete { get; set; }
    }

    public class OpenApiOperation
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, OpenApiResponse> Responses { get; set; }
        public OpenApiRequestBody RequestBody { get; set; }
    }

    // Request and response
    public class OpenApiRequestBody
    {
        public Dictionary<string, OpenApiMediaType> Content { get; set; }
    }

    public class OpenApiResponse
    {
        public string Description { get; set; }
        public Dictionary<string, OpenApiMediaType> Content { get; set; }
    }

    public class OpenApiMediaType
    {
        public OpenApiSchema Schema { get; set; }
        public object Example { get; set; }
    }

    // Components section
    public class OpenApiComponents
    {
        public Dictionary<string, OpenApiSchema> Schemas { get; set; }
        public Dictionary<string, object> SecuritySchemes { get; set; }
    }

    // Schema definition
    public class OpenApiSchema
    {
        public string Type { get; set; }
        public Dictionary<string, OpenApiSchema> Properties { get; set; }
        public List<string> Required { get; set; }
    }

    // Tags
    public class OpenApiTag
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
