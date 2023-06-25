using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class UpdateAttendance
    {
        //COMMAND this class represents a request to update attendance. This class implements
        //an interface, rightside of colon, which menas it will be processed by the handler
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        //HANDLER this class is responsible for processing the command.
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly DataContext _context;

            //CONSTRUCTOR needs access to DB and Interface which allows method for getting user name
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            //LOGIC needs to be converted to async
            public async Task<Result<Unit>> Handle(
                Command request,
                CancellationToken cancellationToken
            )
            {
                //GET ACTIVITY include attendees and host, check user by id? to differentiate between host and attendees
                var activity = await _context.Activities
                    .Include(a => a.Attendees)
                    .ThenInclude(u => u.AppUser)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (activity == null)
                    return null;

                //USER this accesses current user
                var user = await _context.Users.FirstOrDefaultAsync(
                    x => x.UserName == _userAccessor.GetUserName()
                );

                if (user == null)
                    return null;

                var hostUsername = activity.Attendees
                    .FirstOrDefault(x => x.IsHost)
                    ?.AppUser?.UserName;

                var attendance = activity.Attendees.FirstOrDefault(
                    x => x.AppUser.UserName == user.UserName
                );
                // HOST
                if (attendance != null && hostUsername == user.UserName)
                {
                    activity.IsCancelled = !activity.IsCancelled;
                }
                // Attendee
                if (attendance != null && hostUsername != user.UserName)
                {
                    activity.Attendees.Remove(attendance);
                }

                if (attendance == null)
                {
                    attendance = new ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost = false
                    };

                    activity.Attendees.Add(attendance);
                }
                var results = await _context.SaveChangesAsync() > 0;

                return results
                    ? Result<Unit>.Success(Unit.Value)
                    : Result<Unit>.Failure("Problem updating attendance");
            }
        }
    }
}
