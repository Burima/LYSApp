using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;
using LYSApp.Data.DBRepository;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace LYSApp.Domain.SearchManagement
{
    public class SearchManagement : ISearchManagement
    {
        private IUnitOfWork unitOfWork = null;
       
        private IBaseRepository<Data.DBEntity.Area> areaRepository = null;
        private IBaseRepository<Data.DBEntity.House> houseRepository = null;
        private IBaseRepository<Data.DBEntity.Room> roomRepository = null;
        private IBaseRepository<Data.DBEntity.PGDetail> pgDetailRepository = null;
        private IBaseRepository<Data.DBEntity.HouseImage> houseImageRepository = null;
        private IBaseRepository<Data.DBEntity.HouseReview> houseReviewsRepository = null;
        private IBaseRepository<Data.DBEntity.HouseAmenity> houseAmenitiesRepository = null;
        public SearchManagement()
        {
            unitOfWork = new UnitOfWork();
            
            areaRepository = new BaseRepository<Data.DBEntity.Area>(unitOfWork);
            houseRepository = new BaseRepository<Data.DBEntity.House>(unitOfWork);
            roomRepository = new BaseRepository<Data.DBEntity.Room>(unitOfWork);
            pgDetailRepository = new BaseRepository<Data.DBEntity.PGDetail>(unitOfWork);
            houseImageRepository = new BaseRepository<Data.DBEntity.HouseImage>(unitOfWork);
            houseReviewsRepository = new BaseRepository<Data.DBEntity.HouseReview>(unitOfWork);
            houseAmenitiesRepository = new BaseRepository<Data.DBEntity.HouseAmenity>(unitOfWork);

            Mapper.CreateMap<LYSApp.Data.DBEntity.House, LYSApp.Model.House>();
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
            
            IList<House> houseList = new List<House>();
            IEnumerable<LYSApp.Data.DBEntity.House> dbHouseList = houseRepository.ExecWithStoreProcedure(
              "GetHouseListByPGDetailsID @pgDetailsId",
              new SqlParameter("pgDetailsId", SqlDbType.Int) { Value = PGDetailsID });
            foreach (LYSApp.Data.DBEntity.House h in dbHouseList)
            {
                //var house = Mapper.Map<LYSApp.Data.DBEntity.House, LYSApp.Model.House>(h);
                //house.Rooms = (from r in roomRepository.Where(r=> r.HouseID == house.HouseID) select new LYSApp.Model.Room{}).ToList();
                //house.HouseImages = (from r in houseImageRepository.Where(r => r.HouseID == house.HouseID) select new LYSApp.Model.HouseImage { }).ToList();
                //house.HouseReviews = (from r in houseReviewsRepository.Where(r => r.HouseID == house.HouseID) select new LYSApp.Model.HouseReview { }).ToList();
                //house.HouseAmenities = (from r in houseAmenitiesRepository.Where(r => r.HouseID == house.HouseID) select new LYSApp.Model.HouseAmenity { }).ToList();
                var house = GetHouseByID(h.HouseID);
                houseList.Add(house);
            }

            propertyDetailsViewModel.PGDetailsID = PGDetailsID;
            propertyDetailsViewModel.PGName = (from p in pgDetailRepository.Where(p => p.PGDetailID == PGDetailsID).Select(p => p.PGName) select p).ToList().FirstOrDefault();
            propertyDetailsViewModel.Gender = houseList.FirstOrDefault().Gender;
            propertyDetailsViewModel.Address = houseList.FirstOrDefault().Address;
            propertyDetailsViewModel.Latitude = houseList.FirstOrDefault().Latitude;
            propertyDetailsViewModel.Longitude = houseList.FirstOrDefault().Longitude;
            propertyDetailsViewModel.Description = houseList.FirstOrDefault().Description;
            propertyDetailsViewModel.HouseList = houseList;
            return propertyDetailsViewModel;
        }

        public Model.House GetHouseByID(int houseID)
        {
            var house = (from p in houseRepository.Get(p=> p.HouseID == houseID)
                         select new Model.House
                         {
                             HouseID = p.HouseID,
                             HouseName = p.HouseName,
                             Latitude = p.Latitude,
                             Longitude = p.Longitude,
                             Address = p.Address,
                             Landmark = p.Landmark,
                             Gender = p.Gender,
                             NoOfBalconnies = p.NoOfBalconnies,
                             NoOfBathrooms = p.NoOfBathrooms,

                             HouseAmenities = (from g in p.HouseAmenities
                                               select new LYSApp.Model.HouseAmenity
                                               {
                                                   AminityID = g.AminityID,
                                                   AC = g.AC,
                                                   Aquaguard = g.Aquaguard,
                                                   AttachBathrooms = g.AttachBathrooms,
                                                   BreakFastGiven = g.BreakFastGiven,
                                                   Clubhouse = g.Clubhouse,
                                                   CommonTV = g.CommonTV,
                                                   CreatedOn = g.CreatedOn,
                                                   DinnerGiven = g.DinnerGiven,
                                                   EmergencyMedicalServices = g.EmergencyMedicalServices,
                                                   FourWheelerCloseParking = g.FourWheelerCloseParking,
                                                   FourWheelerOpenParking = g.FourWheelerOpenParking,
                                                   Fridge = g.Fridge,
                                                   GuardianEntry = g.GuardianEntry,
                                                   GYM = g.GYM,
                                                   HotColdWaterSupply = g.HotColdWaterSupply,
                                                   Housekeeping = g.Housekeeping,
                                                   IndividualTV = g.IndividualTV,
                                                   IntercomFacility = g.IntercomFacility,
                                                   IroningWashingServices = g.IroningWashingServices,
                                                   KitchenFacilityWithGas = g.KitchenFacilityWithGas,
                                                   LCDTVCableConnection = g.LCDTVCableConnection,
                                                   Lift = g.Lift,
                                                   Lockers = g.Lockers,
                                                   LunchGiven = g.LunchGiven,
                                                   MineralDrinkingWater = g.MineralDrinkingWater,
                                                   Newspaper = g.Newspaper,
                                                   NoBoysEntry = g.NoBoysEntry,
                                                   NoDrinking = g.NoDrinking,
                                                   NoSmoking = g.NoSmoking,
                                                   NonVegAllowed = g.NonVegAllowed,
                                                   Partyhall = g.Partyhall,
                                                   Playground = g.Playground,
                                                   Powerbackup = g.Powerbackup,
                                                   RoomService = g.RoomService,
                                                   Security = g.Security,
                                                   SwimmingPool = g.SwimmingPool,
                                                   TwoWheelerCloseParking = g.TwoWheelerCloseParking,
                                                   TwoWheelerOpenParking = g.TwoWheelerOpenParking,
                                                   VideoSurveillance = g.VideoSurveillance,
                                                   Wardrobes = g.Wardrobes,
                                                   Washingmachine = g.Washingmachine,
                                                   WaterSupply = g.WaterSupply,
                                                   Wifi = g.Wifi

                                               }).ToList(),


                             HouseImages = (from i in p.HouseImages
                                            select new LYSApp.Model.HouseImage
                                            {
                                                HouseImageID = i.HouseImageID,
                                                ImagePath = i.ImagePath
                                            }).ToList(),

                             Rooms = (from r in p.Rooms
                                      select new LYSApp.Model.Room
                                      {
                                          RoomID = r.RoomID,
                                          RoomNumber = r.RoomNumber,
                                          MonthlyRent = r.MonthlyRent,
                                          Deposit = r.Deposit,
                                          NoOfBeds = r.NoOfBeds
                                      }).ToList()
                         }                     
                         ).FirstOrDefault();

            return house;
        }
    }
}
