using AutoMapper;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories.Abstract;

namespace LibraryService.BussinesLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IDbManager<Book> _dbManager;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository,
            IDbManager<Book> dbManager,
            IMapper mapper)
        {
            _repository = repository;
            _dbManager = dbManager;
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

            await _dbManager.SaveChangesAsync();

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

            await _dbManager.SaveChangesAsync();

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

            await _dbManager.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task LockAsync(string message)
        {
            if (!int.TryParse(message, out int id))
            {
                throw new ParseException("Can't parse rabbit message");
            }

            var book = await _repository.GetAsync(id);

            book.IsAvailable = false;

            _repository.Update(book);

            await _dbManager.SaveChangesAsync();

            _dbManager.DetacheEntity(book);
        }

        public async Task UnlockAsync(string message)
        {
            if (!int.TryParse(message, out int id))
            {
                throw new ParseException("Can't parse rabbit message");
            }

            var book = await _repository.GetAsync(id);

            book.IsAvailable = true;

            _repository.Update(book);

            await _dbManager.SaveChangesAsync();

            _dbManager.DetacheEntity(book);
        }
    }
}
