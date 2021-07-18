namespace TestTaskApi.Data.Entities
{
    public class MessageEntity
    {
        public string MessageId { get; set; }
        public string Message { get; set; }

        public string AccountId { get; set; }
        public AccountEntity Account { get; set; }
    }
}
