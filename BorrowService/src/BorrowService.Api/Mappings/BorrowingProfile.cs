using AutoMapper;
using BorrowService.Api.Models;
using BorrowService.Borrowings.Entities;

namespace BorrowService.Api.Mappings
{
    public class BorrowingProfile : Profile
    {
        public BorrowingProfile()
        {
            CreateMap<BorrowingModel, BorrowingEntity>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
