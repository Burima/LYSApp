using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;

namespace LYSApp.Domain.SearchManagement
{
    public interface ISearchManagement
    {
        //IList<Model.PropertyDetailsViewModel> GetPGDetailsBySearchCriteria(SearchViewModel serchViewModel);
        IList<SearchResultViewModel> GetPGsBySearchCriteria(SearchViewModel searchViewModel);

        IList<SearchAreaViewModel> GetAreas(string term);

        PropertyDetailsViewModel GetPropertyDetails(int PGDetailsID, SearchViewModel searchViewModel);

        void GetBookingDetails();
    }

}