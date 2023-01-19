using API2.Context;
using API2.Models;
using API2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        public UniversityRepository (MyContext myContext) : base(myContext)
        {

        }
    }
}
