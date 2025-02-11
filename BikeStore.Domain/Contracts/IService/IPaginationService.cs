using BikeStore.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IService
{
    public interface IPaginationService<T, S> where T : class
    {
        PaginationModel<T> Pagination(IQueryable<S> source, PaginationInput inputs);
    }
}
