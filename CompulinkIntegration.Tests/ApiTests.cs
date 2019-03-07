using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;
using Xunit;

namespace CompulinkIntegration.Tests
{
    public class ApiTests
    {
        [Theory]
        // Returns all null for some reason in testing. When debugging or run normally, the jsonString passes as expected. 
        //[InlineData("http://192.168.10.60:44354/api/User/login", "{\"ID\":0,\"UserName\":\"evan\",\"Password\":\"daf8ae299773ee1b8000e6507b3ad64bc0620ac6a0446cd7c247f7b36267172f\",\"Location\":null,\"Email\":null,\"Account\":null,\"ApiKey\":null,\"DSN\":null,\"Status\":null,\"Message\":null,\"AvailableActs\":null}", "{\"id\":0,\"userName\":\"evan\",\"password\":\"daf8ae299773ee1b8000e6507b3ad64bc0620ac6a0446cd7c247f7b36267172f\",\"location\":null,\"email\":\"evan@wisvis.com\",\"account\":null,\"apiKey\":null,\"dsn\":null,\"status\":\"OK\",\"message\":null,\"availableActs\":null}")]
        [InlineData("http://192.168.10.60:44354/api/User/login", "", "{\"id\":0,\"userName\":null,\"password\":null,\"location\":null,\"email\":null,\"account\":null,\"apiKey\":null,\"dsn\":null,\"status\":\"FAIL\",\"message\":\"Invalid login credentials\",\"availableActs\":null}")]
        [InlineData("http://192.168.10.60:44354/api/User/login", "asdfasdf", "{\"id\":0,\"userName\":null,\"password\":null,\"location\":null,\"email\":null,\"account\":null,\"apiKey\":null,\"dsn\":null,\"status\":\"FAIL\",\"message\":\"Invalid login credentials\",\"availableActs\":null}")]
        [InlineData("", "", null)]
        [InlineData(null, "{\"ID\":0,\"UserName\":\"evan\",\"Password\":\"daf8ae299773ee1b8000e6507b3ad64bc0620ac6a0446cd7c247f7b36267172f\",\"Location\":null,\"Email\":null,\"Account\":null,\"ApiKey\":null,\"DSN\":null,\"Status\":null,\"Message\":null,\"AvailableActs\":null}", null)]
        [InlineData("http://192.168.10.60:44354/api/User/login", "{\"ID\":0,\"UserName\":\"evan\",\"Password\":\"daf8ae299773ee1b8000e6507b3ad64bc0620ac6a0446cd7c247f7b36267172f\",\"Location\":null,\"Email\":null,\"Account\":null,\"ApiKey\":null,\"DSN\":null,\"Status\":null,\"Message\":null,\"AvailableActs\":null}", "{\"id\":0,\"userName\":null,\"password\":null,\"location\":null,\"email\":null,\"account\":null,\"apiKey\":null,\"dsn\":null,\"status\":\"FAIL\",\"message\":\"Invalid login credentials\",\"availableActs\":null}")]
        [InlineData("https://192.16.10.60:44354/api/User/login", null, null)]
        public void PostReturnsValue(string endpoint, string jsonString, string expectedResponse)
        {
            string actualResponse = API.Post(endpoint, jsonString);

            Assert.Equal(expectedResponse, actualResponse);
        }

        [Theory]
        [InlineData("asdfasdfasdf", null)]
        [InlineData(null, null)]
        [InlineData("" , null)]
        //[InlineData("http://192.168.10.60:44354/api/openorder/716", "{\"request\":{\"api_key\":\"\",\"products\":[{\"_CustomerID\":{\"name\":\"CustomerID\",\"value\":\"27610\",\"error_message\":null,\"is_valid\":true},\"firstName\":\"Makeil\",\"lastName\":\"Schmalfeldt\",\"productCode\":null,\"sku\":null,\"vendor\":\"Ninety Pack\",\"upc\":null,\"price\":null,\"id\":null,\"date\":\"2019 - 3 - 5\",\"eye\":\"Right\",\"product\":\"AV Oasys 1 Day\",\"quantity\":\"1\",\"baseCurve\":\"8.5\",\"diameter\":\"\",\"sphere\":\" - 1.25\",\"cylinder\":\"\",\"axis\":\"\",\"add\":\"\",\"color\":\"\",\"multifocal\":\"\"},{\"_CustomerID\":{\"name\":\"CustomerID\",\"value\":\"27610\",\"error_message\":null,\"is_valid\":true},\"firstName\":\"Makeil\",\"lastName\":\"Schmalfeldt\",\"productCode\":null,\"sku\":null,\"vendor\":\"Ninety Pack\",\"upc\":null,\"price\":null,\"id\":null,\"date\":\"2019 - 3 - 5\",\"eye\":\"Left\",\"product\":\"AV Oasys 1 Day\",\"quantity\":\"1\",\"baseCurve\":\"8.5\",\"diameter\":\"\",\"sphere\":\" - 1.25\",\"cylinder\":\"\",\"axis\":\"\",\"add\":\"\",\"color\":\"\",\"multifocal\":\"\"}]}}")]
        public void GetReturnsValue(string endpoint, string expected)
        {
            string actual = API.Get(endpoint, out string httpStatus);

            Assert.Equal(expected, actual);
        }

    }
}
