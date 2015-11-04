using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;
using LYSApp.Data.DBRepository;

namespace LYSApp.Domain.StaticContentManagement
{
    public class StaticContentManagement
    {
         private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.City> cityRepository = null;
        private IBaseRepository<Data.DBEntity.Area> areaRepository = null;
        public StaticContentManagement()
        {
            unitOfWork = new UnitOfWork();
            cityRepository = new BaseRepository<Data.DBEntity.City>(unitOfWork);
            areaRepository = new BaseRepository<Data.DBEntity.Area>(unitOfWork);
        }

        /// <summary>
        /// this method will get all the cities available in India
        /// </summary>
        /// <returns>returns a list of cities</returns>
        public IList<Model.City> GetAllCities()
        {
            IList<Model.City> cities = (from p in cityRepository.Get()
                                        select new Model.City
                                                 {
                                                     CityID=p.CityID,
                                                     CityName=p.CityName
                                                 }).ToList();


            return cities;
        }

        /// <summary>
        /// this method will return all the areas of a 
        /// selected city based on CityID
        /// </summary>
        /// <returns></returns>
        public IList<Model.Area> GetAllAreas()
        {
            IList<Model.Area> areas = (from p in areaRepository.Get()
                                        select new Model.Area
                                        {
                                           AreaName=p.AreaName,
                                           AreaID=p.AreaID,
                                           CityID=p.CityID
                                        }).ToList();


            return areas;
        }
    }

 }

