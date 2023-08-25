using ChallengeN5.Repositories.Dto;

namespace ChallengeN5.Services.Services
{
    public interface IKafkaService
    {
        void ProduceOperationMessage(KafkaMessageDto messageDto);
    }

}
