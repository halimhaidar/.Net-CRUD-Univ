using API2.Base;
using API2.Models;
using API2.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Controllers
{
    public class UniversityController : BaseController<University, UniversityRepository, int>
    {
        public UniversityController(UniversityRepository universityRepository) : base(universityRepository)
        {

        }

    }
}
