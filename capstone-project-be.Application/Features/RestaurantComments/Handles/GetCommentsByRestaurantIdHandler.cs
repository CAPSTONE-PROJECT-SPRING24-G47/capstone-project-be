using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantCommentPhotos;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class GetCommentsByRestaurantIdHandler : IRequestHandler<GetCommentsByRestaurantIdRequest, BaseResponse<RestaurantCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetCommentsByRestaurantIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<BaseResponse<RestaurantCommentDTO>> Handle(GetCommentsByRestaurantIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantId, out int restaurantId))
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var comments = await _unitOfWork.RestaurantCommentRepository.
                Find(rc => rc.RestaurantId == restaurantId);

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            comments = comments.OrderByDescending(c => c.CreatedAt).Skip(skip).Take(pageSize);
            var restaurantComments = _mapper.Map<IEnumerable<RestaurantCommentDTO>>(comments);

            foreach (var rc in restaurantComments)
            {
                var restaurantCommentPhotoList = _mapper.Map<IEnumerable<RestaurantCommentPhotoDTO>>
                (await _unitOfWork.RestaurantCommentPhotoRepository.
                Find(rcp => rcp.RestaurantCommentId == rc.RestaurantCommentId));

                foreach (var item in restaurantCommentPhotoList)
                {
                    item.SignedUrl = await _storageRepository.GetSignedUrlAsync(item.SavedFileName);
                }
                rc.RestaurantCommentPhotos = restaurantCommentPhotoList;
            }

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<RestaurantCommentDTO>>(restaurantComments)
            };
        }
    }
}
