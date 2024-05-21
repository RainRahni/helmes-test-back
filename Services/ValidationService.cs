﻿using Microsoft.Extensions.FileSystemGlobbing.Internal;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models.Enums;
using System.Text.RegularExpressions;

namespace post_office_back.Services
{
    public class ValidationService
    {
        private readonly DataContext _dataContext;
        public ValidationService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        internal void validateBagCreation(string shipmentNumber, string bagNumber)
        {
            bool isCorrectShipmentNumber = Regex.IsMatch(shipmentNumber, Constants.shipmentNumberPattern);
            bool isCorrectBagNumber = Regex.IsMatch(bagNumber, Constants.bagNumberPattern);
            bool isUniqueBagNumber = !_dataContext.Bags.Any(b  => b.BagNumber == bagNumber);
            bool isShipmentAvailable = _dataContext.Shipments.Any(s => s.ShipmentNumber.Equals(shipmentNumber) && !s.IsFinalized);

            if (isCorrectShipmentNumber && isCorrectBagNumber && isUniqueBagNumber && isShipmentAvailable)
            {
                return;
            }
            throw new ArgumentException(Constants.invalidParametersMessage);
        }

        internal void validateShipementCreation(ShipmentDto shipmentDto)
        {
            bool isCorrectShipmentNumber = Regex.IsMatch(shipmentDto.ShipmentNumber, Constants.shipmentNumberPattern);
            bool isUniqueShipmentNumber = !_dataContext.Shipments.Any(s => s.ShipmentNumber.Equals(shipmentDto.ShipmentNumber));
            bool isCorrectEnumValue = Enum.IsDefined(typeof(Airport), shipmentDto.DestinationAirport);
            bool isCorrectFlightNumber = Regex.IsMatch(shipmentDto.FlightNumber, Constants.flightNumberPattern);
            bool isNotInPast = shipmentDto.FlightDate < DateTime.Now;
            if (isCorrectShipmentNumber && isUniqueShipmentNumber && isCorrectEnumValue && isCorrectFlightNumber && isNotInPast)
            {
                return;
            };
            throw new ArgumentException(Constants.invalidParametersMessage);
        }
    }
}