/*
 * API Test Automation Training - Batch 4
 * Marilou A. Ranoy
 * 
 * Homework 4
 * RestApi Test using SOAP and MSTest
 * Service URL: http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso?WSDL
 * 
 * Create tests for CountryInfoService
 * 1. Create a test method to validate the return of ‘ListOfCountryNamesByCode()’ API is by Ascending Order of Country Code
 * 2. Create a test method to validate passing of invalid Country Code to ‘CountryName()’ API returns ‘Country not found in the database’
 * 3. Create a test method that gets the last entry from ‘ListOfCountryNamesByCode()’ API and pass the return value Country Code to
 *      ‘CountryName()’ API then validate the Country Name from both API is the same
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HWSession4
{
    [TestClass]
    public class CountryInfoServiceTests
    {

        //Declare an instance of the CountryInfoServiceSoapTypeClient to be used in making connection and calling the services
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryInfoServiceTest =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        /// <summary>
        /// Test method TestListofCountryNamesByCodeisAscendingOrder
        /// - validates if the returned list of country names by code is in ascending order
        /// </summary>
        [TestMethod]
        public void TestListofCountryNamesByCodeisAscendingOrder()
        {
            //get the list of country names by code
            var returnListCountryCodes = countryInfoServiceTest.ListOfCountryNamesByCode();
            //create another list that sorts the returned list above by ascending order using the country code
            var sortedListCountryCodes = returnListCountryCodes.OrderBy(s => s.sISOCode);
            //use collection assert to check if our lists (original and sorted) are equal
            CollectionAssert.AreEqual(sortedListCountryCodes.ToList(), returnListCountryCodes.ToList());
        }

        /// <summary>
        /// Test method TestInvalidCountryCode
        /// - validates correct return value 'Country not found in the database' if we pass an invalid country code
        /// </summary>
        [TestMethod]
        public void TestInvalidCountryCode()
        {
            //pass an invalid country code
            var returnValue = countryInfoServiceTest.CountryName("ABC");
            //check if the returned value/string is correct if our country code is invalid
            Assert.AreEqual(returnValue, "Country not found in the database");
        }

        /// <summary>
        /// Test method TestValidateCountryName
        /// - validates if the country name returned is same if we pass on the last country code parameter from the
        /// returned value of ListOfCountryNamesByCode
        /// </summary>
        [TestMethod]
        public void TestValidateCountryName()
        {
            //get the list of country names by code
            var returnValue = countryInfoServiceTest.ListOfCountryNamesByCode();
            //get the last of the list returned above
            var lastValue = returnValue.ToList().Last();
            //get the country name returned value if we pass the last country code from the above list
            var returnCountryName = countryInfoServiceTest.CountryName(lastValue.sISOCode);
            //check if the last country name returned from ListOfCountryNamesByCode API is same as the value returned when we pass the country code of the last set in our list
            Assert.AreEqual(lastValue.sName, returnCountryName);
        }
    }
}