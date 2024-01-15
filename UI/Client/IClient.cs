
using DTO;

namespace UI.Client
{
    public interface IClient<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetByID(int Id);
        public Task<DTO.Response> Insert(T objDto);
        public Task<DTO.Response> Update(T objDto);
        public Task<T> Delete(T objDto);

    }
}
