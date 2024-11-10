using Grpc.Core;
using UserDataService.Grpc;

namespace testbot.worker
{
    public class MessageService
    {
        private readonly Greeter.GreeterClient _grpcClient;

        public MessageService(Greeter.GreeterClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public async Task<string> SayHelloAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _grpcClient.SayHelloAsync(new HelloRequest { Name = name }, cancellationToken: cancellationToken);

                return result.Message;
            }
            catch (RpcException ex)
            {
                throw;
            }

        }
    }
}
