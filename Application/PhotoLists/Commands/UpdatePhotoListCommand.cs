using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PhotoLists.Commands
{
    public class UpdatePhotoListCommandHandler : IRequestHandler<UpdatePhotoListCommand, string>
    {
        private IMongoCollection<PhotoList> _photoList { get; }

        public UpdatePhotoListCommandHandler(IAgencyDbConnection connection)
        {
            _photoList = connection.ConnectToMongo<PhotoList>("PhotoList");
        }

        public async Task<string> Handle(UpdatePhotoListCommand request, CancellationToken cancellationToken)
        {
            var newList = new PhotoList
            {
                PhotoID = request.PhotoID,
                AdvertID = request.AdvertID,
                PhotoLink = request.PhotoLink
            };

            await _photoList.ReplaceOneAsync(Builders<PhotoList>.Filter.Eq("_id", request.PhotoID), newList);

            return newList.PhotoID;
        }
    }

    public record UpdatePhotoListCommand : IRequest<string>
    {
        public string PhotoID { get; set; }

        public string AdvertID { get; set; }

        public string PhotoLink { get; set; }
    }
}
