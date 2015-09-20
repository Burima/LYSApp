using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LYSApp.Model
{
    public class HouseAmenity
    {
        [Key]

        public int AminityID { get; set; }
        public int HouseID { get; set; }
        public bool AC { get; set; }
        public bool Fridge { get; set; }
        public bool Wifi { get; set; }
        public bool Washingmachine { get; set; }
        public bool AttachBathrooms { get; set; }
        public bool KitchenFacilityWithGas { get; set; }
        public bool CommonTV { get; set; }
        public bool IndividualTV { get; set; }
        public bool LCDTVCableConnection { get; set; }
        public bool Wardrobes { get; set; }
        public bool IntercomFacility { get; set; }
        public bool IroningWashingServices { get; set; }
        public bool LunchGiven { get; set; }
        public bool BreakFastGiven { get; set; }
        public bool DinnerGiven { get; set; }
        public bool WaterSupply { get; set; }
        public bool HotColdWaterSupply { get; set; }
        public bool MineralDrinkingWater { get; set; }
        public bool Aquaguard { get; set; }
        public bool Housekeeping { get; set; }
        public bool RoomService { get; set; }
        public bool Newspaper { get; set; }
        public bool TwoWheelerOpenParking { get; set; }
        public bool TwoWheelerCloseParking { get; set; }
        public bool FourWheelerOpenParking { get; set; }
        public bool FourWheelerCloseParking { get; set; }
        public bool Lockers { get; set; }
        public bool GYM { get; set; }
        public bool Lift { get; set; }
        public bool Playground { get; set; }
        public bool Clubhouse { get; set; }
        public bool Partyhall { get; set; }
        public bool Security { get; set; }
        public bool SwimmingPool { get; set; }
        public bool VideoSurveillance { get; set; }
        public bool Powerbackup { get; set; }
        public bool EmergencyMedicalServices { get; set; }
        public bool NonVegAllowed { get; set; }
        public bool GuardianEntry { get; set; }
        public bool NoSmoking { get; set; }
        public bool NoDrinking { get; set; }
        public bool NoBoysEntry { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime LastUpdatedOn { get; set; }

        public virtual House House { get; set; }
    }
}
