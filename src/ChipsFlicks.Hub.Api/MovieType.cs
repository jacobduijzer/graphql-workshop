namespace ChipsFlicks.Hub.Api;

public class MovieType : ObjectType<Movie>
{
    protected override void Configure(IObjectTypeDescriptor<Movie> descriptor)
    {
        descriptor
            .Field(movie => movie.SnackRecommendation)
            .Resolve(async context =>
            {
                var movie = context.Parent<Movie>();
                var snackRecommendation = await context.Service<SnackDataLoader>().LoadAsync((movie.Type, movie.Genre));
                return snackRecommendation;
            });

        descriptor
            .Field(movie => movie.Reviews)
            .Resolve(async context =>
            {
                var reviews = await context.Service<ReviewsDataLoader>().LoadAsync(context.Parent<Movie>().Title);
                return reviews;
            });
    }
}