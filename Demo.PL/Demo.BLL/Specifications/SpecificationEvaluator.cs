using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Critria != null)
                query = query.Where(spec.Critria);

            query = spec.Includes.Aggregate(query, (currentquery, includeExpression) => currentquery.Include(includeExpression));

            return query;
        }
    }
}
