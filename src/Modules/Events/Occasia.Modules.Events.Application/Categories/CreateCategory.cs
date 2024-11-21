using ErrorOr;
using FluentValidation;
using Occasia.Common.Application.Messaging;
using Occasia.Modules.Events.Application.Abstractions.Data;
using Occasia.Modules.Events.Domain.Categories;

namespace Occasia.Modules.Events.Application.Categories;

public static class CreateCategory
{
    public sealed record Command(string Name) : ICommand<ErrorOr<CategoryId>>;

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    internal sealed class Handler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<Command, ErrorOr<CategoryId>>
    {
        public async Task<ErrorOr<CategoryId>> Handle(Command request, CancellationToken cancellationToken)
        {
            var category = Category.Create(request.Name);

            categoryRepository.Insert(category);
            await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return category.Id;
        }
    }
}
