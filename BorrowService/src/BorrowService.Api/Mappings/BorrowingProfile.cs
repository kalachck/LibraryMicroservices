using AutoMapper;
using BorrowService.Api.RequestModels;
using BorrowService.Borrowings.Entities;

namespace BorrowService.Api.Mappings
{
    public class BorrowingProfile : Profile
    {
        public BorrowingProfile()
        {
            CreateMap<BorrowingRequestModel, Borrowing>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Borrowing, BorrowingRequestModel>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());

            CreateMap<Borrowing, Borrowing>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());
        }
    }
}
