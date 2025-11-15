using Application.Features.Department.Commands;
using Application.Interfaces;
using MediatR;
namespace Application.Features.Department.Handler
{
    public class UpdateDepartmentCommandHandler:IRequestHandler<UpdateDepartmentCommand,int>
    {
        private readonly IDepartment _department;

        public UpdateDepartmentCommandHandler(IDepartment department)
        {
            _department = department;
        }

        public async Task<int> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _department.Update(request.DepartmentDto);
            return result;
        }
    }

}
