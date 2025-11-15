using Application.Features.Department.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Department.Handler
{
    public class DeleteDepartmentCommandHandler:IRequestHandler<DeleteDepartmentCommand,int>
    {
        private readonly IDepartment _department;

        public DeleteDepartmentCommandHandler(IDepartment department)
        {
            _department = department;
        }

        public async Task<int> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _department.Delete(request.Id);
            return result;
        }
    }
}
