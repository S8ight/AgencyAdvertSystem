using Application.Common.DTO.PhotoListDTO;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PhotoLists.Queries
{
    public record GetAdvertPhotoList : IRequest<List<PhotoResponse>>
    {
        public string Id { get; set; }
    }

    public class GetAdvertPhotoListHandler : IRequestHandler<GetAdvertPhotoList, List<PhotoResponse>>
    {
        private IMongoCollection<PhotoList> _photoList { get; }

        private IMapper _mapper { get; }

        public GetAdvertPhotoListHandler(IAgencyDbConnection context, IMapper mapper)
        {
            _photoList = context.ConnectToMongo<PhotoList>("PhotoList");
            _mapper = mapper;
        }

        public async Task<List<PhotoResponse>> Handle(GetAdvertPhotoList query, CancellationToken cancellationToken)
        {
            var result = await _photoList.Find(x => x.AdvertID == query.Id).ToListAsync();

            if (result.Count == 0) throw new NotFoundException("PhotoList", query.Id);

            return _mapper.Map<List<PhotoList>, List<PhotoResponse>>(result); 
        }
    }
}
