using FastEndpoints;

namespace WebServer.Features.Poster.DeletePosterBatch
{
    public record DeletePosterBatchRequest
    (
        [property: FromQuery]
        Query? Query
    );

    public record Query
    (
        List<Guid> PosterIds
    );
}
