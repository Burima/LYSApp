﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;
using LYSApp.Data.DBRepository;
using System.Web;

namespace LYSApp.Domain.SearchManagement
{
    public class SearchManagement : ISearchManagement
    {
        private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.City> cityRepository = null;
        private IBaseRepository<Data.DBEntity.Area> areaRepository = null;
        private IBaseRepository<Data.DBEntity.House> houseRepository = null;
        private IBaseRepository<Data.DBEntity.Room> roomRepository = null;
        public SearchManagement()
        {
            unitOfWork = new UnitOfWork();
            cityRepository = new BaseRepository<Data.DBEntity.City>(unitOfWork);
            areaRepository = new BaseRepository<Data.DBEntity.Area>(unitOfWork);
            houseRepository = new BaseRepository<Data.DBEntity.House>(unitOfWork);
            roomRepository = new BaseRepository<Data.DBEntity.Room>(unitOfWork);
        }

        public IList<House> GetHouses(SearchViewModel searchViewModel)
        {
            IList<House> houseList = new List<House>();

            var area = areaRepository.FirstOrDefault(m => m.AreaID == searchViewModel.AreaID);
            return null;
        }

        public IList<SearchAreaViewModel> GetAreas(string term)
        {
            IList<SearchAreaViewModel> areaList = new List<SearchAreaViewModel>();
            IList<Model.Area> areas = HttpContext.Current.Application["Areas"] as IList<Model.Area>;
            IList<Model.City> cities = HttpContext.Current.Application["Cities"] as IList<Model.City>;
            
            areaList = areas.Select(area => new LYSApp.Model.SearchAreaViewModel
            {
                AreaID = area.AreaID,
                AreaName = area.AreaName,
                CityName = cities.Where(x => x.CityID == area.CityID).FirstOrDefault().CityName
            }).Where(area => area.AreaName.ToLower().StartsWith(term.ToLower())).ToList();
            return areaList;
        }

        public PropertyDetailsViewModel GetPropertyDetails(int PGDetailsID)
        {
            PropertyDetailsViewModel propertyDetailsViewModel = new PropertyDetailsViewModel();

                     
            return propertyDetailsViewModel;
        }
    }
}
