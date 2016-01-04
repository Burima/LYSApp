using LYSApp.Data.DBRepository;
using LYSApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace LYSApp.Domain.OwnerPropertyListingRequestManagement
{
    public class OwnerPropertyListingRequestManagement : IOwnerPropertyListingRequestManagement
    {
        private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.OwnerPropertyListingRequest> ownerPropertyListingRequestRepository = null;
        public OwnerPropertyListingRequestManagement()
        {
            unitOfWork = new UnitOfWork();
            ownerPropertyListingRequestRepository = new BaseRepository<Data.DBEntity.OwnerPropertyListingRequest>(unitOfWork);
            Mapper.CreateMap<Model.OwnerPropertyListingRequestViewModel, Data.DBEntity.OwnerPropertyListingRequest>();
        }

        public void AddOwnerPropertyListingRequest(OwnerPropertyListingRequestViewModel model)
        {
            var dbOwnerPropertyListingRequest = Mapper.Map<Model.OwnerPropertyListingRequestViewModel, Data.DBEntity.OwnerPropertyListingRequest>(model);
            ownerPropertyListingRequestRepository.Insert(dbOwnerPropertyListingRequest);
            unitOfWork.SaveChanges();
        }
             
    }
}
