using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advocate.Data;
using Advocate.Dtos;
using Advocate.Entities;
using Advocate.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Advocate.AutoMapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{

			CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
			CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
			CreateMap<NavigationEntity, NavigationViewModel>().ReverseMap();
			CreateMap<SubjectEntity, SubjectViewModel>().ReverseMap();
			CreateMap<BookEntity, BookViewModel>().ReverseMap();
			CreateMap<ActTypeEntity, ActTypeViewModel>().ReverseMap();
			CreateMap<GazetteTypeEntity, GazetteViewModel>().ReverseMap();
			CreateMap<PartEntity, SubGazetteViewModel>()
				.ForMember(dest => dest.GazzetId, opt => opt.MapFrom(src => src.GazettId))
				.ForMember(dest => dest.gazetteName, opt => opt.MapFrom(src => src.gazetteTypeEntity.GazetteName))
				.ReverseMap();
			CreateMap<ActEntity, ActDto>();
			CreateMap<ActEntity, ActViewModel>()
				.ForMember(source => source.AssentDate, opt => opt.Ignore())
				.ForMember(source => source.GazetteDate, opt => opt.Ignore())
				.ForMember(source => source.PublishedInId, opt => opt.MapFrom(src => src.GazetteId))
				.ForMember(source => source.GazetteNuture, opt => opt.MapFrom(src => src.Nature))
				.ForMember(source => source.PageNumber, opt => opt.MapFrom(src => src.PageNo))
				.ReverseMap();
			CreateMap<ActViewModel, ActEntity>().ForMember(dest => dest.ActBookList, t => t.Ignore());

			CreateMap<RuleEntity, RuleViewModel>()
				 .ForMember(source => source.RuleDate, opt => opt.Ignore())
				.ForMember(source => source.ComeInforceEFDate, opt => opt.Ignore())
				.ReverseMap();
			CreateMap<NotificationTypeEntity, NotificationTypesViewModel>().ReverseMap();

			CreateMap<NotificationEntity, NotificationViewModel>()
				 .ForMember(source => source.Notification_date, opt => opt.MapFrom(src => src.Notification_date))
				  .ForMember(source => source.PublishedInGazeteDate, opt => opt.MapFrom(src => src.PublishedInGazeteDate))
				 .ForMember(source => source.ComeInforce, opt => opt.Ignore())
				 .ForMember(source => source.GSRSO_No, opt => opt.MapFrom(src => src.GSRSO_Number))
				 .ForMember(source => source.GSRSO_Prefix, opt => opt.MapFrom(src => src.GSR_SO))
				 .ReverseMap();

			CreateMap<BookEntryDetailEntity, BookEntryDetailViewModel>()
				 .ForMember(source => source.GazetteDate, opt => opt.MapFrom(src => src.GazetteDate))
				.ReverseMap();

			CreateMap<GazzetDataDto, EGazzetDataEntity>().ReverseMap();

			

		}
	}
}





