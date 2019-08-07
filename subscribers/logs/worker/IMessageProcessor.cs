using Amazon.SQS.Model;

namespace Dta.Marketplace.Subscribers.Logger.Worker{
    public interface IMessageProcessor {
        void Process(Message message);
    }
}