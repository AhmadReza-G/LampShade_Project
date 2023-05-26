using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg;
public class Comment : EntityBase
{
    private Comment() { }
    public Comment(string name, string email, string message, long ownerParentId, int type, string website, long parentId)
    {
        Name = name;
        Email = email;
        Message = message;
        OwnerRecordId = ownerParentId;
        Type = type;
        Website = website;
        ParentId = parentId;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string? Website { get; private set; }
    public string Message { get; private set; }
    public long OwnerRecordId { get; private set; }
    public long ParentId { get; private set; }
    public Comment Parent { get; private set; }
    public int Type { get; private set; }
    public bool IsConfirmed { get; private set; }
    public bool IsCanceled { get; private set; }

    public void Confirm() => IsConfirmed = true;

    public void Cancel() => IsCanceled = true;
}
