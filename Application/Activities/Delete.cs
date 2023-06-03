using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        //IRequest, pass Result Handler
        public class Command : IRequest<Result<Unit>>
        {
            //Second, we define property access
            public Guid Id { get; set; }
        }

        //Third, we define our Handler class and implement IRequestHandler<Command>
        //Fourth, we implement IRequestHandler's interface
        //Fifth, we generate constructor for Handler
        //Sixth, we Bringin DataContext paramater and require namespace Persistence
        //Seventh, we initialize fields for context parameter, wrench icon
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
                //We retrieve activities
                var activity = await _context.Activities.FindAsync(request.Id);
                //We tried to throw an exception, so we removed conditional for null


                if (activity == null)
                    return null;
                //Remove the activity
                _context.Remove(activity);

                //Save from memory
                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Failed to delete the activity");

                //Default on Commands
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
