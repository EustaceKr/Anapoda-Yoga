using Application.DTOs.ReservationDTOs;
using AutoMapper;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class ReservationsProfile : Profile
    {
        public ReservationsProfile()
        {
            CreateMap<Reservation, ReservationReadDTO>();
            CreateMap<ReservationCreateDTO, Reservation>();
            CreateMap<ReservationAdminCreateDTO, Reservation>();
        }
    }
}
