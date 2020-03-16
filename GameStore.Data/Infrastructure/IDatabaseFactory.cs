

namespace GameStore.Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        ApplicationContext.ApplicationContext Get();
    }
}
