using Application.Activities;
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application
{
    public class Create
    {
        //Since this is a command, it doesn't return anything hence it's brevity.
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }

        //For our Handler, we extend IRequest and pass in our Command classs from above. [Command Type]
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(
                Command request,
                CancellationToken cancellationToken
            )
            {
                //This only adds data to memory, so we are not persisting anything in the database.
                _context.Activities.Add(request.Activity);
                //SaveChangeAsync() shows how many entries have changed
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                    return Result<Unit>.Failure("Failed to create activity");

                //This is equivalent to nothing and is done to let the API know that this process has completed.
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
