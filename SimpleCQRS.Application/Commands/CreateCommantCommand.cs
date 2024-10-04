using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Commands
{
    public class CreateCommantCommand : IRequest<Guid>
    {
        public string Text { get; set; }
        public Guid PostId { get; set; }
    }
}
