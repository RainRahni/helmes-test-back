﻿using AutoMapper;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using System.Net;

namespace post_office_back.Services
{
    public class ParcelService : IParcelService
    {
        private readonly DataContext _dataContext;
        private readonly ValidationService _validationService;
        private readonly IMapper _mapper;

        public ParcelService(DataContext dataContext, ValidationService validationService, IMapper mapper)
        {
            _dataContext = dataContext;
            _validationService = validationService;
            _mapper = mapper;
        }
        public HttpResponseMessage CreateParcel(ParcelCreationDto parcelCreationDto)
        {
            try
            {
                _validationService.ValidateParcelCreation(parcelCreationDto);
            }
            catch (ArgumentException ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message)
                };
                return response;
            }
            Bag bag = _dataContext.Bags.First(b => b.BagNumber.Equals(parcelCreationDto.BagNumber));
            Parcel parcel = _mapper.Map<Parcel>(parcelCreationDto);
            if (bag.Discriminator.Equals("PARCELBAG"))
            {
                ParcelBag parcelBag = bag as ParcelBag;
                parcelBag.Parcels.Add(parcel);
            } else
            {
                String bagNumber = bag.BagNumber;
                Shipment shipment = _dataContext.Shipments.First(s => s.Bags.Any(b => b.BagNumber.Equals(parcelCreationDto.BagNumber)));
                shipment.Bags.Remove(bag);

            }

            _dataContext.SaveChanges();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
