using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.Event.GetEventByLabelForm
{
    public class GetEventByLabelFormRequest
    {
        [FromQuery, Required]
        public Query Query { get; set; } = null!;
    }

    public class Query
    {
        [Required]
        public string CustomUrlName { get; set; } = string.Empty;
    }
}
