using System.Reflection;

namespace GamesAPI.Extensions
{
    public static class EntityExtensions
    {
        public static void Patch<TSource, TTarget>(this TTarget entity, TSource dto)
        {
            var dtoProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var entityProperties = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProperty in dtoProperties)
            {
                // Encontra a propriedade correspondente na entidade
                var entityProperty = entityProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);

                if (entityProperty != null)
                {
                    var value = dtoProperty.GetValue(dto);

                    // Se o valor não for nulo, atualize a propriedade da entidade
                    if (value != null)
                    {
                        entityProperty.SetValue(entity, value);
                    }
                }
            }
        }
    }
}