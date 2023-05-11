using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Comment;

namespace ShopManagement.Domain.CommentAgg;
public interface ICommentRepository : IRepository<long, Comment>
{
    List<CommentViewModel> Search(CommentSearchModel searchModel);
}