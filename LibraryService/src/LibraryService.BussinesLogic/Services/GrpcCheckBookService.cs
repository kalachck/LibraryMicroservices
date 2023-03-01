using Grpc.Core;
using LibraryService.BussinesLogic.Services.Abstract;

namespace LibraryService.BussinesLogic.Services
{
    public class GrpcCheckBookService : CheckBook.CheckBookBase
    {
        private readonly IBookService _bookService;

        public GrpcCheckBookService(IBookService bookService)
        {
            _bookService = bookService;
        }

        public override async Task<CheckBookResponse> Check(RequestId request, ServerCallContext context)
        {
            try
			{
                var book = await _bookService.GetAsync(request.Id);

                return await Task.FromResult(new CheckBookResponse()
                {
                    IsExist = true
                });
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
