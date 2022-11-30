using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SaveLists.Commands
{
    public class DeleteSaveListCommandHandler : IRequestHandler<DeleteSaveListCommand, string>
    {
        private IMongoCollection<SaveList> _saveList { get; }

        public DeleteSaveListCommandHandler(IAgencyDbConnection connection)
        {
            _saveList = connection.ConnectToMongo<SaveList>("SaveList");
        }

        public async Task<string> Handle(DeleteSaveListCommand request, CancellationToken cancellationToken)
        {
            var saveList = await _saveList.Find(x => x.ListID == request.Id).ToListAsync();

            if (saveList.Count == 0) throw new NotFoundException("SaveList", request.Id);

            await _saveList.DeleteOneAsync(x => x.ListID == request.Id);

            return request.Id;
        }
    }

    public record DeleteSaveListCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}
