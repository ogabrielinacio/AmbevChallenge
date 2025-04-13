using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class BranchUserRole
{
    public UserRole Value { get; }

    private BranchUserRole(UserRole value)
    {
        if (value != UserRole.Admin && value != UserRole.Manager)
            throw new InvalidOperationException("Only Admin or Manager allowed for this Operation.");

        Value = value;
    }

    public static BranchUserRole Create(UserRole role) => new(role);

    public static implicit operator UserRole(BranchUserRole role) => role.Value; 
}