namespace QueryConverter.Tests.Templates
{
    public static class QueryTemplate
    {
        public const string SelectWithFilters = "SELECT Name, Type, State, Pin FROM Cities WHERE Name = 'Miami' AND State = 'FL' AND ZipCodes IN(33126, 33151) AND AverageAge BETWEEN 34 AND 65AND AverageSalary >= 55230 AND AverageTemperature< 80";
        public const string SelectWithFilter = "SELECT * FROM Planets WHERE SpacecraftWithinKilometers< 10000";
        public const string SelectWithFilterAndGroupBy = "SELECT SolarSystem, Galaxy FROM Planets WHERE SpacecraftWithinKilometers< 10000 GROUP BY SolarSystem, Galaxy";
    }
}
