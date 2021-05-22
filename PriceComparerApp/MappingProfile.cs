using AutoMapper;
using PriceComparerApp.Models;
using PriceComparerApp.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriceComparerApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Catalog, CatalogDto>();
        }
    }
}
