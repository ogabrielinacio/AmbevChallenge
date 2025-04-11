using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; }
    
    public DateTime Created { get; private set; }
    public DateTime? Updated { get; private set; }

    protected BaseEntity()
    {
        Created = DateTime.UtcNow;
        Updated = DateTime.UtcNow;
    }

    public void Modify() => Updated = DateTime.UtcNow;

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }
}
