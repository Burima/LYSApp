using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;
using LYSApp.Data.DBRepository;

namespace LYSApp.Domain.SearchManagement
{
    public class SearchManagement : ISearchManagement
    {
        private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.City> cityRepository = null;
        private IBaseRepository<Data.DBEntity.Area> areaRepository = null;
        private IBaseRepository<Data.DBEntity.House> houseRepository = null;
        public SearchManagement()
        {
            unitOfWork = new UnitOfWork();
            cityRepository = new BaseRepository<Data.DBEntity.City>(unitOfWork);
            areaRepository = new BaseRepository<Data.DBEntity.Area>(unitOfWork);
            houseRepository = new BaseRepository<Data.DBEntity.House>(unitOfWork);
        }

        public IList<House> getHouses(SerchViewModel searchViewModel)
        {
            IList<House> houseList = new List<House>();

            var city = cityRepository.FirstOrDefault(m => m.CityName.Equals(searchViewModel.City));
            var area = areaRepository.FirstOrDefault(m => m.CityID == city.CityID && m.AreaName.Equals(searchViewModel.Area));
            return null;
        }
    }
}
