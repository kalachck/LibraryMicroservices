using Grpc.Core;
using LibraryService.BussinesLogic.Services.Abstract;
using Newtonsoft.Json;

namespace LibraryService.BussinesLogic.Services
{
    public class GrpcGetBookService : GetBook.GetBookBase
    {
        private readonly IBookService _bookService;

        public GrpcGetBookService(IBookService bookService)
		{
            _bookService = bookService;
        }

        public override async Task<BookResponse> Get(RequestTitle request, ServerCallContext context)
        {
			try
			{
                var book = await _bookService.GetByTitleAsync(request.Title);

                var response = JsonConvert.SerializeObject(book);

                return await Task.FromResult(new BookResponse()
                {
                    Book = response
                });
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
