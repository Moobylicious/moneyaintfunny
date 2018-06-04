using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyAintFunny.Core.Dto.Models;
using MoneyAintFunny.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyAintFunny.Core.Data.Services.Services
{
    public class MoneyAintFunnyDataService : IMoneyAintFunnyDataService
    {
        private readonly IMoneyAintFunnyQueryService _queryService;
        private readonly IMapper _mapper;

        public MoneyAintFunnyDataService(IMoneyAintFunnyQueryService queryService, IMapper mapper)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
      
        public IEnumerable<TransactionRecordDto> GetTransactions()
        {
            //fetch records....
            var records = _queryService.GetQueryable<TransactionRecord>()
                .Include(x=>x.TransactionDetail)
                .ToList();
            //map to dto type & return.
            return records.Select(r => _mapper.Map<TransactionRecord, TransactionRecordDto>(r));
        }
    }
}
