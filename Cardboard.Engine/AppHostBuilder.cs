using Microsoft.Extensions.DependencyInjection;

namespace Cardboard.Engine
{
    public class AppHostBuilder
    {
        private readonly IServiceCollection _services = new ServiceCollection();

        public AppHostBuilder ConfigureServices(Action<IServiceCollection> configure)
        {
            configure(_services);
            return this;
        }

        public AppHost Build()
        {
            var provider = _services.BuildServiceProvider();
            return new AppHost(provider);
        }
    }
}