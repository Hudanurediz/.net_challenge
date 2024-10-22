namespace Enoca_Challenge.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public BaseEntity(int id, DateTime createdDate, DateTime updatedDate, DateTime? deletedDate)
        {
            Id = id;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            DeletedDate = deletedDate;
        }
    }
}
