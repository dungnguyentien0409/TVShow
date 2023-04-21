using System;
namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ICharacteristicRepository Characteristic { get; }
        public IEpisodeRepository Episode { get; }
        public IGenderRepository Gender { get; }
        public ILocationRepository Location { get; }
        public IOriginRepository Origin { get; }
        public ISpeciesRepository Species { get; }
        public IStatusRepository Status { get; }
        public ITypeRepository Type { get; }

        int Save();
    }
}

