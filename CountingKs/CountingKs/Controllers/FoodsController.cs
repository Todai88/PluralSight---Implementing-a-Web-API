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
    public class FoodsController : ApiController
    {
        private ICountingKsRepository _repo;
        private ModelFactory _modelFactory;

        public FoodsController(ICountingKsRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        public IEnumerable<FoodModel> Get(bool includesMeasures = true)
        {
            IQueryable<Food> query;

            if (includesMeasures)
            {
                query = _repo.GetAllFoodsWithMeasures();
            }
            else
            {
                query = _repo.GetAllFoods();
            }

            var result = query.OrderBy(f => f.Description)
                              .Take(25)
                              .ToList()
                              .Select(f => _modelFactory.Create(f));

            return result;
        }

        public FoodModel Get (int foodid)
        {
            return _modelFactory.Create(_repo.GetFood(foodid));
        }
    }
}
