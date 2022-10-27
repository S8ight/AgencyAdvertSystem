using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.PhotoLists.Commands
{
    public class CreatePhotoListCommandHandler : IRequestHandler<CreatePhotoListCommand, string>
    {
        private IMongoCollection<PhotoList> _photoList { get; }
        
        public CreatePhotoListCommandHandler(IAgencyDbConnection connection)
        {
            _photoList = connection.ConnectToMongo<PhotoList>("PhotoList");
        }

        public async Task<string> Handle(CreatePhotoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new PhotoList
            {
                PhotoID = request.PhotoID,
                AdvertID = request.AdvertID,
                PhotoLink = request.PhotoLink
            };

            await _photoList.InsertOneAsync(entity);


            return entity.PhotoID;
        }
    }

    public record CreatePhotoListCommand : IRequest<string>
    {
        public string PhotoID { get; set; }

        public string AdvertID { get; set; }

        public string PhotoLink { get; set; }
    }
}
