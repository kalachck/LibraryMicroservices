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

            if (book == null)
            {
                throw new NotFoundException("Record was not found");
            }

            return await Task.FromResult(_mapper.Map<BookDTO>(book));
        }

        public async Task<BookDTO> GetByTitleAsync(string title)
        {
            var book = await _repository.GetByTitleAsync(title);

            if (book == null)
            {
                throw new NotFoundException("Record was not found");
            }

            return await Task.FromResult(_mapper.Map<BookDTO>(book));
        }

        public async Task<bool> AddAsync(BookDTO book)
        {
            _repository.Add(_mapper.Map<Book>(book));

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(int id, BookDTO book)
        {
            var bookEntity = await _repository.GetAsync(id);

            if (bookEntity == null)
            {
                throw new NotFoundException("Record was not found");
            }

            bookEntity = _mapper.Map<Book>(book);

            bookEntity.Id = id;

            _repository.Update(bookEntity);

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _repository.GetAsync(id);

            if (book == null)
            {
                throw new NotFoundException("Record was not found");
            }

            _repository.Delete(book);

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
