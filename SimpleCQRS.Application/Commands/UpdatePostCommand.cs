using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Commands
{
    public class UpdatePostCommand : IRequest<bool>
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
