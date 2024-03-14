using AutoMapper;
using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Handles
{
    public class UpdateTripHandler : IRequestHandler<UpdateTripRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTripHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateTripRequest request, CancellationToken cancellationToken)
        {
            //Call out tripId
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<TripDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            //Call out Trip Data
            var tripData = request.TripData;
            var trip = _mapper.Map<Trip>(tripData);
            trip.CreatedAt = DateTime.Now;
            trip.TripId = tripId;


            //Update trip 
            await _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.Save();

            //Call out recently added trip
            var tripList = await _unitOfWork.TripRepository.
                Find(t => t.UserId == trip.UserId && t.CreatedAt >= DateTime.Now.AddMinutes(-1));

            //Find new list City Suitable
            var trip_Locations = _mapper.Map<IEnumerable<Trip_Location>>(tripData.Trip_Locations);
            var cityIds = new List<int>();
            foreach (var location in trip_Locations)
            {
                //Add Trip Id for location in the trip
                location.TripId = tripId;

                //Call out location data
                var regionId = location.RegionId;
                var prefectureId = location.PrefectureId;
                var cityId = location.CityId;
                var regionData = await _unitOfWork.RegionRepository.Find(r => r.RegionId == regionId);
                var region = _mapper.Map<IEnumerable<RegionDTO>>(regionData).First();

                //Find list CityId suitable

                if (prefectureId == null)
                {
                    var prefectures = _mapper.Map<IEnumerable<PrefectureDTO>>(region.Prefectures);
                    foreach (var prefecture in prefectures)
                    {
                        var citys = prefecture.Cities;
                        foreach (var city in citys)
                        {
                            cityIds.Add(city.CityId);
                        }
                    }
                }
                else
                {
                    if (cityId != null)
                    {
                        cityIds.Add(cityId.Value);
                    }
                    else
                    {
                        var prefectures = await _unitOfWork.PrefectureRepository.
                            Find(p => p.PrefectureId == prefectureId);
                        var prefectureList = _mapper.Map<IEnumerable<PrefectureDTO>>(prefectures);
                        foreach (var prefecture in prefectureList)
                        {
                            var citys = prefecture.Cities;
                            foreach (var city in citys)
                            {
                                cityIds.Add(city.CityId);
                            }
                        }
                    }
                }
            }

            //Delete old suggested accommodations list
            var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
                Find(ta => ta.TripId == tripId);
            await _unitOfWork.Trip_AccommodationRepository.DeleteRange(trip_AccommodationList);

            //Find new Accommodations list which have categories match with the categories the user enter
            var accommodationCategoyIds = tripData.AccommodationCategories;
            var acc_accCategories = await _unitOfWork.Acc_AccCategoryRepository.
                Find(acc => accommodationCategoyIds.Contains(acc.AccommodationCategoryId));
            var accommodationIds = new List<int>();
            foreach (var acc_accCat in acc_accCategories)
            {
                accommodationIds.Add(acc_accCat.AccommodationId);
            }
            //Find new Suggest Accommodation List
            var suggestAccommodations =
                (await _unitOfWork.AccommodationRepository.Find(acc =>
                cityIds.Contains(acc.CityId)
                && accommodationIds.Contains(acc.AccommodationId)
                && acc.PriceLevel.Trim() == tripData.AccommodationPriceLevel.Trim()))
                .Take(trip.Duration);
            //Add new Trip_Accommodation List
            var trip_Accommodations = new List<CRUDTrip_AccommodationDTO>();
            foreach (var acc in suggestAccommodations)
            {
                trip_Accommodations.Add(new CRUDTrip_AccommodationDTO
                {
                    AccommodationId = acc.AccommodationId
                });
            }
            var suggestTrip_Accommodations = _mapper.Map<IEnumerable<Trip_Accommodation>>(trip_Accommodations);
            if (!suggestTrip_Accommodations.Any())
            {
                await _unitOfWork.TripRepository.Delete(trip);
                await _unitOfWork.Save();
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm được nơi ở phù hợp!"
                };
            }
            foreach (var item in suggestTrip_Accommodations)
            {
                item.TripId = tripId;
            }
            await _unitOfWork.Trip_AccommodationRepository.AddRange(suggestTrip_Accommodations);

            //Delete old suggested restaurants list
            var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
                Find(tr => tr.TripId == tripId);
            await _unitOfWork.Trip_RestaurantRepository.DeleteRange(trip_RestaurantList);

            //Find new Restaurants list which have categories match with the categories the user enter
            var restaurantCategoyIds = tripData.RestaurantCategories;
            var res_resCategories = await _unitOfWork.Res_ResCategoryRepository.
                Find(res => restaurantCategoyIds.Contains(res.RestaurantCategoryId));
            var restaurantIds = new List<int>();
            foreach (var res_resCat in res_resCategories)
            {
                restaurantIds.Add(res_resCat.RestaurantId);
            }
            //Find new Suggest Restaurant List
            var suggestRestaurants =
                (await _unitOfWork.RestaurantRepository.
                Find(res => cityIds.Contains(res.CityId)
                && restaurantIds.Contains(res.RestaurantId)
                && res.PriceLevel.Trim() == tripData.RestaurantPriceLevel.Trim())).
                Take(trip.Duration * 2);
            //Add new Trip_Restaurant List
            var trip_Restaurants = new List<CRUDTrip_RestaurantDTO>();
            foreach (var res in suggestRestaurants)
            {
                trip_Restaurants.Add(new CRUDTrip_RestaurantDTO
                {
                    RestaurantId = res.RestaurantId
                });
            }
            var suggestTrip_Restaurants = _mapper.Map<IEnumerable<Trip_Restaurant>>(trip_Restaurants);
            if (!suggestTrip_Restaurants.Any())
            {
                await _unitOfWork.TripRepository.Delete(trip);
                await _unitOfWork.Save();
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm được nhà hàng phù hợp!"
                };
            }
            foreach (var item in suggestTrip_Restaurants)
            {
                item.TripId = tripId;
            }
            await _unitOfWork.Trip_RestaurantRepository.AddRange(suggestTrip_Restaurants);

            //Delete old suggested tourist attractions list
            var trip_TouristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                Find(ta => ta.TripId == tripId);
            await _unitOfWork.Trip_TouristAttractionRepository.DeleteRange(trip_TouristAttractionList);

            //Find new TAs list which have categories match with the categories the user enter
            var touristAttractionCategoyIds = tripData.TouristAttractionCategories;
            var ta_taCategories = await _unitOfWork.TA_TACategoryRepository.
                Find(ta => touristAttractionCategoyIds.Contains(ta.TouristAttractionCategoryId));
            var touristAttractionIds = new List<int>();
            foreach (var ta_taCat in ta_taCategories)
            {
                touristAttractionIds.Add(ta_taCat.TouristAttractionId);
            }
            //Find new Suggest Tourist Attraction List
            var suggestTouristAttractions = (await _unitOfWork.TouristAttractionRepository.
                Find(ta => cityIds.Contains(ta.CityId)
                && touristAttractionIds.Contains(ta.TouristAttractionId))).Take(trip.Duration * 3);
            //Add new Trip_TouristAttraction List
            var trip_TouristAttractions = new List<CRUDTrip_TouristAttractionDTO>();
            foreach (var ta in suggestTouristAttractions)
            {
                trip_TouristAttractions.Add(new CRUDTrip_TouristAttractionDTO
                {
                    TouristAttractionId = ta.TouristAttractionId
                });
            }
            var suggestTrip_TouristAttractions = _mapper.Map<IEnumerable<Trip_TouristAttraction>>(trip_TouristAttractions);
            if (!suggestTrip_TouristAttractions.Any())
            {
                await _unitOfWork.TripRepository.Delete(trip);
                await _unitOfWork.Save();
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm được địa điểm du lịch phù hợp!"
                };
            }
            foreach (var item in suggestTrip_TouristAttractions)
            {
                item.TripId = tripId;
            }
            await _unitOfWork.Trip_TouristAttractionRepository.AddRange(suggestTrip_TouristAttractions);

            //Delete old locations list
            var trip_LocationList = await _unitOfWork.Trip_LocationRepository.
                Find(tc => tc.TripId == tripId);
            await _unitOfWork.Trip_LocationRepository.DeleteRange(trip_LocationList);
            await _unitOfWork.Save();

            //Add Trip_Location
            await _unitOfWork.Trip_LocationRepository.AddRange(trip_Locations);


            return new BaseResponse<TripDTO>()
            {
                IsSuccess = true,
                Message = "Cập nhật chuyến đi thành công"
            };
        }
    }
}
