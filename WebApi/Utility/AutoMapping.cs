using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Utility
{
    /// <summary>
    /// Utilities that are using to customize mapping between models. 
    /// </summary>
    public class AutoMapping :Profile
    {
        public AutoMapping()
        {
            CreateMap<ItemPayload, Item>()
               .ForMember(dst => dst.InternalId, x => x.MapFrom(src => src.Id))
               .ForMember(dst => dst.Id, x => x.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<Item, ItemResult>()
               .ForMember(dst => dst.Id, x => x.MapFrom(src => src.InternalId));

        }
    }
}
