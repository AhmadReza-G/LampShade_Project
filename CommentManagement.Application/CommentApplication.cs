using _0_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application;
public class CommentApplication : ICommentApplication
{
    private readonly ICommentRepository _commentRepository;

    public CommentApplication(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public OperationResult Add(AddComment command)
    {
        var operation = new OperationResult();
        var comment = new Comment(command.Name, command.Email, command.Message
            , command.OwnerRecordId, command.Type, command.Website, command.ParentId);

        _commentRepository.Create(comment);
        _commentRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Cancel(long id)
    {
        var operation = new OperationResult();
        var comment = _commentRepository.GetBy(id);
        if (comment == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        comment.Cancel();
        _commentRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Confirm(long id)
    {
        var operation = new OperationResult();
        var comment = _commentRepository.GetBy(id);
        if (comment == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        comment.Confirm();
        _commentRepository.SaveChanges();
        return operation.Succeded();
    }

    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        return _commentRepository.Search(searchModel);
    }
}
