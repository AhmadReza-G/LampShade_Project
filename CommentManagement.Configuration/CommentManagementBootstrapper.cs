using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastracture.EFCore;
using CommentManagement.Infrastracture.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Configuration;
public class CommentManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<Application.Contracts.Comment.ICommentApplication, CommentApplication>();

        services.AddDbContext<Infrastracture.EFCore.CommentContext>(x=> x.UseSqlServer(connectionString));
    }
}
