using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationCommentPhotos;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class GetCommentsByAccommodationIdHandler : IRequestHandler<GetCommentsByAccommodationIdRequest, BaseResponse<AccommodationCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetCommentsByAccommodationIdHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AccommodationCommentDTO>> Handle(GetCommentsByAccommodationIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationId, out int accommodationId))
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var comments = await _unitOfWork.AccommodationCommentRepository.
                Find(ac => ac.AccommodationId == accommodationId);

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            comments = comments.OrderByDescending(c => c.CreatedAt).Skip(skip).Take(pageSize);
            var accommodationComments = _mapper.Map<IEnumerable<AccommodationCommentDTO>>(comments);

            foreach (var ac in accommodationComments)
            {
                var accommodationCommentPhotoList = _mapper.Map<IEnumerable<AccommodationCommentPhotoDTO>>
                (await _unitOfWork.AccommodationCommentPhotoRepository.
                Find(acp => acp.AccommodationCommentId == ac.AccommodationCommentId));

                foreach (var item in accommodationCommentPhotoList)
                {
                    item.SignedUrl = await _storageRepository.GetSignedUrlAsync(item.SavedFileName);
                }
                ac.AccommodationCommentPhotos = accommodationCommentPhotoList;
            }

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<AccommodationCommentDTO>>(accommodationComments)
            };
        }
    }
}
