using Grpc.Core;

namespace gRPC.Server.Services
{
    public class FeedService : FeedMeMorService.FeedMeMorServiceBase
    {
        private readonly ILogger<FeedService> _logger;

        public FeedService(ILogger<FeedService> logger)
        {
            this._logger = logger;
        }


        public override Task<HerIsYourFood> AddFood(Food request, ServerCallContext context)
        {
            HerIsYourFood food = new HerIsYourFood()
            {
                IsServed = "Bon Appetit :) "
            };

            _logger.LogInformation("Food Served");

            return Task.FromResult(food);
        }
    }
}
