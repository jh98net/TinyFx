namespace EasyNetQ.Hosepipe;

public interface IQueueInsertion
{
    void PublishMessagesToQueue(IEnumerable<HosepipeMessage> messages, QueueParameters parameters);
}
