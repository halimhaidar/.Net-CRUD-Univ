using API2.Base;
using API2.Models;
using API2.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Controllers
{
    public class EducationController : BaseController<Education, EducationRepository, int>
    {
        public EducationController(EducationRepository educationRepository) : base(educationRepository)
        {

        }

    }
}
