using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FastEndpoints;

namespace WebServer.Features.HelloWorld
{
    public class HelloWorldRequest
    {
        [Required, FromBody]
        public Body Body { get; set; } = null!;
    }

    public class Body
    {
        [Required]
        public OptionList OptionList { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OptionList
    {
        OptionOne,
        OptionTwo,
        OptionThree,
        OptionFour
    }
}
