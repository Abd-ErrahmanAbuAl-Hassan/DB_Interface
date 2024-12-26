using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;

namespace University.Entities.Repositories
{
    public interface IEnrollment:IGeneric<Enrollment>
    {
        void Update(Enrollment course);
    }
}
