namespace Producer.Repositories;

public interface IProducerSvc
{
    public Task ProduceAsync(College collegeModel);
}