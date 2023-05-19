using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application
{
    public class Create
    {
        //Since this is a command, it doesn't return anything hence it's brevity. 
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }
        //For our Handler, we extend IRequest and pass in our Command classs from above. [Command Type]
        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext _context;
            public Handler(DataContext context)
            {
            _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //This only adds data to memory, so we are not persisting anything in the database.
               _context.Activities.Add(request.Activity);
                //This must access database
               await _context.SaveChangesAsync();
                //This is equivalent to nothing and is done to let the API know that this process has completed.
               return Unit.Value;
            }
        }
    }
}