using System.ComponentModel.DataAnnotations;

namespace dotnet_anime_list.API.Models.Abstracts
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; private init; }
        public DateTime Created { get; private init; }
        public DateTime Updated { get; private set; }

        public BaseEntity()
        {
            Guid id = Guid.NewGuid();
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
        public void UpdateEnity()
        {
            this.Updated = DateTime.Now;
        }
    }
}
