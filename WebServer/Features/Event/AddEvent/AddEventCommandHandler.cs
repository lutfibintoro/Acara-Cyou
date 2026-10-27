using System.Text;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Event.AddEvent
{
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand, Result>
    {
        private readonly ILogger<AddEventCommandHandler> _logger;
        private readonly AcaraCyouDbContext _acaraCyouDbContext;

        public AddEventCommandHandler(ILogger<AddEventCommandHandler> logger, AcaraCyouDbContext acaraCyouDbContext)
        {
            _logger = logger;
            _acaraCyouDbContext = acaraCyouDbContext;
        }

        public async Task<Result> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            byte[] byteTeks = Convert.FromBase64String(request.Description);
            request.Description = Encoding.UTF8.GetString(byteTeks);

            bool userExists = await _acaraCyouDbContext.AcaraCyouUser
                .AnyAsync(user => user.Id == request.UserId, cancellationToken);

            if (!userExists)
            {
                NotFoundError<string> resultError = new("userId", $"{request.UserId}", null, FluentValidation.Severity.Error);
                return Result.Fail(resultError);
            }

            List<Infrastructures.Data.Entities.Poster> posters = request.Posters?.Select(poster => new Infrastructures.Data.Entities.Poster
            {
                FullPathUrl = poster.FullPathUrl,
                MimeType = poster.MimeType,
                ByteLength = poster.ByteLength
            }).ToList() ?? [];

            Infrastructures.Data.Entities.Event myEvent = new()
            {
                AcaraCyouUserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Posters = posters
            };

            _acaraCyouDbContext.Event.Add(myEvent);
            await _acaraCyouDbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
