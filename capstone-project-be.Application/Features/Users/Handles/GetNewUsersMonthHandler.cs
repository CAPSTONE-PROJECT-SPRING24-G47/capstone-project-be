using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class GetNewUsersMonthHandler : IRequestHandler<GetNewUsersMonthRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNewUsersMonthHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetNewUsersMonthRequest request, CancellationToken cancellationToken)
        {
            DateTime currentDate = DateTime.UtcNow;
            DateTime startOfCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime startOfPreviousMonth = startOfCurrentMonth.AddMonths(-1);
            var newUsersCurrentMonth = await GetNewUsers(startOfCurrentMonth, currentDate);
            var newUsersPreviousMonth = await GetNewUsers(startOfPreviousMonth, startOfCurrentMonth);
            double percentageChange = 0.0;

            if (newUsersPreviousMonth.Count() != 0)
            {
                percentageChange = ((double)(newUsersCurrentMonth.Count() - newUsersPreviousMonth.Count()) / Math.Abs(newUsersPreviousMonth.Count())) * 100;
            }

            var usersMapped = _mapper.Map<IEnumerable<UserDTO>>(newUsersCurrentMonth);

            return new GetNewUsersResponse<UserDTO>()
            {
                ChangePercentage = percentageChange,
                Data = usersMapped,
                IsSuccess = true
            };
        }

        private async Task<IEnumerable<User>> GetNewUsers(DateTime startDate, DateTime endDate)
        {
            var users = await _unitOfWork.UserRepository.Find(user => user.CreatedAt >= startDate && user.CreatedAt < endDate);
            return users;
        }
    }
} 