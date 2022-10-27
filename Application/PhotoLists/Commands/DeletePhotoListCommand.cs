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

namespace Application.PhotoLists.Commands
{
    public class DeletePhotoListCommandHandler : IRequestHandler<DeletePhotoListCommand, string>
    {
        private IMongoCollection<PhotoList> _photoList { get; }

        public DeletePhotoListCommandHandler(IAgencyDbConnection connection)
        {
            _photoList = connection.ConnectToMongo<PhotoList>("PhotoList");
        }

        public async Task<string> Handle(DeletePhotoListCommand request, CancellationToken cancellationToken)
        {
            var advert = await _photoList.Find(x => x.PhotoID == request.Id).ToListAsync();

            if (advert == null) throw new NotFoundException("PhotoList", request.Id);

            await _photoList.DeleteOneAsync(x => x.PhotoID == request.Id);

            return request.Id;
        }
    }

    public record DeletePhotoListCommand : IRequest<string>
    {
        public string Id { get; set; }

    }
}
