using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SpecificationEvaluator<T> where T : ClaseBase
    {
        public static IQueryable<T> GetQuery (IQueryable<T> inpuQuery, ISpecification<T> spec)
        {
            if (spec.Criteria != null)
            {
                inpuQuery = inpuQuery.Where(spec.Criteria);
            }

            if(spec.OrderBy != null)
            {
                inpuQuery = inpuQuery.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescending != null)
            {
                inpuQuery = inpuQuery.OrderByDescending(spec.OrderByDescending);
            }

            inpuQuery = spec.Includes.Aggregate(inpuQuery, (current, include) => current.Include(include));

            return inpuQuery;
        }
    }
}
