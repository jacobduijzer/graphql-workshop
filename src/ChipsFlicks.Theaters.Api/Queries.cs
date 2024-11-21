namespace ChipsFlicks.Theaters.Api;

public class Queries
{
    public IEnumerable<Theater> Theaters([Service] TheatersRepository theaters) => theaters.All(); 
}