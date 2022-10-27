using Application.Common.DTO.AdvertDTO;
using Application.Common.DTO.PhotoListDTO;
using Application.Common.DTO.SaveListDTO;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<Advert, AdvertResponse>();
            CreateMap<PaginatedList<Advert>, PaginatedList<AdvertResponse>>();
            CreateMap<PhotoList, PhotoResponse>();
            CreateMap<SaveList, SaveListResponse>();

        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {

            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
