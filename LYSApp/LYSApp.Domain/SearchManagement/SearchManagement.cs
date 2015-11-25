﻿using System;
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
using LYSApp.Model.Constants;

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
        private IBaseRepository<Data.DBEntity.Bed> bedRepository = null;
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
            bedRepository = new BaseRepository<Data.DBEntity.Bed>(unitOfWork);

            Mapper.CreateMap<LYSApp.Data.DBEntity.House, LYSApp.Model.House>();
            Mapper.CreateMap<LYSApp.Data.DBEntity.PGDetail, LYSApp.Model.PGDetail>();
        }

        //public IList<Model.PropertyDetailsViewModel> GetPGDetailsBySearchCriteria(SearchViewModel searchViewModel)
        //{
        //    var modelPGDetails = (from pg in pgDetailRepository.Get()
        //                       join h in houseRepository.Get() on pg.PGDetailID equals h.PGDetailID
        //                       join r in roomRepository.Get() on h.HouseID equals r.HouseID
        //                       join b in bedRepository.Get() on r.RoomID equals b.RoomID
        //                       //where pg.AreaID==searchViewModel.AreaID && //selected area
        //                       //      h.Status!=null && h.Status==true && h.Gender==searchViewModel.Gender &&//status active for House
        //                       //      r.Status!=null && r.Status==true && //Status active for Room
        //                       //      b.Status != null && b.Status == true && ((b.UserID==0)||(((DateTime)b.BookingToDate-searchViewModel.BookingFromDate).Days>=30))//Status active for Bed and bed is empty (zero)
        //                       select new PropertyDetailsViewModel
        //                       {
        //                           PGDetailsID = pg.PGDetailID,
        //                           PGName = pg.PGName,
        //                           HouseList = (from p in pg.Houses
        //                                        select new Model.House
        //                                        {
        //                                            HouseID = p.HouseID,
        //                                            HouseName = p.HouseName,
        //                                            Latitude = p.Latitude,
        //                                            Longitude = p.Longitude,
        //                                            Address = p.Address,
        //                                            Landmark = p.Landmark,
        //                                            Gender = p.Gender,
        //                                            NoOfBalconnies = p.NoOfBalconnies,
        //                                            NoOfBathrooms = p.NoOfBathrooms,

        //                                            HouseAmenities = (from g in p.HouseAmenities
        //                                                              select new LYSApp.Model.HouseAmenity
        //                                                              {
        //                                                                  AminityID = g.AminityID,
        //                                                                  AC = g.AC,
        //                                                                  Aquaguard = g.Aquaguard,
        //                                                                  AttachBathrooms = g.AttachBathrooms,
        //                                                                  BreakFastGiven = g.BreakFastGiven,
        //                                                                  Clubhouse = g.Clubhouse,
        //                                                                  CommonTV = g.CommonTV,
        //                                                                  CreatedOn = g.CreatedOn,
        //                                                                  DinnerGiven = g.DinnerGiven,
        //                                                                  EmergencyMedicalServices = g.EmergencyMedicalServices,
        //                                                                  FourWheelerCloseParking = g.FourWheelerCloseParking,
        //                                                                  FourWheelerOpenParking = g.FourWheelerOpenParking,
        //                                                                  Fridge = g.Fridge,
        //                                                                  GuardianEntry = g.GuardianEntry,
        //                                                                  GYM = g.GYM,
        //                                                                  HotColdWaterSupply = g.HotColdWaterSupply,
        //                                                                  Housekeeping = g.Housekeeping,
        //                                                                  IndividualTV = g.IndividualTV,
        //                                                                  IntercomFacility = g.IntercomFacility,
        //                                                                  IroningWashingServices = g.IroningWashingServices,
        //                                                                  KitchenFacilityWithGas = g.KitchenFacilityWithGas,
        //                                                                  LCDTVCableConnection = g.LCDTVCableConnection,
        //                                                                  Lift = g.Lift,
        //                                                                  Lockers = g.Lockers,
        //                                                                  LunchGiven = g.LunchGiven,
        //                                                                  MineralDrinkingWater = g.MineralDrinkingWater,
        //                                                                  Newspaper = g.Newspaper,
        //                                                                  NoBoysEntry = g.NoBoysEntry,
        //                                                                  NoDrinking = g.NoDrinking,
        //                                                                  NoSmoking = g.NoSmoking,
        //                                                                  NonVegAllowed = g.NonVegAllowed,
        //                                                                  Partyhall = g.Partyhall,
        //                                                                  Playground = g.Playground,
        //                                                                  Powerbackup = g.Powerbackup,
        //                                                                  RoomService = g.RoomService,
        //                                                                  Security = g.Security,
        //                                                                  SwimmingPool = g.SwimmingPool,
        //                                                                  TwoWheelerCloseParking = g.TwoWheelerCloseParking,
        //                                                                  TwoWheelerOpenParking = g.TwoWheelerOpenParking,
        //                                                                  VideoSurveillance = g.VideoSurveillance,
        //                                                                  Wardrobes = g.Wardrobes,
        //                                                                  Washingmachine = g.Washingmachine,
        //                                                                  WaterSupply = g.WaterSupply,
        //                                                                  Wifi = g.Wifi

        //                                                              }).ToList(),


        //                                            HouseImages = (from i in p.HouseImages
        //                                                           select new LYSApp.Model.HouseImage
        //                                                           {
        //                                                               HouseImageID = i.HouseImageID,
        //                                                               ImagePath = i.ImagePath
        //                                                           }).ToList(),

        //                                            Rooms = (from room in p.Rooms
        //                                                     select new LYSApp.Model.Room
        //                                                     {
        //                                                         RoomID = room.RoomID,
        //                                                         RoomNumber = room.RoomNumber,
        //                                                         MonthlyRent = room.MonthlyRent,
        //                                                         Deposit = room.Deposit,
        //                                                         NoOfBeds = room.NoOfBeds
        //                                                     }).ToList()//roomList
        //                                        }).ToList(),//houseList
        //                       }).ToList();//pgdetailsList

        //    return modelPGDetails;
        //}

        /// <summary>
        /// This methods returns list of PGs filtered by Search Criteria
        /// </summary>
        /// <param name="searchViewModel">contains search criteria</param>
        /// <returns>List of PGs</returns>
        public IList<SearchResultViewModel> GetPGsBySearchCriteria(SearchViewModel searchViewModel)
        {
            var modelPGs = (from pg in pgDetailRepository.Get()
                            join h in houseRepository.Get() on pg.PGDetailID equals h.PGDetailID
                            join r in roomRepository.Get() on h.HouseID equals r.HouseID
                            join b in bedRepository.Get() on r.RoomID equals b.RoomID
                            //where pg.AreaID==searchViewModel.AreaID && //selected area
                            //      h.Status!=null && h.Status==true && h.Gender==searchViewModel.Gender &&//status active for House
                            //      r.Status!=null && r.Status==true && //Status active for Room
                            //      b.Status != null && b.Status == true && ((b.UserID==0)||(((DateTime)b.BookingToDate-searchViewModel.BookingFromDate).Days>=30))//Status active for Bed and bed is empty (zero)
                            select new SearchResultViewModel
                            {
                                PGDetailID = pg.PGDetailID,
                                PGName = pg.PGName,
                                MinimumRentHouse = (from p in pg.Houses.OrderBy(x => x.Rooms.OrderBy(p => p.MonthlyRent))//order house with minimum MonthlRent first
                                                    select new Model.House
                                                    {
                                                        Latitude = p.Latitude,
                                                        Longitude = p.Longitude,
                                                        Address = p.Address,
                                                        HouseImages = (from i in p.HouseImages
                                                                       select new LYSApp.Model.HouseImage
                                                                       {
                                                                           HouseImageID = i.HouseImageID,
                                                                           ImagePath = i.ImagePath
                                                                       }).ToList(),//get all house images
                                                        HouseReviews = (from review in p.HouseReviews
                                                                        select new Model.HouseReview
                                                                        {
                                                                            Rating = review.Rating,
                                                                        }).ToList(),//get all ratings
                                                        Rooms = (from room in p.Rooms.OrderBy(x=>x.MonthlyRent)//order Rooms with minimum MonthlRent
                                                                 select new LYSApp.Model.Room
                                                                 {                                                                     
                                                                     MonthlyRent = room.MonthlyRent,                                                                     
                                                                     //NoOfBeds = room.NoOfBeds
                                                                 }).ToList(),//get all rooms
                                                    }).FirstOrDefault(),
                                
                            }).ToList();

            return modelPGs;
        }

        /// <summary>
        /// This method provides List of Areas and Cities entered in the Area box while searching.
        /// This is providing data to jQuery autocomplete
        /// </summary>
        /// <param name="term">search term</param>
        /// <returns>List of Areas and Cities </returns>
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

        /// <summary>
        /// This methos provides all the type of houeses group by MonthlyRent 
        /// for a specific PG based on PGID
        /// </summary>
        /// <param name="PGDetailsID">PGID of the PG selected</param>
        /// <returns>List of Houses of the PG selected</returns>
        public PropertyDetailsViewModel GetPropertyDetails(int PGDetailsID, SearchViewModel searchViewModel)
        {
            var propertyDetailsViewModel = (from pg in pgDetailRepository.Get()
                                            join h in houseRepository.Get() on pg.PGDetailID equals h.PGDetailID
                                            join r in roomRepository.Get() on h.HouseID equals r.HouseID
                                            join b in bedRepository.Get() on r.RoomID equals b.RoomID
                                            where pg.PGDetailID==PGDetailsID  //selected area
                                            //      h.Status!=null && h.Status==true && h.Gender==searchViewModel.Gender &&//status active for House
                                            //      r.Status!=null && r.Status==true && //Status active for Room
                                            //      b.Status != null && b.Status == true && ((b.UserID==0)||(((DateTime)b.BookingToDate-searchViewModel.BookingFromDate).Days>=30))//Status active for Bed and bed is empty (zero)
                                            select new PropertyDetailsViewModel {
                                                PGDetailID = pg.PGDetailID,
                                                PGName=pg.PGName,
                                                HouseList = (from p in pg.Houses
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

                                                    Rooms = (from room in p.Rooms.GroupBy(a => a.MonthlyRent).FirstOrDefault()//grouped rooms by MonthlyRent
                                                             select new LYSApp.Model.Room
                                                             {
                                                                 RoomID = room.RoomID,
                                                                 RoomNumber = room.RoomNumber,
                                                                 MonthlyRent = room.MonthlyRent,
                                                                 Deposit = room.Deposit,
                                                                 NoOfBeds = room.NoOfBeds
                                                             }).ToList()//roomList
                                                }).ToList(),//houseList
                                            }).FirstOrDefault();
            return propertyDetailsViewModel;
        }

        public void GetBookingDetails()
        {

        }

    }
}
