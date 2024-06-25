namespace Honey.Common.Base
{
    public class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateUpdated { get; set; }
    }
}