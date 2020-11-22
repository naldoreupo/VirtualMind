using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Infraestructure.Entity;
using VirtualMind.Exam.Service.Entity.Model.Input;

namespace VirtualMind.Exam.Transversal
{
    public static class Maps
    {
        public static IMapper InitMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PurchaseInput, PurchaseDTO>();
                cfg.CreateMap<Purchase, PurchaseDTO>();
                cfg.CreateMap<PurchaseInput, Purchase>();

                cfg.CreateMap<PurchaseDTO, PurchaseInput>();
                cfg.CreateMap<PurchaseDTO,Purchase>();
                cfg.CreateMap<Purchase,PurchaseInput>();

            });

            return config.CreateMapper();
        }
    }
}
