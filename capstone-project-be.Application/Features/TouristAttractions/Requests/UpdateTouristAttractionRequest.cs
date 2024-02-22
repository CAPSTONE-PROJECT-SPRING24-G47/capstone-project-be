using capstone_project_be.Application.DTOs.TouristAttractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class UpdateTouristAttractionRequest(string touristAttractionId, CRUDTouristAttractionDTO touristAttractionData): IRequest<Object>
    {
        public CRUDTouristAttractionDTO TouristAttractionData { get; set; } = touristAttractionData;
        public string TouristAttractionId { get; set; } = touristAttractionId;
    }
}
