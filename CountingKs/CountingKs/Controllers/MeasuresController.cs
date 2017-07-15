using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class MeasuresController : BaseApiController
    {

        public MeasuresController(ICountingKsRepository repo) 
            : base (repo)
        { 
        }

        public IEnumerable<MeasureModel> Get(int foodid)
        {
            var results = TheRepository.GetMeasuresForFood(foodid)
                               .ToList()
                               .Select(m => TheModelFactory.Create(m));
            return results;
        }

        public MeasureModel Get(int foodid, int measuresid)
        {
            var result = TheRepository.GetMeasure(measuresid);

            if (result.Food.Id == foodid)
            {
                return TheModelFactory.Create(result);
            }
            return null;
        }
    }
}
