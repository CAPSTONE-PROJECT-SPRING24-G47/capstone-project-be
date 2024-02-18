using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class GetNewUsersWeekHandler : IRequestHandler<GetNewUsersWeekRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNewUsersWeekHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetNewUsersWeekRequest request, CancellationToken cancellationToken)
        {
            DateTime currentDate = DateTime.UtcNow;
            var startOfCurrentWeek = GetStartOfWeek(currentDate);
            var startOfPreviousWeek = startOfCurrentWeek.AddDays(-7);

            var newUsersCurrentWeek = await GetNewUsers(startOfCurrentWeek, currentDate);
            var newUsersPreviousWeek = await GetNewUsers(startOfPreviousWeek, startOfCurrentWeek);

            double percentageChange = 0.0;

            if (newUsersPreviousWeek.Any())
            {
                percentageChange = ((double)(newUsersCurrentWeek.Count() - newUsersPreviousWeek.Count()) / Math.Abs(newUsersPreviousWeek.Count())) * 100;
            }

            var usersMapped = _mapper.Map<IEnumerable<UserDTO>>(newUsersCurrentWeek);

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

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }
    }
}
