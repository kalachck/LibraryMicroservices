using AutoMapper;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess;
using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories;
using LibraryService.BussinesLogic;
using Newtonsoft.Json;

namespace LibraryService.BussinesLogic.Services
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
            try
            {
                var book = await _repository.GetAsync(id);

                if (book != null)
                {
                    return await Task.FromResult(_mapper.Map<BookDTO>(book));
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookDTO> GetByTitleAsync(string title)
        {
            var book = await _repository.GetByTitleAsync(title);

            if (book != null)
            {
                return await Task.FromResult(_mapper.Map<BookDTO>(book));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<string> AddAsync(BookDTO book)
        {
            try
            {
                _repository.Add(_mapper.Map<Book>(book));

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult("The record was successfully added");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UpdateAsync(int id, BookDTO book)
        {
            try
            {
                var bookEntity = await _repository.GetAsync(id);

                if (bookEntity != null)
                {
                    bookEntity = _mapper.Map<Book>(book);

                    bookEntity.Id = id;

                    _repository.Update(bookEntity);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully updated");
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var book = await _repository.GetAsync(id);

                if (book != null)
                {
                    _repository.Delete(book);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully deleted");
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ChangeStatus(string message)
        {
            var rabbitMessage = JsonConvert.DeserializeObject<RabbitMessage>(message);

            var book = await _repository.GetAsync(rabbitMessage.Id);

            if (book == null)
            {
                throw new NotFoundException("Record was not found");
            }

            if (rabbitMessage.Action == Enums.Action.Lock)
            {
                book.IsAvailable = false;
            }
            if (rabbitMessage.Action == Enums.Action.Unlock)
            {
                book.IsAvailable = true;
            }

            _repository.Update(book);

            await _applicationContext.SaveChangesAsync();
        }
    }
}
