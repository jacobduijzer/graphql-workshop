namespace ChipsFlicks.Hub.Api;

public class MovieType : ObjectType<Movie>
{
    protected override void Configure(IObjectTypeDescriptor<Movie> descriptor)
    {
        descriptor
            .Field(p => p.SnackRecommendation)
            .Resolve(context =>
            {
                var movie = context.Parent<Movie>();
                return context.Service<ISnacksApi>().Recommendation(movie.Type, movie.Genre);
            });
    }
}