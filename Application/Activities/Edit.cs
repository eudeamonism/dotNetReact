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
        public class Command : IRequest
        {
            //To access Activity class
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
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
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //Awaiting Id
                var activity = await _context.Activities.FindAsync(request.Activity.Id);
                //AutoMapper will map from Body to memory.
                _mapper.Map(request.Activity, activity);
                //Save changes from memory to database.
                await _context.SaveChangesAsync();
                //Nothing returned, but needed
                return Unit.Value;
            }
        }
    }
}
