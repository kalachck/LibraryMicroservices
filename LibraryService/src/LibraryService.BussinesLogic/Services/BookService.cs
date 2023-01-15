using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;
using Microsoft.Data.SqlClient;

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
            var book = _repository.GetAsync(id);

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
                _repository.AddAsync(_mapper.Map<Book>(book));

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult("The record was successfully added");
            }
            catch (SqlException)
            {
                return await Task.FromResult("The record was not added. There were technical problems");
            }
        }

        public async Task<string> UpdateAsync(int id, BookDTO book)
        {
            var bookEntity = _repository.GetAsync(id);

            if (bookEntity != null)
            {
                bookEntity = _mapper.Map<Book>(book);

                bookEntity.Id = id;

                try
                {
                    _repository.UpdateAsync(bookEntity);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully updated");
                }
                catch (SqlException)
                {
                    return await Task.FromResult("The record was not updated. There were technical problems");
                }
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<string> DeleteAsync(int id)
        {
            var book = _repository.GetAsync(id);

            if (book != null)
            {
                try
                {
                    _repository.DeleteAsync(book);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully deleted");
                }
                catch (Exception)
                {
                    return await Task.FromResult("The record was not deleted. There were technical problems");
                }
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
