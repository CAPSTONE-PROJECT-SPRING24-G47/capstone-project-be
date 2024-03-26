using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationCommentPhotos;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.RestaurantCommentPhotos;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class GetRestaurantCommentHandler : IRequestHandler<GetRestaurantCommentRequest, BaseResponse<RestaurantCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetRestaurantCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<BaseResponse<RestaurantCommentDTO>> Handle(GetRestaurantCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantCommentId, out int restaurantCommentId))
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var restaurantComment = _mapper.Map<RestaurantCommentDTO>
                (await _unitOfWork.RestaurantCommentRepository.GetByID(restaurantCommentId));

            if (restaurantComment == null)
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy comment!"
                };
            }

            var restaurantCommentPhotoList = _mapper.Map<IEnumerable<RestaurantCommentPhotoDTO>>
                (await _unitOfWork.RestaurantCommentPhotoRepository.
                Find(rcp => rcp.RestaurantCommentId == restaurantCommentId));

            foreach (var item in restaurantCommentPhotoList)
            {
                item.SignedUrl = await _storageRepository.GetSignedUrlAsync(item.SavedFileName);
            }
            restaurantComment.RestaurantCommentPhotos = restaurantCommentPhotoList;

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Data = new List<RestaurantCommentDTO> { restaurantComment }
            };
        }
    }
}
