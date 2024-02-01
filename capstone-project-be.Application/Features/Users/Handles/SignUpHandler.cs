using AutoMapper;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    //IRequestHandler<t1,t2(optional)>
    //t1 là kiểu request thằng handle này sẽ nhận để xử lý
    //t2 là kiểu dữ liệu trả về (trong trường hợp này k có tại không trả về cái gì)
    public class SignUpHandler : IRequestHandler<SignUpRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SignUpHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            //xử lý logic signup tronng này

            //xử lý xong gọi repository thông qua unitOfWork để add
            _unitOfWork.UserRepository.Add(_mapper.Map<User>(request.UserSignUpData));

            throw new NotImplementedException();
        }
    }
}
