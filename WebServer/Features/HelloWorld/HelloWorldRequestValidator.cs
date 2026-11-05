using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.HelloWorld
{
    public class HelloWorldRequestValidator : Validator<HelloWorldRequest>
    {
        public HelloWorldRequestValidator()
        {
            RuleFor(x => x.Body.OptionList)
                .IsInEnum();
        }
    }
}
