namespace ChipsFlicks.Hub.Api;

public class MovieType : ObjectType<Movie>
{
    protected override void Configure(IObjectTypeDescriptor<Movie> descriptor)
    {
        descriptor
            .Field(p => p.SnackRecommendation)
            .Resolve(async context =>
            {
                var movie = context.Parent<Movie>();
                
                // Not using a data loader
                return await context.Service<ISnacksApi>().Recommendation(movie.Type, movie.Genre);
                
                // Using a data loader
                // var snackRecommendation = await context.Service<SnackDataLoader>().LoadAsync((movie.Type, movie.Genre));
                // return snackRecommendation;
            });
    }
}