using MediatR;
using SimpleCQRS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Queries
{
    public class GetPostsQuerie : IRequest<IEnumerable<GetPostDto>>
    { 
        //get all idont need any parametre
    }
}
