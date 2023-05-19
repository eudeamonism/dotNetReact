using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        //First we implement IRequest
        public class Command : IRequest
        {
            //Second, we define property access
            public Guid Id { get; set; }
        }

        //Third, we define our Handler class and implement IRequestHandler<Command>
        //Fourth, we implement IRequestHandler's interface
        //Fifth, we generate constructor for Handler
        //Sixth, we Bringin DataContext paramater and require namespace Persistence
        //Seventh, we initialize fields for context parameter, wrench icon
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //We retrieve activities
                var activity = await _context.Activities.FindAsync(request.Id);
                //Remove the activity
                _context.Remove(activity);

                //Save from memory
                await _context.SaveChangesAsync();

                //Default on Commands
                return Unit.Value;
            }
        }
    }
}
