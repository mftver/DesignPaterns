using Factory.Interfaces;
using Model;
using File = Factory.models.File;

namespace Factory
{
    internal class OmniFactory : IFactory
    {
        private IEnumerable<IFactory> _factories;
        
        public OmniFactory()
        {
            var factories = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IFactory).IsAssignableFrom(type) && GetType() != type && !type.IsInterface && !type.IsAbstract);

            _factories = factories.Select(factory => (IFactory)Activator.CreateInstance(factory)!);

        }
        
        public Sudoku Create(File file) {
            foreach (var factory in _factories) if (factory.Supports(file)) return factory.Create(file);

            throw new NotImplementedException($"This file is not supported");
        }

        public bool Supports(File file)
        {
            return _factories.Any(factory => factory.Supports(file));
        }
    }
}
