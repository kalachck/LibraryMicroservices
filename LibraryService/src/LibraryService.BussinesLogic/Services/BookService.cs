using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;

namespace LibrarySevice.BussinesLogic.Services
{
    public class BookService : IBookService
    {
        private readonly BookRepository _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public BookService(BookRepository repository,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task<BookDTO> GetAsync(int id)
        {
            var book = await _repository.GetAsync(id);

            if (book != null)
            {
                return await Task.FromResult(_mapper.Map<BookDTO>(book));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<List<BookDTO>> TakeAsync(int amount)
        {
            var books = await _repository.TakeAsync(amount);

            if (books != null)
            {
                return _mapper.Map<List<BookDTO>>(books);
            }

            throw new NotFoundException("Not a single record was found");
        }

        public async Task<BookDTO> AddAsync(BookDTO book)
        {
            var result = await _repository.AddAsync(_mapper.Map<BookEntity>(book));

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<BookDTO>(result));
        }

        public async Task<BookDTO> UpdateAsync(int id, BookDTO book)
        {
            var bookEntity = await _repository.GetAsync(id);

            if (bookEntity != null)
            {
                bookEntity = _mapper.Map<BookEntity>(book);

                bookEntity.Id = id;

                var result = await _repository.UpdateAsync(bookEntity);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(_mapper.Map<BookDTO>(result));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<BookDTO> DeleteAsync(int id)
        {
            var book = await _repository.GetAsync(id);

            if (book != null)
            {
                var result = await _repository.DeleteAsync(book);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(_mapper.Map<BookDTO>(result));
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
