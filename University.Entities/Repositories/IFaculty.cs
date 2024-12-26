using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;

namespace University.Entities.Repositories
{
    public interface IFaculty:IGeneric<Faculty>
    {
        void Update(Faculty faculty);
    }
}
