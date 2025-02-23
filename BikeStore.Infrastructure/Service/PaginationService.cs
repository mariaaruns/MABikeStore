using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Service
{
    public class PaginationService<T,S> : IPaginationService<T, S> where T:class
    {
        public PaginationModel<T> Pagination(IQueryable<S> source, PaginationInput inputs)
        {
            var currentPage = inputs.PageNumber;

            var pagesize = inputs.PageSize;

            var totalNoOfRecords = source.Count();

            var totalPages = (int)Math.Ceiling(totalNoOfRecords / (double)pagesize);

            var result = source.Skip((inputs.PageNumber - 1) * (inputs.PageSize))
                            .Take(inputs.PageSize).ToList();
          
            var items = result.Adapt<List<T>>();

            PaginationModel<T> paginationVM = new PaginationModel<T>(currentPage, totalPages, pagesize, totalNoOfRecords,items);
            return paginationVM;
        }
    }
}
