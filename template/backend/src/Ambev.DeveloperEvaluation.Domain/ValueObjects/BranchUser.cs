using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class BranchUser
{
    public Guid UserId { get; private set; }
    public BranchUserRole Role { get; private set; }

    protected BranchUser() {}

    public BranchUser(Guid userId, BranchUserRole role)
    {
        UserId = userId;
        Role = role;
    }
}