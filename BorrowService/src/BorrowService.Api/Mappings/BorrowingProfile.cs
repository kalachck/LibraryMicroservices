using AutoMapper;
using BorrowService.Api.RequestModels;
using BorrowService.Borrowings.Entities;

namespace BorrowService.Api.Mappings
{
    public class BorrowingProfile : Profile
    {
        public BorrowingProfile()
        {
            CreateMap<BorrowingRequestModel, BorrowingEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<BorrowingEntity, BorrowingRequestModel>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());

            CreateMap<BorrowingEntity, BorrowingEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());
        }
    }
}
