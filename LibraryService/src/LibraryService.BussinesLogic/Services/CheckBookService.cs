using Grpc.Core;
using LibrarySevice.BussinesLogic.Services.Abstract;

namespace LibrarySevice.BussinesLogic.Services
{
    public class CheckBookService : CheckBook.CheckBookBase
    {
        private readonly IBookService _bookService;

        public CheckBookService(IBookService bookService)
        {
            _bookService = bookService;
        }

        public override async Task<ResponseMessage> Check(RequestId request, ServerCallContext context)
        {
            try
            {
                var book = await _bookService.GetAsync(request.Id);

                return await Task.FromResult(new ResponseMessage()
                {
                    IsAvailable = book.IsAvailable,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
