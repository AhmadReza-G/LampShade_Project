using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastracture.EFCore.Repository;
public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
{
    private readonly CommentContext _context;

    public CommentRepository(CommentContext context) : base(context)
    {
        _context = context;
    }
    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        var query = _context.Comments
            .Select(x => new CommentViewModel
            {
                Id = x.Id,
                Email = x.Email,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                Message = x.Message,
                Name = x.Name,
                Website = x.Website,
                Type = x.Type,
                OwnerRecordId = x.OwnerRecordId,
                CommentDate = x.CreationDate.ToFarsi()
            });

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Name.Contains(searchModel.Name));

        if (!string.IsNullOrWhiteSpace(searchModel.Email))
            query = query.Where(x => x.Email.Contains(searchModel.Email));

        return query.OrderByDescending(x => x.Id).ToList();
    }
}
