using CountingKs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using CountingKs.Data.Entities;

namespace CountingKs.Controllers
{
    public class FoodsController : ApiController
    {
        private ICountingKsRepository _repo;

        public FoodsController(ICountingKsRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<object> Get()
        {
            var repo = new CountingKsRepository(
                new CountingKsContext());

            var results = repo.GetAllFoodsWithMeassures()
                              .OrderBy(f => f.Description)
                              .Take(25)
                              .ToList()
                              .Select(f => new
                              {
                                  Description = f.Description,
                                  Measures = f.Measures
                                              .Select(m => new
                                              {
                                                  Description = m.Description,
                                                  Calories = m.Calories
                                              })
                              });

            return results;
        }
    }
}
