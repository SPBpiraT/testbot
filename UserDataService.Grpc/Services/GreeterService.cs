using Grpc.Core;
using UserDataService.Grpc.Data;
using UserDataService.Grpc.Models;

namespace UserDataService.Grpc.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        protected readonly UserDataContext _context;
        public GreeterService(ILogger<GreeterService> logger, UserDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _context.Set<Message>().Add(new Message { Text = $"{request.Name}", UserId = 1, CreationDate = DateTime.Now}); //
            _context.SaveChanges();//

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
