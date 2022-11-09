namespace Quinela_TPD.Models.Helper
{
    public interface IEntity<T> where T : notnull
    {
        public T Clave { get; set; }
    }
}
