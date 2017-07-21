using CountingKs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using CountingKs.Data.Entities;
using CountingKs.Models;
using System.Web.Http.Routing;
using CountingKs.Filters;

namespace CountingKs.Controllers
{
    [CountingKsAuthorize(false)]
    public class FoodsController : BaseApiController
    {

        const int PAGE_SIZE = 50; 

        public FoodsController(ICountingKsRepository repo)
            : base(repo)
        { 
        }

        public object Get(bool includesMeasures = true, int page = 0)
        {
            IQueryable<Food> query;

            if (includesMeasures)
            {
                query = TheRepository.GetAllFoodsWithMeasures();
            }
            else
            {
                query = TheRepository.GetAllFoods();
            }

            var baseQuery = query.OrderBy(f => f.Description);

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double) totalCount / PAGE_SIZE);

            var helper = new UrlHelper(Request);
            var prevUrl = page > 0 ? helper.Link("Food", new { page = page - 1 }) : "";
            var nextUrl = page < totalPages - 1 ? helper.Link("Food", new { page = page + 1 }) : "";


            var result = baseQuery.Skip(page * PAGE_SIZE)
                                   .Take(PAGE_SIZE)
                                   .ToList()
                                   .Select(f => TheModelFactory.Create(f));
            return new
            {
                TotalPage = totalPages,
                TotalCount = totalCount,
                PrevPageUrl = prevUrl,
                NextPageUrl = nextUrl,
                Results = result
            };
        }

        public FoodModel Get (int foodid)
        {
            return TheModelFactory.Create(TheRepository.GetFood(foodid));
        }
    }
}
