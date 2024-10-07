using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Infrastructure.Data
{
    class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            if (spec.Criteria != null) 
            { 
             query = query.Where(spec.Criteria);// x => x.Brand == Brand
            
            }

            return query;
        }
    }
}
