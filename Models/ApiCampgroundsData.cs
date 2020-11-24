using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models.CampgroundsData
{



    public class ApiCampgroundsData
    {
        public string total { get; set; }
        public string limit { get; set; }
        public string start { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string parkCode { get; set; }
        public string description { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string latLong { get; set; }
        public string audioDescription { get; set; }
        public string isPassportStampLocation { get; set; }
        public string passportStampLocationDescription { get; set; }
        public object[] passportStampImages { get; set; }
        public string reservationInfo { get; set; }
        public string reservationUrl { get; set; }
        public string regulationsurl { get; set; }
        public string regulationsOverview { get; set; }
        public Amenities amenities { get; set; }
        public Contacts contacts { get; set; }
        public Fee[] fees { get; set; }
        public string directionsOverview { get; set; }
        public string directionsUrl { get; set; }
        public Operatinghour[] operatingHours { get; set; }
        public Address[] addresses { get; set; }
        public Image[] images { get; set; }
        public string weatherOverview { get; set; }
        public string numberOfSitesReservable { get; set; }
        public string numberOfSitesFirstComeFirstServe { get; set; }
        public Campsites campsites { get; set; }
        public Accessibility accessibility { get; set; }
    }

    public class Amenities
    {
        public string trashRecyclingCollection { get; set; }
        public string[] toilets { get; set; }
        public string internetConnectivity { get; set; }
        public string[] showers { get; set; }
        public string cellPhoneReception { get; set; }
        public string laundry { get; set; }
        public string amphitheater { get; set; }
        public string dumpStation { get; set; }
        public string campStore { get; set; }
        public string staffOrVolunteerHostOnsite { get; set; }
        public string[] potableWater { get; set; }
        public string iceAvailableForSale { get; set; }
        public string firewoodForSale { get; set; }
        public string foodStorageLockers { get; set; }
    }

    public class Contacts
    {
        public Phonenumber[] phoneNumbers { get; set; }
        public Emailaddress[] emailAddresses { get; set; }
    }

    public class Phonenumber
    {
        public string phoneNumber { get; set; }
        public string description { get; set; }
        public string extension { get; set; }
        public string type { get; set; }
    }

    public class Emailaddress
    {
        public string description { get; set; }
        public string emailAddress { get; set; }
    }

    public class Campsites
    {
        public string totalSites { get; set; }
        public string group { get; set; }
        public string horse { get; set; }
        public string tentOnly { get; set; }
        public string electricalHookups { get; set; }
        public string rvOnly { get; set; }
        public string walkBoatTo { get; set; }
        public string other { get; set; }
    }

    public class Accessibility
    {
        public string wheelchairAccess { get; set; }
        public string internetInfo { get; set; }
        public string cellPhoneInfo { get; set; }
        public string fireStovePolicy { get; set; }
        public string rvAllowed { get; set; }
        public string rvInfo { get; set; }
        public string rvMaxLength { get; set; }
        public string additionalInfo { get; set; }
        public string trailerMaxLength { get; set; }
        public string adaInfo { get; set; }
        public string trailerAllowed { get; set; }
        public string[] accessRoads { get; set; }
        public string[] classifications { get; set; }
    }

    public class Fee
    {
        public string cost { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }

    public class Operatinghour
    {
        public Exception[] exceptions { get; set; }
        public string description { get; set; }
        public Standardhours standardHours { get; set; }
        public string name { get; set; }
    }

    public class Standardhours
    {
        public string wednesday { get; set; }
        public string monday { get; set; }
        public string thursday { get; set; }
        public string sunday { get; set; }
        public string tuesday { get; set; }
        public string friday { get; set; }
        public string saturday { get; set; }
    }

    public class Exception
    {
        public Exceptionhours exceptionHours { get; set; }
        public string startDate { get; set; }
        public string name { get; set; }
        public string endDate { get; set; }
    }

    public class Exceptionhours
    {
        public string wednesday { get; set; }
        public string monday { get; set; }
        public string thursday { get; set; }
        public string sunday { get; set; }
        public string tuesday { get; set; }
        public string friday { get; set; }
        public string saturday { get; set; }
    }

    public class Address
    {
        public string postalCode { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string line1 { get; set; }
        public string type { get; set; }
        public string line3 { get; set; }
        public string line2 { get; set; }
    }

    public class Image
    {
        public string credit { get; set; }
        public object[] crops { get; set; }
        public string title { get; set; }
        public string altText { get; set; }
        public string caption { get; set; }
        public string url { get; set; }
    }
}

