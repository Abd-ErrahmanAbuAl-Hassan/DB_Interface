using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;
namespace University.Entities.Repositories
{
    public interface IStudent:IGeneric<Student>
    {
        void Update(Student student);
    }
}
