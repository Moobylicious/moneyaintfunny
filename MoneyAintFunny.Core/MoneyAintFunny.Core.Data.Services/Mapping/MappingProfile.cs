using AutoMapper;
using MoneyAintFunny.Core.Dto.Models;
using MoneyAintFunny.Data.Models;
using System;

namespace MoneyAintFunny.Core.Data.Services.Mapping
{   
    public static class MappingExtensions
    {
        public static DateTime GetDateTimeViaIntMaths(this int DateId)
        {
            var yr = DateId / 10000;
            var mth = (DateId - (yr * 10000)) / 100;
            var day = (DateId - (yr * 10000) - (mth * 100));
            return new DateTime(yr, mth, day);
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionRecord, TransactionRecordDto>()
                .ForMember(dest => dest.Date,
                            opts => opts.MapFrom(src => src.DateId.GetDateTimeViaIntMaths()))
                .ForMember(dest=>dest.Details,
                opts=>opts.MapFrom(src=>src.TransactionDetail))
                ;
        }
    }
}
