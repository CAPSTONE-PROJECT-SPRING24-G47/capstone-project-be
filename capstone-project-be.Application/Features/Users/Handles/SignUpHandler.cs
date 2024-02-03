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

        public async Task Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            //xử lý logic signup tronng này
            var data = request.UserSignUpData;

            //check email có thật hay không (chưa làm)

            //check email có trong database hay không
            var user = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);
            //nếu đã có thì sẽ return message (ở đây t chưa thêm nên return không)
            if (user != null) return;

            //mã hóa password trước khi lưu (chưa làm)

            //xử lý xong gọi repository thông qua unitOfWork để add
            await _unitOfWork.UserRepository.Add(_mapper.Map<User>(data));
            //sau khi thưc hiện viết vào db (update, add, delete) đều phải goi hàm save
            await _unitOfWork.Save();

        }
    }
}
