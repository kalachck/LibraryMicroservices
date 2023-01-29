using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;
using Newtonsoft.Json;

namespace LibrarySevice.BussinesLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IBaseRepository<Book, ApplicationContext> _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public BookService(IBaseRepository<Book, ApplicationContext> repository,
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

        public async void ChangeStatus(string message)
        {
            var rabbitMessage = JsonConvert.DeserializeObject<RabbitMessage>(message);

            var book = await _repository.GetAsync(int.Parse(rabbitMessage.Message));

            if (rabbitMessage.Topic == Enums.Topic.Borrow)
            {
                book.IsAvailable = false;
            }
            if (rabbitMessage.Topic == Enums.Topic.Delete)
            {
                book.IsAvailable = true;
            }

            _repository.Update(book);

            await _applicationContext.SaveChangesAsync();
        }
    }
}
