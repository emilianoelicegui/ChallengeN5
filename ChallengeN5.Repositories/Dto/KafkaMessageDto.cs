using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeN5.Repositories.Dto
{
    public class KafkaMessageDto
    {
        public Guid Id { get; set; }
        public string OperationName { get; set; } = null!;
    }
}
