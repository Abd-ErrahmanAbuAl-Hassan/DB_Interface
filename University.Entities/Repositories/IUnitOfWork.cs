
namespace University.Entities.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IStudent student { get; }
        IStaff staff { get; }
        IDepartment department { get; }
        IFaculty faculty { get; }
        ICourse course { get; }
        IEmploying employing { get; }
        IEnrollment enrollment { get; }
        int Complete();
    }
}
