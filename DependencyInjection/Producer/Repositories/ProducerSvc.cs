using Confluent.Kafka;
using Newtonsoft.Json;

namespace Producer.Repositories;

public class ProducerSvc : IProducerSvc
{
    private readonly IProducer<Null, string> _producer;
    
    public ProducerSvc(IProducer<Null, string> producer)
    {
        _producer = producer;
    }

    public async Task ProduceAsync(College collegeModel) =>
        await _producer.ProduceAsync("college-topic", new Message<Null, string>
            {
                //Key = "",
                Value =  JsonConvert.SerializeObject(collegeModel)
            });
}

public record College(int studentId, string studentName);
