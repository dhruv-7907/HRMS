using Application.Features.Department.Commands;
using Application.Interfaces;
using MediatR;



namespace Application.Features.Department.Handler
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IDepartment _department;

        public CreateDepartmentCommandHandler(IDepartment department)
        {
            _department = department;
        }

        public async Task<int> Handle(CreateDepartmentCommand request,CancellationToken cancellationToken)
        {
          var result =  await _department.Create(request.Department);
            return result;
        }
    }
}
