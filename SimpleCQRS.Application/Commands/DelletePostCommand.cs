using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Commands
{
    public class DelletePostCommand : IRequest<bool>
    {
        public Guid postId { get; set; }
    }
}
