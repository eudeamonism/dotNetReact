using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    //Name of Class
    public class Edit
    {
        //Command type, no data sent out; IRequest
        public class Command : IRequest<Result<Unit>>
        {
            //To access Activity class
            public Activity Activity { get; set; }
        }

        public class CommandValidator : FluentValidation.AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            //initialize field from mapper
            private readonly IMapper _mapper;

            //We create Handler constructor.
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            //Task types are for asynchronous operations.
            public async Task<Result<Unit>> Handle(
                Command request,
                CancellationToken cancellationToken
            )
            {
                //Awaiting Id
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                if (activity == null)
                    return null;
                //AutoMapper will map from Body to memory.
                _mapper.Map(request.Activity, activity);
                //Save changes from memory to database.
                var result = await _context.SaveChangesAsync() > 0;

                //Return Failure or return success
                if (!result)
                    return Result<Unit>.Failure("Failed to update activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
