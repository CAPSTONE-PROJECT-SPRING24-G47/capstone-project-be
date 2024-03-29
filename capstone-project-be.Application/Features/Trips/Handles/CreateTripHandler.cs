﻿using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Locations;
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
    public class CreateTripHandler : IRequestHandler<CreateTripRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTripHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateTripRequest request, CancellationToken cancellationToken)
        {
            //Call out Trip Data
            var tripData = request.TripData;
            var trip = _mapper.Map<Trip>(tripData);
            trip.CreatedAt = DateTime.Now;
            trip.IsPublic = false;
            trip.AccommodationCategories = string.Join(",", tripData.AccommodationCategories);
            trip.RestaurantCategories = string.Join(",", tripData.RestaurantCategories);
            trip.TouristAttractionCategories = string.Join(",", tripData.TouristAttractionCategories);

            //Check userId exist
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == trip.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại User với ID = {trip.UserId}"
                };
            }

            //Check locations null
            if (!trip.Trip_Locations.Any())
            {
                trip.IsCreatedAutomatically = false;
                await _unitOfWork.TripRepository.Add(trip);
                await _unitOfWork.Save();
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = true,
                    Message = "Tự tạo chuyến đi thành công",
                    Data = new List<TripDTO> { _mapper.Map<TripDTO>(trip) }
                };
            }
            else
            {
                foreach (var location in trip.Trip_Locations)
                {
                    if (location.PrefectureId == null)
                    {
                        location.LocationName = "Vùng " + (await _unitOfWork.RegionRepository.
                            Find(r => r.RegionId == location.RegionId)).First().RegionName;
                    }
                    else
                    {
                        if (location.CityId == null)
                        {
                            location.LocationName = "Tỉnh " + (await _unitOfWork.PrefectureRepository.
                            Find(p => p.PrefectureId == location.PrefectureId)).First().PrefectureName;
                        }
                        else
                        {
                            location.LocationName = "Thành phố " + (await _unitOfWork.CityRepository.
                            Find(c => c.CityId == location.CityId)).First().CityName;
                        }
                    }
                }
                //Add trip temporarily
                trip.IsCreatedAutomatically = true;
                await _unitOfWork.TripRepository.Add(trip);
                await _unitOfWork.Save();
            }

            //Call out recently added trip
            var tripList = await _unitOfWork.TripRepository.
                Find(t => t.UserId == trip.UserId && t.CreatedAt >= DateTime.Now.AddMinutes(-1));

            //Find list City Suitable
            var tripId = tripList.First().TripId;
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
                    var prefectures = await _unitOfWork.PrefectureRepository.Find(p => p.RegionId == regionId);
                    foreach (var prefecture in prefectures)
                    {
                        prefectureId = prefecture.PrefectureId;
                        var citys = await _unitOfWork.CityRepository.Find(c => c.PrefectureId == prefectureId);
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
                        foreach (var prefecture in prefectures)
                        {
                            prefectureId = prefecture.PrefectureId;
                            var citys = await _unitOfWork.CityRepository.Find(c => c.PrefectureId == prefectureId);
                            foreach (var city in citys)
                            {
                                cityIds.Add(city.CityId);
                            }
                        }
                    }
                }
            }

            //Check accommodation categories null
            if (trip.AccommodationPriceLevel == null)
            {
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = true,
                    Message = "Tự tạo chuyến đi thành công",
                    Data = new List<TripDTO> { _mapper.Map<TripDTO>(trip) }
                };
            }

            //Find list Accommodations which have categories match with the categories the user enter
            var accommodationCategoyIds = tripData.AccommodationCategories;
            var acc_accCategories = await _unitOfWork.Acc_AccCategoryRepository.
                Find(acc => accommodationCategoyIds.Contains(acc.AccommodationCategoryId));
            var accommodationIds = new List<int>();
            foreach (var acc_accCat in acc_accCategories)
            {
                accommodationIds.Add(acc_accCat.AccommodationId);
            }
            //Find list Suggest Accommodation
            var suggestAccommodations =
                (await _unitOfWork.AccommodationRepository.Find(acc =>
                cityIds.Contains(acc.CityId)
                && accommodationIds.Contains(acc.AccommodationId)
                && acc.PriceLevel.Trim() == tripData.AccommodationPriceLevel.Trim()));
            var suggestAccommodationList = _mapper.Map<IEnumerable<SuggestedAccommodationDTO>>(suggestAccommodations);
            //foreach (var sa in suggestAccommodationList)
            //{
            //    var accommodationId = sa.AccommodationId;
            //    var accommodationComments = await _unitOfWork.AccommodationCommentRepository.
            //        Find(ac => ac.AccommodationId == accommodationId);
            //    var numberOfComment = accommodationComments.Count();
            //    sa.NumberOfComment = numberOfComment;
            //    if (numberOfComment == 0) sa.Star = 0;
            //    else
            //    {
            //        float sumOfStar = 0;
            //        foreach (var ac in accommodationComments)
            //        {
            //            sumOfStar += ac.Stars;
            //        }
            //        sa.Star = sumOfStar / numberOfComment;
            //    }
            //}
            //suggestAccommodationList = suggestAccommodationList.Where(sa => sa.Star >= 4).
            //    OrderBy(sa => sa.NumberOfComment).Take(10);
            suggestAccommodations = _mapper.Map<IEnumerable<Accommodation>>(suggestAccommodationList.Take(10));

            //Add list Trip_Accommodation
            var trip_Accommodations = new List<CRUDTrip_AccommodationDTO>();
            foreach (var acc in suggestAccommodations)
            {
                trip_Accommodations.Add(new CRUDTrip_AccommodationDTO
                {
                    AccommodationId = acc.AccommodationId,
                    SuggestedDay = 0
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

            //Check restaurants null
            if (trip.RestaurantPriceLevel == null)
            {
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = true,
                    Message = "Tự tạo chuyến đi thành công",
                    Data = new List<TripDTO> { _mapper.Map<TripDTO>(trip) }
                };
            }

            //Find list Restaurants which have categories match with the categories the user enter
            var restaurantCategoyIds = tripData.RestaurantCategories;
            var res_resCategories = await _unitOfWork.Res_ResCategoryRepository.
                Find(res => restaurantCategoyIds.Contains(res.RestaurantCategoryId));
            var restaurantIds = new List<int>();
            foreach (var res_resCat in res_resCategories)
            {
                restaurantIds.Add(res_resCat.RestaurantId);
            }
            //Find list Suggest Restaurant
            var suggestRestaurants =
                (await _unitOfWork.RestaurantRepository.
                Find(res => cityIds.Contains(res.CityId)
                && restaurantIds.Contains(res.RestaurantId)
                && res.PriceLevel.Trim() == tripData.RestaurantPriceLevel.Trim()));
            var suggestRestaurantList = _mapper.Map<IEnumerable<SuggestedRestaurantDTO>>(suggestRestaurants);
            //foreach (var sr in suggestRestaurantList)
            //{
            //    var restaurantId = sr.RestaurantId;
            //    var restaurantComments = await _unitOfWork.RestaurantCommentRepository.
            //        Find(rc => rc.RestaurantId == restaurantId);
            //    var numberOfComment = restaurantComments.Count();
            //    sr.NumberOfComment = numberOfComment;
            //    if (numberOfComment == 0) sr.Star = 0;
            //    else
            //    {
            //        float sumOfStar = 0;
            //        foreach (var rc in restaurantComments)
            //        {
            //            sumOfStar += rc.Stars;
            //        }
            //        sr.Star = sumOfStar / numberOfComment;
            //    }
            //}
            //suggestRestaurantList = suggestRestaurantList.Where(sr => sr.Star >= 4).
            //    OrderBy(sr => sr.NumberOfComment).Take(trip.Duration * 2);
            suggestRestaurants = _mapper.Map<IEnumerable<Restaurant>>(suggestRestaurantList.Take(trip.Duration * 2));

            //Add list Trip_Restaurant
            var trip_Restaurants = new List<CRUDTrip_RestaurantDTO>();
            foreach (var res in suggestRestaurants)
            {
                trip_Restaurants.Add(new CRUDTrip_RestaurantDTO
                {
                    RestaurantId = res.RestaurantId,
                    SuggestedDay = 0
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

            //Find list TAs which have categories match with the categories the user enter
            var touristAttractionCategoyIds = tripData.TouristAttractionCategories;
            var ta_taCategories = await _unitOfWork.TA_TACategoryRepository.
                Find(ta => touristAttractionCategoyIds.Contains(ta.TouristAttractionCategoryId));
            var touristAttractionIds = new List<int>();
            foreach (var ta_taCat in ta_taCategories)
            {
                touristAttractionIds.Add(ta_taCat.TouristAttractionId);
            }
            //Find list Suggest Tourist Attraction
            var suggestTouristAttractions = (await _unitOfWork.TouristAttractionRepository.
                Find(ta => cityIds.Contains(ta.CityId)
                && touristAttractionIds.Contains(ta.TouristAttractionId)));
            var suggestTouristAttractionList = _mapper.Map<IEnumerable<SuggestedTouristAttractonDTO>>(suggestTouristAttractions);
            //foreach (var sta in suggestTouristAttractionList)
            //{
            //    var touristAttractionId = sta.TouristAttractionId;
            //    var touristAttractionComments = await _unitOfWork.TouristAttractionCommentRepository.
            //        Find(tac => tac.TouristAttractionId == touristAttractionId);
            //    var numberOfComment = touristAttractionComments.Count();
            //    sta.NumberOfComment = numberOfComment;
            //    if (numberOfComment == 0) sta.Star = 0;
            //    else
            //    {
            //        float sumOfStar = 0;
            //        foreach (var tac in touristAttractionComments)
            //        {
            //            sumOfStar += tac.Stars;
            //        }
            //        sta.Star = sumOfStar / numberOfComment;
            //    }
            //}
            //suggestTouristAttractionList = suggestTouristAttractionList.Where(sta => sta.Star >= 4).
            //    OrderBy(sta => sta.NumberOfComment).Take(trip.Duration * 3);
            suggestTouristAttractions = _mapper.Map<IEnumerable<TouristAttraction>>(suggestTouristAttractionList.Take(trip.Duration * 3));
            //Add list Trip_TouristAttraction
            var trip_TouristAttractions = new List<CRUDTrip_TouristAttractionDTO>();
            foreach (var ta in suggestTouristAttractions)
            {
                trip_TouristAttractions.Add(new CRUDTrip_TouristAttractionDTO
                {
                    TouristAttractionId = ta.TouristAttractionId,
                    SuggestedDay = 0
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
            await _unitOfWork.Save();

            //Call out recently added trip_res and trip_ta
            var addedTrip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
                Find(atr => atr.TripId == tripId);
            var addedTrip_TouristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                Find(atta => atta.TripId == tripId);

            //Add suggestedDay for restaurant and tourist attraction in the trip
            int suggestedDay = 1;
            foreach (var TTA in addedTrip_TouristAttractionList)
            {
                if (TTA.SuggestedDay == 0)
                {
                    TTA.SuggestedDay = suggestedDay;
                    await _unitOfWork.Trip_TouristAttractionRepository.Update(TTA);

                    // Find the two closest touristAttractions
                    Trip_TouristAttraction closest1 = null;
                    Trip_TouristAttraction closest2 = null;
                    double minDistance1 = double.MaxValue;
                    double minDistance2 = double.MaxValue;

                    foreach (var otherTTA in addedTrip_TouristAttractionList)
                    {
                        if (otherTTA != TTA && otherTTA.SuggestedDay == 0)
                        {
                            //Call out lat lon of tourist attraction
                            var touristAttraction1 = await _unitOfWork.TouristAttractionRepository.GetByID(TTA.TouristAttractionId);
                            string[] taParts1 = touristAttraction1.TouristAttractionLocation.Split('-');
                            var taLat1 = Double.Parse(taParts1[0]);
                            var taLon1 = Double.Parse(taParts1[1]);

                            var touristAttraction2 = await _unitOfWork.TouristAttractionRepository.GetByID(otherTTA.TouristAttractionId);
                            string[] taParts2 = touristAttraction2.TouristAttractionLocation.Split('-');
                            var taLat2 = Double.Parse(taParts2[0]);
                            var taLon2 = Double.Parse(taParts2[1]);
                            double distance = CalculateDistance(taLat1, taLat2, taLon1, taLon2);
                            if (distance < minDistance1)
                            {
                                minDistance2 = minDistance1;
                                closest2 = closest1;
                                minDistance1 = distance;
                                closest1 = otherTTA;
                            }
                            else if (distance < minDistance2)
                            {
                                minDistance2 = distance;
                                closest2 = otherTTA;
                            }
                        }
                    }

                    //Update the two closest touristAttraction with suggestedDay
                    if (closest1 != null)
                    {
                        closest1.SuggestedDay = suggestedDay;
                        await _unitOfWork.Trip_TouristAttractionRepository.Update(closest1);
                    }
                    if (closest2 != null)
                    {
                        closest2.SuggestedDay = suggestedDay;
                        await _unitOfWork.Trip_TouristAttractionRepository.Update(closest2);
                    }

                    // Find the closest restaurants
                    Trip_Restaurant closestRes1 = null;
                    double minResDistance1 = double.MaxValue;

                    foreach (var TR in addedTrip_RestaurantList)
                    {
                        if (TR.SuggestedDay == 0)
                        {
                            //Call out lat lon of tourist attraction
                            var touristAttraction1 = await _unitOfWork.TouristAttractionRepository.GetByID(TTA.TouristAttractionId);
                            string[] taParts1 = touristAttraction1.TouristAttractionLocation.Split('-');
                            var taLat1 = Double.Parse(taParts1[0]);
                            var taLon1 = Double.Parse(taParts1[1]);

                            //Call out lat lon of restaurant
                            var restaurant1 = await _unitOfWork.RestaurantRepository.GetByID(TR.RestaurantId);
                            string[] resParts1 = restaurant1.RestaurantLocation.Split('-');
                            var resLat1 = Double.Parse(resParts1[0]);
                            var resLon1 = Double.Parse(resParts1[1]);

                            double distance = CalculateDistance(taLat1, resLat1, taLon1, resLon1);
                            if(distance < minResDistance1) 
                            {
                                minResDistance1 = distance;
                                closestRes1 = TR;
                            }
                        }
                    }

                    // Find the next closest restaurants
                    Trip_Restaurant closestRes2 = null;
                    double minResDistance2 = double.MaxValue;

                    foreach (var otherTR in addedTrip_RestaurantList)
                    {
                        if (otherTR.SuggestedDay == 0 && otherTR!=closestRes1)
                        {
                            //Call out lat lon of closest restaurant
                            var restaurant1 = await _unitOfWork.RestaurantRepository.GetByID(closestRes1.RestaurantId);
                            string[] resParts1 = restaurant1.RestaurantLocation.Split('-');
                            var resLat1 = Double.Parse(resParts1[0]);
                            var resLon1 = Double.Parse(resParts1[1]);

                            //Call out lat lon of next closest restaurant
                            var restaurant2 = await _unitOfWork.RestaurantRepository.GetByID(otherTR.RestaurantId);
                            string[] resParts2 = restaurant2.RestaurantLocation.Split('-');
                            var resLat2 = Double.Parse(resParts2[0]);
                            var resLon2 = Double.Parse(resParts2[1]);

                            double distance = CalculateDistance(resLat1, resLat2, resLon1, resLon2);
                            if (distance < minResDistance2)
                            {
                                minResDistance2 = distance;
                                closestRes2 = otherTR;
                            }
                        }
                    }
                    //Update the closest restaurant with suggestedDay
                    if (closestRes1 != null)
                    {
                        closestRes1.SuggestedDay = suggestedDay;
                        await _unitOfWork.Trip_RestaurantRepository.Update(closestRes1);
                    }
                    if (closestRes2 != null)
                    {
                        closestRes2.SuggestedDay = suggestedDay;
                        await _unitOfWork.Trip_RestaurantRepository.Update(closestRes2);
                    }

                    //Update the two closest touristAttraction with suggestedDay
                    if (closest1 != null)
                    {
                        closest1.SuggestedDay = suggestedDay;
                        await _unitOfWork.Trip_TouristAttractionRepository.Update(closest1);
                    }
                    if (closest2 != null)
                    {
                        closest2.SuggestedDay = suggestedDay;
                        await _unitOfWork.Trip_TouristAttractionRepository.Update(closest2);
                    }
                    await _unitOfWork.Save();
                    suggestedDay++;
                }
            }

            //Add Trip_Location
            await _unitOfWork.Trip_LocationRepository.AddRange(trip_Locations);

            //Return recently added trip
            var resultData = await _unitOfWork.TripRepository.Find(t => t.TripId == tripId);
            var result = _mapper.Map<TripDTO>(resultData.First());

            var trip_LocationList = await _unitOfWork.Trip_LocationRepository.
                Find(tl => tl.TripId == tripId);
            result.Trip_Locations = _mapper.Map<IEnumerable<Trip_LocationDTO>>(trip_LocationList);

            var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
                GetAccommodationsByTripId(tripId);
            foreach (var tripAcc in trip_AccommodationList)
            {
                var trip_Accommodation = await _unitOfWork.Trip_AccommodationRepository.
                    Find(ta => ta.TripId == tripId && ta.AccommodationId == tripAcc.AccommodationId);
                var Id = trip_Accommodation.First().Id;
                tripAcc.Id = Id;
            }
            result.Trip_Accommodations = _mapper.Map<IEnumerable<CRUDTrip_AccommodationDTO>>(trip_AccommodationList);

            var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
            GetRestaurantsByTripId(tripId);
            foreach (var tripRes in trip_RestaurantList)
            {
                var trip_Restaurant = await _unitOfWork.Trip_RestaurantRepository.
                    Find(tr => tr.TripId == tripId && tr.RestaurantId == tripRes.RestaurantId);
                var Id = trip_Restaurant.First().Id;
                tripRes.Id = Id;
            }
            result.Trip_Restaurants = _mapper.Map<IEnumerable<CRUDTrip_RestaurantDTO>>(trip_RestaurantList);

            var trip_touristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                GetTouristAttractionsByTripId(tripId);
            foreach (var tripTa in trip_touristAttractionList)
            {
                var trip_TouristAttraction = await _unitOfWork.Trip_TouristAttractionRepository.
                    Find(tta => tta.TripId == tripId && tta.TouristAttractionId == tripTa.TouristAttractionId);
                var Id = trip_TouristAttraction.First().Id;
                tripTa.Id = Id;
            }
            result.Trip_TouristAttractions = _mapper.Map<IEnumerable<CRUDTrip_TouristAttractionDTO>>(trip_touristAttractionList);


            return new BaseResponse<TripDTO>()
            {
                IsSuccess = true,
                Message = "Chuyến đi đã được tạo tự động",
                Data = new List<TripDTO> { result }
            };
        }

        //Haversine formula to find distance between 2 points on a sphere
        private static double CalculateDistance(double lat1, double lat2, double lon1, double lon2)
        {
            lat1 = lat1 * Math.PI / 180.0;
            lat2 = lat2 * Math.PI / 180.0;
            lon1 = lon1 * Math.PI / 180.0;
            lon2 = lon2 * Math.PI / 180.0;
            const double r = 6371; //Radius of the earth : kilometers
            var dLat = lat2 - lat1; //Latitude : Vĩ độ
            var dLon = lon2 - lon1; //Longitude : Kinh độ 
            var q = Math.Pow(Math.Sin(dLat / 2), 2) +
               Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dLon / 2), 2);
            var d = 2 * r * Math.Asin(Math.Sqrt(q));
            return d;
        }

    }
}
