﻿using Enoca_Challenge.Domain.Entities;
using MediatR;

namespace Enoca_Challenge.Application.Features.Carriers.Commands.CreateCarrier
{
    public class CreateCarrierQueryRequest:IRequest<CreateCarrierQueryResponse>
    {
        public string CarrierName { get; set; }

        public bool CarrierIsActive { get; set; }

        public int CarrierPlusDesiCost { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

        public virtual ICollection<CarrierConfiguration>? CarrierConfigurations { get; set; }
    }
}
