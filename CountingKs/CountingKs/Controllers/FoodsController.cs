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

namespace CountingKs.Controllers
{
    public class FoodsController : BaseApiController
    {

        public FoodsController(ICountingKsRepository repo)
            : base(repo)
        { 
        }

        public IEnumerable<FoodModel> Get(bool includesMeasures = true)
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

            var result = query.OrderBy(f => f.Description)
                              .Take(25)
                              .ToList()
                              .Select(f => TheModelFactory.Create(f));

            return result;
        }

        public FoodModel Get (int foodid)
        {
            return TheModelFactory.Create(TheRepository.GetFood(foodid));
        }
    }
}
