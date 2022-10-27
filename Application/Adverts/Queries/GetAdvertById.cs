using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.Common.DTO.AdvertDTO;

namespace Application.Adverts.Queries
{
    public record GetAdvertsById : IRequest<AdvertResponse>
    {
        public string Id { get; set; }
    }

    public class GetAdvertsByIdHandler : IRequestHandler<GetAdvertsById, AdvertResponse>
    {
        private IMongoCollection<Advert> _advert { get; }

        private IMapper _mapper { get; }

        public GetAdvertsByIdHandler(IAgencyDbConnection context, IMapper mapper)
        {
            _advert = context.ConnectToMongo<Advert>("Advert");
            _mapper = mapper;
        }

        public async Task<AdvertResponse> Handle(GetAdvertsById query, CancellationToken cancellationToken)   
        {
            var result = await _advert.Find(x => x.AdvertID == query.Id).ToListAsync();

            if(result.Count == 0) throw new NotFoundException("Advert", query.Id);

            return _mapper.Map<Advert,AdvertResponse>(result.FirstOrDefault());

        }
    }
}
