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
    public class CreateSaveListCommandHandler : IRequestHandler<CreateSaveListCommand, string>
    {
        private IMongoCollection<SaveList> _saveList { get; }

        public CreateSaveListCommandHandler(IAgencyDbConnection connection)
        {
            _saveList = connection.ConnectToMongo<SaveList>("SaveList");
        }

        public async Task<string> Handle(CreateSaveListCommand request, CancellationToken cancellationToken)
        {
            var entity = new SaveList
            {
                ListID = request.ListID,
                AdvertID = request.AdvertID,
                UserID = request.UserID
            };

            await _saveList.InsertOneAsync(entity);


            return entity.ListID;
        }
    }

    public record CreateSaveListCommand : IRequest<string>
    {
        public string ListID { get; set; }

        public string AdvertID { get; set; }

        public string UserID { get; set; }
    }
}
